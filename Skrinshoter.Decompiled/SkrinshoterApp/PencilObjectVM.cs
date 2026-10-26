using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public class PencilObjectVM : VisualObjectVM
{
	internal readonly List<Point> points = new List<Point>();

	internal bool smoothingEnabled;

	private MultiPointObjectDrawingBehavior drawingBehavior;

	private RectBoundObjectMovingBehavior movingBehavior;

	private Pen cachedPen;

	private double cachedThickness;

	[ObservableProperty]
	private Geometry geometry;

	[ObservableProperty]
	private Rect boundingRectangle;

	public TranslateTransform MoveTransform { get; } = new TranslateTransform();

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Geometry Geometry
	{
		get
		{
			return geometry;
		}
		set
		{
			if (!EqualityComparer<System.Windows.Media.Geometry>.Default.Equals(geometry, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Geometry);
				geometry = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Geometry);
			}
		}
	}

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

	public PencilObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
		PropertyChanged += OnPropertyChanged;
	}

	private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "LineThickness")
		{
			UpdateBoundingRectangle();
		}
	}

	protected override void StartDraw(Point mousePosition)
	{
		points.Clear();
		points.Add(mousePosition);
		UpdateProperties();
		drawingBehavior = new MultiPointObjectDrawingBehavior(mousePosition);
	}

	protected override void Draw(Point mousePosition)
	{
		points.Add(drawingBehavior.ProcessMouseMove(mousePosition, Keyboard.Modifiers == ModifierKeys.Shift));
		UpdateProperties();
	}

	protected override void EndDraw()
	{
		base.EndDraw();
		smoothingEnabled = true;
		UpdateProperties();
	}

	protected override void StartMove(Point mousePosition)
	{
		movingBehavior = new RectBoundObjectMovingBehavior(BoundingRectangle, mousePosition);
	}

	protected override void Move(Point mousePosition)
	{
		Rect rect = BoundingRectangle;
		Rect rect2 = movingBehavior.ProcessMouseMove(mousePosition);
		MoveTransform.X = rect2.X - rect.X;
		MoveTransform.Y = rect2.Y - rect.Y;
	}

	protected override void EndMove()
	{
		Vector vector = new Vector(MoveTransform.X, MoveTransform.Y);
		ApplyOffsetToPoints(vector);
		MoveTransform.X = 0.0;
		MoveTransform.Y = 0.0;
		UpdateProperties();
		ForceRender();
		if (vector != default(Vector))
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<PencilObjectVM, (Vector, int)>(UndoActionKind.Move, this, (-vector, oldZIndex), (vector, newZIndex), (PencilObjectVM obj, (Vector Vector, int ZIndex) state) =>
			{
				obj.ApplyOffsetToPoints(state.Vector);
				obj.UpdateProperties();
				obj.SetZIndex(state.ZIndex);
			}));
		}
	}

	protected override void MoveInFloatingMode(Point mousePosition)
	{
		if (points.Count > 0)
		{
			points[0] = mousePosition;
		}
		else
		{
			points.Add(mousePosition);
		}
		UpdateProperties();
	}

	internal void UpdateProperties()
	{
		Geometry = GetGeometry();
		UpdateBoundingRectangle();
	}

	protected void UpdateBoundingRectangle()
	{
		BoundingRectangle = Geometry.GetRenderBounds(GetPen());
	}

	private Geometry GetGeometry()
	{
		if (points.Count == 0)
		{
			return System.Windows.Media.Geometry.Empty;
		}
		if (!smoothingEnabled)
		{
			return new PathGeometry(new _003C_003Ez__ReadOnlySingleElementList<PathFigure>(new PathFigure(points[0], points.Select((Point x) => new LineSegment(x, isStroked: true)), closed: false)));
		}
		return CatmullRomToBezierGeometry(ChaikinSmooth(FilterClosePoints(points, 10.0), 1));
	}

	private static List<Point> FilterClosePoints(List<Point> points, double minDistance)
	{
		if (points.Count < 2)
		{
			return points.ToList();
		}
		List<Point> list = new List<Point> { points[0] };
		foreach (Point item in points.Skip(1))
		{
			if ((item - list.Last()).Length >= minDistance)
			{
				list.Add(item);
			}
		}
		if ((list.Last() - points.Last()).Length > 1E-06)
		{
			list.Add(points.Last());
		}
		return list;
	}

	private static List<Point> ChaikinSmooth(List<Point> points, int iterations)
	{
		if (points.Count < 2)
		{
			return points.ToList();
		}
		for (int i = 0; i < iterations; i++)
		{
			List<Point> list = new List<Point> { points.First() };
			for (int j = 0; j < points.Count - 1; j++)
			{
				Point point = points[j];
				Point point2 = points[j + 1];
				Point item = new Point(0.75 * point.X + 0.25 * point2.X, 0.75 * point.Y + 0.25 * point2.Y);
				Point item2 = new Point(0.25 * point.X + 0.75 * point2.X, 0.25 * point.Y + 0.75 * point2.Y);
				list.Add(item);
				list.Add(item2);
			}
			list.Add(points.Last());
			points = list;
		}
		return points;
	}

	private static Geometry CatmullRomToBezierGeometry(List<Point> points)
	{
		if (points.Count == 0)
		{
			return System.Windows.Media.Geometry.Empty;
		}
		if (points.Count < 3)
		{
			return new PathGeometry(new _003C_003Ez__ReadOnlySingleElementList<PathFigure>(new PathFigure(points[0], points.Select((Point x) => new LineSegment(x, isStroked: true)), closed: false)));
		}
		PathFigure pathFigure = new PathFigure(points[0], Array.Empty<PathSegment>(), closed: false);
		for (int num = 0; num < points.Count - 1; num++)
		{
			Point point = ((num > 0) ? points[num - 1] : points[num]);
			Point point2 = points[num];
			Point point3 = points[num + 1];
			Point point4 = ((num + 2 < points.Count) ? points[num + 2] : point3);
			Point point5 = new Point(point2.X + (point3.X - point.X) / 6.0, point2.Y + (point3.Y - point.Y) / 6.0);
			Point point6 = new Point(point3.X - (point4.X - point2.X) / 6.0, point3.Y - (point4.Y - point2.Y) / 6.0);
			pathFigure.Segments.Add(new BezierSegment(point5, point6, point3, isStroked: true));
		}
		return new PathGeometry(new _003C_003Ez__ReadOnlySingleElementList<PathFigure>(pathFigure));
	}

	private Pen GetPen()
	{
		if (cachedPen == null || cachedThickness != EffectiveLineThickness)
		{
			cachedPen = new Pen(Brushes.Black, EffectiveLineThickness)
			{
				StartLineCap = PenLineCap.Round,
				EndLineCap = PenLineCap.Round,
				LineJoin = PenLineJoin.Round
			};
			cachedThickness = EffectiveLineThickness;
		}
		return cachedPen;
	}

	private void ApplyOffsetToPoints(Vector offset)
	{
		if (!(offset == default(Vector)))
		{
			for (int i = 0; i < points.Count; i++)
			{
				points[i] += offset;
			}
		}
	}

	private void ForceRender()
	{
		Application.Current.Dispatcher.Invoke(() =>
		{
		}, DispatcherPriority.Render);
	}
}
