using System;
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
using CommunityToolkit.Mvvm.Input;

namespace SkrinshoterApp;

public class TextObjectVM : VisualObjectVM
{
	private OnePointObjectMovingBehavior movingBehavior;

	[ObservableProperty]
	private Point point;

	[ObservableProperty]
	private string text;

	[ObservableProperty]
	private double fontSize;

	[ObservableProperty]
	private bool focused;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? startEditCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? endEditCommand;

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
				string oldValue = text;
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Text);
				text = value;
				OnTextChanged(oldValue, value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Text);
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

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool Focused
	{
		get
		{
			return focused;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(focused, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Focused);
				focused = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Focused);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand StartEditCommand => startEditCommand ?? (startEditCommand = new RelayCommand(StartEdit));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EndEditCommand => endEditCommand ?? (endEditCommand = new RelayCommand(EndEdit));

	public TextObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
		FloatingCursor = Cursors.None;
		overlayWindowVM.PropertyChanged += OverlayWindowVM_PropertyChanged;
		PropertyChanged += OnPropertyChanged;
		UpdateProperties();
	}

	private void OverlayWindowVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "FloatingObject")
		{
			UpdateSelectionProperties();
		}
	}

	private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "LineThickness")
		{
			UpdateProperties();
		}
	}

	[RelayCommand]
	private void StartEdit()
	{
		if (stateManager.State == OverlayWindowState.DrawingToolSelected)
		{
			stateManager.SetState(OverlayWindowState.EditingText, OverlayWindowVM);
		}
	}

	[RelayCommand]
	private void EndEdit()
	{
		if (stateManager.State == OverlayWindowState.EditingText)
		{
			if (string.IsNullOrWhiteSpace(Text))
			{
				OverlayWindowVM.VisualObjects.Remove(this);
			}
			stateManager.SetState(OverlayWindowState.DrawingToolSelected);
		}
	}

	protected override void StartDraw(Point mousePosition)
	{
		Point = mousePosition;
	}

	protected override void EndDraw()
	{
		stateManager.SetState(OverlayWindowState.EditingText, OverlayWindowVM);
	}

	protected override void StartMove(Point mousePosition)
	{
		movingBehavior = new OnePointObjectMovingBehavior(Point, mousePosition);
	}

	protected override void Move(Point mousePosition)
	{
		Point = movingBehavior.ProcessMouseMove(mousePosition);
	}

	protected override void EndMove()
	{
		if (Point != movingBehavior.InitialPoint)
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<TextObjectVM, (Point, int)>(UndoActionKind.Move, this, (movingBehavior.InitialPoint, oldZIndex), (Point, newZIndex), (TextObjectVM obj, (Point Point, int ZIndex) state) =>
			{
				(obj.Point, _) = state;
				obj.SetZIndex(state.ZIndex);
			}));
		}
	}

	protected override void MoveInFloatingMode(Point mousePosition)
	{
		Point = mousePosition;
	}

	private void UpdateProperties()
	{
		FontSize = CalculateFontSize(LineThickness);
	}

	protected override void UpdateSelectionProperties()
	{
		bool flag = selectionManager.SelectedObject == this;
		Focused = flag && stateManager.State == OverlayWindowState.EditingText;
		SelectionVisible = (flag && stateManager.State.In(OverlayWindowState.DrawingToolSelected, OverlayWindowState.DrawingObject, OverlayWindowState.EditingText)) || IsFloating;
	}

	private static double CalculateFontSize(double lineThickness)
	{
		double num = 14.0;
		double num2 = 130.0;
		double num3 = 2.0;
		double num4 = 60.0;
		double y = 1.5;
		double x = (lineThickness - num3) / (num4 - num3);
		return Math.Round(num + (num2 - num) * Math.Pow(x, y));
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnTextChanged(string? oldValue, string newValue)
	{
		if (string.IsNullOrWhiteSpace(newValue))
		{
			OverlayWindowVM.VisualObjects.Remove(this);
			if (oldValue != null)
			{
				text = oldValue;
				undoManager.RegisterAction(new DeleteObjectAction(this, selectionManager));
			}
		}
		else if (oldValue == null)
		{
			undoManager.RegisterAction(new AddObjectAction(this, selectionManager));
		}
		else
		{
			undoManager.RegisterAction(new ChangeObjectStateAction<TextObjectVM, string>(UndoActionKind.ChangeText, this, oldValue, newValue, (TextObjectVM obj, string state) =>
			{
				obj.Text = state;
			}));
		}
	}
}
