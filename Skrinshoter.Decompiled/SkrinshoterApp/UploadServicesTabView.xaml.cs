using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace SkrinshoterApp;

public partial class UploadServicesTabView : UserControl
{
	public UploadServicesTabView()
	{
		InitializeComponent();
	}

	private Settings Settings => ((SettingsWindowVM)DataContext).Settings;

	private void AddService_Click(object sender, RoutedEventArgs e)
	{
		CustomUploadProvider provider = new CustomUploadProvider();
		if (new UploadProviderWindow(provider) { Owner = Window.GetWindow(this) }.ShowDialog() == true)
		{
			Settings.CustomUploadProviders.Add(provider);
		}
	}

	private void EditService_Click(object sender, RoutedEventArgs e)
	{
		CustomUploadProvider provider = (CustomUploadProvider)((FrameworkElement)sender).DataContext;
		if (new UploadProviderWindow(provider) { Owner = Window.GetWindow(this) }.ShowDialog() == true)
		{
			int index = Settings.CustomUploadProviders.IndexOf(provider);
			Settings.CustomUploadProviders.RemoveAt(index);
			Settings.CustomUploadProviders.Insert(index, provider);
		}
	}

	private void RemoveService_Click(object sender, RoutedEventArgs e)
	{
		Settings.CustomUploadProviders.Remove((CustomUploadProvider)((FrameworkElement)sender).DataContext);
	}

	private void ImportShareX_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog dialog = new OpenFileDialog
		{
			Title = "Импорт конфигурации ShareX",
			Filter = "ShareX Custom Uploader (*.sxcu)|*.sxcu|JSON (*.json)|*.json|Все файлы (*.*)|*.*",
			Multiselect = true
		};
		if (dialog.ShowDialog(Window.GetWindow(this)) != true)
		{
			return;
		}
		int imported = 0;
		try
		{
			foreach (string fileName in dialog.FileNames)
			{
				Settings.CustomUploadProviders.Add(ShareXCustomUploaderImporter.Import(fileName));
				imported++;
			}
			Settings.ScreenshotUploadDestinations |= ScreenshotUploadDestination.Custom;
			MessageBox.Show(Window.GetWindow(this), $"Импортировано сервисов: {imported}.", "ShareX", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		catch (Exception exception)
		{
			MessageBox.Show(Window.GetWindow(this), $"Импортировано: {imported}. Ошибка: {exception.Message}", "ShareX", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
