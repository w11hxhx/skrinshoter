using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using AudioDevices;
using Common;
using MediatR;
using SkrinshoterApp.Commands;

namespace SkrinshoterApp;

[UIThread]
public sealed class AudioCaptureSession : BaseDisposable
{
	private sealed class LastSentAudioDevice
	{
		public string DeviceId { get; set; }

		public bool IsMuted { get; set; }
	}

	private readonly SelectAudioDevicesVM selectAudioDevicesVM;

	private readonly IMediator mediator;

	private readonly Dictionary<AudioDataFlow, LastSentAudioDevice> lastSent = new Dictionary<AudioDataFlow, LastSentAudioDevice>
	{
		[AudioDataFlow.Capture] = new LastSentAudioDevice(),
		[AudioDataFlow.Render] = new LastSentAudioDevice()
	};

	public string InputDeviceId
	{
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = lastSent[AudioDataFlow.Capture].DeviceId;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public string OutputDeviceId
	{
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = lastSent[AudioDataFlow.Render].DeviceId;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public bool IsInputMuted
	{
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool isMuted = lastSent[AudioDataFlow.Capture].IsMuted;
				uIThreadAttribute.OnExit();
				return isMuted;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public bool IsOutputMuted
	{
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool isMuted = lastSent[AudioDataFlow.Render].IsMuted;
				uIThreadAttribute.OnExit();
				return isMuted;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public AudioCaptureSession(SelectAudioDevicesVM selectAudioDevicesVM, IMediator mediator)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(null, methodFromHandle, new object[2] { selectAudioDevicesVM, mediator });
		uIThreadAttribute.OnEntry();
		try
		{
			this.selectAudioDevicesVM = selectAudioDevicesVM;
			this.mediator = mediator;
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	public void Begin()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			lastSent[AudioDataFlow.Capture].DeviceId = selectAudioDevicesVM.ActualInputAudioDeviceId;
			lastSent[AudioDataFlow.Capture].IsMuted = selectAudioDevicesVM.IsInputAudioDeviceMuted;
			lastSent[AudioDataFlow.Render].DeviceId = selectAudioDevicesVM.ActualOutputAudioDeviceId;
			lastSent[AudioDataFlow.Render].IsMuted = selectAudioDevicesVM.IsOutputAudioDeviceMuted;
			selectAudioDevicesVM.PropertyChanged += SelectAudioDevicesVM_PropertyChanged;
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
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { sender, e });
		uIThreadAttribute.OnEntry();
		try
		{
			switch (e.PropertyName)
			{
			case "ActualInputAudioDeviceId":
			case "IsInputAudioDeviceMuted":
				ApplyChange(AudioDataFlow.Capture, selectAudioDevicesVM.ActualInputAudioDeviceId, selectAudioDevicesVM.IsInputAudioDeviceMuted);
				break;
			case "ActualOutputAudioDeviceId":
			case "IsOutputAudioDeviceMuted":
				ApplyChange(AudioDataFlow.Render, selectAudioDevicesVM.ActualOutputAudioDeviceId, selectAudioDevicesVM.IsOutputAudioDeviceMuted);
				break;
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	private void ApplyChange(AudioDataFlow dataFlow, string deviceId, bool isMuted)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[3] { dataFlow, deviceId, isMuted });
		uIThreadAttribute.OnEntry();
		try
		{
			LastSentAudioDevice lastSentAudioDevice = lastSent[dataFlow];
			if (!(deviceId == lastSentAudioDevice.DeviceId) || isMuted != lastSentAudioDevice.IsMuted)
			{
				lastSentAudioDevice.DeviceId = deviceId;
				lastSentAudioDevice.IsMuted = isMuted;
				if (deviceId != null)
				{
					if (dataFlow == AudioDataFlow.Capture)
					{
						selectAudioDevicesVM.InputAudioDeviceErrorMessage = null;
					}
					else
					{
						selectAudioDevicesVM.OutputAudioDeviceErrorMessage = null;
					}
					mediator.Send((IRequest<Unit>)new SetAudioDeviceCommand(dataFlow, deviceId, isMuted), default(CancellationToken));
				}
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	protected override void Dispose(bool disposing)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(AudioCaptureSession), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[1] { disposing });
		uIThreadAttribute.OnEntry();
		try
		{
			if (disposing)
			{
				selectAudioDevicesVM.PropertyChanged -= SelectAudioDevicesVM_PropertyChanged;
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}
}
