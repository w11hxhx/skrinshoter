using System;
using System.Globalization;
using System.Reflection;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

[UIThread]
public class LocalizationManager
{
	private readonly SettingsManager settingsManager;

	private readonly ILogger logger;

	public LocalizationManager(SettingsManager settingsManager, ILogger<LocalizationManager> logger)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(LocalizationManager), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(null, methodFromHandle, new object[2] { settingsManager, logger });
		uIThreadAttribute.OnEntry();
		try
		{
			this.settingsManager = settingsManager;
			this.logger = logger;
			settingsManager.SettingsChanged += SettingsManager_SettingsChanged;
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	private void SettingsManager_SettingsChanged(object sender, SettingsChangedEventArgs e)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(LocalizationManager), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[2] { sender, e });
		uIThreadAttribute.OnEntry();
		try
		{
			if (e.Old.Language != e.New.Language)
			{
				ApplyLanguage(e.New.Language);
			}
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	public void SetInitialLanguage()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(LocalizationManager), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			ApplyLanguage(settingsManager.Settings.Language);
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	private void ApplyLanguage(string language)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(LocalizationManager), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(this, methodFromHandle, new object[1] { language });
		uIThreadAttribute.OnEntry();
		try
		{
			Resources.Culture = CultureInfo.GetCultureInfo(language);
			Resources.RaiseAllPropertiesChanged();
			WeakReferenceMessenger.Default.Send(new LanguageChangedMessage());
			if (AppInfoProvider.IsFloomby)
			{
				logger.LogInformation("Language set to '{language}'", language);
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
