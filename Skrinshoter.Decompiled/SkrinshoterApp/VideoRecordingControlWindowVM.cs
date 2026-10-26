using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using SkrinshoterApp.Commands;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class VideoRecordingControlWindowVM : LocalizableObservableObject
{
	private readonly IMediator mediator;

	private readonly Func<bool> cancelConfirmation;

	[ObservableProperty]
	private VideoRecordingControlWindowState state;

	[ObservableProperty]
	private double panelObservedWidth;

	[ObservableProperty]
	private double panelObservedHeight;

	[ObservableProperty]
	private TimeSpan duration = TimeSpan.MinValue;

	[ObservableProperty]
	private string durationText;

	[ObservableProperty]
	private int durationWidth;

	[ObservableProperty]
	private bool paused;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? startCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? cancelSelectionCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? stopCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? togglePauseCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? cancelCommand;

	public Settings Settings { get; }

	public SelectAudioDevicesVM SelectAudioDevicesVM { get; }

	public SelectCameraDevicesVM SelectCameraDevicesVM { get; }

	public AudioLevelMeter InputAudioLevelMeter { get; }

	public AudioLevelMeter OutputAudioLevelMeter { get; }

	public string StartToolTip => Resources.VideoRecording_StartToolTip;

	public string PauseToolTip => Resources.VideoRecording_PauseToolTip + Settings.PauseVideoHotkey.ToDisplaySuffix();

	public string ResumeToolTip => Resources.VideoRecording_ResumeToolTip + Settings.PauseVideoHotkey.ToDisplaySuffix();

	public string StopToolTip => Resources.VideoRecording_StopToolTip + Settings.RecordVideoHotkey.ToDisplaySuffix();

	public string CloseToolTip => Resources.VideoRecording_CloseToolTip + " (Esc)";

	public string CancelToolTip => Resources.VideoRecording_CancelToolTip;

	public string TurnOffCameraToolTip => Resources.VideoRecording_TurnOffCameraToolTip;

	public string TurnOnCameraToolTip => Resources.VideoRecording_TurnOnCameraToolTip;

	public string SelectCameraToolTip => Resources.VideoRecording_SelectCameraToolTip;

	public string MuteMicrophoneToolTip => Resources.VideoRecording_MuteMicrophoneToolTip;

	public string UnmuteMicrophoneToolTip => Resources.VideoRecording_UnmuteMicrophoneToolTip;

	public string SelectMicrophoneToolTip => Resources.VideoRecording_SelectMicrophoneToolTip;

	public string MuteSystemSoundToolTip => Resources.VideoRecording_MuteSystemSoundToolTip;

	public string UnmuteSystemSoundToolTip => Resources.VideoRecording_UnmuteSystemSoundToolTip;

	public string SelectSystemSoundToolTip => Resources.VideoRecording_SelectSystemSoundToolTip;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public VideoRecordingControlWindowState State
	{
		get
		{
			return state;
		}
		set
		{
			if (!EqualityComparer<VideoRecordingControlWindowState>.Default.Equals(state, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.State);
				state = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.State);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double PanelObservedWidth
	{
		get
		{
			return panelObservedWidth;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(panelObservedWidth, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PanelObservedWidth);
				panelObservedWidth = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PanelObservedWidth);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double PanelObservedHeight
	{
		get
		{
			return panelObservedHeight;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(panelObservedHeight, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.PanelObservedHeight);
				panelObservedHeight = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.PanelObservedHeight);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public TimeSpan Duration
	{
		get
		{
			return duration;
		}
		set
		{
			if (!EqualityComparer<TimeSpan>.Default.Equals(duration, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Duration);
				duration = value;
				OnDurationChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Duration);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string DurationText
	{
		get
		{
			return durationText;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(durationText, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DurationText);
				durationText = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DurationText);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int DurationWidth
	{
		get
		{
			return durationWidth;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(durationWidth, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DurationWidth);
				durationWidth = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DurationWidth);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool Paused
	{
		get
		{
			return paused;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(paused, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Paused);
				paused = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Paused);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand StartCommand => startCommand ?? (startCommand = new RelayCommand(Start, CanExecuteStart));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CancelSelectionCommand => cancelSelectionCommand ?? (cancelSelectionCommand = new RelayCommand(CancelSelection, CanExecuteCancelSelection));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand StopCommand => stopCommand ?? (stopCommand = new AsyncRelayCommand(Stop));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand TogglePauseCommand => togglePauseCommand ?? (togglePauseCommand = new AsyncRelayCommand(TogglePause));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand CancelCommand => cancelCommand ?? (cancelCommand = new AsyncRelayCommand(Cancel));

	public VideoRecordingControlWindowVM(Settings settings, SelectAudioDevicesVM selectAudioDevicesVM, SelectCameraDevicesVM selectCameraDevicesVM, AudioLevelMeter inputAudioLevelMeter, AudioLevelMeter outputAudioLevelMeter, IMediator mediator, Func<bool> cancelConfirmation)
	{
		Settings = settings;
		SelectAudioDevicesVM = selectAudioDevicesVM;
		SelectCameraDevicesVM = selectCameraDevicesVM;
		InputAudioLevelMeter = inputAudioLevelMeter;
		OutputAudioLevelMeter = outputAudioLevelMeter;
		this.mediator = mediator;
		this.cancelConfirmation = cancelConfirmation;
		State = VideoRecordingControlWindowState.Selecting;
		Duration = TimeSpan.Zero;
	}

	public void UpdateDuration(TimeSpan duration, TimeSpan maxDuration)
	{
		if (maxDuration <= TimeSpan.FromMinutes(5.0))
		{
			Duration = maxDuration - duration;
		}
		else
		{
			Duration = duration;
		}
	}

	public void SetPauseState(bool paused)
	{
		Paused = paused;
	}

	[RelayCommand(CanExecute = "CanExecuteStart")]
	private void Start()
	{
		AppLogger.Logger.Info("ControlWindow: On start video");
		State = VideoRecordingControlWindowState.Recording;
	}

	private bool CanExecuteStart()
	{
		return State == VideoRecordingControlWindowState.Selecting;
	}

	[RelayCommand(CanExecute = "CanExecuteCancelSelection")]
	private void CancelSelection()
	{
		State = VideoRecordingControlWindowState.Canceled;
	}

	private bool CanExecuteCancelSelection()
	{
		return State == VideoRecordingControlWindowState.Selecting;
	}

	[RelayCommand]
	private Task Stop()
	{
		AppLogger.Logger.Info("ControlWindow: On stop video");
		return mediator.Send((IRequest<Unit>)new StopVideoRecordingCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private Task TogglePause()
	{
		AppLogger.Logger.Info("ControlWindow: On pause/resume video");
		return mediator.Send((IRequest<Unit>)new ToggleVideoRecordingPauseCommand(), default(CancellationToken));
	}

	[RelayCommand]
	private async Task Cancel()
	{
		AppLogger.Logger.Info("ControlWindow: On cancel video");
		bool wasPaused = Paused;
		if (!wasPaused)
		{
			await mediator.Send((IRequest<Unit>)new ToggleVideoRecordingPauseCommand(), default(CancellationToken));
		}
		if (cancelConfirmation?.Invoke() ?? true)
		{
			await mediator.Send((IRequest<Unit>)new CancelVideoRecordingCommand(), default(CancellationToken));
		}
		else if (!wasPaused && Paused)
		{
			await mediator.Send((IRequest<Unit>)new ToggleVideoRecordingPauseCommand(), default(CancellationToken));
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnDurationChanged(TimeSpan value)
	{
		if (value.Ticks < 36000000000L)
		{
			DurationText = value.ToString("mm\\:ss", CultureInfo.InvariantCulture);
			DurationWidth = 36;
		}
		else
		{
			DurationText = value.ToString("hh\\:mm\\:ss", CultureInfo.InvariantCulture);
			DurationWidth = 55;
		}
	}
}
