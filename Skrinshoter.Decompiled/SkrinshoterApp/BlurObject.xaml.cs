using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class BlurObject : UserControl
{
	public BlurObject()
	{
		InitializeComponent();
	}

	private void UserControl_Loaded(object sender, RoutedEventArgs e)
	{
		(DataContext as VisualObjectVM).OnViewReady(MainElement);
	}
}
