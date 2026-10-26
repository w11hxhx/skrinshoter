using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Threading;
using AudioDevices;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using ScreenRecorderCommon;

namespace SkrinshoterApp;

public sealed class VideoRecordingMutedMicrophoneWarningManager
{
	private readonly SettingsManager settingsManager;

	private readonly Dispatcher dispatcher;

	private readonly SelectAudioDevicesVM selectAudioDevicesVM;

	private readonly ILogger logger;

	private readonly SpeechActivityMonitor speechActivityMonitor = new SpeechActivityMonitor();

	private VideoRecordingMutedMicrophoneWindow window;

	private bool recordingSessionActive;

	private bool recordingPaused;

	private bool suppressed;

	public event EventHandler<VideoRecordingMutedMicrophoneWindow> WindowPlacementRequested;

	public VideoRecordingMutedMicrophoneWarningManager(SettingsManager settingsManager, Dispatcher dispatcher, SelectAudioDevicesVM selectAudioDevicesVM, ILogger<VideoRecordingMutedMicrophoneWarningManager> logger = null)
	{
		this.settingsManager = settingsManager;
		this.dispatcher = dispatcher;
		this.selectAudioDevicesVM = selectAudioDevicesVM;
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
		selectAudioDevicesVM.PropertyChanged += SelectAudioDevicesVM_PropertyChanged;
		speechActivityMonitor.WarningDesiredChanged += SpeechActivityMonitor_WarningDesiredChanged;
	}

	[AnyThread]
	public void UpdateSpeechActivity(string[] args)
	{
		try
		{
			CommandArgumentReader commandArgumentReader = new CommandArgumentReader(args, 2);
			AudioDataFlow audioDataFlow = commandArgumentReader.ReadEnum<AudioDataFlow>();
			if (!audioDataFlow.In(AudioDataFlow.Capture, AudioDataFlow.Render))
			{
				throw new FormatException($"Invalid value of audio data flow: {audioDataFlow}.");
			}
			bool isActive = commandArgumentReader.ReadFlag("speech activity");
			if (audioDataFlow == AudioDataFlow.Capture)
			{
				speechActivityMonitor.Report(isActive);
			}
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to handle speech activity event");
		}
	}

	[UIThread]
	public void NotifyRecordingStarted()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			recordingSessionActive = true;
			speechActivityMonitor.Reset();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	public void SetPauseState(bool paused)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[1] { paused });
		uIThreadAttribute.OnEntry();
		try
		{
			recordingPaused = paused;
			if (!paused)
			{
				speechActivityMonitor.Reset();
			}
			UpdateVisibility();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	public void Hide()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			HideWindow();
			recordingSessionActive = false;
			recordingPaused = false;
			suppressed = false;
			speechActivityMonitor.Reset();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private void UpdateVisibility()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			bool flag = (speechActivityMonitor.WarningDesired || (window != null && window.IsMouseOver)) && settingsManager.Settings.ShowMutedMicrophoneWarning && WindowCaptureExclusion.CanHideWindowsFromCapture() && recordingSessionActive && !recordingPaused && selectAudioDevicesVM.IsInputAudioDeviceMuted && selectAudioDevicesVM.IsActualInputAudioDeviceWorking && !suppressed;
			if (flag && window == null)
			{
				ShowWindow();
			}
			else if (!flag && window != null)
			{
				HideWindow();
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private void ShowWindow()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			logger.LogInformation("Showing muted microphone warning");
			VideoRecordingMutedMicrophoneWindowVM videoRecordingMutedMicrophoneWindowVM = new VideoRecordingMutedMicrophoneWindowVM(selectAudioDevicesVM);
			videoRecordingMutedMicrophoneWindowVM.CloseRequested += WindowVM_CloseRequested;
			window = new VideoRecordingMutedMicrophoneWindow();
			window.DataContext = videoRecordingMutedMicrophoneWindowVM;
			window.MouseLeave += Window_MouseLeave;
			window.Left = 1000000.0;
			window.Show();
			window.UpdateLayout();
			WindowPlacementRequested?.Invoke(this, window);
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private void HideWindow()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			if (window != null)
			{
				logger.LogInformation("Hiding muted microphone warning");
				window.MouseLeave -= Window_MouseLeave;
				window.ForceClose();
				window = null;
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private void SettingsManager_SettingsChanged(object sender, SettingsChangedEventArgs e)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { sender, e });
		uIThreadAttribute.OnEntry();
		try
		{
			if (e.Old.ShowMutedMicrophoneWarning != e.New.ShowMutedMicrophoneWarning)
			{
				if (e.New.ShowMutedMicrophoneWarning)
				{
					speechActivityMonitor.Reset();
				}
				UpdateVisibility();
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	private void SelectAudioDevicesVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (!dispatcher.CheckAccess())
		{
			dispatcher.Invoke(() =>
			{
				SelectAudioDevicesVM_PropertyChanged(sender, e);
			});
			return;
		}
		if (e.PropertyName == "IsInputAudioDeviceMuted")
		{
			suppressed = false;
			speechActivityMonitor.Reset();
		}
		if (e.PropertyName.In("IsInputAudioDeviceMuted", "ActualInputAudioDeviceId", "InputAudioDeviceErrorMessage"))
		{
			UpdateVisibility();
		}
	}

	[AnyThread]
	private void SpeechActivityMonitor_WarningDesiredChanged(object sender, EventArgs e)
	{
		dispatcher.BeginInvoke(new Action(UpdateVisibility));
	}

	[UIThread]
	private void WindowVM_CloseRequested(object sender, EventArgs e)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { sender, e });
		uIThreadAttribute.OnEntry();
		try
		{
			suppressed = true;
			UpdateVisibility();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[UIThread]
	private void Window_MouseLeave(object sender, MouseEventArgs e)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { sender, e });
		uIThreadAttribute.OnEntry();
		try
		{
			UpdateVisibility();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}
}
