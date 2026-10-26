using System.Windows;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class FileNameTemplateWindow : Window
{
	public FileNameTemplateWindow(FileNameTemplateWindowVM vm)
	{
		InitializeComponent();
		DataContext = vm;
	}

	private void ApplyButton_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
	}
}
