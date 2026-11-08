using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Markup;

namespace SkrinshoterApp;

public partial class VideoTabView : UserControl
{
	private bool updatingPreset;

	public VideoTabView()
	{
		InitializeComponent();
	}

	private void VideoTabView_Loaded(object sender, RoutedEventArgs e)
	{
		if (DataContext is not SettingsWindowVM viewModel)
		{
			return;
		}
		string current = $"{viewModel.Settings.VideoResolutionHeight},{viewModel.Settings.VideoFrameRate},{viewModel.Settings.VideoBitrateKbps}";
		updatingPreset = true;
		QualityPreset.SelectedIndex = -1;
		foreach (ComboBoxItem item in QualityPreset.Items)
		{
			if ((string)item.Tag == current)
			{
				QualityPreset.SelectedItem = item;
				break;
			}
		}
		updatingPreset = false;
	}

	private void QualityPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (updatingPreset || DataContext is not SettingsWindowVM viewModel || QualityPreset.SelectedItem is not ComboBoxItem item)
		{
			return;
		}
		string[] values = ((string)item.Tag).Split(',');
		viewModel.Settings.VideoResolutionHeight = int.Parse(values[0]);
		viewModel.Settings.VideoFrameRate = int.Parse(values[1]);
		viewModel.Settings.VideoBitrateKbps = int.Parse(values[2]);
	}

}
