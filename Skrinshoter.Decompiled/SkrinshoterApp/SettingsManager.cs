using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp;

[UIThread]
public sealed class SettingsManager
{
	private readonly ILogger logger;

	private readonly SettingsStorage settingsStorage;

	private string lastSavedJson;

	[CompilerGenerated]
	private EventHandler<SettingsChangedEventArgs> m_SettingsChanged;

	public Settings Settings
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(SettingsManager), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Settings result = field;
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

	public event EventHandler<SettingsChangedEventArgs> SettingsChanged
	{
		[CompilerGenerated]
		add
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(SettingsManager), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				EventHandler<SettingsChangedEventArgs> eventHandler = this.m_SettingsChanged;
				EventHandler<SettingsChangedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SettingsChangedEventArgs> value2 = (EventHandler<SettingsChangedEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_SettingsChanged, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		remove
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(SettingsManager), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				EventHandler<SettingsChangedEventArgs> eventHandler = this.m_SettingsChanged;
				EventHandler<SettingsChangedEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<SettingsChangedEventArgs> value2 = (EventHandler<SettingsChangedEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange(ref this.m_SettingsChanged, value2, eventHandler2);
				}
				while ((object)eventHandler != eventHandler2);
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public SettingsManager(Settings settings, SettingsStorage settingsStorage, ILogger<SettingsManager> logger = null)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(SettingsManager), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(null, methodFromHandle, new object[3] { settings, settingsStorage, logger });
		uIThreadAttribute.OnEntry();
		try
		{
			Settings = settings;
			this.settingsStorage = settingsStorage;
			this.logger = logger;
			lastSavedJson = settingsStorage.Serialize(Settings);
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	public void SaveSettings()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(SettingsManager), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			string text = settingsStorage.Serialize(Settings);
			if (!(text == lastSavedJson))
			{
				settingsStorage.Save(Settings);
				Settings oldSettings = settingsStorage.Deserialize(lastSavedJson);
				lastSavedJson = text;
				try
				{
					this.m_SettingsChanged?.Invoke(this, new SettingsChangedEventArgs(oldSettings, Settings));
				}
				catch (Exception exception)
				{
					logger.LogError(exception, "Error in {eventName} event handler", "SettingsChanged");
				}
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception2)
		{
			uIThreadAttribute.OnException(exception2);
			throw;
		}
	}
}
