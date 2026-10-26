using System.ComponentModel;

namespace SkrinshoterApp;

public class CameraMicrophoneLinker
{
	private readonly SelectCameraDevicesVM selectCameraDevicesVM;

	private readonly SelectAudioDevicesVM selectAudioDevicesVM;

	public CameraMicrophoneLinker(SelectCameraDevicesVM selectCameraDevicesVM, SelectAudioDevicesVM selectAudioDevicesVM)
	{
		this.selectCameraDevicesVM = selectCameraDevicesVM;
		this.selectAudioDevicesVM = selectAudioDevicesVM;
		selectCameraDevicesVM.PropertyChanged += SelectCameraDevicesVM_PropertyChanged;
	}

	private void SelectCameraDevicesVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "EffectiveCameraDeviceId" && selectCameraDevicesVM.EffectiveCameraDeviceId != null && selectAudioDevicesVM.IsActualInputAudioDeviceWorking && selectAudioDevicesVM.IsInputAudioDeviceMuted)
		{
			selectAudioDevicesVM.IsInputAudioDeviceMuted = false;
		}
	}
}
