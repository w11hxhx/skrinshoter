using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SkrinshoterApp;

public static class WpfHelpers
{
	public static T FindChild<T>(this DependencyObject parent, string childName) where T : DependencyObject
	{
		if (parent == null)
		{
			return null;
		}
		T val = null;
		int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
		for (int i = 0; i < childrenCount; i++)
		{
			DependencyObject child = VisualTreeHelper.GetChild(parent, i);
			if (child as T == null)
			{
				val = child.FindChild<T>(childName);
				if (val != null)
				{
					break;
				}
				continue;
			}
			if (!string.IsNullOrEmpty(childName))
			{
				if (child is FrameworkElement frameworkElement && frameworkElement.Name == childName)
				{
					val = (T)child;
					break;
				}
				continue;
			}
			val = (T)child;
			break;
		}
		return val;
	}

	public static void FixComboBoxOutClick(object sender, MouseButtonEventArgs e)
	{
		if (!(sender is ComboBox comboBox))
		{
			return;
		}
		Point position = Mouse.GetPosition(comboBox);
		if (position.X < 0.0 || position.Y < 0.0 || position.X > comboBox.ActualWidth || position.Y > comboBox.ActualHeight)
		{
			UIElement child = comboBox.FindChild<Popup>(null).Child;
			Point position2 = Mouse.GetPosition(child);
			if (position2.X < 0.0 || position2.Y < 0.0 || position2.X > child.RenderSize.Width || position2.Y > child.RenderSize.Height)
			{
				comboBox.IsDropDownOpen = false;
			}
		}
	}
}
