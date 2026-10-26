using System;
using System.Threading;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace AudioDevices;

internal class NotificationClient : SafeDisposable, IMMNotificationClient
{
	private const int EventDelayMs = 250;

	private readonly ILogger logger;

	private readonly Timer timer;

	public event EventHandler<EventArgs> DevicesChanged;

	public NotificationClient(ILogger<NotificationClient> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		timer = new Timer(TimerCallback);
	}

	public void OnDeviceStateChanged(string deviceId, AudioDeviceState newState)
	{
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("{event}: deviceId={deviceId}, newState={newState}", "OnDeviceStateChanged", deviceId, newState);
		}
		StartTimer();
	}

	public void OnDeviceAdded(string deviceId)
	{
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("{event}: deviceId={deviceId}", "OnDeviceAdded", deviceId);
		}
		StartTimer();
	}

	public void OnDeviceRemoved(string deviceId)
	{
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("{event}: deviceId={deviceId}", "OnDeviceRemoved", deviceId);
		}
		StartTimer();
	}

	public void OnDefaultDeviceChanged(AudioDataFlow flow, AudioDeviceRole role, string defaultDeviceId)
	{
		if (logger.IsEnabled(LogLevel.Debug))
		{
			logger.LogDebug("{event}: deviceId={deviceId}, flow={flow}, role={role}", "OnDefaultDeviceChanged", defaultDeviceId, flow, role);
		}
		StartTimer();
	}

	public void OnPropertyValueChanged(string deviceId, PROPERTYKEY key)
	{
		if (key.fmtid == PInvoke.PKEY_Device_DeviceDesc.fmtid && key.pid == PInvoke.PKEY_Device_DeviceDesc.pid)
		{
			if (logger.IsEnabled(LogLevel.Debug))
			{
				logger.LogDebug("{event}: deviceId={deviceId}, key={key}", "OnPropertyValueChanged", deviceId, "PKEY_Device_DeviceDesc");
			}
			StartTimer();
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
				logger.LogError(exception, "Failed to invoke {eventName} event handler", "DevicesChanged");
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			timer.Dispose();
		}
	}
}
