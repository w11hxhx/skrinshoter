using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace SkrinshoterApp;

public sealed class ToggleButtonUseTransitionsOnUserInputBehavior : Behavior<ToggleButton>
{
	private bool useTransitions;

	protected override void OnAttached()
	{
		base.OnAttached();
		AssociatedObject.Loaded += OnLoaded;
		AssociatedObject.Checked += OnCheckedChanged;
		AssociatedObject.Unchecked += OnCheckedChanged;
		AssociatedObject.PreviewMouseLeftButtonDown += OnUserInputMouse;
		AssociatedObject.PreviewKeyDown += OnUserInputKey;
	}

	protected override void OnDetaching()
	{
		AssociatedObject.Loaded -= OnLoaded;
		AssociatedObject.Checked -= OnCheckedChanged;
		AssociatedObject.Unchecked -= OnCheckedChanged;
		AssociatedObject.PreviewMouseLeftButtonDown -= OnUserInputMouse;
		AssociatedObject.PreviewKeyDown -= OnUserInputKey;
		base.OnDetaching();
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		ApplyState(animate: false);
	}

	private void OnUserInputMouse(object sender, MouseButtonEventArgs e)
	{
		useTransitions = true;
	}

	private void OnUserInputKey(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Space)
		{
			useTransitions = true;
		}
	}

	private void OnCheckedChanged(object sender, RoutedEventArgs e)
	{
		ApplyState(useTransitions);
		useTransitions = false;
	}

	private void ApplyState(bool animate)
	{
		string stateName = ((AssociatedObject.IsChecked == true) ? "Checked" : "Unchecked");
		VisualStateManager.GoToState(AssociatedObject, stateName, animate);
	}
}
