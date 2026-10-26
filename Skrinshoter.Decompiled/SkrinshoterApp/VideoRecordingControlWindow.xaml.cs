using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public partial class VideoRecordingControlWindow : PanelWindow
{
	private readonly ILogger logger;

	private bool windowSizeFrozen;

	protected override bool ExcludeFromCapture => true;

	public HorizontalAlignment PanelHorizontalAnchor { get; set; }

	public VerticalAlignment PanelVerticalAnchor { get; set; }

	public event EventHandler<DragMoveFinishedEventArgs> DragMoveFinished;

	public VideoRecordingControlWindow(ILogger<VideoRecordingControlWindow> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		InitializeComponent();
	}

	public Size MeasureAndFreezeMaxSize()
	{
		Size first = MeasureCurrentSize();
		if (DataContext is VideoRecordingControlWindowVM { State: var state, Duration: var duration, Paused: var paused } videoRecordingControlWindowVM)
		{
			try
			{
				videoRecordingControlWindowVM.State = VideoRecordingControlWindowState.Recording;
				videoRecordingControlWindowVM.Duration = new TimeSpan(99, 59, 59);
				videoRecordingControlWindowVM.Paused = false;
				first = MaxSize(first, MeasureCurrentSize());
				videoRecordingControlWindowVM.Paused = true;
				first = MaxSize(first, MeasureCurrentSize());
			}
			finally
			{
				videoRecordingControlWindowVM.State = state;
				videoRecordingControlWindowVM.Duration = duration;
				videoRecordingControlWindowVM.Paused = paused;
				UpdateLayout();
			}
		}
		first = new Size(Math.Ceiling(first.Width), Math.Ceiling(first.Height));
		SizeToContent = SizeToContent.Manual;
		Width = first.Width * 2.0;
		Height = first.Height;
		Canvas.SetLeft(panelContainer, Math.Round((Width - panelContainer.ActualWidth) / 2.0));
		Canvas.SetTop(panelContainer, Math.Round((Height - panelContainer.ActualHeight) / 2.0));
		windowSizeFrozen = true;
		UpdateLayout();
		return first;
	}

	public void ShowPanel()
	{
		Opacity = 1.0;
	}

	public void HidePanel()
	{
		Opacity = 0.0;
	}

	public Point CalculateWindowLocation(Point panelLocation)
	{
		return new Point(panelLocation.X - Canvas.GetLeft(panelContainer), panelLocation.Y - Canvas.GetTop(panelContainer));
	}

	public Rect GetPanelRect()
	{
		return new Rect(Canvas.GetLeft(panelContainer), Canvas.GetTop(panelContainer), panelContainer.ActualWidth, panelContainer.ActualHeight);
	}

	private void PanelContainer_SizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (windowSizeFrozen)
		{
			if (e.WidthChanged)
			{
				double num = e.NewSize.Width - e.PreviousSize.Width;
				double left = Canvas.GetLeft(panelContainer);
				double position = left - PanelHorizontalAnchor switch
				{
					HorizontalAlignment.Center => num / 2.0, 
					HorizontalAlignment.Right => num, 
					_ => 0.0, 
				};
				Canvas.SetLeft(panelContainer, ClampPanelPosition(position, e.NewSize.Width, ActualWidth));
			}
			if (e.HeightChanged)
			{
				double num2 = e.NewSize.Height - e.PreviousSize.Height;
				double top = Canvas.GetTop(panelContainer);
				double position2 = top - PanelVerticalAnchor switch
				{
					VerticalAlignment.Center => num2 / 2.0, 
					VerticalAlignment.Bottom => num2, 
					_ => 0.0, 
				};
				Canvas.SetTop(panelContainer, ClampPanelPosition(position2, e.NewSize.Height, ActualHeight));
			}
		}
	}

	private double ClampPanelPosition(double position, double panelExtent, double windowExtent)
	{
		double num = MathEx.Clamp(position, 0.0, windowExtent - panelExtent);
		if (num != position)
		{
			logger.LogError("Recording control panel hit invisible window bounds: position={Position}, panelExtent={PanelExtent}, windowExtent={WindowExtent}. Window size reserve may be insufficient.", position, panelExtent, windowExtent);
		}
		return num;
	}

	private Size MeasureCurrentSize()
	{
		UpdateLayout();
		return new Size(panelContainer.ActualWidth, panelContainer.ActualHeight);
	}

	private static Size MaxSize(Size first, Size second)
	{
		return new Size(Math.Max(first.Width, second.Width), Math.Max(first.Height, second.Height));
	}

	private void Window_MouseDown(object sender, MouseButtonEventArgs e)
	{
		if (e.ChangedButton == MouseButton.Left)
		{
			double left = Left;
			double top = Top;
			DragMove();
			bool isMoved = left != Left || top != Top;
			DragMoveFinished?.Invoke(this, new DragMoveFinishedEventArgs(isMoved));
		}
	}

	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);
		HWND hWnd = new HWND(new WindowInteropHelper(this).Handle);
		int windowLong = PInvoke.GetWindowLong(hWnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE);
		PInvoke.SetWindowLong(hWnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, windowLong | 0x80);
	}

}
