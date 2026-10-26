using System;
using System.Buffers;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.Commands;
using SkrinshoterApp.SkrChildApp;
using SkrinshoterApp.SkrSlaveApp;
using Windows.Win32;
using Windows.Win32.Graphics.Gdi;

namespace SkrinshoterApp;

public class ScreenshotEditingWindowManager
{
	private class OverlayWindowInfo
	{
		public OverlayWindow OverlayWindow { get; }

		public ScreenshotEditingDrawingWindow DrawingWindow { get; set; }

		public ScreenshotEditingActionWindow ActionWindow { get; set; }

		public string MonitorName { get; }

		public Rectangle MonitorRectangle { get; }

		public int MonitorDpi { get; }

		public SharedMemoryRawImage Image { get; }

		public OverlayWindowInfo(OverlayWindow overlayWindow, string monitorName, Rectangle monitorRectangle, int monitorDpi, SharedMemoryRawImage image)
		{
			OverlayWindow = overlayWindow;
			MonitorName = monitorName;
			MonitorRectangle = monitorRectangle;
			MonitorDpi = monitorDpi;
			Image = image;
		}
	}

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly VideoModuleAppProxy videoModule;

	private readonly ScreenshotSaveFileDialog screenshotSaveFileDialog;

	private readonly ILoggerFactory loggerFactory;

	private readonly ILogger logger;

	private string baseFileName;

	private OverlayWindowStateManager stateManager;

	private AreaSelectionManager areaSelectionManager;

	private OverlayWindowManager overlayWindowManager;

	private readonly Dictionary<OverlayWindowVM, OverlayWindowInfo> overlayWindows = new Dictionary<OverlayWindowVM, OverlayWindowInfo>();

	private ScreenshotEditingDrawingWindowVM drawingWindowVM;

	private UndoManager undoManager;

	private ScreenshotEditor screenshotEditor;

	private ScreenshotEditorShowState screenshotEditorShowState;

	private CtrlPressWatcher ctrlPressWatcher;

	private bool selectedToolRestored;

	private TaskCompletionSource<ScreenshotEditingResult> resultTaskSource;

	public ScreenshotEditingWindowManager(SettingsManager settingsManager, VideoModuleAppProxy videoModule, ScreenshotSaveFileDialog screenshotSaveFileDialog, ILoggerFactory loggerFactory)
	{
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.videoModule = videoModule;
		this.screenshotSaveFileDialog = screenshotSaveFileDialog;
		this.loggerFactory = loggerFactory;
		logger = loggerFactory.CreateLogger<ScreenshotEditingWindowManager>();
	}

	public async Task<ScreenshotEditingResult> Show(string baseFileName, CommandInvokeType commandInvokeType)
	{
		this.baseFileName = baseFileName;
		HMONITOR hMONITOR = PInvoke.MonitorFromWindow(PInvoke.GetForegroundWindow(), MONITOR_FROM_FLAGS.MONITOR_DEFAULTTOPRIMARY);
		MonitorInfo activeMonitor = new MonitorInfo(hMONITOR);
		stateManager = new OverlayWindowStateManager();
		stateManager.StateChanged += StateManager_StateChanged;
		areaSelectionManager = new AreaSelectionManager();
		bool flag = settings.ShouldShowEditor;
		ctrlPressWatcher = new CtrlPressWatcher();
		if (commandInvokeType != CommandInvokeType.Hotkey && ctrlPressWatcher.IsDown)
		{
			flag = !flag;
		}
		screenshotEditorShowState = new ScreenshotEditorShowState(flag);
		ctrlPressWatcher.Pressed += CtrlPressWatcher_Pressed;
		foreach (MonitorInfo monitor in VirtualScreen.GetMonitors())
		{
			Rectangle monitorRectangle = monitor.GetRectangle();
			Rectangle captureArea = monitorRectangle;
			SharedMemoryRawImage image = new SharedMemoryRawImage(new ImageFormat(captureArea.Size, AudioVideoCore.PixelFormat.BGRA));
			await new TakeScreenshotSlaveAppCommand(videoModule, monitor.MonitorName, captureArea, image.SharedMemoryName, settings.DrawCursorOnScreenshot, ScreenCaptureApi.Auto, logger).Execute();
			OverlayWindow overlayWindow = new OverlayWindow(createForVideo: false)
			{
				Width = 1.0,
				Height = 1.0,
				Left = 1000000.0
			};
			overlayWindow.PreviewKeyDown += Window_PreviewKeyDown;
			overlayWindow.PreviewMouseWheel += Window_PreviewMouseWheel;
			overlayWindow.PreviewMouseRightButtonDown += OverlayWindow_PreviewMouseRightButtonDown;
			OverlayWindowVM key = (OverlayWindowVM)(overlayWindow.DataContext = new OverlayWindowVM(stateManager, areaSelectionManager, overlayWindow, monitor.GetScaleFactor(), default, image.ToBitmapSource()));
			overlayWindows.Add(key, new OverlayWindowInfo(overlayWindow, monitor.MonitorName, monitorRectangle, monitor.GetDpi(), image));
		}
		overlayWindowManager = new OverlayWindowManager(overlayWindows.Select((KeyValuePair<OverlayWindowVM, OverlayWindowInfo> x) => x.Value.OverlayWindow), areaSelectionManager);
		foreach (OverlayWindowInfo value in overlayWindows.Values)
		{
			value.OverlayWindow.Show();
			value.OverlayWindow.SetPosition(value.MonitorRectangle.Location);
			value.OverlayWindow.WindowState = WindowState.Maximized;
		}
		undoManager = new UndoManager(loggerFactory.CreateLogger<UndoManager>());
		drawingWindowVM = new ScreenshotEditingDrawingWindowVM(stateManager, undoManager);
		ScreenshotEditingActionWindowVM screenshotEditingActionWindowVM = new ScreenshotEditingActionWindowVM(settings);
		screenshotEditingActionWindowVM.ActionSelected += ActionWindowVM_ActionSelected;
		foreach (OverlayWindowInfo value2 in overlayWindows.Values)
		{
			ScreenshotEditingDrawingWindow screenshotEditingDrawingWindow = (value2.DrawingWindow = new ScreenshotEditingDrawingWindow());
			screenshotEditingDrawingWindow.Owner = value2.OverlayWindow;
			screenshotEditingDrawingWindow.PreviewKeyDown += Window_PreviewKeyDown;
			screenshotEditingDrawingWindow.PreviewMouseWheel += Window_PreviewMouseWheel;
			screenshotEditingDrawingWindow.DataContext = drawingWindowVM;
			screenshotEditingDrawingWindow.Left = 1000000.0;
			screenshotEditingDrawingWindow.Show();
			screenshotEditingDrawingWindow.Hide();
			ScreenshotEditingActionWindow screenshotEditingActionWindow = (value2.ActionWindow = new ScreenshotEditingActionWindow());
			screenshotEditingActionWindow.Owner = value2.OverlayWindow;
			screenshotEditingActionWindow.PreviewKeyDown += Window_PreviewKeyDown;
			screenshotEditingActionWindow.PreviewMouseWheel += Window_PreviewMouseWheel;
			screenshotEditingActionWindow.DataContext = screenshotEditingActionWindowVM;
			screenshotEditingActionWindow.Left = 1000000.0;
			screenshotEditingActionWindow.Show();
			screenshotEditingActionWindow.Hide();
		}
		screenshotEditor = new ScreenshotEditor(drawingWindowVM, overlayWindowManager, stateManager, undoManager);
		CreateSelectionHintObjects();
		overlayWindows.Values.Where((OverlayWindowInfo x) => x.MonitorName == activeMonitor.MonitorName).FirstOrDefault()?.OverlayWindow.Activate();
		resultTaskSource = new TaskCompletionSource<ScreenshotEditingResult>();
		selectedToolRestored = false;
		RestoreEditorSettings();
		if (settings.UseLastSelectionArea)
		{
			RestoreLastSelectionArea();
		}
		return await resultTaskSource.Task;
	}

	public void Hide()
	{
		foreach (OverlayWindowInfo value in overlayWindows.Values)
		{
			value.OverlayWindow.ForceClose();
			value.Image.Dispose();
			value.DrawingWindow?.ForceClose();
			value.ActionWindow?.ForceClose();
		}
		overlayWindows.Clear();
		ctrlPressWatcher?.Dispose();
	}

	private void StateManager_StateChanged(object sender, EventArgs e)
	{
		switch (stateManager.State)
		{
		case OverlayWindowState.Selecting:
		case OverlayWindowState.MovingFrame:
		case OverlayWindowState.ResizingFrame:
			HidePanels();
			break;
		case OverlayWindowState.DrawingObject:
			if (!drawingWindowVM.SelectedTool.In(DrawingTool.Number, DrawingTool.Text) && overlayWindowManager.GetSelectedWindowVM() == stateManager.ActiveWindowVM)
			{
				HidePanels();
			}
			break;
		case OverlayWindowState.MovingObject:
		case OverlayWindowState.ResizingObject:
			if (overlayWindowManager.GetSelectedWindowVM() == stateManager.ActiveWindowVM)
			{
				HidePanels();
			}
			break;
		case OverlayWindowState.Selected:
			if (areaSelectionManager.HasArea)
			{
				if (screenshotEditorShowState.ShouldShow)
				{
					ShowPanels(overlayWindows[areaSelectionManager.Owner]);
				}
				else
				{
					ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.Default));
				}
			}
			else
			{
				CreateSelectionHintObjects();
			}
			break;
		case OverlayWindowState.DrawingToolSelected:
			if (areaSelectionManager.HasArea)
			{
				ShowPanels(overlayWindows[areaSelectionManager.Owner]);
			}
			break;
		case OverlayWindowState.Canceling:
			areaSelectionManager.Clear();
			HidePanels();
			break;
		case OverlayWindowState.Canceled:
			ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.Close));
			break;
		case OverlayWindowState.AdjustingFrame:
		case OverlayWindowState.ReadyToMoveObject:
		case OverlayWindowState.EditingText:
		case OverlayWindowState.Recording:
		case OverlayWindowState.Scrolling:
			break;
		}
	}

	private void OverlayWindow_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
	{
		screenshotEditor.OverlayWindow_PreviewMouseRightButtonDown(sender, e);
		if (!e.Handled)
		{
			OverlayWindowVM selectedWindowVM = overlayWindowManager.GetSelectedWindowVM();
			if (selectedWindowVM != null && stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.DrawingToolSelected))
			{
				stateManager.SetState(OverlayWindowState.Selecting, selectedWindowVM);
				areaSelectionManager.Clear();
				stateManager.SetState(OverlayWindowState.Selected);
				HidePanels();
				e.Handled = true;
			}
		}
	}

	private unsafe void ActionWindowVM_ActionSelected(object sender, ScreenshotEditingActionSelectedEventArgs e)
	{
		SharedMemoryRawImage sharedMemoryRawImage = null;
		string text = null;
		logger.LogInformation("Screenshot editing action: {action}", e.Action);
		try
		{
			if (e.Action == ScreenshotEditingAction.SaveToFile)
			{
				text = screenshotSaveFileDialog.Show(baseFileName);
				if (text == null)
				{
					return;
				}
			}
			PrintDialogService printDialogService = null;
			if (e.Action == ScreenshotEditingAction.Print)
			{
				printDialogService = new PrintDialogService(loggerFactory.CreateLogger<PrintDialogService>());
				if (!printDialogService.Show(overlayWindowManager.GetSelectedWindow()))
				{
					logger.LogInformation("Print canceled");
					return;
				}
			}
			SaveEditorSettings();
			if (e.Action != ScreenshotEditingAction.Close)
			{
				OverlayWindowVM selectedWindowVM = overlayWindowManager.GetSelectedWindowVM();
				OverlayWindowInfo overlayWindowInfo = overlayWindows[selectedWindowVM];
				Rectangle selectedAreaInRelativeScreenCoordinates = selectedWindowVM.GetSelectedAreaInRelativeScreenCoordinates();
				stateManager.SetState(OverlayWindowState.Selected);
				RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(overlayWindowInfo.MonitorRectangle.Width, overlayWindowInfo.MonitorRectangle.Height, overlayWindowInfo.MonitorDpi, overlayWindowInfo.MonitorDpi, PixelFormats.Pbgra32);
				renderTargetBitmap.Render(overlayWindowInfo.OverlayWindow.DrawingCanvas);
				sharedMemoryRawImage = new SharedMemoryRawImage(new ImageFormat(selectedAreaInRelativeScreenCoordinates.Size, AudioVideoCore.PixelFormat.BGRA));
				using (MemoryHandle memoryHandle = sharedMemoryRawImage.Data[0].Pin())
				{
					renderTargetBitmap.CopyPixels(new Int32Rect(selectedAreaInRelativeScreenCoordinates.X, selectedAreaInRelativeScreenCoordinates.Y, selectedAreaInRelativeScreenCoordinates.Width, selectedAreaInRelativeScreenCoordinates.Height), (IntPtr)memoryHandle.Pointer, sharedMemoryRawImage.Stride[0] * sharedMemoryRawImage.Format.Height, sharedMemoryRawImage.Stride[0]);
				}
				if (printDialogService != null)
				{
					new BitmapPrintService().Print(sharedMemoryRawImage.ToBitmapSource(), printDialogService.PrinterName, printDialogService.PrintTicket);
				}
				SaveLastSelectionArea(selectedWindowVM);
			}
			Hide();
			resultTaskSource.TrySetResult(new ScreenshotEditingResult(e.Action, sharedMemoryRawImage, text));
		}
		catch
		{
			sharedMemoryRawImage?.Dispose();
			throw;
		}
	}

	private void CtrlPressWatcher_Pressed(object sender, EventArgs e)
	{
		if ((stateManager.State == OverlayWindowState.Selected && !areaSelectionManager.HasArea) || stateManager.State == OverlayWindowState.Selecting)
		{
			screenshotEditorShowState.ToggleShouldShow();
		}
	}

	private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Source is ComboBox { IsDropDownOpen: not false } || e.OriginalSource is TextBox)
		{
			return;
		}
		OverlayWindowVM overlayWindowVM = overlayWindowManager.GetSelectedWindowVM();
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
				if (overlayWindowVM != null && stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.DrawingToolSelected))
				{
					screenshotEditorShowState.SetShouldShow(shouldShow: true);
					overlayWindowVM.SelectAllScreenCommand.ExecuteIfCan();
				}
				e.Handled = true;
				return;
			}
			break;
		}
		if (MatchesHotkey(e, settings.UploadScreenshotToAllHotkey))
		{
			ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.UploadToAll));
			e.Handled = true;
			return;
		}
		if (overlayWindowVM == null || overlayWindowVM.ProcessKey(e.Key, e.KeyboardDevice.Modifiers))
		{
			return;
		}
		Key key = e.Key;
		if (key <= Key.Insert)
		{
			if (key != Key.Return)
			{
				if (key == Key.Insert && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
				{
					goto IL_014c;
				}
			}
			else if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.UploadToCloud));
				return;
			}
		}
		else if (key != Key.C)
		{
			switch (key)
			{
			case Key.P:
				if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
				{
					ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.Print));
					return;
				}
				break;
			case Key.S:
				if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
				{
					ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.SaveToFile));
					return;
				}
				break;
			}
		}
		else if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
		{
			goto IL_014c;
		}
		switch (e.Key)
		{
		case Key.A:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Arrow);
				return;
			}
			break;
		case Key.L:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Line);
				return;
			}
			break;
		case Key.P:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Pencil);
				return;
			}
			break;
		case Key.H:
			if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
			{
				break;
			}
			goto IL_0297;
		case Key.M:
			if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
			{
				break;
			}
			goto IL_0297;
		case Key.R:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Rectangle);
				return;
			}
			break;
		case Key.O:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Oval);
				return;
			}
			break;
		case Key.T:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Text);
				return;
			}
			break;
		case Key.N:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Number);
				return;
			}
			break;
		case Key.B:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.None)
			{
				drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Blur);
				return;
			}
			break;
		case Key.Z:
			if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
			{
				undoManager?.UndoCommand.ExecuteIfCan();
				return;
			}
			break;
		case Key.Y:
			{
				if (e.KeyboardDevice.Modifiers == ModifierKeys.Control)
				{
					undoManager?.RedoCommand.ExecuteIfCan();
					return;
				}
				break;
			}
			IL_0297:
			drawingWindowVM?.SelectToolByHotkeyCommand.ExecuteIfCan(DrawingTool.Marker);
			return;
		}
		switch (e.Key)
		{
		case Key.Delete:
			if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
			{
				break;
			}
			goto IL_03e2;
		case Key.Back:
			{
				if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
				{
					break;
				}
				goto IL_03e2;
			}
			IL_03e2:
			screenshotEditor?.DeleteSelectedObject();
			break;
		}
		return;
		IL_014c:
		ActionWindowVM_ActionSelected(sender, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.CopyToClipboard));
	}

	private static bool MatchesHotkey(KeyEventArgs e, Hotkey hotkey)
	{
		Key key = e.Key == Key.System ? e.SystemKey : e.Key;
		return !hotkey.IsEmpty && hotkey.Key == key && hotkey.Modifiers == e.KeyboardDevice.Modifiers;
	}

	private void Window_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
	{
		drawingWindowVM?.ChangeLineThicknessByMouseCommand.Execute(e);
	}

	private void ShowPanels(OverlayWindowInfo overlayWindowInfo)
	{
		OverlayWindow overlayWindow = overlayWindowInfo.OverlayWindow;
		_ = overlayWindow.DataContext;
		ScreenshotEditingDrawingWindow drawingWindow = overlayWindowInfo.DrawingWindow;
		ScreenshotEditingActionWindow actionWindow = overlayWindowInfo.ActionWindow;
		if (drawingWindow != null && actionWindow != null)
		{
			System.Windows.Size drawingPanelSize = new System.Windows.Size(drawingWindow.ActualWidth, drawingWindow.ActualHeight);
			System.Windows.Size actionPanelSize = new System.Windows.Size(actionWindow.ActualWidth, actionWindow.ActualHeight);
			Rect area = areaSelectionManager.Area;
			Rect boundingRect = new Rect(0.0, 0.0, overlayWindow.ActualWidth, overlayWindow.ActualHeight);
			new ScreenshotEditingPanelPlacer().CalculatePanelRects(drawingPanelSize, actionPanelSize, area, boundingRect, out var drawingPanelRect, out var actionPanelRect);
			System.Windows.Point position = overlayWindow.PointToScreen(drawingPanelRect.Location);
			System.Windows.Point position2 = overlayWindow.PointToScreen(actionPanelRect.Location);
			bool num = !drawingWindow.IsVisible || !actionWindow.IsVisible;
			drawingWindow.SetPosition(position, activate: false);
			actionWindow.SetPosition(position2, activate: false);
			if (!drawingWindow.IsVisible && drawingWindowVM.SelectedTool != DrawingTool.None)
			{
				stateManager.SetState(OverlayWindowState.DrawingToolSelected);
			}
			if (!drawingWindow.IsVisible)
			{
				drawingWindow.Show();
			}
			if (!actionWindow.IsVisible)
			{
				actionWindow.Show();
			}
			if (!selectedToolRestored)
			{
				RestoreSelectedTool();
				selectedToolRestored = true;
			}
			if (num)
			{
				overlayWindow.Activate();
			}
		}
	}

	private void HidePanels()
	{
		foreach (OverlayWindowInfo value in overlayWindows.Values)
		{
			ScreenshotEditingDrawingWindow drawingWindow = value.DrawingWindow;
			if (drawingWindow != null && drawingWindow.IsVisible)
			{
				value.DrawingWindow.Hide();
			}
			ScreenshotEditingActionWindow actionWindow = value.ActionWindow;
			if (actionWindow != null && actionWindow.IsVisible)
			{
				value.ActionWindow.Hide();
			}
		}
	}

	private void SaveEditorSettings()
	{
		if (drawingWindowVM != null)
		{
			bool flag = false;
			DrawingTool selectedTool = drawingWindowVM.SelectedTool;
			if (settings.EditorSelectedTool != selectedTool)
			{
				settings.EditorSelectedTool = selectedTool;
				flag = true;
			}
			int num = (int)Math.Round(screenshotEditor.LineThickness);
			if (settings.EditorSelectedLineThickness != num)
			{
				settings.EditorSelectedLineThickness = num;
				flag = true;
			}
			System.Windows.Media.Color color = screenshotEditor.Color;
			if (settings.EditorSelectedColor != color)
			{
				settings.EditorSelectedColor = color;
				flag = true;
			}
			List<System.Windows.Media.Color> list = drawingWindowVM.AvailableColors.ToList();
			if (!settings.EditorAvailableColors.SequenceEqual(list))
			{
				settings.EditorAvailableColors = list;
				flag = true;
			}
			if (flag)
			{
				settingsManager.SaveSettings();
			}
		}
	}

	private void RestoreEditorSettings()
	{
		drawingWindowVM.SelectedLineThickness = MathEx.Clamp(settings.EditorSelectedLineThickness, 2, 60);
		for (int i = 0; i < Math.Min(settings.EditorAvailableColors.Count, 9); i++)
		{
			drawingWindowVM.AvailableColors[i] = settings.EditorAvailableColors[i];
		}
		drawingWindowVM.SelectedColor = settings.EditorSelectedColor;
		drawingWindowVM.RaiseSelectedColorChanged();
	}

	private void RestoreSelectedTool()
	{
		if (settings.EditorDefaultTool == EditorDefaultTool.None)
		{
			drawingWindowVM.SelectedTool = DrawingTool.None;
		}
		else if (settings.EditorDefaultTool == EditorDefaultTool.Arrow)
		{
			drawingWindowVM.SelectedTool = DrawingTool.Arrow;
		}
		else
		{
			drawingWindowVM.SelectedTool = settings.EditorSelectedTool;
		}
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
		foreach (OverlayWindowInfo value in overlayWindows.Values)
		{
			if (value.MonitorRectangle.Contains(screenshotLastSelectionArea))
			{
				overlayWindow = value.OverlayWindow;
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
		foreach (OverlayWindow window in overlayWindowManager.Windows)
		{
			OverlayWindowVM overlayWindowVM = window.DataContext as OverlayWindowVM;
			SelectionHintObjectVM selectionHintObjectVM = new SelectionHintObjectVM(overlayWindowVM, "Icon.Screenshot", screenshotEditorShowState);
			System.Windows.Point position = Mouse.GetPosition(window);
			selectionHintObjectVM.UpdateFloatingObjectPosition(position);
			overlayWindowVM.FloatingObject = selectionHintObjectVM;
		}
	}
}
