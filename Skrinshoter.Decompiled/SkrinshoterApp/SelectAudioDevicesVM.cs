using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Windows.Threading;
using AudioDevices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class SelectAudioDevicesVM : LocalizableObservableObject
{
	private readonly AudioDeviceEnumerator audioDeviceEnumerator;

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly AudioDeviceResolver audioDeviceResolver;

	private readonly Dispatcher dispatcher;

	private readonly ILogger logger;

	private bool suppressSettingsWrite;

	[ObservableProperty]
	private string selectedInputAudioDevice;

	[ObservableProperty]
	private string selectedOutputAudioDevice;

	[ObservableProperty]
	[NotifyPropertyChangedFor("IsActualInputAudioDeviceWorking")]
	[NotifyCanExecuteChangedFor("ToggleInputAudioDeviceMutedCommand")]
	private string actualInputAudioDeviceId;

	[ObservableProperty]
	[NotifyPropertyChangedFor("IsActualOutputAudioDeviceWorking")]
	[NotifyCanExecuteChangedFor("ToggleOutputAudioDeviceMutedCommand")]
	private string actualOutputAudioDeviceId;

	[ObservableProperty]
	[NotifyPropertyChangedFor("IsActualInputAudioDeviceWorking")]
	[NotifyPropertyChangedFor("InputAudioProblemToolTip")]
	private string inputAudioDeviceErrorMessage;

	[ObservableProperty]
	[NotifyPropertyChangedFor("IsActualOutputAudioDeviceWorking")]
	[NotifyPropertyChangedFor("OutputAudioProblemToolTip")]
	private string outputAudioDeviceErrorMessage;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("ToggleInputAudioDeviceMutedCommand")]
	private bool isInputAudioDeviceMuted;

	[ObservableProperty]
	[NotifyCanExecuteChangedFor("ToggleOutputAudioDeviceMutedCommand")]
	private bool isOutputAudioDeviceMuted;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleInputAudioDeviceMutedCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? toggleOutputAudioDeviceMutedCommand;

	public ObservableCollection<AudioDeviceInfo> InputAudioDevices { get; } = new ObservableCollection<AudioDeviceInfo>();

	public ObservableCollection<AudioDeviceInfo> OutputAudioDevices { get; } = new ObservableCollection<AudioDeviceInfo>();

	public bool IsActualInputAudioDeviceWorking
	{
		get
		{
			if (ActualInputAudioDeviceId != null)
			{
				return InputAudioDeviceErrorMessage == null;
			}
			return false;
		}
	}

	public bool IsActualOutputAudioDeviceWorking
	{
		get
		{
			if (ActualOutputAudioDeviceId != null)
			{
				return OutputAudioDeviceErrorMessage == null;
			}
			return false;
		}
	}

	public string InputAudioProblemToolTip => InputAudioDeviceErrorMessage ?? Resources.VideoRecording_NoMicrophonesToolTip;

	public string OutputAudioProblemToolTip => OutputAudioDeviceErrorMessage ?? Resources.VideoRecording_NoSystemSoundSourcesToolTip;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string SelectedInputAudioDevice
	{
		get
		{
			return selectedInputAudioDevice;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(selectedInputAudioDevice, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedInputAudioDevice);
				selectedInputAudioDevice = value;
				OnSelectedInputAudioDeviceChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedInputAudioDevice);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string SelectedOutputAudioDevice
	{
		get
		{
			return selectedOutputAudioDevice;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(selectedOutputAudioDevice, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedOutputAudioDevice);
				selectedOutputAudioDevice = value;
				OnSelectedOutputAudioDeviceChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedOutputAudioDevice);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ActualInputAudioDeviceId
	{
		get
		{
			return actualInputAudioDeviceId;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(actualInputAudioDeviceId, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ActualInputAudioDeviceId);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsActualInputAudioDeviceWorking);
				actualInputAudioDeviceId = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ActualInputAudioDeviceId);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsActualInputAudioDeviceWorking);
				ToggleInputAudioDeviceMutedCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ActualOutputAudioDeviceId
	{
		get
		{
			return actualOutputAudioDeviceId;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(actualOutputAudioDeviceId, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ActualOutputAudioDeviceId);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsActualOutputAudioDeviceWorking);
				actualOutputAudioDeviceId = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ActualOutputAudioDeviceId);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsActualOutputAudioDeviceWorking);
				ToggleOutputAudioDeviceMutedCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string InputAudioDeviceErrorMessage
	{
		get
		{
			return inputAudioDeviceErrorMessage;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(inputAudioDeviceErrorMessage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.InputAudioDeviceErrorMessage);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsActualInputAudioDeviceWorking);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.InputAudioProblemToolTip);
				inputAudioDeviceErrorMessage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.InputAudioDeviceErrorMessage);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsActualInputAudioDeviceWorking);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.InputAudioProblemToolTip);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string OutputAudioDeviceErrorMessage
	{
		get
		{
			return outputAudioDeviceErrorMessage;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(outputAudioDeviceErrorMessage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.OutputAudioDeviceErrorMessage);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsActualOutputAudioDeviceWorking);
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.OutputAudioProblemToolTip);
				outputAudioDeviceErrorMessage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.OutputAudioDeviceErrorMessage);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsActualOutputAudioDeviceWorking);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.OutputAudioProblemToolTip);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsInputAudioDeviceMuted
	{
		get
		{
			return isInputAudioDeviceMuted;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isInputAudioDeviceMuted, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsInputAudioDeviceMuted);
				isInputAudioDeviceMuted = value;
				OnIsInputAudioDeviceMutedChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsInputAudioDeviceMuted);
				ToggleInputAudioDeviceMutedCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsOutputAudioDeviceMuted
	{
		get
		{
			return isOutputAudioDeviceMuted;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isOutputAudioDeviceMuted, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsOutputAudioDeviceMuted);
				isOutputAudioDeviceMuted = value;
				OnIsOutputAudioDeviceMutedChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsOutputAudioDeviceMuted);
				ToggleOutputAudioDeviceMutedCommand.NotifyCanExecuteChanged();
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleInputAudioDeviceMutedCommand => toggleInputAudioDeviceMutedCommand ?? (toggleInputAudioDeviceMutedCommand = new RelayCommand(ToggleInputAudioDeviceMuted, CanExecuteToggleInputAudioDeviceMuted));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand ToggleOutputAudioDeviceMutedCommand => toggleOutputAudioDeviceMutedCommand ?? (toggleOutputAudioDeviceMutedCommand = new RelayCommand(ToggleOutputAudioDeviceMuted, CanExecuteToggleOutputAudioDeviceMuted));

	public SelectAudioDevicesVM(AudioDeviceEnumerator audioDeviceEnumerator, SettingsManager settingsManager, AudioDeviceResolver audioDeviceResolver, Dispatcher dispatcher, ILogger<SelectAudioDevicesVM> logger)
	{
		this.audioDeviceEnumerator = audioDeviceEnumerator;
		this.settingsManager = settingsManager;
		this.settings = settingsManager.Settings;
		this.audioDeviceResolver = audioDeviceResolver;
		this.dispatcher = dispatcher;
		this.logger = logger;
		audioDeviceEnumerator.DevicesChanged += DevicesChangedEventHandler;
		UpdateAudioDevices();
		Settings settings = this.settings;
		bool? isCaptureInputAudioDeviceMuted = settings.IsCaptureInputAudioDeviceMuted;
		bool valueOrDefault = isCaptureInputAudioDeviceMuted == true;
		if (!isCaptureInputAudioDeviceMuted.HasValue)
		{
			valueOrDefault = ActualInputAudioDeviceId == null;
			Settings settings2 = settings;
			bool? isCaptureInputAudioDeviceMuted2 = valueOrDefault;
			settings2.IsCaptureInputAudioDeviceMuted = isCaptureInputAudioDeviceMuted2;
		}
		settings = this.settings;
		isCaptureInputAudioDeviceMuted = settings.IsCaptureOutputAudioDeviceMuted;
		valueOrDefault = isCaptureInputAudioDeviceMuted == true;
		if (!isCaptureInputAudioDeviceMuted.HasValue)
		{
			valueOrDefault = ActualOutputAudioDeviceId == null;
			Settings settings3 = settings;
			bool? isCaptureInputAudioDeviceMuted2 = valueOrDefault;
			settings3.IsCaptureOutputAudioDeviceMuted = isCaptureInputAudioDeviceMuted2;
		}
		settingsManager.SaveSettings();
		isInputAudioDeviceMuted = this.settings.IsCaptureInputAudioDeviceMuted.Value;
		isOutputAudioDeviceMuted = this.settings.IsCaptureOutputAudioDeviceMuted.Value;
	}

	[RelayCommand(CanExecute = "CanExecuteToggleInputAudioDeviceMuted")]
	private void ToggleInputAudioDeviceMuted()
	{
		IsInputAudioDeviceMuted = !IsInputAudioDeviceMuted;
	}

	private bool CanExecuteToggleInputAudioDeviceMuted()
	{
		if (IsInputAudioDeviceMuted)
		{
			return ActualInputAudioDeviceId != null;
		}
		return true;
	}

	[RelayCommand(CanExecute = "CanExecuteToggleOutputAudioDeviceMuted")]
	private void ToggleOutputAudioDeviceMuted()
	{
		IsOutputAudioDeviceMuted = !IsOutputAudioDeviceMuted;
	}

	private bool CanExecuteToggleOutputAudioDeviceMuted()
	{
		if (IsOutputAudioDeviceMuted)
		{
			return ActualOutputAudioDeviceId != null;
		}
		return true;
	}

	[UIThread]
	public void ReportAudioDeviceFailure(AudioDataFlow dataFlow, string deviceId, string errorMessage)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[3] { dataFlow, deviceId, errorMessage });
		uIThreadAttribute.OnEntry();
		try
		{
			if (dataFlow == AudioDataFlow.Capture)
			{
				if (deviceId == ActualInputAudioDeviceId)
				{
					InputAudioDeviceErrorMessage = errorMessage;
				}
			}
			else if (deviceId == ActualOutputAudioDeviceId)
			{
				OutputAudioDeviceErrorMessage = errorMessage;
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[AnyThread]
	private void DevicesChangedEventHandler(object sender, EventArgs e)
	{
		logger.LogInformation("Change detected in audio devices");
		dispatcher.Invoke(UpdateAudioDevices);
	}

	[UIThread]
	protected override void OnLanguageChanged()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			RelocalizeAudioDevices(InputAudioDevices);
			RelocalizeAudioDevices(OutputAudioDevices);
			RestoreSelectedDevices();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	private void UpdateAudioDevices()
	{
		FillAudioDeviceCollection(InputAudioDevices, AudioDataFlow.Capture);
		FillAudioDeviceCollection(OutputAudioDevices, AudioDataFlow.Render);
		RestoreSelectedDevices();
		UpdateActualDevice(AudioDataFlow.Capture);
		UpdateActualDevice(AudioDataFlow.Render);
	}

	private void RestoreSelectedDevices()
	{
		suppressSettingsWrite = true;
		try
		{
			SelectedInputAudioDevice = (InputAudioDevices.Any((AudioDeviceInfo x) => x.Id == settings.CaptureInputAudioDeviceId) ? settings.CaptureInputAudioDeviceId : "Default");
			SelectedOutputAudioDevice = (OutputAudioDevices.Any((AudioDeviceInfo x) => x.Id == settings.CaptureOutputAudioDeviceId) ? settings.CaptureOutputAudioDeviceId : "Default");
		}
		finally
		{
			suppressSettingsWrite = false;
		}
	}

	private void UpdateActualDevice(AudioDataFlow dataFlow)
	{
		if (dataFlow == AudioDataFlow.Capture)
		{
			string text = audioDeviceResolver.Resolve(settings.CaptureInputAudioDeviceId, dataFlow);
			if (text != ActualInputAudioDeviceId)
			{
				InputAudioDeviceErrorMessage = null;
			}
			ActualInputAudioDeviceId = text;
		}
		else
		{
			string text2 = audioDeviceResolver.Resolve(settings.CaptureOutputAudioDeviceId, dataFlow);
			if (text2 != ActualOutputAudioDeviceId)
			{
				OutputAudioDeviceErrorMessage = null;
			}
			ActualOutputAudioDeviceId = text2;
		}
	}

	private void FillAudioDeviceCollection(ICollection<AudioDeviceInfo> collection, AudioDataFlow dataFlow)
	{
		List<AudioDeviceInfo> list = new List<AudioDeviceInfo>();
		AddDefaultDeviceItem(list, dataFlow);
		try
		{
			foreach (AudioDevice item in audioDeviceEnumerator.EnumerateDevices(dataFlow, AudioDeviceState.Active))
			{
				list.Add(new AudioDeviceInfo(item.Id, item.FriendlyName));
			}
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Failed to enumerate audio devices for {dataFlow}", dataFlow);
		}
		collection.Clear();
		foreach (AudioDeviceInfo item2 in list)
		{
			collection.Add(item2);
		}
	}

	private void AddDefaultDeviceItem(ICollection<AudioDeviceInfo> collection, AudioDataFlow dataFlow)
	{
		AudioDeviceRole defaultDeviceRole = AudioDeviceResolver.GetDefaultDeviceRole(dataFlow);
		AudioDevice defaultDevice = audioDeviceEnumerator.GetDefaultDevice(dataFlow, defaultDeviceRole);
		if (defaultDevice != null)
		{
			collection.Add(new AudioDeviceInfo("Default", Resources.AudioDevice_Default, defaultDevice.FriendlyName));
		}
	}

	private void RelocalizeAudioDevices(IList<AudioDeviceInfo> collection)
	{
		for (int i = 0; i < collection.Count; i++)
		{
			AudioDeviceInfo audioDeviceInfo = collection[i];
			if (audioDeviceInfo.Id == "Default")
			{
				collection[i] = new AudioDeviceInfo(audioDeviceInfo.Id, Resources.AudioDevice_Default, audioDeviceInfo.RealDeviceName);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedInputAudioDeviceChanged(string value)
	{
		if (value != null && settings.CaptureInputAudioDeviceId != value && !suppressSettingsWrite)
		{
			settings.CaptureInputAudioDeviceId = value;
			settingsManager.SaveSettings();
			UpdateActualDevice(AudioDataFlow.Capture);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedOutputAudioDeviceChanged(string value)
	{
		if (value != null && settings.CaptureOutputAudioDeviceId != value && !suppressSettingsWrite)
		{
			settings.CaptureOutputAudioDeviceId = value;
			settingsManager.SaveSettings();
			UpdateActualDevice(AudioDataFlow.Render);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsInputAudioDeviceMutedChanged(bool value)
	{
		if (settings.IsCaptureInputAudioDeviceMuted != value)
		{
			settings.IsCaptureInputAudioDeviceMuted = value;
			settingsManager.SaveSettings();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsOutputAudioDeviceMutedChanged(bool value)
	{
		if (settings.IsCaptureOutputAudioDeviceMuted != value)
		{
			settings.IsCaptureOutputAudioDeviceMuted = value;
			settingsManager.SaveSettings();
		}
	}
}
