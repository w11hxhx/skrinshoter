using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public class BlurObjectVM : RectangleObjectVM
{
	private readonly BlurredImageProvider blurredImageProvider;

	[ObservableProperty]
	private BitmapSource blurredImage;

	[ObservableProperty]
	private Rect scaledRect;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public BitmapSource BlurredImage
	{
		get
		{
			return blurredImage;
		}
		set
		{
			if (!EqualityComparer<BitmapSource>.Default.Equals(blurredImage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.BlurredImage);
				blurredImage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.BlurredImage);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public Rect ScaledRect
	{
		get
		{
			return scaledRect;
		}
		set
		{
			if (!EqualityComparer<Rect>.Default.Equals(scaledRect, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ScaledRect);
				scaledRect = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ScaledRect);
			}
		}
	}

	public BlurObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color, BlurredImageProvider blurredImageProvider)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
		this.blurredImageProvider = blurredImageProvider;
		FloatingCursor = Cursors.Cross;
		PropertyChanged += OnPropertyChanged;
		SetBlurredImageAsync(overlayWindowVM);
	}

	private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "Rect")
		{
			double monitorScaleFactor = OverlayWindowVM.MonitorScaleFactor;
			ScaledRect = new Rect(Rect.X * monitorScaleFactor, Rect.Y * monitorScaleFactor, Rect.Width * monitorScaleFactor, Rect.Height * monitorScaleFactor);
		}
	}

	public override int GetInsertZIndex()
	{
		return OverlayWindowVM.VisualObjects.TakeWhile((VisualObjectVM x) => x is BlurObjectVM).Count();
	}

	private async Task SetBlurredImageAsync(OverlayWindowVM overlayWindowVM)
	{
		BlurredImage = await blurredImageProvider.GetBlurredImageAsync(overlayWindowVM);
	}
}
