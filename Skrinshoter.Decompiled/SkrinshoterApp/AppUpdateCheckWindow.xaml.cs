using System;
using System.Windows;
namespace SkrinshoterApp;
public partial class AppUpdateCheckWindow : Window
{
    public AppUpdateCheckWindow(AppUpdateCheckWindowVM viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        viewModel.RequestClose += ViewModel_RequestClose;
    }
    private void ViewModel_RequestClose(object sender, EventArgs e) => Close();
    private void Later_Click(object sender, RoutedEventArgs e) => Close();
    private void Window_Closed(object sender, EventArgs e)
    {
        if (DataContext is AppUpdateCheckWindowVM vm) { vm.RequestClose -= ViewModel_RequestClose; vm.Dispose(); }
    }
}
