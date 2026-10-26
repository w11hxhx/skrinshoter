using System;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace SkrinshoterApp;

public class LocalizableObservableObject : ObservableObject
{
	public LocalizableObservableObject()
	{
		WeakReferenceMessenger.Default.Register(this, (object recipient, LanguageChangedMessage _) =>
		{
			LocalizableObservableObject localizableObservableObject = (LocalizableObservableObject)recipient;
			localizableObservableObject.OnLanguageChanged();
			localizableObservableObject.OnPropertyChanged(string.Empty);
		});
	}

	[UIThread]
	protected virtual void OnLanguageChanged()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}
}
