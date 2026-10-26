using System;
using System.Reflection;

namespace SkrinshoterApp;

[UIThread]
public sealed class ScrollingCaptureAvailabilityService
{
	public const int FreeScrollingCaptureLimit = 20;

	private readonly TrialStateManager trialStateManager;

	public int ScrollingCaptureSuccessCount
	{
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(ScrollingCaptureAvailabilityService), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				int scrollingCaptureSuccessCount = trialStateManager.State.ScrollingCaptureSuccessCount;
				uIThreadAttribute.OnExit();
				return scrollingCaptureSuccessCount;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public ScrollingCaptureAvailabilityService(TrialStateManager trialStateManager)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(ScrollingCaptureAvailabilityService), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(null, methodFromHandle, new object[1] { trialStateManager });
		uIThreadAttribute.OnEntry();
		try
		{
			this.trialStateManager = trialStateManager;
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	public bool CanCreateScrollingCapture()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(ScrollingCaptureAvailabilityService), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			bool result = trialStateManager.State.ScrollingCaptureSuccessCount < 20;
			uIThreadAttribute.OnExit();
			return result;
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	public void OnScrollingCaptureSucceeded()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(ScrollingCaptureAvailabilityService), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			trialStateManager.State.ScrollingCaptureSuccessCount++;
			trialStateManager.SaveState();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}
}
