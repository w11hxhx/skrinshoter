using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Xaml.Behaviors;

namespace SkrinshoterApp;

public sealed class AutoSaveOnInputBehavior : Behavior<FrameworkElement>
{
	public static readonly RoutedEvent AutoSaveRequestEvent = EventManager.RegisterRoutedEvent("AutoSaveRequest", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(AutoSaveOnInputBehavior));

	protected override void OnAttached()
	{
		base.OnAttached();
		UIElement uIElement = AssociatedObject;
		if (uIElement != null)
		{
			uIElement.AddHandler(UIElement.LostKeyboardFocusEvent, new KeyboardFocusChangedEventHandler(OnLostKeyboardFocus), handledEventsToo: true);
			uIElement.AddHandler(UIElement.PreviewKeyDownEvent, new KeyEventHandler(OnPreviewKeyDown), handledEventsToo: true);
			uIElement.AddHandler(ToggleButton.CheckedEvent, new RoutedEventHandler(OnImmediate), handledEventsToo: true);
			uIElement.AddHandler(ToggleButton.UncheckedEvent, new RoutedEventHandler(OnImmediate), handledEventsToo: true);
			uIElement.AddHandler(Selector.SelectionChangedEvent, new SelectionChangedEventHandler(OnSelectionChanged), handledEventsToo: true);
			uIElement.AddHandler(Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragCompleted), handledEventsToo: true);
			uIElement.AddHandler(AutoSaveRequestEvent, new RoutedEventHandler(OnAutoSaveRequested), handledEventsToo: true);
		}
	}

	private void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (e.OriginalSource is TextBoxBase || e.OriginalSource is PasswordBox)
		{
			SaveAfterBindings();
		}
	}

	private void OnPreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return && e.OriginalSource is TextBox textBox)
		{
			textBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
			SaveAfterBindings();
		}
	}

	private void OnImmediate(object sender, RoutedEventArgs e)
	{
		SaveAfterBindings();
	}

	private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		SaveAfterBindings();
	}

	private void OnDragCompleted(object sender, DragCompletedEventArgs e)
	{
		SaveAfterBindings();
	}

	private void OnAutoSaveRequested(object sender, RoutedEventArgs e)
	{
		SaveAfterBindings();
	}

	private void SaveAfterBindings()
	{
		AssociatedObject.Dispatcher.BeginInvoke((Action)(() =>
		{
			(AssociatedObject?.DataContext as SettingsWindowVM)?.SaveSettings();
		}), DispatcherPriority.Background);
	}
}
