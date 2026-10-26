using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class ScreenshotEditingDrawingWindowVM : LocalizableObservableObject
{
	private readonly OverlayWindowStateManager stateManager;

	private bool isProgrammaticSelectedLineThicknessChange;

	private bool isProgrammaticSelectedColorChange;

	[ObservableProperty]
	private DrawingTool selectedTool;

	[ObservableProperty]
	private double selectedLineThickness = 6.0;

	[ObservableProperty]
	private System.Windows.Media.Color selectedColor = System.Windows.Media.Color.FromRgb(byte.MaxValue, 59, 48);

	[ObservableProperty]
	private ObservableCollection<System.Windows.Media.Color> availableColors = new ObservableCollection<System.Windows.Media.Color>
	{
		System.Windows.Media.Color.FromRgb(0, 0, 0),
		System.Windows.Media.Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue),
		System.Windows.Media.Color.FromRgb(byte.MaxValue, 59, 48),
		System.Windows.Media.Color.FromRgb(byte.MaxValue, 149, 0),
		System.Windows.Media.Color.FromRgb(byte.MaxValue, 204, 0),
		System.Windows.Media.Color.FromRgb(52, 199, 89),
		System.Windows.Media.Color.FromRgb(90, 200, 250),
		System.Windows.Media.Color.FromRgb(0, 122, byte.MaxValue),
		System.Windows.Media.Color.FromRgb(175, 82, 222)
	};

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<DrawingTool>? selectToolByMouseCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<DrawingTool>? selectToolByHotkeyCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseWheelEventArgs>? changeLineThicknessByMouseCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? selectCustomColorCommand;

	public UndoManager UndoManager { get; }

	public string ArrowToolTip => Resources.ScreenshotEditing_ArrowToolTip;

	public string LineToolTip => Resources.ScreenshotEditing_LineToolTip;

	public string PencilToolTip => Resources.ScreenshotEditing_PencilToolTip;

	public string MarkerToolTip => Resources.ScreenshotEditing_MarkerToolTip;

	public string RectangleToolTip => Resources.ScreenshotEditing_RectangleToolTip;

	public string OvalToolTip => Resources.ScreenshotEditing_OvalToolTip;

	public string TextToolTip => Resources.ScreenshotEditing_TextToolTip;

	public string NumberToolTip => Resources.ScreenshotEditing_NumberToolTip;

	public string BlurToolTip => Resources.ScreenshotEditing_BlurToolTip;

	public string LineThicknessToolTip => Resources.ScreenshotEditing_LineThicknessToolTip;

	public string ColorToolTip => Resources.ScreenshotEditing_ColorToolTip;

	public string UndoToolTip => Resources.ScreenshotEditing_UndoToolTip;

	public string CustomColorButton => Resources.ScreenshotEditing_CustomColorButtonText;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public DrawingTool SelectedTool
	{
		get
		{
			return selectedTool;
		}
		set
		{
			if (!EqualityComparer<DrawingTool>.Default.Equals(selectedTool, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedTool);
				selectedTool = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedTool);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double SelectedLineThickness
	{
		get
		{
			return selectedLineThickness;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(selectedLineThickness, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedLineThickness);
				selectedLineThickness = value;
				OnSelectedLineThicknessChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedLineThickness);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public System.Windows.Media.Color SelectedColor
	{
		get
		{
			return selectedColor;
		}
		set
		{
			if (!EqualityComparer<System.Windows.Media.Color>.Default.Equals(selectedColor, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectedColor);
				selectedColor = value;
				OnSelectedColorChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectedColor);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ObservableCollection<System.Windows.Media.Color> AvailableColors
	{
		get
		{
			return availableColors;
		}
		set
		{
			if (!EqualityComparer<ObservableCollection<System.Windows.Media.Color>>.Default.Equals(availableColors, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.AvailableColors);
				availableColors = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.AvailableColors);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<DrawingTool> SelectToolByMouseCommand => selectToolByMouseCommand ?? (selectToolByMouseCommand = new RelayCommand<DrawingTool>(SelectToolByMouse, (DrawingTool _) => CanExecuteSelectTool()));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<DrawingTool> SelectToolByHotkeyCommand => selectToolByHotkeyCommand ?? (selectToolByHotkeyCommand = new RelayCommand<DrawingTool>(SelectToolByHotkey, (DrawingTool _) => CanExecuteSelectTool()));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseWheelEventArgs> ChangeLineThicknessByMouseCommand => changeLineThicknessByMouseCommand ?? (changeLineThicknessByMouseCommand = new RelayCommand<MouseWheelEventArgs>(ChangeLineThicknessByMouse));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SelectCustomColorCommand => selectCustomColorCommand ?? (selectCustomColorCommand = new RelayCommand(SelectCustomColor));

	public event EventHandler<PropertyChangeSourceEventArgs> SelectedLineThicknessChanged;

	public event EventHandler<PropertyChangeSourceEventArgs> SelectedColorChanged;

	public ScreenshotEditingDrawingWindowVM(OverlayWindowStateManager stateManager, UndoManager undoManager)
	{
		this.stateManager = stateManager;
		UndoManager = undoManager;
	}

	[RelayCommand(CanExecute = "CanExecuteSelectTool")]
	private void SelectToolByMouse(DrawingTool tool)
	{
		SelectedTool = ((SelectedTool != tool) ? tool : DrawingTool.None);
	}

	[RelayCommand(CanExecute = "CanExecuteSelectTool")]
	private void SelectToolByHotkey(DrawingTool tool)
	{
		SelectedTool = tool;
	}

	private bool CanExecuteSelectTool()
	{
		return stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.DrawingToolSelected);
	}

	[RelayCommand]
	private void ChangeLineThicknessByMouse(MouseWheelEventArgs e)
	{
		int num = e.Delta / 120;
		SelectedLineThickness = MathEx.Clamp(SelectedLineThickness + (double)(num * 2), 2.0, 60.0);
	}

	[RelayCommand]
	private void SelectCustomColor()
	{
		ColorDialog colorDialog = new ColorDialog
		{
			FullOpen = true,
			Color = System.Drawing.Color.FromArgb(SelectedColor.R, SelectedColor.G, SelectedColor.B)
		};
		if (colorDialog.ShowDialog() == DialogResult.OK)
		{
			for (int num = AvailableColors.Count - 1; num > 0; num--)
			{
				AvailableColors[num] = AvailableColors[num - 1];
			}
			AvailableColors[0] = System.Windows.Media.Color.FromRgb(colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B);
			SelectedColor = AvailableColors[0];
		}
	}

	public void SetSelectedLineThickness(double value)
	{
		if (SelectedLineThickness != value)
		{
			isProgrammaticSelectedLineThicknessChange = true;
			SelectedLineThickness = value;
		}
	}

	public void SetSelectedColor(System.Windows.Media.Color value)
	{
		if (SelectedColor != value)
		{
			isProgrammaticSelectedColorChange = true;
			SelectedColor = value;
		}
	}

	public void RaiseSelectedColorChanged()
	{
		OnPropertyChanged("SelectedColor");
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedLineThicknessChanged(double value)
	{
		SelectedLineThicknessChanged?.Invoke(this, new PropertyChangeSourceEventArgs(isProgrammaticSelectedLineThicknessChange));
		isProgrammaticSelectedLineThicknessChange = false;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnSelectedColorChanged(System.Windows.Media.Color value)
	{
		SelectedColorChanged?.Invoke(this, new PropertyChangeSourceEventArgs(isProgrammaticSelectedColorChange));
		isProgrammaticSelectedColorChange = false;
	}
}
