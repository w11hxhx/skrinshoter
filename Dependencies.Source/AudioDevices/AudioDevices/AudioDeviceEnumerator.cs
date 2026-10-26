using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Media.Audio;
using Windows.Win32.System.Com;
using Windows.Win32.UI.Shell.PropertiesSystem;

namespace AudioDevices;

public class AudioDeviceEnumerator : SafeDisposable
{
	private ComPtr<IMMDeviceEnumerator> deviceEnumerator;

	private readonly NotificationClient notificationClient;

	private IntPtr notificationClientComPtr;

	private bool isCallbackRegistered;

	public event EventHandler<EventArgs> DevicesChanged;

	public AudioDeviceEnumerator(bool subscribeToChanges, ILoggerFactory loggerFactory)
	{
		if (loggerFactory == null)
		{
			loggerFactory = NullLoggerFactory.Instance;
		}
		try
		{
			ThreadEx.RunInMTA(CreateDeviceEnumerator);
			if (subscribeToChanges)
			{
				notificationClient = new NotificationClient(loggerFactory.CreateLogger<NotificationClient>());
				notificationClient.DevicesChanged += (object s, EventArgs e) =>
				{
					DevicesChanged?.Invoke(this, e);
				};
				RegisterCallback();
			}
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public unsafe IReadOnlyList<AudioDevice> EnumerateDevices(AudioDataFlow dataFlow, AudioDeviceState stateMask)
	{
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			List<AudioDevice> list = new List<AudioDevice>();
			IMMDeviceCollection* ptr = default;
			deviceEnumerator.Ptr->EnumAudioEndpoints((EDataFlow)dataFlow, (DEVICE_STATE)stateMask, &ptr).ThrowOnFailure("IMMDeviceEnumerator.EnumAudioEndpoints()");
			using ComPtr<IMMDeviceCollection> comPtr = ptr;
			comPtr.Ptr->GetCount(out var pcDevices).ThrowOnFailure("IMMDeviceCollection.GetCount()");
			IMMDevice* ptr2 = default;
			for (uint num = 0u; num < pcDevices; num++)
			{
				if (comPtr.Ptr->Item(num, &ptr2).Failed)
				{
					continue;
				}
				using ComPtr<IMMDevice> comPtr2 = ptr2;
				AudioDevice audioDevice = ReadDeviceInfo(comPtr2);
				if (audioDevice != null)
				{
					list.Add(audioDevice);
				}
			}
			return list;
		}
	}

	public unsafe AudioDevice GetDefaultDevice(AudioDataFlow dataFlow, AudioDeviceRole role)
	{
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			IMMDevice* ptr = default;
			if (deviceEnumerator.Ptr->GetDefaultAudioEndpoint((EDataFlow)dataFlow, (ERole)role, &ptr).Failed)
			{
				return null;
			}
			using ComPtr<IMMDevice> comPtr = ptr;
			return ReadDeviceInfo(comPtr);
		}
	}

	public unsafe AudioDevice GetDevice(string deviceId)
	{
		if (string.IsNullOrEmpty(deviceId))
		{
			return null;
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			IMMDevice* ptr = default;
			if (deviceEnumerator.Ptr->GetDevice(deviceId, &ptr).Failed)
			{
				return null;
			}
			using ComPtr<IMMDevice> comPtr = ptr;
			return ReadDeviceInfo(comPtr);
		}
	}

	public unsafe IntPtr GetDevicePointer(string deviceId)
	{
		if (deviceId == null)
		{
			throw new ArgumentNullException("deviceId");
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				throw new ObjectDisposedException(GetType().FullName);
			}
			IMMDevice* ptr = default;
			deviceEnumerator.Ptr->GetDevice(deviceId, &ptr).ThrowOnFailure("IMMDeviceEnumerator.GetDevice()");
			return (IntPtr)ptr;
		}
	}

	public unsafe static AudioDevice GetDeviceInfo(IntPtr device)
	{
		if (device == IntPtr.Zero)
		{
			throw new ArgumentNullException("device");
		}
		return ReadDeviceInfo((IMMDevice*)(void*)device);
	}

	private unsafe void CreateDeviceEnumerator()
	{
		PInvoke.CoCreateInstance(typeof(MMDeviceEnumerator).GUID, null, CLSCTX.CLSCTX_INPROC_SERVER, typeof(IMMDeviceEnumerator).GUID, out var ppv).ThrowOnFailure("CoCreateInstance(MMDeviceEnumerator)");
		deviceEnumerator = (IMMDeviceEnumerator*)ppv;
	}

	private unsafe void RegisterCallback()
	{
		notificationClientComPtr = Marshal.GetComInterfaceForObject(notificationClient, typeof(IMMNotificationClient));
		deviceEnumerator.Ptr->RegisterEndpointNotificationCallback((Windows.Win32.Media.Audio.IMMNotificationClient*)(void*)notificationClientComPtr).ThrowOnFailure("IMMDeviceEnumerator.RegisterEndpointNotificationCallback()");
		isCallbackRegistered = true;
	}

	private unsafe static AudioDevice ReadDeviceInfo(IMMDevice* device)
	{
		try
		{
			device->GetId(out var ppstrId).ThrowOnFailure("IMMDevice.GetId()");
			string id;
			try
			{
				id = ppstrId.ToString();
			}
			finally
			{
				PInvoke.CoTaskMemFree((char*)ppstrId);
			}
			device->GetState(out var pdwState).ThrowOnFailure("IMMDevice.GetState()");
			device->QueryInterface<IMMEndpoint>(out var ppv).ThrowOnFailure("IMMDevice.QueryInterface<IMMEndpoint>()");
			using ComPtr<IMMEndpoint> comPtr = ppv;
			comPtr.Ptr->GetDataFlow(out var pDataFlow).ThrowOnFailure("IMMEndpoint.GetDataFlow()");
			IPropertyStore* ptr = default;
			device->OpenPropertyStore(STGM.STGM_DIRECT, &ptr).ThrowOnFailure("IMMDevice.OpenPropertyStore()");
			using ComPtr<IPropertyStore> comPtr2 = ptr;
			if (!PropVariantHelper.TryReadStringProperty(comPtr2.Ptr, PInvoke.PKEY_Device_FriendlyName, out var value))
			{
				return null;
			}
			return new AudioDevice(id, value, (AudioDataFlow)pDataFlow, (AudioDeviceState)pdwState);
		}
		catch
		{
			return null;
		}
	}

	protected unsafe override void Dispose(bool disposing)
	{
		if (isCallbackRegistered)
		{
			deviceEnumerator.Ptr->UnregisterEndpointNotificationCallback((Windows.Win32.Media.Audio.IMMNotificationClient*)(void*)notificationClientComPtr);
		}
		if (notificationClientComPtr != IntPtr.Zero)
		{
			Marshal.Release(notificationClientComPtr);
		}
		if (disposing)
		{
			notificationClient?.Dispose();
		}
		deviceEnumerator.Dispose();
	}
}
