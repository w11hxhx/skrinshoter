using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace SkrinshoterApp;

public static class DpiScaleObserver
{
	private sealed class State
	{
		private readonly FrameworkElement frameworkElement;

		private HwndSource hwndSource;

		public State(FrameworkElement frameworkElement)
		{
			this.frameworkElement = frameworkElement;
		}

		public void OnLoaded(object sender, RoutedEventArgs e)
		{
			TryAttachToHwndSource();
			UpdateScale();
		}

		public void OnUnloaded(object sender, RoutedEventArgs e)
		{
			DetachFromHwndSource();
		}

		public void OnSourceChanged(object sender, SourceChangedEventArgs e)
		{
			DetachFromHwndSource();
			TryAttachToHwndSource();
			UpdateScale();
		}

		public void TryAttachToHwndSource()
		{
			if (hwndSource == null)
			{
				hwndSource = PresentationSource.FromVisual(frameworkElement) as HwndSource;
				if (hwndSource != null)
				{
					hwndSource.DpiChanged += OnDpiChanged;
				}
			}
		}

		public void DetachFromHwndSource()
		{
			if (hwndSource != null)
			{
				hwndSource.DpiChanged -= OnDpiChanged;
				hwndSource = null;
			}
		}

		private void OnDpiChanged(object sender, HwndDpiChangedEventArgs e)
		{
			UpdateScale();
		}

		public void UpdateScale()
		{
			double num = VisualTreeHelper.GetDpi(frameworkElement).DpiScaleX;
			if (num <= 0.0)
			{
				num = 1.0;
			}
			frameworkElement.SetCurrentValue(DpiScaleProperty, num);
		}
	}

	public static readonly DependencyProperty ObserveProperty = DependencyProperty.RegisterAttached("Observe", typeof(bool), typeof(DpiScaleObserver), new FrameworkPropertyMetadata(OnObserveChanged));

	public static readonly DependencyProperty DpiScaleProperty = DependencyProperty.RegisterAttached("DpiScale", typeof(double), typeof(DpiScaleObserver), new FrameworkPropertyMetadata(1.0));

	private static readonly DependencyProperty StateProperty = DependencyProperty.RegisterAttached("State", typeof(State), typeof(DpiScaleObserver));

	public static bool GetObserve(FrameworkElement frameworkElement)
	{
		return (bool)frameworkElement.GetValue(ObserveProperty);
	}

	public static void SetObserve(FrameworkElement frameworkElement, bool observe)
	{
		frameworkElement.SetValue(ObserveProperty, observe);
	}

	public static double GetDpiScale(FrameworkElement frameworkElement)
	{
		return (double)frameworkElement.GetValue(DpiScaleProperty);
	}

	public static void SetDpiScale(FrameworkElement frameworkElement, double scale)
	{
		frameworkElement.SetValue(DpiScaleProperty, scale);
	}

	private static void OnObserveChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is FrameworkElement frameworkElement)
		{
			if ((bool)e.NewValue)
			{
				Attach(frameworkElement);
			}
			else
			{
				Detach(frameworkElement);
			}
		}
	}

	private static void Attach(FrameworkElement frameworkElement)
	{
		State state = (State)frameworkElement.GetValue(StateProperty);
		if (state == null)
		{
			state = new State(frameworkElement);
			frameworkElement.SetValue(StateProperty, state);
			frameworkElement.Loaded += state.OnLoaded;
			frameworkElement.Unloaded += state.OnUnloaded;
			PresentationSource.AddSourceChangedHandler(frameworkElement, state.OnSourceChanged);
			state.UpdateScale();
			state.TryAttachToHwndSource();
		}
	}

	private static void Detach(FrameworkElement frameworkElement)
	{
		State state = (State)frameworkElement.GetValue(StateProperty);
		if (state != null)
		{
			PresentationSource.RemoveSourceChangedHandler(frameworkElement, state.OnSourceChanged);
			frameworkElement.Loaded -= state.OnLoaded;
			frameworkElement.Unloaded -= state.OnUnloaded;
			state.DetachFromHwndSource();
			frameworkElement.ClearValue(StateProperty);
		}
	}
}
