using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Media.KernelStreaming;
using Windows.Win32.Media.MediaFoundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace CameraCapture;

public class CameraDeviceMonitor : SafeDisposable
{
	private const int EventDelayMs = 250;

	private readonly ILogger logger;

	private readonly Timer timer;

	private HWND hWnd;

	private WNDPROC wndProc;

	private HDEVNOTIFY hDevNotify;

	public event EventHandler<EventArgs> DevicesChanged;

	public CameraDeviceMonitor(MediaFoundationInitializer _, ILogger<CameraDeviceMonitor> logger = null)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		timer = new Timer(TimerCallback);
		try
		{
			CreateMessageWindow();
			RegisterForDeviceNotification();
		}
		catch
		{
			Dispose();
			throw;
		}
	}

	public unsafe List<CameraDeviceDescription> GetDevices()
	{
		return ThreadEx.RunInMTA(() =>
		{
			List<CameraDeviceDescription> list = new List<CameraDeviceDescription>();
			IMFAttributes* ptr = default;
			PInvoke.MFCreateAttributes(&ptr, 1u).ThrowOnFailure("MFCreateAttributes()");
			using ComPtr<IMFAttributes> comPtr = ptr;
			comPtr.Ptr->SetGUID(in PInvoke.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE, in PInvoke.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_GUID).ThrowOnFailure("IMFAttributes.SetGUID(MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE)");
			PInvoke.MFEnumDeviceSources(comPtr, out var pppSourceActivate, out var pcSourceActivate).ThrowOnFailure("MFEnumDeviceSources()");
			for (int i = 0; i < pcSourceActivate; i++)
			{
				try
				{
					using ComPtr<IMFActivate> comPtr2 = pppSourceActivate[i];
					comPtr2.Ptr->GetAllocatedString(in PInvoke.MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK, out var ppwszValue, out var pcchLength).ThrowOnFailure("IMFActivate.GetAllocatedString(MF_DEVSOURCE_ATTRIBUTE_SOURCE_TYPE_VIDCAP_SYMBOLIC_LINK)");
					string id = ppwszValue.ToString();
					Marshal.FreeCoTaskMem((IntPtr)(char*)ppwszValue);
					comPtr2.Ptr->GetAllocatedString(in PInvoke.MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME, out ppwszValue, out pcchLength).ThrowOnFailure("IMFActivate.GetAllocatedString(MF_DEVSOURCE_ATTRIBUTE_FRIENDLY_NAME)");
					string name = ppwszValue.ToString();
					Marshal.FreeCoTaskMem((IntPtr)(char*)ppwszValue);
					list.Add(new CameraDeviceDescription(id, name));
				}
				catch
				{
				}
			}
			Marshal.FreeCoTaskMem((IntPtr)pppSourceActivate);
			return list;
		});
	}

	private unsafe void CreateMessageWindow()
	{
		string text = typeof(CameraDeviceMonitor).Name + "_" + Guid.NewGuid().ToString();
		wndProc = WndProc;
		fixed (char* ptr = text)
		{
			if (PInvokeCorrections.RegisterClass(new WNDCLASSW
			{
				lpfnWndProc = wndProc,
				lpszClassName = ptr
			}) == 0)
			{
				throw new WinApiException("RegisterClass(" + text + ")", new Win32Exception());
			}
			hWnd = PInvoke.CreateWindowEx(WINDOW_EX_STYLE.WS_EX_LEFT, text, "", WINDOW_STYLE.WS_OVERLAPPED, 0, 0, 1, 1, HWND.HWND_MESSAGE, null, null, null);
			if (hWnd.IsNull)
			{
				throw new WinApiException("CreateWindowEx(" + text + ")", new Win32Exception());
			}
		}
	}

	private void DestroyMessageWindow()
	{
		if (!hWnd.IsNull)
		{
			PInvoke.DestroyWindow(hWnd);
			hWnd = HWND.Null;
		}
	}

	private LRESULT WndProc(HWND hWnd, uint uMsg, WPARAM wParam, LPARAM lParam)
	{
		if (uMsg == 537)
		{
			StartTimer();
			return (LRESULT)1;
		}
		return PInvoke.DefWindowProc(hWnd, uMsg, wParam, lParam);
	}

	private unsafe void RegisterForDeviceNotification()
	{
		DEV_BROADCAST_DEVICEINTERFACE_W dEV_BROADCAST_DEVICEINTERFACE_W = new DEV_BROADCAST_DEVICEINTERFACE_W
		{
			dbcc_size = (uint)sizeof(DEV_BROADCAST_DEVICEINTERFACE_W),
			dbcc_devicetype = 5u,
			dbcc_classguid = typeof(KSCATEGORY_CAPTURE).GUID
		};
		hDevNotify = PInvoke.RegisterDeviceNotification(hWnd, &dEV_BROADCAST_DEVICEINTERFACE_W, REGISTER_NOTIFICATION_FLAGS.DEVICE_NOTIFY_WINDOW_HANDLE);
		if (hDevNotify.Value == null)
		{
			throw new WinApiException("RegisterDeviceNotification()", new Win32Exception());
		}
	}

	private unsafe void UnregisterForDeviceNotification()
	{
		if (hDevNotify.Value != null)
		{
			PInvoke.UnregisterDeviceNotification(hDevNotify);
			hDevNotify = default;
		}
	}

	private void StartTimer()
	{
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed)
			{
				timer.Change(250, -1);
			}
		}
	}

	private void TimerCallback(object state)
	{
		if (!IsDisposed)
		{
			try
			{
				DevicesChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to call event handler of CameraDeviceMonitor.DevicesChanged");
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		UnregisterForDeviceNotification();
		DestroyMessageWindow();
		if (disposing)
		{
			timer.Dispose();
		}
	}
}
