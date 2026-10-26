using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;

namespace SkrinshoterApp;

public class OverlayWindowVM : LocalizableObservableObject
{
	private readonly System.Windows.Size minSelectionAreaSize;

	private readonly OverlayWindowStateManager stateManager;

	private readonly AreaSelectionManager areaSelectionManager;

	private readonly Visual visual;

	private RectangleDrawingBehavior drawingBehavior;

	private RectangleMovingBehavior movingBehavior;

	private RectangleResizingBehavior resizingBehavior;

	private Rect oldFrameRect;

	[ObservableProperty]
	private BitmapSource backgroundImage;

	[ObservableProperty]
	private ObservableCollection<VisualObjectVM> visualObjects = new ObservableCollection<VisualObjectVM>();

	[ObservableProperty]
	private FloatingObjectVM floatingObject;

	[ObservableProperty]
	private bool floatingObjectVisible;

	[ObservableProperty]
	private bool isMouseOverWindow;

	[ObservableProperty]
	private Cursor cursor = Cursors.Cross;

	[ObservableProperty]
	private double windowObservedWidth;

	[ObservableProperty]
	private double windowObservedHeight;

	[ObservableProperty]
	private Rect frameRect;

	[ObservableProperty]
	private Rect frameBorderRect;

	[ObservableProperty]
	private bool frameBorderVisible;

	[ObservableProperty]
	private bool canResize;

	[ObservableProperty]
	private bool resizeHandlesVisible;

	[ObservableProperty]
	private bool areaInsideFrameVisible;

	[ObservableProperty]
	private string frameSizeBoxText;

	[ObservableProperty]
	private double frameSizeBoxLeft;

	[ObservableProperty]
	private double frameSizeBoxTop;

	[ObservableProperty]
	private double frameSizeBoxObservedWidth;

	[ObservableProperty]
	private double frameSizeBoxObservedHeight;

	[ObservableProperty]
	private bool frameSizeBoxVisible;

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

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseEventArgs>? resizeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? endResizeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseEventArgs>? updateFloatingObjectOnMouseMoveCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? updateFloatingObjectOnMouseLeaveCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? selectAllScreenCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? startCancelSelectionCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? endCancelSelectionCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? cancelSelectionCommand;

	public double MonitorScaleFactor { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public BitmapSource BackgroundImage
	{
		get
		{
			return backgroundImage;
		}
		set
		{
			if (!EqualityComparer<BitmapSource>.Default.Equals(backgroundImage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.BackgroundImage);
				backgroundImage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.BackgroundImage);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public ObservableCollection<VisualObjectVM> VisualObjects
	{
		get
		{
			return visualObjects;
		}
		set
		{
			if (!EqualityComparer<ObservableCollection<VisualObjectVM>>.Default.Equals(visualObjects, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.VisualObjects);
				visualObjects = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.VisualObjects);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public FloatingObjectVM FloatingObject
	{
		get
		{
			return floatingObject;
		}
		set
		{
			if (!EqualityComparer<FloatingObjectVM>.Default.Equals(floatingObject, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FloatingObject);
				floatingObject = value;
				OnFloatingObjectChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FloatingObject);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FloatingObjectVisible
	{
		get
		{
			return floatingObjectVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(floatingObjectVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FloatingObjectVisible);
				floatingObjectVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FloatingObjectVisible);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsMouseOverWindow
	{
		get
		{
			return isMouseOverWindow;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isMouseOverWindow, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsMouseOverWindow);
				isMouseOverWindow = value;
				OnIsMouseOverWindowChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsMouseOverWindow);
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
	public double WindowObservedWidth
	{
		get
		{
			return windowObservedWidth;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(windowObservedWidth, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WindowObservedWidth);
				windowObservedWidth = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WindowObservedWidth);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double WindowObservedHeight
	{
		get
		{
			return windowObservedHeight;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(windowObservedHeight, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WindowObservedHeight);
				windowObservedHeight = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WindowObservedHeight);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Rect FrameRect
	{
		get
		{
			return frameRect;
		}
		set
		{
			if (!EqualityComparer<Rect>.Default.Equals(frameRect, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameRect);
				frameRect = value;
				OnFrameRectChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameRect);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Rect FrameBorderRect
	{
		get
		{
			return frameBorderRect;
		}
		set
		{
			if (!EqualityComparer<Rect>.Default.Equals(frameBorderRect, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameBorderRect);
				frameBorderRect = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameBorderRect);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FrameBorderVisible
	{
		get
		{
			return frameBorderVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(frameBorderVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameBorderVisible);
				frameBorderVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameBorderVisible);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool CanResize
	{
		get
		{
			return canResize;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(canResize, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CanResize);
				canResize = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CanResize);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool ResizeHandlesVisible
	{
		get
		{
			return resizeHandlesVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(resizeHandlesVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ResizeHandlesVisible);
				resizeHandlesVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ResizeHandlesVisible);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool AreaInsideFrameVisible
	{
		get
		{
			return areaInsideFrameVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(areaInsideFrameVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.AreaInsideFrameVisible);
				areaInsideFrameVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.AreaInsideFrameVisible);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string FrameSizeBoxText
	{
		get
		{
			return frameSizeBoxText;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(frameSizeBoxText, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameSizeBoxText);
				frameSizeBoxText = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameSizeBoxText);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double FrameSizeBoxLeft
	{
		get
		{
			return frameSizeBoxLeft;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(frameSizeBoxLeft, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameSizeBoxLeft);
				frameSizeBoxLeft = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameSizeBoxLeft);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double FrameSizeBoxTop
	{
		get
		{
			return frameSizeBoxTop;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(frameSizeBoxTop, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameSizeBoxTop);
				frameSizeBoxTop = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameSizeBoxTop);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double FrameSizeBoxObservedWidth
	{
		get
		{
			return frameSizeBoxObservedWidth;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(frameSizeBoxObservedWidth, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameSizeBoxObservedWidth);
				frameSizeBoxObservedWidth = value;
				OnFrameSizeBoxObservedWidthChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameSizeBoxObservedWidth);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double FrameSizeBoxObservedHeight
	{
		get
		{
			return frameSizeBoxObservedHeight;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(frameSizeBoxObservedHeight, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameSizeBoxObservedHeight);
				frameSizeBoxObservedHeight = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameSizeBoxObservedHeight);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool FrameSizeBoxVisible
	{
		get
		{
			return frameSizeBoxVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(frameSizeBoxVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FrameSizeBoxVisible);
				frameSizeBoxVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FrameSizeBoxVisible);
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

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseEventArgs> ResizeCommand => resizeCommand ?? (resizeCommand = new RelayCommand<MouseEventArgs>(Resize));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> EndResizeCommand => endResizeCommand ?? (endResizeCommand = new RelayCommand<MouseButtonEventArgs>(EndResize));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseEventArgs> UpdateFloatingObjectOnMouseMoveCommand => updateFloatingObjectOnMouseMoveCommand ?? (updateFloatingObjectOnMouseMoveCommand = new RelayCommand<MouseEventArgs>(UpdateFloatingObjectOnMouseMove));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand UpdateFloatingObjectOnMouseLeaveCommand => updateFloatingObjectOnMouseLeaveCommand ?? (updateFloatingObjectOnMouseLeaveCommand = new RelayCommand(UpdateFloatingObjectOnMouseLeave));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SelectAllScreenCommand => selectAllScreenCommand ?? (selectAllScreenCommand = new RelayCommand(SelectAllScreen));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand StartCancelSelectionCommand => startCancelSelectionCommand ?? (startCancelSelectionCommand = new RelayCommand(StartCancelSelection));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand EndCancelSelectionCommand => endCancelSelectionCommand ?? (endCancelSelectionCommand = new RelayCommand(EndCancelSelection));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CancelSelectionCommand => cancelSelectionCommand ?? (cancelSelectionCommand = new RelayCommand(CancelSelection));

	public OverlayWindowVM(OverlayWindowStateManager stateManager, AreaSelectionManager areaSelectionManager, Visual visual, double monitorScaleFactor, System.Windows.Size minSelectionAreaSize = default(System.Windows.Size), BitmapSource backgroundImage = null)
	{
		this.stateManager = stateManager;
		this.areaSelectionManager = areaSelectionManager;
		this.visual = visual;
		MonitorScaleFactor = monitorScaleFactor;
		this.minSelectionAreaSize = ((minSelectionAreaSize == default(System.Windows.Size)) ? new System.Windows.Size(11.0, 11.0) : minSelectionAreaSize);
		BackgroundImage = backgroundImage;
		stateManager.StateChanged += StateManager_StateChanged;
		areaSelectionManager.AreaChanged += AreaSelectionManager_AreaChanged;
	}

	private void StateManager_StateChanged(object sender, EventArgs e)
	{
		UpdateProperties();
		UpdateFloatingObjectVisibility();
	}

	private void AreaSelectionManager_AreaChanged(object sender, EventArgs e)
	{
		FrameRect = ((areaSelectionManager.Owner == this) ? areaSelectionManager.Area : default(Rect));
	}

	[RelayCommand]
	private void StartDraw(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.Selected)
		{
			CaptureMouse(e.Source as UIElement);
			System.Windows.Point position = e.GetPosition(null);
			drawingBehavior = new RectangleDrawingBehavior(GetBoundingRect(), position, minSelectionAreaSize, visual.GetScreenPixelSize());
			areaSelectionManager.SetArea(new Rect(position, position), this);
			stateManager.SetState(OverlayWindowState.Selecting, this);
		}
	}

	[RelayCommand]
	private void Draw(MouseEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.Selecting && stateManager.ActiveWindowVM == this)
		{
			areaSelectionManager.SetArea(drawingBehavior.ProcessMouseMove(e.GetPosition(null), Keyboard.Modifiers == ModifierKeys.Shift), this);
		}
	}

	[RelayCommand]
	private void EndDraw(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.Selecting && stateManager.ActiveWindowVM == this)
		{
			areaSelectionManager.SetArea(drawingBehavior.Complete(), this);
			stateManager.SetState(OverlayWindowState.Selected);
			(e.Source as UIElement).ReleaseMouseCapture();
		}
	}

	[RelayCommand]
	private void StartMove(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.Selected)
		{
			CaptureMouse(e.Source as UIElement);
			movingBehavior = new RectangleMovingBehavior(FrameRect, GetBoundingRect(), e.GetPosition(null));
			stateManager.SetState(OverlayWindowState.MovingFrame, this);
		}
	}

	[RelayCommand]
	private void Move(MouseEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.MovingFrame && stateManager.ActiveWindowVM == this)
		{
			areaSelectionManager.SetArea(movingBehavior.ProcessMouseMove(e.GetPosition(null)), this);
		}
	}

	[RelayCommand]
	private void EndMove(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.MovingFrame && stateManager.ActiveWindowVM == this)
		{
			stateManager.SetState(OverlayWindowState.Selected);
			(e.Source as UIElement).ReleaseMouseCapture();
		}
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

	private void StartResize(MouseButtonEventArgs e, RectHandle resizeHandle)
	{
		if (stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.DrawingToolSelected, OverlayWindowState.EditingText))
		{
			CaptureMouse(e.Source as UIElement);
			resizingBehavior = new RectangleResizingBehavior(FrameRect, GetBoundingRect(), resizeHandle, e.GetPosition(null), minSelectionAreaSize);
			stateManager.SetState(OverlayWindowState.ResizingFrame, this);
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void Resize(MouseEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.ResizingFrame && stateManager.ActiveWindowVM == this)
		{
			areaSelectionManager.SetArea(resizingBehavior.ProcessMouseMove(e.GetPosition(null), Keyboard.Modifiers == ModifierKeys.Shift), this);
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void EndResize(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.ResizingFrame && stateManager.ActiveWindowVM == this)
		{
			stateManager.SetState(OverlayWindowState.Selected);
			(e.Source as UIElement).ReleaseMouseCapture();
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void UpdateFloatingObjectOnMouseMove(MouseEventArgs e)
	{
		FloatingObject?.UpdateFloatingObjectPosition(e.GetPosition(null));
		UpdateFloatingObjectVisibility();
	}

	[RelayCommand]
	private void UpdateFloatingObjectOnMouseLeave()
	{
		UpdateFloatingObjectVisibility();
	}

	[RelayCommand]
	private void SelectAllScreen()
	{
		Rect boundingRect = GetBoundingRect();
		if (FrameRect != boundingRect)
		{
			stateManager.SetState(OverlayWindowState.Selecting, this);
			oldFrameRect = FrameRect;
			areaSelectionManager.SetArea(boundingRect, this);
			stateManager.SetState(OverlayWindowState.Selected);
		}
		else if (oldFrameRect.Width > 0.0 && oldFrameRect.Height > 0.0)
		{
			stateManager.SetState(OverlayWindowState.Selecting, this);
			areaSelectionManager.SetArea(oldFrameRect, this);
			oldFrameRect = default;
			stateManager.SetState(OverlayWindowState.Selected);
		}
	}

	[RelayCommand]
	private void StartCancelSelection()
	{
		if (stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.DrawingToolSelected))
		{
			stateManager.SetState(OverlayWindowState.Canceling, this);
		}
	}

	[RelayCommand]
	private void EndCancelSelection()
	{
		if (stateManager.State == OverlayWindowState.Canceling && stateManager.ActiveWindowVM == this)
		{
			stateManager.SetState(OverlayWindowState.Canceled);
		}
	}

	[RelayCommand]
	private void CancelSelection()
	{
		stateManager.SetState(OverlayWindowState.Canceled);
	}

	public Rectangle GetSelectedAreaInScreenCoordinates()
	{
		if (PresentationSource.FromVisual(visual) == null || FrameRect.Width <= 0.0 || FrameRect.Height <= 0.0)
		{
			return default;
		}
		return visual.RectToScreen(FrameRect).ToRectangle();
	}

	public Rectangle GetSelectedAreaInRelativeScreenCoordinates()
	{
		if (PresentationSource.FromVisual(visual) == null || FrameRect.Width <= 0.0 || FrameRect.Height <= 0.0)
		{
			return default;
		}
		Rectangle result = visual.RectToScreen(FrameRect).ToRectangle();
		System.Drawing.Point point = visual.PointToScreen(new System.Windows.Point(0.0, 0.0)).ToIntPoint();
		result.Offset(-point.X, -point.Y);
		return result;
	}

	public bool ProcessKey(Key key, ModifierKeys modifiers)
	{
		switch (key)
		{
		case Key.Up:
			switch (modifiers)
			{
			case ModifierKeys.None:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorMovingBehavior(FrameRect, GetBoundingRect()).Move(new Vector(0.0, 0.0 - visual.GetScreenPixelSize().Height)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			case ModifierKeys.Shift:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorResizingBehavior(FrameRect, GetBoundingRect(), minSelectionAreaSize).Resize(new Vector(0.0, 0.0 - visual.GetScreenPixelSize().Height)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			}
			break;
		case Key.Down:
			switch (modifiers)
			{
			case ModifierKeys.None:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorMovingBehavior(FrameRect, GetBoundingRect()).Move(new Vector(0.0, visual.GetScreenPixelSize().Height)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			case ModifierKeys.Shift:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorResizingBehavior(FrameRect, GetBoundingRect(), minSelectionAreaSize).Resize(new Vector(0.0, visual.GetScreenPixelSize().Height)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			}
			break;
		case Key.Left:
			switch (modifiers)
			{
			case ModifierKeys.None:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorMovingBehavior(FrameRect, GetBoundingRect()).Move(new Vector(0.0 - visual.GetScreenPixelSize().Width, 0.0)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			case ModifierKeys.Shift:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorResizingBehavior(FrameRect, GetBoundingRect(), minSelectionAreaSize).Resize(new Vector(0.0 - visual.GetScreenPixelSize().Width, 0.0)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			}
			break;
		case Key.Right:
			switch (modifiers)
			{
			case ModifierKeys.None:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorMovingBehavior(FrameRect, GetBoundingRect()).Move(new Vector(visual.GetScreenPixelSize().Width, 0.0)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			case ModifierKeys.Shift:
				if (stateManager.State == OverlayWindowState.Selected)
				{
					stateManager.SetState(OverlayWindowState.AdjustingFrame, this);
					areaSelectionManager.SetArea(new RectangleVectorResizingBehavior(FrameRect, GetBoundingRect(), minSelectionAreaSize).Resize(new Vector(visual.GetScreenPixelSize().Width, 0.0)), this);
					stateManager.SetState(OverlayWindowState.Selected);
				}
				return true;
			}
			break;
		}
		return false;
	}

	private void UpdateProperties()
	{
		FrameBorderVisible = areaSelectionManager.HasArea && areaSelectionManager.Owner == this;
		FrameBorderRect = (FrameBorderVisible ? new Rect(FrameRect.X - 1.0, FrameRect.Y - 1.0, FrameRect.Width + 1.0, FrameRect.Height + 1.0) : FrameRect);
		CanResize = FrameBorderVisible && stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.ResizingFrame, OverlayWindowState.DrawingToolSelected, OverlayWindowState.DrawingObject, OverlayWindowState.ReadyToMoveObject, OverlayWindowState.MovingObject, OverlayWindowState.ResizingObject, OverlayWindowState.EditingText);
		ResizeHandlesVisible = CanResize && stateManager.State != OverlayWindowState.ResizingFrame;
		AreaInsideFrameVisible = FrameBorderVisible && stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.MovingFrame, OverlayWindowState.ResizingFrame, OverlayWindowState.AdjustingFrame);
		UpdateFrameSizeBoxText();
		UpdateFrameSizeBoxPosition();
		FrameSizeBoxVisible = FrameBorderVisible && stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.Selecting, OverlayWindowState.MovingFrame, OverlayWindowState.ResizingFrame, OverlayWindowState.AdjustingFrame, OverlayWindowState.DrawingToolSelected, OverlayWindowState.DrawingObject, OverlayWindowState.ReadyToMoveObject, OverlayWindowState.MovingObject, OverlayWindowState.ResizingObject, OverlayWindowState.EditingText);
	}

	private void UpdateFrameSizeBoxText()
	{
		Rectangle selectedAreaInScreenCoordinates = GetSelectedAreaInScreenCoordinates();
		FrameSizeBoxText = $"{selectedAreaInScreenCoordinates.Width} x {selectedAreaInScreenCoordinates.Height}";
	}

	private void UpdateFrameSizeBoxPosition()
	{
		Rect movingRect = new Rect(FrameBorderRect.Left, FrameBorderRect.Top - FrameSizeBoxObservedHeight - 3.0, FrameSizeBoxObservedWidth, FrameSizeBoxObservedHeight);
		if (movingRect.Top < 0.0)
		{
			movingRect.Location = new System.Windows.Point(FrameBorderRect.Left + 4.0, FrameBorderRect.Top + 4.0);
		}
		movingRect = movingRect.MoveInsideRect(GetBoundingRect());
		FrameSizeBoxLeft = movingRect.Left;
		FrameSizeBoxTop = movingRect.Top;
	}

	private void UpdateFloatingObjectVisibility()
	{
		FloatingObjectVisible = FloatingObject != null && IsMouseOverWindow && ((stateManager.State == OverlayWindowState.Selected && !areaSelectionManager.HasArea) || (stateManager.State.In(OverlayWindowState.DrawingToolSelected, OverlayWindowState.EditingText) && IsMouseOverFloatingZone()));
	}

	private bool IsMouseOverFloatingZone()
	{
		for (DependencyObject dependencyObject = Mouse.DirectlyOver as DependencyObject; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
		{
			if (dependencyObject is FrameworkElement frameworkElement && frameworkElement.DataContext != FloatingObject)
			{
				if (frameworkElement.DataContext is VisualObjectVM)
				{
					return false;
				}
				if (!string.IsNullOrEmpty(frameworkElement.Name) && frameworkElement.Name.In("DrawingCanvas", "DarkArea"))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void CaptureMouse(UIElement element)
	{
		element.CaptureMouse();
		Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Input, (Action)(() =>
		{
			if (stateManager.State.In(OverlayWindowState.Selecting, OverlayWindowState.MovingFrame, OverlayWindowState.ResizingFrame) && stateManager.ActiveWindowVM == this && Mouse.LeftButton == MouseButtonState.Pressed && Mouse.Captured != element)
			{
				element.CaptureMouse();
			}
		}));
	}

	private Rect GetBoundingRect()
	{
		return new Rect(0.0, 0.0, WindowObservedWidth, WindowObservedHeight);
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnFloatingObjectChanged(FloatingObjectVM value)
	{
		UpdateFloatingObjectVisibility();
		Cursor = value?.FloatingCursor ?? Cursors.Cross;
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnIsMouseOverWindowChanged(bool value)
	{
		UpdateFloatingObjectVisibility();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnFrameRectChanged(Rect value)
	{
		UpdateProperties();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnFrameSizeBoxObservedWidthChanged(double value)
	{
		UpdateFrameSizeBoxPosition();
	}
}
