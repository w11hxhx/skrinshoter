using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;

namespace SkrinshoterApp;

public class LineObjectVM : ResizableObjectVM<LineHandle>
{
	private TwoPointObjectMovingBehavior movingBehavior;

	private TwoPointObjectSizingBehavior sizingBehavior;

	[ObservableProperty]
	public Point startPoint;

	[ObservableProperty]
	public Point endPoint;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeFromStartHandleCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeFromEndHandleCommand;

	protected override bool IsEmpty => StartPoint.AlmostEquals(EndPoint);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Point StartPoint
	{
		get
		{
			return startPoint;
		}
		set
		{
			if (!EqualityComparer<Point>.Default.Equals(startPoint, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.StartPoint);
				startPoint = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.StartPoint);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Point EndPoint
	{
		get
		{
			return endPoint;
		}
		set
		{
			if (!EqualityComparer<Point>.Default.Equals(endPoint, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.EndPoint);
				endPoint = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.EndPoint);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeFromStartHandleCommand => startResizeFromStartHandleCommand ?? (startResizeFromStartHandleCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeFromStartHandle));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeFromEndHandleCommand => startResizeFromEndHandleCommand ?? (startResizeFromEndHandleCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeFromEndHandle));

	public LineObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
	}

	[RelayCommand]
	private void StartResizeFromStartHandle(MouseButtonEventArgs e)
	{
		StartResize(e, LineHandle.Start);
	}

	[RelayCommand]
	private void StartResizeFromEndHandle(MouseButtonEventArgs e)
	{
		StartResize(e, LineHandle.End);
	}

	protected override void StartDraw(Point mousePosition)
	{
		StartPoint = mousePosition;
		EndPoint = mousePosition;
		sizingBehavior = new TwoPointObjectSizingBehavior(StartPoint, EndPoint, LineHandle.End, mousePosition);
	}

	protected override void Draw(Point mousePosition)
	{
		(StartPoint, EndPoint) = sizingBehavior.ProcessMouseMove(mousePosition, Keyboard.Modifiers == ModifierKeys.Shift);
	}

	protected override void StartMove(Point mousePosition)
	{
		movingBehavior = new TwoPointObjectMovingBehavior(StartPoint, EndPoint, mousePosition);
	}

	protected override void Move(Point mousePosition)
	{
		(StartPoint, EndPoint) = movingBehavior.ProcessMouseMove(mousePosition);
	}

	protected override void EndMove()
	{
		Point point = StartPoint;
		Point point2 = EndPoint;
		Point initialStartPoint = movingBehavior.InitialStartPoint;
		Point initialEndPoint = movingBehavior.InitialEndPoint;
		if (point != initialStartPoint || point2 != initialEndPoint)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<LineObjectVM, (Point, Point, int)>(UndoActionKind.Move, this, (movingBehavior.InitialStartPoint, movingBehavior.InitialEndPoint, oldZIndex), (StartPoint, EndPoint, newZIndex), (LineObjectVM obj, (Point StartPoint, Point EndPoint, int ZIndex) state) =>
			{
				Point item = state.StartPoint;
				Point item2 = state.EndPoint;
				obj.StartPoint = item;
				obj.EndPoint = item2;
				obj.SetZIndex(state.ZIndex);
			}));
		}
	}

	protected override void MoveInFloatingMode(Point mousePosition)
	{
		StartPoint = mousePosition;
		EndPoint = mousePosition;
	}

	protected override void StartResize(Point mousePosition, LineHandle lineHandle)
	{
		sizingBehavior = new TwoPointObjectSizingBehavior(StartPoint, EndPoint, lineHandle, mousePosition);
	}

	protected override void Resize(Point mousePosition)
	{
		(StartPoint, EndPoint) = sizingBehavior.ProcessMouseMove(mousePosition, Keyboard.Modifiers == ModifierKeys.Shift);
	}

	protected override void EndResize()
	{
		Point point = StartPoint;
		Point point2 = EndPoint;
		Point initialStartPoint = sizingBehavior.InitialStartPoint;
		Point initialEndPoint = sizingBehavior.InitialEndPoint;
		if (point != initialStartPoint || point2 != initialEndPoint)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<LineObjectVM, (Point, Point)>(UndoActionKind.Resize, this, (sizingBehavior.InitialStartPoint, sizingBehavior.InitialEndPoint), (StartPoint, EndPoint), (LineObjectVM obj, (Point StartPoint, Point EndPoint) state) =>
			{
				Point item = state.StartPoint;
				Point item2 = state.EndPoint;
				obj.StartPoint = item;
				obj.EndPoint = item2;
			}));
		}
	}
}
