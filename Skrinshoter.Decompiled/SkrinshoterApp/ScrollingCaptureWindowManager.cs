using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Properties;
using SkrinshoterApp.SkrChildApp;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;

namespace SkrinshoterApp;

public class ScrollingCaptureWindowManager
{
	private sealed class OverlayContext : OverlayWindowContext
	{
		public ScrollingCaptureStartWindow StartWindow { get; }

		public ScrollingCaptureActionWindow ActionWindow { get; }

		public ScrollingCapturePreviewWindow PreviewWindow { get; }

		public ScrollingCaptureMessageWindow MessageWindow { get; }

		public OverlayContext(MonitorInfo monitor, OverlayWindow overlayWindow, OverlayWindowVM overlayWindowVM, ScrollingCaptureStartWindow startWindow, ScrollingCaptureActionWindow actionWindow, ScrollingCapturePreviewWindow previewWindow, ScrollingCaptureMessageWindow messageWindow)
			: base(monitor, overlayWindow, overlayWindowVM)
		{
			StartWindow = startWindow ?? throw new ArgumentNullException("startWindow");
			ActionWindow = actionWindow ?? throw new ArgumentNullException("actionWindow");
			PreviewWindow = previewWindow ?? throw new ArgumentNullException("previewWindow");
			MessageWindow = messageWindow ?? throw new ArgumentNullException("messageWindow");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				StartWindow.ForceClose();
				ActionWindow.ForceClose();
				PreviewWindow.ForceClose();
				MessageWindow.ForceClose();
			}
			base.Dispose(disposing);
		}
	}

	private static readonly TimeSpan MinPreviewRenderInterval = TimeSpan.FromMilliseconds(100.0);

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly Dispatcher dispatcher;

	private readonly VideoModuleAppProxy videoModule;

	private readonly ScreenshotSaveFileDialog screenshotSaveFileDialog;

	private readonly ILoggerFactory loggerFactory;

	private readonly ILogger logger;

	private string baseFileName;

	private OverlayWindowStateManager stateManager;

	private AreaSelectionManager areaSelectionManager;

	private OverlayWindowManager<OverlayContext> overlayWindowManager;

	private TaskCompletionSource<ScreenshotEditingResult> resultTaskSource;

	private ScrollingCaptureEngine engine;

	private readonly ScrollingCapturePanelPlacer panelPlacer;

	private ScrollingCapturePreviewBuilder previewBuilder;

	private ActionRateLimiter previewRenderLimiter;

	private ScrollingCaptureAutoScroller autoScroller;

	private int mouseInsideCaptureAreaReportCount;

	private int mouseOutsideCaptureAreaReportCount;

	private bool _isClosing;

	private bool IsClosing
	{
		get
		{
			return Volatile.Read(ref _isClosing);
		}
		set
		{
			Volatile.Write(ref _isClosing, value);
		}
	}

	public ScrollingCaptureWindowManager(SettingsManager settingsManager, Dispatcher dispatcher, VideoModuleAppProxy videoModule, ScreenshotSaveFileDialog screenshotSaveFileDialog, ILoggerFactory loggerFactory)
	{
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.dispatcher = dispatcher;
		this.videoModule = videoModule;
		this.screenshotSaveFileDialog = screenshotSaveFileDialog;
		this.loggerFactory = loggerFactory;
		logger = loggerFactory.CreateLogger<ScrollingCaptureWindowManager>();
		panelPlacer = new ScrollingCapturePanelPlacer(WindowCaptureExclusion.CanHideWindowsFromCapture() ? 100 : 25);
	}

	public async Task<ScreenshotEditingResult> Show(string baseFileName)
	{
		IsClosing = false;
		this.baseFileName = baseFileName;
		MonitorInfo monitorInfo = new MonitorInfo(PInvoke.MonitorFromWindow(PInvoke.GetForegroundWindow(), MONITOR_FROM_FLAGS.MONITOR_DEFAULTTOPRIMARY));
		stateManager = new OverlayWindowStateManager();
		stateManager.StateChanged += StateManager_StateChanged;
		areaSelectionManager = new AreaSelectionManager();
		overlayWindowManager = new OverlayWindowManager<OverlayContext>(areaSelectionManager, CreateOverlayContext);
		overlayWindowManager.ShowAll();
		foreach (OverlayContext context in overlayWindowManager.Contexts)
		{
			context.StartWindow.Owner = context.OverlayWindow;
			context.ActionWindow.Owner = context.OverlayWindow;
			context.PreviewWindow.Owner = context.OverlayWindow;
			context.MessageWindow.Owner = context.OverlayWindow;
			context.StartWindow.EnsureLayoutUpdated();
			context.ActionWindow.EnsureLayoutUpdated();
			context.PreviewWindow.EnsureLayoutUpdated();
		}
		CreateSelectionHintObjects();
		overlayWindowManager.GetContextForMonitor(monitorInfo.MonitorName).OverlayWindow.Activate();
		if (settings.UseLastSelectionArea)
		{
			RestoreLastSelectionArea();
		}
		resultTaskSource = new TaskCompletionSource<ScreenshotEditingResult>();
		return await resultTaskSource.Task;
	}

	public void Hide()
	{
		IsClosing = true;
		StopCapture();
		overlayWindowManager?.Dispose();
		overlayWindowManager = null;
		if (engine != null)
		{
			engine.ReachedMaxHeight -= Engine_ReachedMaxHeight;
			engine.Image.Updated -= Image_Updated;
			engine.Dispose();
			engine = null;
		}
		previewBuilder?.Dispose();
		previewBuilder = null;
		previewRenderLimiter?.Dispose();
		previewRenderLimiter = null;
		if (stateManager != null)
		{
			stateManager.StateChanged -= StateManager_StateChanged;
			stateManager = null;
		}
		areaSelectionManager = null;
	}

	private async void StateManager_StateChanged(object sender, EventArgs e)
	{
		if (IsClosing)
		{
			return;
		}
		switch (stateManager.State)
		{
		case OverlayWindowState.Selecting:
		case OverlayWindowState.MovingFrame:
		case OverlayWindowState.ResizingFrame:
			HideStartPanel();
			break;
		case OverlayWindowState.Scrolling:
			await StartCapture();
			break;
		case OverlayWindowState.Selected:
			if (areaSelectionManager.HasArea)
			{
				ShowStartPanel(overlayWindowManager.GetContext(areaSelectionManager.Owner));
			}
			break;
		case OverlayWindowState.Canceled:
			ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.Close));
			break;
		}
	}

	private void OverlayWindow_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (!IsClosing)
		{
			OverlayWindowVM overlayWindowVM = overlayWindowManager.GetSelectedContext()?.OverlayWindowVM;
			if (overlayWindowVM != null && stateManager.State == OverlayWindowState.Selected)
			{
				stateManager.SetState(OverlayWindowState.Selecting, overlayWindowVM);
				areaSelectionManager.Clear();
				stateManager.SetState(OverlayWindowState.Selected);
				HideStartPanel();
				e.Handled = true;
			}
		}
	}

	private void ActionWindowVM_ActionSelected(object sender, ScreenshotEditingActionSelectedEventArgs e)
	{
		if (IsClosing)
		{
			return;
		}
		RawImage rawImage = null;
		string text = null;
		logger.LogInformation("Scrolling capture action: {action}", e.Action);
		try
		{
			StopCapture();
			if (e.Action == ScreenshotEditingAction.SaveToFile)
			{
				text = screenshotSaveFileDialog.Show(baseFileName);
				if (text == null)
				{
					return;
				}
			}
			if (e.Action != ScreenshotEditingAction.Close)
			{
				rawImage = new UnmanagedRawImage(new ImageFormat(engine.Image.Width, engine.Image.Height, PixelFormat.BGRA));
				engine.Image.CopyTo(rawImage);
				SaveLastSelectionArea(overlayWindowManager.GetSelectedContext().OverlayWindowVM);
			}
			Hide();
			resultTaskSource.TrySetResult(new ScreenshotEditingResult(e.Action, rawImage, text));
		}
		catch
		{
			rawImage?.Dispose();
			throw;
		}
	}

	private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (IsClosing || e.Source is ComboBox { IsDropDownOpen: not false })
		{
			return;
		}
		OverlayWindowVM overlayWindowVM = overlayWindowManager.GetSelectedContext()?.OverlayWindowVM;
		switch (e.Key)
		{
		case Key.Escape:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.Close));
				return;
			}
			break;
		case Key.A:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
			{
				if (overlayWindowVM == null)
				{
					overlayWindowVM = (sender as OverlayWindow)?.DataContext as OverlayWindowVM;
				}
				if (overlayWindowVM != null && stateManager.State == OverlayWindowState.Selected)
				{
					overlayWindowVM.SelectAllScreenCommand.ExecuteIfCan();
				}
				e.Handled = true;
				return;
			}
			break;
		}
		if (overlayWindowVM == null || overlayWindowVM.ProcessKey(e.Key, e.KeyboardDevice.Modifiers))
		{
			return;
		}
		if (stateManager.State == OverlayWindowState.Scrolling && MatchesHotkey(e, settings.UploadScreenshotToAllHotkey))
		{
			ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.UploadToAll));
			e.Handled = true;
			return;
		}
		if (stateManager.State == OverlayWindowState.Selected && e.Key == Key.Return && e.KeyboardDevice.Modifiers == ModifierKeys.None)
		{
			stateManager.SetState(OverlayWindowState.Scrolling, overlayWindowVM);
		}
		else
		{
			if (stateManager.State != OverlayWindowState.Scrolling)
			{
				return;
			}
			Key key = e.Key;
			if (key <= Key.Insert)
			{
				switch (key)
				{
				default:
					return;
				case Key.Return:
					if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
					{
						ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.UploadToCloud));
					}
					return;
				case Key.Insert:
					break;
				}
				if (e.KeyboardDevice.Modifiers != ModifierKeys.Control)
				{
					return;
				}
			}
			else
			{
				if (key != Key.C)
				{
					if (key == Key.S && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
					{
						ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.SaveToFile));
					}
					return;
				}
				if (e.KeyboardDevice.Modifiers != ModifierKeys.Control)
				{
					return;
				}
			}
			ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.CopyToClipboard));
		}
	}

	private static bool MatchesHotkey(KeyEventArgs e, Hotkey hotkey)
	{
		Key key = e.Key == Key.System ? e.SystemKey : e.Key;
		return !hotkey.IsEmpty && hotkey.Key == key && hotkey.Modifiers == e.KeyboardDevice.Modifiers;
	}

	[AnyThread]
	private void AutoScroller_MousePositionReport(object sender, MousePositionReportEventArgs e)
	{
		dispatcher.InvokeIfRequired(() =>
		{
			if (!IsClosing)
			{
				if (e.IsMouseInsideCaptureArea)
				{
					mouseInsideCaptureAreaReportCount++;
					mouseOutsideCaptureAreaReportCount = 0;
					HideMessagePanel();
					if (mouseInsideCaptureAreaReportCount < 3)
					{
						e.CanInject = false;
					}
				}
				else if (IsMouseOverActionPanel())
				{
					mouseInsideCaptureAreaReportCount = 0;
					mouseOutsideCaptureAreaReportCount = 0;
					HideMessagePanel();
				}
				else
				{
					mouseInsideCaptureAreaReportCount = 0;
					mouseOutsideCaptureAreaReportCount++;
					if (mouseOutsideCaptureAreaReportCount == 3)
					{
						ShowMessagePanel(Resources.ScrollingCapture_Message_AutoScrollHint);
					}
				}
			}
		});
	}

	[AnyThread]
	private void Engine_ReachedMaxHeight(object sender, EventArgs e)
	{
		if (IsClosing)
		{
			return;
		}
		StopCapture();
		dispatcher.InvokeIfRequired(() =>
		{
			if (!IsClosing)
			{
				ShowMessagePanel(Resources.ScrollingCapture_Message_ReachedMaxHeight);
			}
		});
	}

	[AnyThread]
	private void Image_Updated(object sender, ScrollingCaptureImageUpdatedEventArgs e)
	{
		if (!IsClosing)
		{
			previewBuilder?.OnSourceUpdated(e.ModifiedFromRow);
			previewRenderLimiter?.Run();
		}
	}

	private OverlayContext CreateOverlayContext(MonitorInfo monitor)
	{
		OverlayWindow overlayWindow = new OverlayWindow(createForVideo: true)
		{
			Width = 1.0,
			Height = 1.0,
			Left = 1000000.0
		};
		overlayWindow.PreviewKeyDown += Window_PreviewKeyDown;
		overlayWindow.PreviewMouseRightButtonDown += OverlayWindow_PreviewMouseRightButtonDown;
		OverlayWindowVM overlayWindowVM = new OverlayWindowVM(stateManager, areaSelectionManager, overlayWindow, monitor.GetScaleFactor(), new System.Windows.Size(192.0, 192.0));
		ScrollingCaptureStartWindow scrollingCaptureStartWindow = new ScrollingCaptureStartWindow();
		scrollingCaptureStartWindow.PreviewKeyDown += Window_PreviewKeyDown;
		scrollingCaptureStartWindow.DataContext = new ScrollingCaptureStartWindowVM(stateManager);
		ScrollingCaptureActionWindow scrollingCaptureActionWindow = new ScrollingCaptureActionWindow();
		scrollingCaptureActionWindow.PreviewKeyDown += Window_PreviewKeyDown;
		ScreenshotEditingActionWindowVM screenshotEditingActionWindowVM = new ScreenshotEditingActionWindowVM(settings);
		screenshotEditingActionWindowVM.ActionSelected += ActionWindowVM_ActionSelected;
		scrollingCaptureActionWindow.DataContext = screenshotEditingActionWindowVM;
		ScrollingCapturePreviewWindow scrollingCapturePreviewWindow = new ScrollingCapturePreviewWindow();
		scrollingCapturePreviewWindow.PreviewKeyDown += Window_PreviewKeyDown;
		scrollingCapturePreviewWindow.DataContext = new ScrollingCapturePreviewWindowVM(loggerFactory.CreateLogger<ScrollingCapturePreviewWindowVM>());
		ScrollingCaptureMessageWindow scrollingCaptureMessageWindow = new ScrollingCaptureMessageWindow();
		scrollingCaptureMessageWindow.PreviewKeyDown += Window_PreviewKeyDown;
		return new OverlayContext(monitor, overlayWindow, overlayWindowVM, scrollingCaptureStartWindow, scrollingCaptureActionWindow, scrollingCapturePreviewWindow, scrollingCaptureMessageWindow);
	}

	private async Task StartCapture()
	{
		try
		{
			OverlayContext overlayContext = overlayWindowManager.GetSelectedContext();
			if (overlayContext != null)
			{
				overlayWindowManager.HideAllExceptSelected();
				HideStartPanel();
				System.Windows.Size actionPanelSize = new System.Windows.Size(overlayContext.ActionWindow.ActualWidth, overlayContext.ActionWindow.ActualHeight);
				panelPlacer.CalculatePanelRects(actionPanelSize, 1.0, out var actionPanelRect, out var _);
				overlayContext.ActionWindow.SetPosition(overlayContext.OverlayWindow.PointToScreen(actionPanelRect.Location));
				Rectangle captureArea = overlayContext.OverlayWindowVM.GetSelectedAreaInRelativeScreenCoordinates();
				engine = new ScrollingCaptureEngine(videoModule, captureArea, overlayContext.Monitor.MonitorName, loggerFactory);
				engine.ReachedMaxHeight += Engine_ReachedMaxHeight;
				engine.Image.Updated += Image_Updated;
				previewBuilder = new ScrollingCapturePreviewBuilder(engine.Image);
				previewRenderLimiter = new ActionRateLimiter(dispatcher, MinPreviewRenderInterval, RenderPreview);
				await engine.Start();
				if (!IsClosing)
				{
					overlayContext.ActionWindow.Show();
					overlayContext.OverlayWindow.Activate();
					mouseInsideCaptureAreaReportCount = 0;
					mouseOutsideCaptureAreaReportCount = 0;
					autoScroller = new ScrollingCaptureAutoScroller(overlayContext.Monitor.MonitorName, captureArea, loggerFactory);
					autoScroller.MousePositionReport += AutoScroller_MousePositionReport;
					autoScroller.Start();
				}
			}
		}
		catch (Exception exception)
		{
			Hide();
			resultTaskSource.TrySetException(exception);
		}
	}

	[AnyThread]
	private void StopCapture()
	{
		if (autoScroller != null)
		{
			autoScroller.MousePositionReport -= AutoScroller_MousePositionReport;
			autoScroller.Dispose();
			autoScroller = null;
		}
		engine?.Stop();
	}

	private bool RenderPreview()
	{
		if (IsClosing)
		{
			return false;
		}
		if (engine.Image.Height == 0)
		{
			return false;
		}
		OverlayContext selectedContext = overlayWindowManager.GetSelectedContext();
		if (selectedContext == null)
		{
			return false;
		}
		System.Windows.Size actionPanelSize = new System.Windows.Size(selectedContext.ActionWindow.ActualWidth, selectedContext.ActionWindow.ActualHeight);
		panelPlacer.CalculatePanelRects(actionPanelSize, engine.Image.AspectRatio, out var _, out var previewPanelRect);
		ScrollingCapturePreviewWindow previewWindow = selectedContext.PreviewWindow;
		(previewWindow.DataContext as ScrollingCapturePreviewWindowVM).RenderPreview(previewBuilder, previewPanelRect.Size, selectedContext.Monitor.GetScaleFactor());
		previewWindow.Width = previewPanelRect.Width;
		previewWindow.Height = previewPanelRect.Height;
		previewWindow.SetPosition(selectedContext.OverlayWindow.PointToScreen(previewPanelRect.Location));
		if (!previewWindow.IsVisible)
		{
			previewWindow.Show();
			selectedContext.OverlayWindow.Activate();
		}
		return true;
	}

	private void ShowStartPanel(OverlayContext overlayContext)
	{
		System.Windows.Size actionPanelSize = new System.Windows.Size(overlayContext.StartWindow.ActualWidth, overlayContext.StartWindow.ActualHeight);
		Rect boundingRect = new Rect(0.0, 0.0, overlayContext.OverlayWindow.ActualWidth, overlayContext.OverlayWindow.ActualHeight);
		panelPlacer.CalculatePanelPlacements(actionPanelSize, areaSelectionManager.Area, boundingRect);
		panelPlacer.CalculatePanelRects(actionPanelSize, 1.0, out var actionPanelRect, out var _);
		overlayContext.StartWindow.SetPosition(overlayContext.OverlayWindow.PointToScreen(actionPanelRect.Location));
		overlayContext.StartWindow.Show();
		overlayContext.OverlayWindow.Activate();
	}

	private void HideStartPanel()
	{
		foreach (OverlayContext context in overlayWindowManager.Contexts)
		{
			if (context.StartWindow.IsVisible)
			{
				context.StartWindow.Hide();
			}
		}
	}

	private void ShowMessagePanel(string message)
	{
		OverlayContext selectedContext = overlayWindowManager.GetSelectedContext();
		if (selectedContext != null)
		{
			ScrollingCaptureMessageWindow messageWindow = selectedContext.MessageWindow;
			if (messageWindow.IsVisible)
			{
				messageWindow.Hide();
			}
			messageWindow.SetMessage(message);
			messageWindow.EnsureLayoutUpdated();
			System.Windows.Size messagePanelSize = new System.Windows.Size(messageWindow.ActualWidth, messageWindow.ActualHeight);
			Rect boundingRect = new Rect(0.0, 0.0, selectedContext.OverlayWindow.ActualWidth, selectedContext.OverlayWindow.ActualHeight);
			panelPlacer.CalculateMessagePanelRect(messagePanelSize, areaSelectionManager.Area, boundingRect, out var messagePanelRect);
			messageWindow.SetPosition(selectedContext.OverlayWindow.PointToScreen(messagePanelRect.Location));
			messageWindow.Show();
			selectedContext.OverlayWindow.Activate();
		}
	}

	private void HideMessagePanel()
	{
		OverlayContext selectedContext = overlayWindowManager.GetSelectedContext();
		if (selectedContext != null && selectedContext.MessageWindow.IsVisible)
		{
			selectedContext.MessageWindow.Hide();
		}
	}

	private bool IsMouseOverActionPanel()
	{
		ScrollingCaptureActionWindow scrollingCaptureActionWindow = overlayWindowManager.GetSelectedContext()?.ActionWindow;
		if (scrollingCaptureActionWindow == null)
		{
			return false;
		}
		if (!PInvoke.GetCursorPos(out var lpPoint))
		{
			return false;
		}
		HWND hWND = PInvoke.WindowFromPoint(lpPoint);
		if (hWND.IsNull)
		{
			return false;
		}
		return hWND == new WindowInteropHelper(scrollingCaptureActionWindow).Handle;
	}

	private void SaveLastSelectionArea(OverlayWindowVM overlayWindowVM)
	{
		Rectangle selectedAreaInScreenCoordinates = overlayWindowVM.GetSelectedAreaInScreenCoordinates();
		if (settings.ScreenshotLastSelectionArea != selectedAreaInScreenCoordinates)
		{
			settings.ScreenshotLastSelectionArea = selectedAreaInScreenCoordinates;
			settingsManager.SaveSettings();
		}
	}

	private void RestoreLastSelectionArea()
	{
		Rectangle screenshotLastSelectionArea = settings.ScreenshotLastSelectionArea;
		if (screenshotLastSelectionArea.Width <= 0 || screenshotLastSelectionArea.Height <= 0)
		{
			return;
		}
		OverlayWindow overlayWindow = null;
		foreach (OverlayContext context in overlayWindowManager.Contexts)
		{
			if (context.Monitor.GetRectangle().Contains(screenshotLastSelectionArea))
			{
				overlayWindow = context.OverlayWindow;
				break;
			}
		}
		if (overlayWindow != null)
		{
			overlayWindow.Activate();
			OverlayWindowVM overlayWindowVM = overlayWindow.DataContext as OverlayWindowVM;
			stateManager.SetState(OverlayWindowState.Selecting, overlayWindowVM);
			areaSelectionManager.SetArea(overlayWindow.RectFromScreen(screenshotLastSelectionArea.ToRect()), overlayWindowVM);
			stateManager.SetState(OverlayWindowState.Selected);
		}
	}

	private void CreateSelectionHintObjects()
	{
		foreach (OverlayContext context in overlayWindowManager.Contexts)
		{
			SelectionHintObjectVM selectionHintObjectVM = new SelectionHintObjectVM(context.OverlayWindowVM, "Icon.ScrollingCapture.Small");
			System.Windows.Point position = Mouse.GetPosition(context.OverlayWindow);
			selectionHintObjectVM.UpdateFloatingObjectPosition(position);
			context.OverlayWindowVM.FloatingObject = selectionHintObjectVM;
		}
	}
}
