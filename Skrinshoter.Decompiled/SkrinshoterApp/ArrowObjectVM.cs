using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Media;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public class ArrowObjectVM : LineObjectVM
{
	[ObservableProperty]
	private double scale;

	[ObservableProperty]
	private double rotateAngle;

	[ObservableProperty]
	private Geometry arrowHeadGeometry = Geometry.Parse("M0.5 11.8268C-0.166667 11.4419 -0.166666 10.4797 0.5 10.0948L17.75 0.135466C18.4167 -0.249434 19.25 0.231692 19.25 1.00149L16.25 10.9608L19.25 20.9201C19.25 21.6899 18.4167 22.171 17.75 21.7861L0.5 11.8268Z");

	[ObservableProperty]
	public Point arrowHeadAnchorPoint = new Point(1.0, 10.9608);

	[ObservableProperty]
	public Geometry arrowShaftGeometry;

	[ObservableProperty]
	public bool arrowShaftVisible;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double Scale
	{
		get
		{
			return scale;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(scale, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Scale);
				scale = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Scale);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double RotateAngle
	{
		get
		{
			return rotateAngle;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(rotateAngle, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.RotateAngle);
				rotateAngle = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.RotateAngle);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Geometry ArrowHeadGeometry
	{
		get
		{
			return arrowHeadGeometry;
		}
		set
		{
			if (!EqualityComparer<Geometry>.Default.Equals(arrowHeadGeometry, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ArrowHeadGeometry);
				arrowHeadGeometry = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ArrowHeadGeometry);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Point ArrowHeadAnchorPoint
	{
		get
		{
			return arrowHeadAnchorPoint;
		}
		set
		{
			if (!EqualityComparer<Point>.Default.Equals(arrowHeadAnchorPoint, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ArrowHeadAnchorPoint);
				arrowHeadAnchorPoint = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ArrowHeadAnchorPoint);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Geometry ArrowShaftGeometry
	{
		get
		{
			return arrowShaftGeometry;
		}
		set
		{
			if (!EqualityComparer<Geometry>.Default.Equals(arrowShaftGeometry, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ArrowShaftGeometry);
				arrowShaftGeometry = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ArrowShaftGeometry);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ArrowShaftVisible
	{
		get
		{
			return arrowShaftVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(arrowShaftVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ArrowShaftVisible);
				arrowShaftVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ArrowShaftVisible);
			}
		}
	}

	public ArrowObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
		PropertyChanged += OnPropertyChanged;
	}

	private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName.In("StartPoint", "EndPoint", "LineThickness"))
		{
			UpdateProperties();
		}
	}

	private void UpdateProperties()
	{
		Scale = 0.5 + (LineThickness - 2.0) * 9.5 / 58.0;
		Vector vector = EndPoint - StartPoint;
		RotateAngle = ((vector.Length == 0.0) ? 0.0 : (Math.Atan2(vector.Y, vector.X) * 180.0 / Math.PI + 180.0));
		double num = vector.Length - 13.0 * Scale;
		ArrowShaftVisible = num > Scale;
		if (ArrowShaftVisible)
		{
			ArrowShaftGeometry = BuildArrowShaftGeometry(num, Scale);
		}
	}

	private static PathGeometry BuildArrowShaftGeometry(double length, double scale)
	{
		if (length < 0.0)
		{
			throw new ArgumentOutOfRangeException("length", "Length must be non-negative.");
		}
		if (scale <= 0.0)
		{
			throw new ArgumentOutOfRangeException("scale", "Scale must be positive.");
		}
		double num = 7.0 * scale;
		double num2 = 1.43544 * scale;
		double num3 = num * 0.5;
		double num4 = num2 * num2;
		double num5 = Math.Sqrt(length * length + num4);
		double x = (0.0 - num4) / num5;
		double num6 = num2 * length / num5;
		double x2 = 0.0 - length;
		return new PathGeometry(new _003C_003Ez__ReadOnlySingleElementList<PathFigure>(new PathFigure
		{
			StartPoint = new Point(x2, 0.0 - num3),
			IsClosed = true,
			Segments = 
			{
				(PathSegment)new LineSegment(new Point(x, 0.0 - num6), isStroked: true),
				(PathSegment)new ArcSegment(new Point(x, num6), new Size(num2, num2), 0.0, isLargeArc: false, SweepDirection.Clockwise, isStroked: true),
				(PathSegment)new LineSegment(new Point(x2, num3), isStroked: true)
			}
		}));
	}
}
