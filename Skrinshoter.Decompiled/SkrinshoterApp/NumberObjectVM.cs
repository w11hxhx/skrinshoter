using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public class NumberObjectVM : VisualObjectVM
{
	private OnePointObjectMovingBehavior movingBehavior;

	[ObservableProperty]
	private Rect boundingRectangle;

	[ObservableProperty]
	private Point center;

	[ObservableProperty]
	private double radius;

	[ObservableProperty]
	private double strokeThickness;

	[ObservableProperty]
	private int number;

	[ObservableProperty]
	private double fontSize;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Rect BoundingRectangle
	{
		get
		{
			return boundingRectangle;
		}
		set
		{
			if (!EqualityComparer<Rect>.Default.Equals(boundingRectangle, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.BoundingRectangle);
				boundingRectangle = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.BoundingRectangle);
			}
		}
	}

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
	public double Radius
	{
		get
		{
			return radius;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(radius, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Radius);
				radius = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Radius);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double StrokeThickness
	{
		get
		{
			return strokeThickness;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(strokeThickness, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.StrokeThickness);
				strokeThickness = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.StrokeThickness);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int Number
	{
		get
		{
			return number;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(number, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Number);
				number = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Number);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double FontSize
	{
		get
		{
			return fontSize;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(fontSize, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FontSize);
				fontSize = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FontSize);
			}
		}
	}

	public NumberObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color, int number)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
		Number = number;
		FloatingCursor = Cursors.None;
		PropertyChanged += OnPropertyChanged;
	}

	private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName.In("Center", "LineThickness"))
		{
			UpdateProperties();
		}
	}

	protected override void StartDraw(Point mousePosition)
	{
		Center = mousePosition;
	}

	protected override void StartMove(Point mousePosition)
	{
		movingBehavior = new OnePointObjectMovingBehavior(Center, mousePosition);
	}

	protected override void Move(Point mousePosition)
	{
		Center = movingBehavior.ProcessMouseMove(mousePosition);
	}

	protected override void EndMove()
	{
		if (Center != movingBehavior.InitialPoint)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<NumberObjectVM, (Point, int)>(UndoActionKind.Move, this, (movingBehavior.InitialPoint, oldZIndex), (Center, newZIndex), (NumberObjectVM obj, (Point Point, int ZIndex) state) =>
			{
				(obj.Center, _) = state;
				obj.SetZIndex(state.ZIndex);
			}));
		}
	}

	protected override void MoveInFloatingMode(Point mousePosition)
	{
		Center = mousePosition;
	}

	private void UpdateProperties()
	{
		Radius = LineThickness + 8.0;
		StrokeThickness = LineThickness / 3.0 + 2.0;
		FontSize = LineThickness + 10.0;
		BoundingRectangle = new Rect(Center.X - Radius - StrokeThickness / 2.0, Center.Y - Radius - StrokeThickness / 2.0, Radius * 2.0 + StrokeThickness, Radius * 2.0 + StrokeThickness);
	}

	protected override void UpdateCursor()
	{
		if (stateManager.State == OverlayWindowState.DrawingObject)
		{
			Cursor = Cursors.Arrow;
		}
		else
		{
			base.UpdateCursor();
		}
	}
}
