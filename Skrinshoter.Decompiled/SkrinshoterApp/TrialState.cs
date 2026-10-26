using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace SkrinshoterApp;

[UIThread]
public sealed class TrialState
{
	[JsonProperty("revision")]
	internal long Revision
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(TrialState), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				long result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(TrialState), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("app_id")]
	public string AppId
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(TrialState), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(TrialState), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("scrolling_capture_success_count")]
	public int ScrollingCaptureSuccessCount
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(TrialState), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				int result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(TrialState), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public TrialState()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(TrialState), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(null, methodFromHandle, args);
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
