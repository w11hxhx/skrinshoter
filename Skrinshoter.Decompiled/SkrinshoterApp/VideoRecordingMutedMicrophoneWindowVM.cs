using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;

namespace SkrinshoterApp;

public class VideoRecordingMutedMicrophoneWindowVM : LocalizableObservableObject
{
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeCommand;

	public SelectAudioDevicesVM SelectAudioDevicesVM { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(Close));

	public event EventHandler CloseRequested;

	public VideoRecordingMutedMicrophoneWindowVM(SelectAudioDevicesVM selectAudioDevicesVM)
	{
		SelectAudioDevicesVM = selectAudioDevicesVM ?? throw new ArgumentNullException("selectAudioDevicesVM");
	}

	[RelayCommand]
	private void Close()
	{
		CloseRequested?.Invoke(this, EventArgs.Empty);
	}
}
