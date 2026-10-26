using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class ArrowObject : UserControl
{
	public ArrowObject()
	{
		InitializeComponent();
	}

	private void UserControl_Loaded(object sender, RoutedEventArgs e)
	{
		(DataContext as VisualObjectVM).OnViewReady(MainElement);
	}
}
