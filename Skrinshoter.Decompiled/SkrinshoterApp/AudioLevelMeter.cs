using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Windows.Threading;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public sealed class AudioLevelMeter : ObservableObject, IDisposable
{
	private const double RefreshRateHz = 30.0;

	private const double MeterFloorDb = -50.0;

	private const double SilenceFloorRms = 1E-05;

	private const double ReleaseSeconds = 0.4;

	private const double ReleasePerTick = 1.0 / 12.0;

	private readonly DispatcherTimer timer;

	private readonly object syncRoot = new object();

	private double peak;

	[ObservableProperty]
	private double level;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double Level
	{
		get
		{
			return level;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(level, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Level);
				level = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Level);
			}
		}
	}

	[UIThread]
	public AudioLevelMeter(Dispatcher dispatcher)
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		uIThreadAttribute.Init(null, methodFromHandle, new object[1] { dispatcher });
		uIThreadAttribute.OnEntry();
		try
		{
			timer = new DispatcherTimer(DispatcherPriority.Normal, dispatcher)
			{
				Interval = TimeSpan.FromSeconds(1.0 / 30.0)
			};
			timer.Tick += TimerTickHandler;
			timer.Start();
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}

	[AnyThread]
	public void Report(float rms)
	{
		double num = NormalizeRms(rms);
		lock (syncRoot)
		{
			if (num > peak)
			{
				peak = num;
			}
		}
	}

	private void TimerTickHandler(object sender, EventArgs e)
	{
		double target;
		lock (syncRoot)
		{
			target = peak;
			peak = 0.0;
		}
		Level = ApplyAttackRelease(Level, target);
	}

	private static double NormalizeRms(float rms)
	{
		double d = Math.Max(rms, 1E-05);
		return MathEx.Clamp((20.0 * Math.Log10(d) - -50.0) / 50.0, 0.0, 1.0);
	}

	private static double ApplyAttackRelease(double current, double target)
	{
		if (target >= current)
		{
			return target;
		}
		double num = current - 1.0 / 12.0;
		if (num < target)
		{
			return target;
		}
		return num;
	}

	[UIThread]
	public void Dispose()
	{
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(methodFromHandle, typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(this, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			timer.Stop();
			timer.Tick -= TimerTickHandler;
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}
}
