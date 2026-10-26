using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Threading;
using CameraCapture;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class SelectCameraDevicesVM : LocalizableObservableObject
{
	private readonly ILogger logger;

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly Dispatcher dispatcher;

	private readonly CameraDeviceMonitor cameraDeviceMonitor;

	private bool suppressSettingsWrite;

	[ObservableProperty]
	private string selectedCameraDevice;

	[ObservableProperty]
	[NotifyPropertyChangedFor("EffectiveCameraDeviceId")]
	[NotifyPropertyChangedFor("IsActualCameraDeviceWorking")]
	[NotifyCanExecuteChangedFor("ToggleCameraDeviceOffCommand")]
	private string actualCameraDeviceId;

	[ObservableProperty]
	[NotifyPropertyChangedFor("EffectiveCameraDeviceId")]
	[NotifyCanExecuteChangedFor("ToggleCameraDeviceOffCommand")]
	private bool isCameraDeviceOff;

	[ObservableProperty]
	[NotifyPropertyChangedFor("IsActualCameraDeviceWorking")]
	[NotifyPropertyChangedFor("CameraProblemToolTip")]
	private string cameraDeviceErrorMessage;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleCameraDeviceOffCommand;

	public ObservableCollection<CameraDeviceDescription> CameraDevices { get; } = new ObservableCollection<CameraDeviceDescription>();

	public string EffectiveCameraDeviceId
	{
		get
		{
			if (!IsCameraDeviceOff)
			{
				return ActualCameraDeviceId;
			}
			return null;
		}
	}

	public bool IsActualCameraDeviceWorking
	{
		get
		{
			if (ActualCameraDeviceId != null)
			{
				return CameraDeviceErrorMessage == null;
			}
			return false;
		}
	}

	public string CameraProblemToolTip => CameraDeviceErrorMessage ?? Resources.VideoRecording_NoCamerasToolTip;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string SelectedCameraDevice
	{
		get
		{
			return selectedCameraDevice;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(selectedCameraDevice, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedCameraDevice);
				selectedCameraDevice = value;
				OnSelectedCameraDeviceChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedCameraDevice);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ActualCameraDeviceId
	{
		get
		{
			return actualCameraDeviceId;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(actualCameraDeviceId, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ActualCameraDeviceId);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.EffectiveCameraDeviceId);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsActualCameraDeviceWorking);
				actualCameraDeviceId = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ActualCameraDeviceId);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.EffectiveCameraDeviceId);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsActualCameraDeviceWorking);
				ToggleCameraDeviceOffCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsCameraDeviceOff
	{
		get
		{
			return isCameraDeviceOff;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isCameraDeviceOff, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsCameraDeviceOff);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.EffectiveCameraDeviceId);
				isCameraDeviceOff = value;
				OnIsCameraDeviceOffChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsCameraDeviceOff);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.EffectiveCameraDeviceId);
				ToggleCameraDeviceOffCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CameraDeviceErrorMessage
	{
		get
		{
			return cameraDeviceErrorMessage;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(cameraDeviceErrorMessage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CameraDeviceErrorMessage);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsActualCameraDeviceWorking);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CameraProblemToolTip);
				cameraDeviceErrorMessage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CameraDeviceErrorMessage);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsActualCameraDeviceWorking);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CameraProblemToolTip);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleCameraDeviceOffCommand => toggleCameraDeviceOffCommand ?? (toggleCameraDeviceOffCommand = new RelayCommand(ToggleCameraDeviceOff, CanExecuteToggleCameraDeviceOff));

	public SelectCameraDevicesVM(SettingsManager settingsManager, Dispatcher dispatcher, CameraDeviceMonitor cameraDeviceMonitor, ILogger<SelectCameraDevicesVM> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.settingsManager = settingsManager;
		this.settings = settingsManager.Settings;
		this.dispatcher = dispatcher;
		this.cameraDeviceMonitor = cameraDeviceMonitor;
		cameraDeviceMonitor.DevicesChanged += DevicesChangedEventHandler;
		UpdateCameraDevices();
		Settings settings = this.settings;
		bool? isCaptureCameraDeviceOff = settings.IsCaptureCameraDeviceOff;
		bool valueOrDefault = isCaptureCameraDeviceOff == true;
		if (!isCaptureCameraDeviceOff.HasValue)
		{
			valueOrDefault = ActualCameraDeviceId == null;
			bool? isCaptureCameraDeviceOff2 = valueOrDefault;
			settings.IsCaptureCameraDeviceOff = isCaptureCameraDeviceOff2;
		}
		settingsManager.SaveSettings();
		isCameraDeviceOff = this.settings.IsCaptureCameraDeviceOff.Value;
	}

	[RelayCommand(CanExecute = "CanExecuteToggleCameraDeviceOff")]
	private void ToggleCameraDeviceOff()
	{
		IsCameraDeviceOff = !IsCameraDeviceOff;
	}

	private bool CanExecuteToggleCameraDeviceOff()
	{
		if (IsCameraDeviceOff)
		{
			return ActualCameraDeviceId != null;
		}
		return true;
	}

	[AnyThread]
	private void DevicesChangedEventHandler(object sender, EventArgs e)
	{
		logger.LogInformation("Change detected in camera devices");
		dispatcher.Invoke(UpdateCameraDevices);
	}

	private void UpdateCameraDevices()
	{
		FillCameraDeviceCollection(CameraDevices);
		RestoreSelectedCameraDevice();
	}

	private void FillCameraDeviceCollection(ICollection<CameraDeviceDescription> collection)
	{
		List<CameraDeviceDescription> list = new List<CameraDeviceDescription>();
		try
		{
			list.AddRange(cameraDeviceMonitor.GetDevices());
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to enumerate camera devices");
		}
		collection.Clear();
		foreach (CameraDeviceDescription item in list)
		{
			collection.Add(item);
		}
	}

	private string ResolveCameraDevice()
	{
		if (CameraDevices.Count == 0)
		{
			return null;
		}
		if (CameraDevices.Any((CameraDeviceDescription x) => x.Id == settings.CaptureCameraDeviceId))
		{
			return settings.CaptureCameraDeviceId;
		}
		return CameraDevices[0].Id;
	}

	private void RestoreSelectedCameraDevice()
	{
		string text = ResolveCameraDevice();
		suppressSettingsWrite = true;
		try
		{
			SelectedCameraDevice = text;
		}
		finally
		{
			suppressSettingsWrite = false;
		}
		ActualCameraDeviceId = text;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedCameraDeviceChanged(string value)
	{
		if (value != null && settings.CaptureCameraDeviceId != value && !suppressSettingsWrite)
		{
			settings.CaptureCameraDeviceId = value;
			settingsManager.SaveSettings();
			ActualCameraDeviceId = value;
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsCameraDeviceOffChanged(bool value)
	{
		if (settings.IsCaptureCameraDeviceOff != value)
		{
			settings.IsCaptureCameraDeviceOff = value;
			settingsManager.SaveSettings();
		}
	}
}
