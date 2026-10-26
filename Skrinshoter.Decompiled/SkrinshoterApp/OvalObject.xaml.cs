using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class OvalObject : UserControl
{
	public OvalObject()
	{
		InitializeComponent();
	}

	private void UserControl_Loaded(object sender, RoutedEventArgs e)
	{
		(DataContext as VisualObjectVM).OnViewReady(MainElement);
	}
}
