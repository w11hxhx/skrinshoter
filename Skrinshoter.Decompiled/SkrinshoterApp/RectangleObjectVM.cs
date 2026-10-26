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

public class RectangleObjectVM : ResizableObjectVM<RectHandle>
{
	private RectBoundObjectMovingBehavior movingBehavior;

	private RectBoundObjectSizingBehavior sizingBehavior;

	[ObservableProperty]
	private Rect rect;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeTopLeftCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeTopCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeTopRightCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeRightCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeBottomRightCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeBottomCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeBottomLeftCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startResizeLeftCommand;

	protected override bool IsEmpty => Rect.Size.AlmostEquals(default);

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Rect Rect
	{
		get
		{
			return rect;
		}
		set
		{
			if (!EqualityComparer<Rect>.Default.Equals(rect, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Rect);
				rect = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Rect);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeTopLeftCommand => startResizeTopLeftCommand ?? (startResizeTopLeftCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeTopLeft));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeTopCommand => startResizeTopCommand ?? (startResizeTopCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeTop));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeTopRightCommand => startResizeTopRightCommand ?? (startResizeTopRightCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeTopRight));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeRightCommand => startResizeRightCommand ?? (startResizeRightCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeRight));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeBottomRightCommand => startResizeBottomRightCommand ?? (startResizeBottomRightCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeBottomRight));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeBottomCommand => startResizeBottomCommand ?? (startResizeBottomCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeBottom));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeBottomLeftCommand => startResizeBottomLeftCommand ?? (startResizeBottomLeftCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeBottomLeft));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartResizeLeftCommand => startResizeLeftCommand ?? (startResizeLeftCommand = new RelayCommand<MouseButtonEventArgs>(StartResizeLeft));

	public RectangleObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
	}

	[RelayCommand]
	private void StartResizeTopLeft(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.TopLeft);
	}

	[RelayCommand]
	private void StartResizeTop(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.Top);
	}

	[RelayCommand]
	private void StartResizeTopRight(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.TopRight);
	}

	[RelayCommand]
	private void StartResizeRight(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.Right);
	}

	[RelayCommand]
	private void StartResizeBottomRight(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.BottomRight);
	}

	[RelayCommand]
	private void StartResizeBottom(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.Bottom);
	}

	[RelayCommand]
	private void StartResizeBottomLeft(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.BottomLeft);
	}

	[RelayCommand]
	private void StartResizeLeft(MouseButtonEventArgs e)
	{
		StartResize(e, RectHandle.Left);
	}

	protected override void StartDraw(Point mousePosition)
	{
		Rect = new Rect(mousePosition, mousePosition);
		sizingBehavior = new RectBoundObjectSizingBehavior(Rect, RectHandle.BottomRight, mousePosition);
	}

	protected override void Draw(Point mousePosition)
	{
		Rect = sizingBehavior.ProcessMouseMove(mousePosition, Keyboard.Modifiers == ModifierKeys.Shift);
	}

	protected override void StartMove(Point mousePosition)
	{
		movingBehavior = new RectBoundObjectMovingBehavior(Rect, mousePosition);
	}

	protected override void Move(Point mousePosition)
	{
		Rect = movingBehavior.ProcessMouseMove(mousePosition);
	}

	protected override void EndMove()
	{
		if (Rect != movingBehavior.InitialRect)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<RectangleObjectVM, (Rect, int)>(UndoActionKind.Move, this, (movingBehavior.InitialRect, oldZIndex), (Rect, newZIndex), (RectangleObjectVM obj, (Rect Rect, int ZIndex) state) =>
			{
				(obj.Rect, _) = state;
				obj.SetZIndex(state.ZIndex);
			}));
		}
	}

	protected override void MoveInFloatingMode(Point mousePosition)
	{
		Rect = new Rect(mousePosition, mousePosition);
	}

	protected override void StartResize(Point mousePosition, RectHandle rectHandle)
	{
		sizingBehavior = new RectBoundObjectSizingBehavior(Rect, rectHandle, mousePosition);
	}

	protected override void Resize(Point mousePosition)
	{
		Rect = sizingBehavior.ProcessMouseMove(mousePosition, Keyboard.Modifiers == ModifierKeys.Shift);
	}

	protected override void EndResize()
	{
		if (Rect != sizingBehavior.InitialRect)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<RectangleObjectVM, Rect>(UndoActionKind.Resize, this, sizingBehavior.InitialRect, Rect, (RectangleObjectVM obj, Rect state) =>
			{
				obj.Rect = state;
			}));
		}
	}
}
