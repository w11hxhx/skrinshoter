using System;
using System.Buffers;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AudioVideoCore;
using CameraCapture;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SkrinshoterApp.Properties;
using Windows.Win32.Foundation;

namespace SkrinshoterApp;

public class VideoRecordingCameraWindowVM : LocalizableObservableObject, IDisposable
{
	private const double WindowBorderSize = 15.0;

	private readonly ILogger logger;

	private readonly LoggerFactory loggerFactory;

	private readonly SettingsManager settingsManager;

	private readonly Settings settings;

	private readonly Dispatcher dispatcher;

	private readonly Visual visual;

	private readonly int frameRate;

	private CameraCapturer cameraCapturer;

	private long cameraCapturerInstanceId;

	[ObservableProperty]
	private bool canUpdateCameraCapturer;

	[ObservableProperty]
	private WriteableBitmap writeableBitmap;

	[ObservableProperty]
	private VideoRecordingCameraWindowState state;

	[ObservableProperty]
	private string errorMessage;

	[ObservableProperty]
	private Rectangle selectionRectangle;

	[ObservableProperty]
	private double windowAspectRatio;

	[ObservableProperty]
	private double windowMinWidth;

	[ObservableProperty]
	private double windowMinHeight;

	[ObservableProperty]
	private double windowObservedWidth;

	[ObservableProperty]
	private double windowObservedHeight;

	[ObservableProperty]
	private double videoAreaObservedWidth;

	[ObservableProperty]
	private double videoAreaObservedHeight;

	[ObservableProperty]
	private VideoRecordingCameraWindowShape windowShape;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? switchWindowShapeCommand;

	public string CameraDeviceId { get; }

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool CanUpdateCameraCapturer
	{
		get
		{
			return canUpdateCameraCapturer;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(canUpdateCameraCapturer, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CanUpdateCameraCapturer);
				canUpdateCameraCapturer = value;
				OnCanUpdateCameraCapturerChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CanUpdateCameraCapturer);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public WriteableBitmap WriteableBitmap
	{
		get
		{
			return writeableBitmap;
		}
		set
		{
			if (!EqualityComparer<System.Windows.Media.Imaging.WriteableBitmap>.Default.Equals(writeableBitmap, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WriteableBitmap);
				writeableBitmap = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WriteableBitmap);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public VideoRecordingCameraWindowState State
	{
		get
		{
			return state;
		}
		set
		{
			if (!EqualityComparer<VideoRecordingCameraWindowState>.Default.Equals(state, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.State);
				state = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.State);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ErrorMessage
	{
		get
		{
			return errorMessage;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(errorMessage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ErrorMessage);
				errorMessage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ErrorMessage);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Rectangle SelectionRectangle
	{
		get
		{
			return selectionRectangle;
		}
		set
		{
			if (!EqualityComparer<Rectangle>.Default.Equals(selectionRectangle, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.SelectionRectangle);
				selectionRectangle = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.SelectionRectangle);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double WindowAspectRatio
	{
		get
		{
			return windowAspectRatio;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(windowAspectRatio, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WindowAspectRatio);
				windowAspectRatio = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WindowAspectRatio);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double WindowMinWidth
	{
		get
		{
			return windowMinWidth;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(windowMinWidth, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WindowMinWidth);
				windowMinWidth = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WindowMinWidth);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double WindowMinHeight
	{
		get
		{
			return windowMinHeight;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(windowMinHeight, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WindowMinHeight);
				windowMinHeight = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WindowMinHeight);
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
	public double VideoAreaObservedWidth
	{
		get
		{
			return videoAreaObservedWidth;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(videoAreaObservedWidth, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.VideoAreaObservedWidth);
				videoAreaObservedWidth = value;
				OnVideoAreaObservedWidthChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.VideoAreaObservedWidth);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double VideoAreaObservedHeight
	{
		get
		{
			return videoAreaObservedHeight;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(videoAreaObservedHeight, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.VideoAreaObservedHeight);
				videoAreaObservedHeight = value;
				OnVideoAreaObservedHeightChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.VideoAreaObservedHeight);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public VideoRecordingCameraWindowShape WindowShape
	{
		get
		{
			return windowShape;
		}
		set
		{
			if (!EqualityComparer<VideoRecordingCameraWindowShape>.Default.Equals(windowShape, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.WindowShape);
				windowShape = value;
				OnWindowShapeChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.WindowShape);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SwitchWindowShapeCommand => switchWindowShapeCommand ?? (switchWindowShapeCommand = new RelayCommand(SwitchWindowShape));

	public VideoRecordingCameraWindowVM(SettingsManager settingsManager, Dispatcher dispatcher, Visual visual, string cameraDeviceId, int frameRate, LoggerFactory loggerFactory)
	{
		this.settingsManager = settingsManager;
		settings = settingsManager.Settings;
		this.dispatcher = dispatcher;
		this.visual = visual;
		CameraDeviceId = cameraDeviceId;
		this.frameRate = frameRate;
		this.loggerFactory = loggerFactory;
		logger = (ILogger)(((object)loggerFactory?.CreateLogger<VideoRecordingCameraWindowVM>()) ?? ((object)NullLogger.Instance));
		WindowShape = settings.CameraWindowShape;
		OnWindowShapeChanged(WindowShape);
	}

	[RelayCommand]
	private void SwitchWindowShape()
	{
		WindowShape = ((WindowShape != VideoRecordingCameraWindowShape.Circle) ? VideoRecordingCameraWindowShape.Circle : VideoRecordingCameraWindowShape.Tv);
	}

	public Rect CalculateWindowRect(Rect selectionRect)
	{
		Rect cameraWindowRelativeRect = settings.CameraWindowRelativeRect;
		double num = selectionRect.Width * cameraWindowRelativeRect.Width;
		double num2 = selectionRect.Height * cameraWindowRelativeRect.Height;
		int num3 = ((WindowShape != VideoRecordingCameraWindowShape.Circle) ? 1 : (-1));
		double num4 = num2 * WindowAspectRatio;
		if (num.CompareTo(num4) == num3)
		{
			num2 = num / WindowAspectRatio;
		}
		else
		{
			num = num4;
		}
		if (num < WindowMinWidth)
		{
			num2 *= WindowMinWidth / num;
			num = WindowMinWidth;
		}
		if (num2 < WindowMinHeight)
		{
			num *= WindowMinHeight / num2;
			num2 = WindowMinHeight;
		}
		if (num > selectionRect.Width)
		{
			num2 *= selectionRect.Width / num;
			num = selectionRect.Width;
		}
		if (num2 > selectionRect.Height)
		{
			num *= selectionRect.Height / num2;
			num2 = selectionRect.Height;
		}
		double num5 = selectionRect.X + selectionRect.Width * cameraWindowRelativeRect.X;
		if (cameraWindowRelativeRect.X > 0.1)
		{
			num5 = ((!(cameraWindowRelativeRect.X + cameraWindowRelativeRect.Width >= 0.9)) ? (num5 + (selectionRect.Width * cameraWindowRelativeRect.Width - num) / 2.0) : (num5 + (selectionRect.Width * cameraWindowRelativeRect.Width - num)));
		}
		double num6 = selectionRect.Y + selectionRect.Height * cameraWindowRelativeRect.Y;
		if (cameraWindowRelativeRect.Y > 0.1)
		{
			num6 = ((!(cameraWindowRelativeRect.Y + cameraWindowRelativeRect.Height >= 0.9)) ? (num6 + (selectionRect.Height * cameraWindowRelativeRect.Height - num2) / 2.0) : (num6 + (selectionRect.Height * cameraWindowRelativeRect.Height - num2)));
		}
		return new Rect(num5, num6, num, num2).MoveInsideRect(selectionRect);
	}

	public void SaveWindowRelativeRectToSettings(Rectangle windowRectangle)
	{
		double x = (double)(windowRectangle.X - SelectionRectangle.X) / (double)SelectionRectangle.Width;
		double y = (double)(windowRectangle.Y - SelectionRectangle.Y) / (double)SelectionRectangle.Height;
		double width = (double)windowRectangle.Width / (double)SelectionRectangle.Width;
		double height = (double)windowRectangle.Height / (double)SelectionRectangle.Height;
		settings.CameraWindowRelativeRect = new Rect(x, y, width, height);
		settingsManager.SaveSettings();
	}

	private void UpdateCameraCapturer()
	{
		if (VideoAreaObservedWidth == 0.0 || VideoAreaObservedHeight == 0.0)
		{
			return;
		}
		System.Drawing.Size size = visual.RectToScreen(new Rect(0.0, 0.0, VideoAreaObservedWidth, VideoAreaObservedHeight)).ToRectangle().Size;
		long newCameraCapturerInstanceId = 0L;
		bool reducedLogging = false;
		if (cameraCapturer != null)
		{
			if (!cameraCapturer.ShouldCurrentVideoMediaTypeBeChanged(size, frameRate))
			{
				return;
			}
			dispatcher.Invoke(() =>
			{
				cameraCapturerInstanceId++;
				newCameraCapturerInstanceId = cameraCapturerInstanceId;
				DisposeCameraCapturerAsync();
				WriteableBitmap = null;
				State = VideoRecordingCameraWindowState.Loading;
				reducedLogging = true;
				logger.LogInformation("Changing video format on the camera");
			});
		}
		cameraCapturer = new CameraCapturer(CameraDeviceId, size, frameRate, loggerFactory, reducedLogging);
		Task.Run(() => ProcessCameraFramesAsync(cameraCapturer, newCameraCapturerInstanceId));
	}

	private Task DisposeCameraCapturerAsync()
	{
		if (cameraCapturer != null && !cameraCapturer.IsDisposed)
		{
			return Task.Run((Action)cameraCapturer.Dispose);
		}
		return Task.CompletedTask;
	}

	private async Task ProcessCameraFramesAsync(CameraCapturer cameraCapturer, long cameraCapturerInstanceId)
	{
		try
		{
			while (await cameraCapturer.Frames.WaitToReadAsync().ConfigureAwait(continueOnCapturedContext: false))
			{
				if (!cameraCapturer.Frames.TryRead(out var item))
				{
					continue;
				}
				using (item)
				{
					using RawImage image = item.GetImage();
					DrawCameraFrame(image, cameraCapturerInstanceId);
				}
			}
		}
		catch (OperationCanceledException)
		{
		}
		catch (MediaFoundationException ex2) when (ex2.InnerException.HResult == (int)HRESULT.MF_E_VIDEO_RECORDING_DEVICE_INVALIDATED)
		{
			logger.LogWarning("Camera disconnected");
		}
		catch (MediaFoundationException ex3) when (ex3.InnerException.HResult == (int)HRESULT.MF_E_HW_MFT_FAILED_START_STREAMING)
		{
			logger.LogWarning("Camera is used by another app");
			State = VideoRecordingCameraWindowState.Error;
			ErrorMessage = Resources.Camera_UsedByAnotherApp;
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Error processing video frame from camera");
			State = VideoRecordingCameraWindowState.Error;
			ErrorMessage = Resources.Camera_GeneralError;
		}
	}

	private unsafe void DrawCameraFrame(RawImage image, long cameraCapturerInstanceId)
	{
		dispatcher.Invoke(() =>
		{
			if (cameraCapturerInstanceId != this.cameraCapturerInstanceId)
			{
				return;
			}
			State = VideoRecordingCameraWindowState.Streaming;
			using MemoryHandle memoryHandle = image.Data[0].Pin();
			if (WriteableBitmap == null)
			{
				WriteableBitmap writeableBitmap = (WriteableBitmap = new WriteableBitmap(image.Format.Width, image.Format.Height, 96.0, 96.0, PixelFormats.Bgr32, null));
			}
			WriteableBitmap.WritePixels(new Int32Rect(0, 0, image.Format.Width, image.Format.Height), (IntPtr)memoryHandle.Pointer, image.Data[0].Length, image.Stride[0], 0, 0);
		});
	}

	public virtual void Dispose()
	{
		DisposeCameraCapturerAsync();
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnCanUpdateCameraCapturerChanged(bool value)
	{
		if (CanUpdateCameraCapturer)
		{
			UpdateCameraCapturer();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnVideoAreaObservedWidthChanged(double value)
	{
		if (CanUpdateCameraCapturer)
		{
			UpdateCameraCapturer();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnVideoAreaObservedHeightChanged(double value)
	{
		if (CanUpdateCameraCapturer)
		{
			UpdateCameraCapturer();
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnWindowShapeChanged(VideoRecordingCameraWindowShape value)
	{
		switch (value)
		{
		case VideoRecordingCameraWindowShape.Circle:
			WindowAspectRatio = 1.0;
			WindowMinWidth = 130.0;
			WindowMinHeight = WindowMinWidth;
			break;
		case VideoRecordingCameraWindowShape.Tv:
			WindowAspectRatio = 1.153466;
			WindowMinWidth = 130.0;
			WindowMinHeight = WindowMinWidth / WindowAspectRatio;
			break;
		}
		if (CanUpdateCameraCapturer)
		{
			UpdateCameraCapturer();
		}
		if (settings.CameraWindowShape != value)
		{
			settings.CameraWindowShape = value;
			settingsManager.SaveSettings();
		}
	}
}
