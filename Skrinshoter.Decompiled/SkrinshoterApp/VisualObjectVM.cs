using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;

namespace SkrinshoterApp;

public abstract class VisualObjectVM : FloatingObjectVM
{
	protected readonly OverlayWindowStateManager stateManager;

	protected readonly SelectionManager selectionManager;

	protected readonly UndoManager undoManager;

	protected int oldZIndex;

	protected int newZIndex;

	protected bool isDragging;

	[ObservableProperty]
	private double lineThickness;

	[ObservableProperty]
	private double effectiveLineThickness;

	[ObservableProperty]
	private Color color;

	[ObservableProperty]
	private Cursor cursor;

	[ObservableProperty]
	private bool selectionVisible;

	[ObservableProperty]
	private bool selectionHitTestEnabled;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startDrawCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseEventArgs>? drawCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? endDrawCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? startMoveCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseEventArgs>? moveCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? endMoveCommand;

	protected virtual bool IsEmpty => false;

	public bool NeedsInteractiveDrawing { get; set; } = true;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double LineThickness
	{
		get
		{
			return lineThickness;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(lineThickness, value))
			{
				double oldValue = lineThickness;
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.LineThickness);
				lineThickness = value;
				OnLineThicknessChanged(oldValue, value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.LineThickness);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double EffectiveLineThickness
	{
		get
		{
			return effectiveLineThickness;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(effectiveLineThickness, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.EffectiveLineThickness);
				effectiveLineThickness = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.EffectiveLineThickness);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Color Color
	{
		get
		{
			return color;
		}
		set
		{
			if (!EqualityComparer<Color>.Default.Equals(color, value))
			{
				Color oldValue = color;
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Color);
				color = value;
				OnColorChanged(oldValue, value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Color);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Cursor Cursor
	{
		get
		{
			return cursor;
		}
		set
		{
			if (!EqualityComparer<System.Windows.Input.Cursor>.Default.Equals(cursor, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Cursor);
				cursor = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Cursor);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool SelectionVisible
	{
		get
		{
			return selectionVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(selectionVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectionVisible);
				selectionVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectionVisible);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool SelectionHitTestEnabled
	{
		get
		{
			return selectionHitTestEnabled;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(selectionHitTestEnabled, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectionHitTestEnabled);
				selectionHitTestEnabled = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectionHitTestEnabled);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartDrawCommand => startDrawCommand ?? (startDrawCommand = new RelayCommand<MouseButtonEventArgs>(StartDraw));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseEventArgs> DrawCommand => drawCommand ?? (drawCommand = new RelayCommand<MouseEventArgs>(Draw));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> EndDrawCommand => endDrawCommand ?? (endDrawCommand = new RelayCommand<MouseButtonEventArgs>(EndDraw));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> StartMoveCommand => startMoveCommand ?? (startMoveCommand = new RelayCommand<MouseButtonEventArgs>(StartMove));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseEventArgs> MoveCommand => moveCommand ?? (moveCommand = new RelayCommand<MouseEventArgs>(Move));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> EndMoveCommand => endMoveCommand ?? (endMoveCommand = new RelayCommand<MouseButtonEventArgs>(EndMove));

	protected VisualObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM)
	{
		this.stateManager = stateManager;
		this.selectionManager = selectionManager;
		this.undoManager = undoManager;
		this.lineThickness = lineThickness;
		this.color = color;
		effectiveLineThickness = GetEffectiveLineThickness();
		stateManager.StateChanged += StateManager_StateChanged;
		selectionManager.SelectedObjectChanged += SelectionManager_SelectedObjectChanged;
	}

	private void StateManager_StateChanged(object sender, EventArgs e)
	{
		UpdateSelectionProperties();
		UpdateCursor();
	}

	private void SelectionManager_SelectedObjectChanged(object sender, EventArgs e)
	{
		UpdateSelectionProperties();
	}

	[RelayCommand]
	private void StartDraw(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.DrawingToolSelected)
		{
			CaptureMouse(e.Source as UIElement);
			isDragging = true;
			stateManager.SetState(OverlayWindowState.DrawingObject, OverlayWindowVM);
			selectionManager.Select(this);
			StartDraw(e.GetPosition(null));
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void Draw(MouseEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.DrawingObject && isDragging)
		{
			Draw(e.GetPosition(null));
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void EndDraw(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.DrawingObject && isDragging)
		{
			isDragging = false;
			EndDraw();
			(e.Source as UIElement).ReleaseMouseCapture();
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void StartMove(MouseButtonEventArgs e)
	{
		if (stateManager.State.In(OverlayWindowState.DrawingToolSelected, OverlayWindowState.EditingText))
		{
			CaptureMouse(e.Source as UIElement);
			isDragging = true;
			stateManager.SetState(OverlayWindowState.ReadyToMoveObject, OverlayWindowVM);
			selectionManager.Select(this);
			StartMove(e.GetPosition(null));
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void Move(MouseEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.ReadyToMoveObject && isDragging)
		{
			stateManager.SetState(OverlayWindowState.MovingObject, OverlayWindowVM);
			CaptureMouse(e.Source as UIElement);
			newZIndex = GetInsertZIndex() - 1;
			oldZIndex = SetZIndex(newZIndex);
		}
		if (stateManager.State == OverlayWindowState.MovingObject && isDragging)
		{
			Move(e.GetPosition(null));
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void EndMove(MouseButtonEventArgs e)
	{
		if (stateManager.State.In(OverlayWindowState.ReadyToMoveObject, OverlayWindowState.MovingObject) && isDragging)
		{
			isDragging = false;
			EndMove();
			stateManager.SetState(OverlayWindowState.DrawingToolSelected);
			(e.Source as UIElement).ReleaseMouseCapture();
			e.Handled = true;
		}
	}

	public void OnViewReady(UIElement mainElement)
	{
		if (NeedsInteractiveDrawing)
		{
			MouseButtonEventArgs parameter = new MouseButtonEventArgs(Mouse.PrimaryDevice, Environment.TickCount, MouseButton.Left)
			{
				RoutedEvent = UIElement.MouseLeftButtonDownEvent,
				Source = mainElement
			};
			StartDrawCommand.Execute(parameter);
			NeedsInteractiveDrawing = false;
		}
	}

	public virtual int GetInsertZIndex()
	{
		return OverlayWindowVM.VisualObjects.Count;
	}

	public override string ToString()
	{
		return GetType().Name.Replace("ObjectVM", "");
	}

	protected virtual void StartDraw(Point mousePosition)
	{
	}

	protected virtual void Draw(Point mousePosition)
	{
	}

	protected virtual void EndDraw()
	{
		selectionManager.Select(null);
		stateManager.SetState(OverlayWindowState.DrawingToolSelected);
		if (IsEmpty)
		{
			OverlayWindowVM.VisualObjects.Remove(this);
		}
		else
		{
			undoManager.RegisterAction(new AddObjectAction(this, selectionManager));
		}
	}

	protected virtual void StartMove(Point mousePosition)
	{
	}

	protected virtual void Move(Point mousePosition)
	{
	}

	protected virtual void EndMove()
	{
	}

	protected virtual void UpdateSelectionProperties()
	{
		bool flag = selectionManager.SelectedObject == this;
		SelectionVisible = flag && stateManager.State == OverlayWindowState.DrawingToolSelected;
		SelectionHitTestEnabled = flag;
	}

	protected virtual void UpdateCursor()
	{
		switch (stateManager.State)
		{
		case OverlayWindowState.DrawingObject:
			Cursor = FloatingCursor;
			break;
		case OverlayWindowState.DrawingToolSelected:
		case OverlayWindowState.ReadyToMoveObject:
		case OverlayWindowState.MovingObject:
		case OverlayWindowState.EditingText:
			Cursor = Cursors.Arrow;
			break;
		default:
			Cursor = null;
			break;
		}
	}

	protected void CaptureMouse(UIElement element)
	{
		element.CaptureMouse();
		Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action)(() =>
		{
			element.CaptureMouse();
		}));
	}

	protected int SetZIndex(int newZIndex)
	{
		int num = OverlayWindowVM.VisualObjects.IndexOf(this);
		if (num >= 0 && newZIndex >= 0 && newZIndex < OverlayWindowVM.VisualObjects.Count)
		{
			OverlayWindowVM.VisualObjects.Move(num, newZIndex);
		}
		return num;
	}

	protected virtual double GetEffectiveLineThickness()
	{
		return LineThickness;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnLineThicknessChanged(double oldValue, double newValue)
	{
		EffectiveLineThickness = GetEffectiveLineThickness();
		if (selectionManager.SelectedObject == this)
		{
			selectionManager.RaiseSelectedObjectChanged();
		}
		if (stateManager.State != OverlayWindowState.DrawingObject && !IsFloating)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<VisualObjectVM, double>(UndoActionKind.ChangeLineThickness, this, oldValue, newValue, (VisualObjectVM obj, double state) =>
			{
				obj.LineThickness = state;
			}, mergeEnabled: true));
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnColorChanged(Color oldValue, Color newValue)
	{
		if (selectionManager.SelectedObject == this)
		{
			selectionManager.RaiseSelectedObjectChanged();
		}
		if (stateManager.State != OverlayWindowState.DrawingObject && !IsFloating)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<VisualObjectVM, Color>(UndoActionKind.ChangeColor, this, oldValue, newValue, (VisualObjectVM obj, Color state) =>
			{
				obj.Color = state;
			}));
		}
	}
}
