using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using AudioDevices;
using Common;
using MediatR;
using Microsoft.Extensions.Logging;
using ScreenRecorderCommon;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class VideoRecordingWindowManager : BaseDisposable
{
	private readonly SettingsManager settingsManager;

	private readonly Dispatcher dispatcher;

	private readonly SelectAudioDevicesVM selectAudioDevicesVM;

	private readonly SelectCameraDevicesVM selectCameraDevicesVM;

	private readonly IMediator mediator;

	private readonly LoggerFactory loggerFactory;

	private readonly ILogger logger;

	private OverlayWindowStateManager stateManager;

	private AreaSelectionManager areaSelectionManager;

	private OverlayWindowManager overlayWindowManager;

	private readonly Dictionary<OverlayWindowVM, OverlayWindow> overlayWindowsVMs = new Dictionary<OverlayWindowVM, OverlayWindow>();

	private VideoRecordingControlWindow controlWindow;

	private VideoRecordingControlWindowVM controlWindowVM;

	private readonly VideoRecordingPanelPlacer panelPlacer = new VideoRecordingPanelPlacer();

	private bool controlWindowRepositionScheduled;

	private System.Windows.Size maxControlPanelSize;

	private AudioLevelMeter inputAudioLevelMeter;

	private AudioLevelMeter outputAudioLevelMeter;

	private VideoRecordingCameraWindow cameraWindow;

	private VideoRecordingCameraWindowVM cameraWindowVM;

	private int frameRate;

	private VideoRecordingLowDiskSpaceWindow lowDiskSpaceWindow;

	private string diskName;

	private readonly VideoRecordingMutedMicrophoneWarningManager mutedMicrophoneWarningManager;

	private TaskCompletionSource<VideoRecordingAreaSelection> tcs;

	public VideoRecordingWindowManager(SettingsManager settingsManager, Dispatcher dispatcher, SelectAudioDevicesVM selectAudioDevicesVM, SelectCameraDevicesVM selectCameraDevicesVM, IMediator mediator, VideoRecordingMutedMicrophoneWarningManager mutedMicrophoneWarningManager, LoggerFactory loggerFactory)
	{
		this.settingsManager = settingsManager;
		this.dispatcher = dispatcher;
		this.selectAudioDevicesVM = selectAudioDevicesVM;
		this.selectCameraDevicesVM = selectCameraDevicesVM;
		this.mediator = mediator;
		this.mutedMicrophoneWarningManager = mutedMicrophoneWarningManager;
		this.loggerFactory = loggerFactory;
		logger = loggerFactory.CreateLogger<VideoRecordingWindowManager>();
		selectCameraDevicesVM.PropertyChanged += SelectCameraDevicesVM_PropertyChanged;
		mutedMicrophoneWarningManager.WindowPlacementRequested += (object _, VideoRecordingMutedMicrophoneWindow warningWindow) =>
		{
			PlaceMutedMicrophoneWarningWindow(warningWindow);
		};
	}

	public Task<VideoRecordingAreaSelection> SelectVideoRecordingArea(string diskName, int frameRate, NotificationService notificationService, IIPCProxy videoModule)
	{
		if (!dispatcher.CheckAccess())
		{
			return dispatcher.InvokeAsync(() => SelectVideoRecordingArea(diskName, frameRate, notificationService, videoModule)).Task.Unwrap();
		}
		this.diskName = diskName;
		this.frameRate = frameRate;
		stateManager = new OverlayWindowStateManager();
		stateManager.StateChanged += StateManager_StateChanged;
		areaSelectionManager = new AreaSelectionManager();
		try
		{
			OverlayWindow overlayWindow = null;
			foreach (MonitorInfo monitor in VirtualScreen.GetMonitors())
			{
				OverlayWindow overlayWindow2 = new OverlayWindow(createForVideo: true)
				{
					Width = 1.0,
					Height = 1.0
				};
				overlayWindow2.PreviewKeyDown += Window_PreviewKeyDown;
				OverlayWindowVM key = (OverlayWindowVM)(overlayWindow2.DataContext = new OverlayWindowVM(stateManager, areaSelectionManager, overlayWindow2, monitor.GetScaleFactor()));
				overlayWindowsVMs.Add(key, overlayWindow2);
				if (monitor.IsPrimary())
				{
					overlayWindow = overlayWindow2;
				}
				overlayWindow2.Show();
				overlayWindow2.SetPosition(monitor.GetRectangle().Location);
				overlayWindow2.WindowState = WindowState.Maximized;
			}
			overlayWindowManager = new OverlayWindowManager(overlayWindowsVMs.Values, areaSelectionManager);
			controlWindow = new VideoRecordingControlWindow(loggerFactory.CreateLogger<VideoRecordingControlWindow>());
			controlWindow.PreviewKeyDown += Window_PreviewKeyDown;
			inputAudioLevelMeter = new AudioLevelMeter(dispatcher);
			outputAudioLevelMeter = new AudioLevelMeter(dispatcher);
			controlWindowVM = new VideoRecordingControlWindowVM(settingsManager.Settings, selectAudioDevicesVM, selectCameraDevicesVM, inputAudioLevelMeter, outputAudioLevelMeter, mediator, ConfirmRecordingCancellation);
			controlWindow.DataContext = controlWindowVM;
			controlWindow.DragMoveFinished += ControlWindow_DragMoveFinished;
			controlWindow.Left = 1000000.0;
			controlWindow.Show();
			maxControlPanelSize = controlWindow.MeasureAndFreezeMaxSize();
			controlWindow.HidePanel();
			controlWindowVM.PropertyChanged += ControlWindowVM_PropertyChanged;
			UpdateCameraWindow();
			CreateSelectionHintObjects();
			overlayWindow?.Activate();
			tcs = new TaskCompletionSource<VideoRecordingAreaSelection>();
			return tcs.Task;
		}
		catch
		{
			Hide();
			throw;
		}
	}

	public void ShowLowDiskSpaceWindow()
	{
		if (!dispatcher.CheckAccess())
		{
			dispatcher.Invoke(ShowLowDiskSpaceWindow);
			return;
		}
		if (lowDiskSpaceWindow == null)
		{
			lowDiskSpaceWindow = new VideoRecordingLowDiskSpaceWindow
			{
				Owner = controlWindow,
				DataContext = new VideoRecordingLowDiskSpaceWindowVM(diskName)
			};
		}
		lowDiskSpaceWindow.Show();
	}

	public void Hide()
	{
		if (!dispatcher.CheckAccess())
		{
			dispatcher.Invoke(Hide);
			return;
		}
		inputAudioLevelMeter?.Dispose();
		inputAudioLevelMeter = null;
		outputAudioLevelMeter?.Dispose();
		outputAudioLevelMeter = null;
		selectAudioDevicesVM.InputAudioDeviceErrorMessage = null;
		selectAudioDevicesVM.OutputAudioDeviceErrorMessage = null;
		lowDiskSpaceWindow?.Close();
		lowDiskSpaceWindow = null;
		mutedMicrophoneWarningManager.Hide();
		foreach (OverlayWindow value in overlayWindowsVMs.Values)
		{
			value.ForceClose();
		}
		overlayWindowsVMs.Clear();
		controlWindow?.ForceClose();
		controlWindow = null;
		DestroyCameraWindow();
		overlayWindowManager = null;
	}

	public void UpdateRecordingProgress(string[] args)
	{
		if (args != null && args.Length != 0 && TimeSpan.TryParse(args[0], out var result) && TimeSpan.TryParse(args[1], out var result2))
		{
			controlWindowVM?.UpdateDuration(result, result2);
		}
	}

	[AnyThread]
	public void UpdateAudioLevel(string[] args)
	{
		try
		{
			CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 2);
			AudioDataFlow audioDataFlow = commandArgumentReader.ReadEnum<AudioDataFlow>();
			if (!audioDataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
			{
				throw new FormatException($"Invalid value of audio data flow: {audioDataFlow}.");
			}
			float rms = commandArgumentReader.ReadSingle("audio level", 0f, 1f);
			((audioDataFlow == AudioDataFlow.Capture) ? inputAudioLevelMeter : outputAudioLevelMeter)?.Report(rms);
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to handle audio level event");
		}
	}

	[AnyThread]
	public void UpdateAudioDeviceFailed(string[] args)
	{
		try
		{
			CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, Math.Max(args.Length, 3));
			AudioDataFlow dataFlow = commandArgumentReader.ReadEnum<AudioDataFlow>();
			if (!dataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
			{
				throw new FormatException($"Invalid value of audio data flow: {dataFlow}.");
			}
			string deviceId = commandArgumentReader.ReadNonEmptyString("device id");
			ErrorCode errorCode = commandArgumentReader.ReadEnum<ErrorCode>();
			logger.LogWarning("Audio device failed: flow={dataFlow}, code={errorCode}", dataFlow, errorCode);
			string errorMessage = GetAudioDeviceErrorMessage(dataFlow, errorCode);
			dispatcher.Invoke(() =>
			{
				selectAudioDevicesVM.ReportAudioDeviceFailure(dataFlow, deviceId, errorMessage);
			});
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to handle audio device failure event");
		}
	}

	public void SetPauseState(bool paused)
	{
		if (!dispatcher.CheckAccess())
		{
			dispatcher.Invoke(() =>
			{
				SetPauseState(paused);
			});
		}
		else
		{
			controlWindowVM?.SetPauseState(paused);
			mutedMicrophoneWarningManager.SetPauseState(paused);
		}
	}

	private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Source is ComboBox { IsDropDownOpen: not false } || overlayWindowManager == null)
		{
			return;
		}
		OverlayWindowVM overlayWindowVM = overlayWindowManager.GetSelectedWindowVM();
		if (e.Key == Key.A && e.KeyboardDevice.Modifiers == ModifierKeys.Control)
		{
			if (overlayWindowVM == null)
			{
				overlayWindowVM = (sender as OverlayWindow)?.DataContext as OverlayWindowVM;
			}
			if (overlayWindowVM != null && stateManager.State == OverlayWindowState.Selected)
			{
				overlayWindowVM.SelectAllScreenCommand.ExecuteIfCan();
			}
		}
		else
		{
			overlayWindowVM?.ProcessKey(e.Key, e.KeyboardDevice.Modifiers);
		}
	}

	private void StateManager_StateChanged(object sender, EventArgs e)
	{
		switch (stateManager.State)
		{
		case OverlayWindowState.Selecting:
		case OverlayWindowState.MovingFrame:
		case OverlayWindowState.ResizingFrame:
			controlWindow.HidePanel();
			UpdateCameraWindow();
			break;
		case OverlayWindowState.Selected:
		{
			OverlayWindow overlayWindow = overlayWindowManager?.GetSelectedWindow();
			if (overlayWindow != null)
			{
				System.Windows.Size size = controlWindow.GetPanelRect().Size;
				System.Windows.Size maxPanelSize = new System.Windows.Size(Math.Max(maxControlPanelSize.Width, size.Width), Math.Max(maxControlPanelSize.Height, size.Height));
				panelPlacer.CalculatePanelPlacement(maxPanelSize, areaSelectionManager.Area, new Rect(0.0, 0.0, overlayWindow.ActualWidth, overlayWindow.ActualHeight));
				controlWindow.PanelHorizontalAnchor = panelPlacer.HorizontalAnchor;
				controlWindow.PanelVerticalAnchor = panelPlacer.VerticalAnchor;
				PlaceControlWindow();
				if (controlWindowVM.Settings.StartRecordingVideoImmediatelyAfterSelection)
				{
					controlWindowVM.State = VideoRecordingControlWindowState.Recording;
				}
				UpdateCameraWindow();
				controlWindow?.ShowPanel();
				controlWindow?.Focus();
			}
			break;
		}
		case OverlayWindowState.Canceling:
			areaSelectionManager.Clear();
			controlWindow.HidePanel();
			UpdateCameraWindow();
			break;
		case OverlayWindowState.Canceled:
			Hide();
			tcs.TrySetResult(null);
			break;
		}
	}

	private void ControlWindowVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "State")
		{
			switch (controlWindowVM.State)
			{
			case VideoRecordingControlWindowState.Recording:
			{
				OverlayWindowVM selectedWindowVM = overlayWindowManager.GetSelectedWindowVM();
				if (selectedWindowVM == null)
				{
					AppLogger.Logger.Error(new InvalidOperationException("Cannot find the recording frame window."));
					Hide();
					tcs.TrySetResult(null);
					break;
				}
				OverlayWindow overlayWindow = overlayWindowsVMs[selectedWindowVM];
				foreach (KeyValuePair<OverlayWindowVM, OverlayWindow> item in overlayWindowsVMs.ToList())
				{
					if (item.Key != selectedWindowVM)
					{
						item.Value.ForceClose();
						overlayWindowsVMs.Remove(item.Key);
					}
				}
				overlayWindow.MakeTransparentForMouseEvents();
				stateManager.SetState(OverlayWindowState.Recording, selectedWindowVM);
				MonitorInfo monitor = overlayWindow.GetMonitor();
				string monitorName = monitor.MonitorName;
				Rectangle rectangle = monitor.GetRectangle();
				Rectangle selectedAreaInScreenCoordinates = selectedWindowVM.GetSelectedAreaInScreenCoordinates();
				selectedAreaInScreenCoordinates.Offset(-rectangle.X, -rectangle.Y);
				if (!controlWindowVM.Settings.ShowVideoControlPanel)
				{
					controlWindow?.ForceClose();
					controlWindow = null;
				}
				mutedMicrophoneWarningManager.NotifyRecordingStarted();
				tcs.TrySetResult(new VideoRecordingAreaSelection(monitorName, selectedAreaInScreenCoordinates));
				break;
			}
			case VideoRecordingControlWindowState.Canceled:
				Hide();
				tcs.TrySetResult(null);
				break;
			}
		}
		else if (e.PropertyName == "PanelObservedWidth" || e.PropertyName == "PanelObservedHeight")
		{
			ScheduleControlWindowReposition();
		}
	}

	private void ControlWindow_DragMoveFinished(object sender, DragMoveFinishedEventArgs e)
	{
		if (e.IsMoved)
		{
			panelPlacer.SetManualPlacement();
			controlWindow.PanelHorizontalAnchor = panelPlacer.HorizontalAnchor;
			controlWindow.PanelVerticalAnchor = panelPlacer.VerticalAnchor;
		}
		MakeControlWindowFullyVisibleOnOneScreen();
	}

	private static string GetAudioDeviceErrorMessage(AudioDataFlow dataFlow, ErrorCode errorCode)
	{
		if (errorCode == ErrorCode.MicrophoneAccessDenied)
		{
			return Resources.Microphone_AccessDenied;
		}
		if (dataFlow != AudioDataFlow.Capture)
		{
			return Resources.SystemSound_GeneralError;
		}
		return Resources.Microphone_GeneralError;
	}

	private void ScheduleControlWindowReposition()
	{
		if (!controlWindowRepositionScheduled)
		{
			controlWindowRepositionScheduled = true;
			dispatcher.BeginInvoke((Action)(() =>
			{
				controlWindowRepositionScheduled = false;
				RepositionControlWindow();
			}), DispatcherPriority.Render);
		}
	}

	private void PlaceControlWindow()
	{
		if (controlWindow == null || stateManager == null || !stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.Recording))
		{
			return;
		}
		OverlayWindow overlayWindow = overlayWindowManager?.GetSelectedWindow();
		if (overlayWindow == null)
		{
			return;
		}
		panelPlacer.CalculatePanelRect(controlWindow.GetPanelRect().Size, out var panelRect);
		System.Windows.Point position = overlayWindow.PointToScreen(controlWindow.CalculateWindowLocation(panelRect.Location));
		System.Windows.Point point = controlWindow.PointToScreen(new System.Windows.Point(0.0, 0.0));
		if (!(Math.Abs(position.X - point.X) < 1.0) || !(Math.Abs(position.Y - point.Y) < 1.0))
		{
			if (VisualTreeHelper.GetDpi(controlWindow).DpiScaleX != VisualTreeHelper.GetDpi(overlayWindow).DpiScaleX)
			{
				controlWindow.SetPosition(overlayWindow.PointToScreen(new System.Windows.Point(0.0, 0.0)));
			}
			controlWindow.SetPosition(position);
		}
	}

	private void RepositionControlWindow()
	{
		if (controlWindow == null)
		{
			return;
		}
		if (panelPlacer.IsManual)
		{
			if (stateManager != null && stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.Recording))
			{
				MakeControlWindowFullyVisibleOnOneScreen();
			}
		}
		else
		{
			PlaceControlWindow();
		}
	}

	private void MakeControlWindowFullyVisibleOnOneScreen()
	{
		Rect movingRect = controlWindow.RectToScreen(controlWindow.GetPanelRect());
		MonitorInfo monitor = controlWindow.GetMonitor();
		Rectangle rectangle = monitor.GetRectangle();
		OverlayWindowVM selectedWindowVM = overlayWindowManager.GetSelectedWindowVM();
		if (selectedWindowVM != null)
		{
			MonitorInfo monitor2 = overlayWindowsVMs[selectedWindowVM].GetMonitor();
			if (monitor.MonitorName != monitor2.MonitorName)
			{
				rectangle = monitor.GetWorkingArea();
			}
		}
		Rect rect = movingRect.MoveInsideRect(rectangle.ToRect());
		if (!(rect.Location == movingRect.Location))
		{
			System.Windows.Point point = controlWindow.PointToScreen(new System.Windows.Point(0.0, 0.0));
			controlWindow.SetPosition(new System.Windows.Point(point.X + rect.X - movingRect.X, point.Y + rect.Y - movingRect.Y));
		}
	}

	[UIThread]
	private bool ConfirmRecordingCancellation()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			VideoRecordingCancelConfirmationWindow window = new VideoRecordingCancelConfirmationWindow
			{
				Owner = controlWindow
			};
			Rect screenRect = overlayWindowManager.GetSelectedWindow().RectToScreen(overlayWindowManager.GetSelectedWindowVM().FrameRect);
			bool valueOrDefault = window.ShowDialogCentered(screenRect) == true;
			uIThreadAttribute.OnExit();
			return valueOrDefault;
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private void PlaceMutedMicrophoneWarningWindow(VideoRecordingMutedMicrophoneWindow warningWindow)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[1] { warningWindow });
		uIThreadAttribute.OnEntry();
		try
		{
			OverlayWindow overlayWindow = overlayWindowManager?.GetSelectedWindow();
			OverlayWindowVM overlayWindowVM = overlayWindowManager?.GetSelectedWindowVM();
			if (overlayWindow != null && overlayWindowVM != null)
			{
				System.Windows.Size warningWindowSize = new System.Windows.Size(warningWindow.ActualWidth, warningWindow.ActualHeight);
				Rect boundingRect = new Rect(0.0, 0.0, overlayWindow.ActualWidth, overlayWindow.ActualHeight);
				Rect? panelRect = null;
				if (controlWindow != null)
				{
					Rect rect = controlWindow.RectToScreen(controlWindow.GetPanelRect());
					panelRect = new Rect(overlayWindow.PointFromScreen(rect.TopLeft), overlayWindow.PointFromScreen(rect.BottomRight));
				}
				panelPlacer.CalculateMutedMicrophoneWarningRect(warningWindowSize, overlayWindowVM.FrameRect, boundingRect, panelRect, out var warningWindowRect);
				warningWindow.SetPosition(overlayWindow.PointToScreen(warningWindowRect.Location), activate: false);
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	private void SelectCameraDevicesVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (!dispatcher.CheckAccess())
		{
			dispatcher.Invoke(() =>
			{
				SelectCameraDevicesVM_PropertyChanged(sender, e);
			});
		}
		else if (e.PropertyName == "EffectiveCameraDeviceId")
		{
			UpdateCameraWindow();
		}
	}

	private void CameraWindowVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (!dispatcher.CheckAccess())
		{
			dispatcher.Invoke(() =>
			{
				CameraWindowVM_PropertyChanged(sender, e);
			});
			return;
		}
		if (e.PropertyName == "WindowShape")
		{
			UpdateCameraWindow();
		}
		if (e.PropertyName == "State" || e.PropertyName == "ErrorMessage")
		{
			UpdateCameraDeviceErrorMessage();
		}
	}

	private void UpdateCameraWindow()
	{
		OverlayWindow overlayWindow = overlayWindowManager?.GetSelectedWindow();
		OverlayWindowVM overlayWindowVM = overlayWindowManager?.GetSelectedWindowVM();
		if (overlayWindow == null || overlayWindowVM == null)
		{
			DestroyCameraWindow();
			return;
		}
		string effectiveCameraDeviceId = selectCameraDevicesVM.EffectiveCameraDeviceId;
		if (effectiveCameraDeviceId == null)
		{
			DestroyCameraWindow();
			return;
		}
		if (cameraWindowVM != null && cameraWindowVM.CameraDeviceId != effectiveCameraDeviceId)
		{
			DestroyCameraWindow();
		}
		if (cameraWindow == null)
		{
			cameraWindow = new VideoRecordingCameraWindow();
			cameraWindow.WindowActivated += (object s, EventArgs e) =>
			{
				controlWindow?.Activate();
			};
			cameraWindow.Width = 0.0;
			cameraWindow.Height = 0.0;
			cameraWindow.Show();
			cameraWindow.Hide();
		}
		if (!stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.Recording))
		{
			if (cameraWindow.IsVisible)
			{
				cameraWindow.Hide();
			}
			return;
		}
		if (cameraWindowVM == null)
		{
			cameraWindowVM = new VideoRecordingCameraWindowVM(settingsManager, dispatcher, cameraWindow, effectiveCameraDeviceId, frameRate, loggerFactory);
			cameraWindowVM.PropertyChanged += CameraWindowVM_PropertyChanged;
			cameraWindow.DataContext = cameraWindowVM;
		}
		Rect area = areaSelectionManager.Area;
		if (area.Width < cameraWindowVM.WindowMinWidth || area.Height < cameraWindowVM.WindowMinHeight)
		{
			DestroyCameraWindow();
			return;
		}
		cameraWindowVM.SelectionRectangle = overlayWindowVM.GetSelectedAreaInScreenCoordinates();
		Rect rect = cameraWindowVM.CalculateWindowRect(area);
		cameraWindowVM.CanUpdateCameraCapturer = false;
		cameraWindow.Width = rect.Width;
		cameraWindow.Height = rect.Height;
		if (!cameraWindow.IsVisible)
		{
			cameraWindow.SetPosition(overlayWindow.PointToScreen(new System.Windows.Point(0.0, 0.0)));
		}
		cameraWindow.SetPosition(overlayWindow.PointToScreen(rect.Location));
		cameraWindowVM.CanUpdateCameraCapturer = true;
		if (!cameraWindow.IsVisible)
		{
			cameraWindow.Show();
		}
	}

	private void DestroyCameraWindow()
	{
		cameraWindow?.ForceClose();
		cameraWindow = null;
		if (cameraWindowVM != null)
		{
			cameraWindowVM.PropertyChanged -= CameraWindowVM_PropertyChanged;
			cameraWindowVM.Dispose();
			cameraWindowVM = null;
		}
		selectCameraDevicesVM.CameraDeviceErrorMessage = null;
	}

	private void UpdateCameraDeviceErrorMessage()
	{
		if (cameraWindowVM != null)
		{
			if (cameraWindowVM.State == VideoRecordingCameraWindowState.Error)
			{
				selectCameraDevicesVM.CameraDeviceErrorMessage = cameraWindowVM.ErrorMessage;
			}
			else if (cameraWindowVM.State == VideoRecordingCameraWindowState.Streaming)
			{
				selectCameraDevicesVM.CameraDeviceErrorMessage = null;
			}
		}
	}

	private void CreateSelectionHintObjects()
	{
		foreach (OverlayWindow window in overlayWindowManager.Windows)
		{
			OverlayWindowVM overlayWindowVM = window.DataContext as OverlayWindowVM;
			SelectionHintObjectVM selectionHintObjectVM = new SelectionHintObjectVM(overlayWindowVM, "Icon.Video");
			System.Windows.Point position = Mouse.GetPosition(window);
			selectionHintObjectVM.UpdateFloatingObjectPosition(position);
			overlayWindowVM.FloatingObject = selectionHintObjectVM;
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			cameraWindowVM?.Dispose();
			cameraWindowVM = null;
		}
	}
}
