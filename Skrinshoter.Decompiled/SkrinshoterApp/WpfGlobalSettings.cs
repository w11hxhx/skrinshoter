using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkrinshoterApp;

public static class WpfGlobalSettings
{
	public static void Apply()
	{
		ToolTipService.InitialShowDelayProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(500));
		EventManager.RegisterClassHandler(typeof(ComboBox), UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(WpfHelpers.FixComboBoxOutClick));
	}
}
