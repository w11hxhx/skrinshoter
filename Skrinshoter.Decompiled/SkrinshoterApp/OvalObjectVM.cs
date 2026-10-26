using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public class OvalObjectVM : RectangleObjectVM
{
	[ObservableProperty]
	private Point center;

	[ObservableProperty]
	private double radiusX;

	[ObservableProperty]
	private double radiusY;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Point Center
	{
		get
		{
			return center;
		}
		set
		{
			if (!EqualityComparer<Point>.Default.Equals(center, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Center);
				center = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Center);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double RadiusX
	{
		get
		{
			return radiusX;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(radiusX, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.RadiusX);
				radiusX = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.RadiusX);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double RadiusY
	{
		get
		{
			return radiusY;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(radiusY, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.RadiusY);
				radiusY = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.RadiusY);
			}
		}
	}

	public OvalObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
		PropertyChanged += OnPropertyChanged;
	}

	private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "Rect")
		{
			Center = new Point(Rect.X + Rect.Width / 2.0, Rect.Y + Rect.Height / 2.0);
			RadiusX = Rect.Width / 2.0;
			RadiusY = Rect.Height / 2.0;
		}
	}
}
