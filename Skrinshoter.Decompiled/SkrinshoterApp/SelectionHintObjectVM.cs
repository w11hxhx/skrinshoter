using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class SelectionHintObjectVM : FloatingObjectVM
{
	[ObservableProperty]
	private Point point;

	[ObservableProperty]
	private string text = Resources.SelectArea;

	[ObservableProperty]
	private Geometry image;

	[ObservableProperty]
	private bool imageVisible = true;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Point Point
	{
		get
		{
			return point;
		}
		set
		{
			if (!EqualityComparer<Point>.Default.Equals(point, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Point);
				point = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Point);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Text
	{
		get
		{
			return text;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(text, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Text);
				text = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Text);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Geometry Image
	{
		get
		{
			return image;
		}
		set
		{
			if (!EqualityComparer<Geometry>.Default.Equals(image, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Image);
				image = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Image);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ImageVisible
	{
		get
		{
			return imageVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(imageVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ImageVisible);
				imageVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ImageVisible);
			}
		}
	}

	public SelectionHintObjectVM(OverlayWindowVM overlayWindowVM, string imageResourceKey, ScreenshotEditorShowState screenshotEditorShowState = null)
		: base(overlayWindowVM)
	{
		Image = (Geometry)Application.Current.FindResource(imageResourceKey);
		FloatingCursor = Cursors.Cross;
		if (screenshotEditorShowState != null)
		{
			ImageVisible = screenshotEditorShowState.ShouldShow;
			screenshotEditorShowState.ShouldShowChanged += ScreenshotEditorShowState_ShouldShowChanged;
		}
	}

	private void ScreenshotEditorShowState_ShouldShowChanged(object sender, EventArgs e)
	{
		ImageVisible = (sender as ScreenshotEditorShowState).ShouldShow;
	}

	protected override void MoveInFloatingMode(Point mousePosition)
	{
		Point = mousePosition;
	}
}
