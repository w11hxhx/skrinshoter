using System.Drawing;
using System.Reflection;
using System.Windows;
using AudioVideoCore;

namespace SkrinshoterApp;

public class WpfWarmer
{
	public WpfWarmer()
	{
		Window window = new Window();
		window.Width = 1.0;
		window.Height = 1.0;
		window.Left = 1000000.0;
		window.Top = 1000000.0;
		window.ShowInTaskbar = false;
		window.ShowActivated = false;
		window.Show();
		window.Hide();
		using (new SharedMemoryRawImage(new ImageFormat(new Rectangle(0, 0, 1, 1).Size, PixelFormat.BGRA)))
		{
			OverlayWindow overlayWindow = new OverlayWindow(createForVideo: false)
			{
				Width = 1.0,
				Height = 1.0,
				Left = 1000000.0
			};
			OverlayWindowVM dataContext = new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), overlayWindow, 1.0);
			overlayWindow.DataContext = dataContext;
			overlayWindow.Show();
			overlayWindow.Hide();
			overlayWindow.ForceClose();
			ScreenshotEditingDrawingWindow screenshotEditingDrawingWindow = new ScreenshotEditingDrawingWindow();
			screenshotEditingDrawingWindow.Left = 1000000.0;
			ScreenshotEditingDrawingWindowVM dataContext2 = new ScreenshotEditingDrawingWindowVM(new OverlayWindowStateManager(), new UndoManager());
			screenshotEditingDrawingWindow.DataContext = dataContext2;
			screenshotEditingDrawingWindow.Show();
			screenshotEditingDrawingWindow.Hide();
			screenshotEditingDrawingWindow.ForceClose();
			ScreenshotEditingActionWindow screenshotEditingActionWindow = new ScreenshotEditingActionWindow();
			screenshotEditingActionWindow.Left = 1000000.0;
			ScreenshotEditingActionWindowVM dataContext3 = new ScreenshotEditingActionWindowVM();
			screenshotEditingActionWindow.DataContext = dataContext3;
			screenshotEditingActionWindow.Show();
			screenshotEditingActionWindow.ForceClose();
			Assembly.Load("ScreenRecorderCommon");
			Assembly.Load("System.Numerics.Vectors");
			Assembly.Load("Polly.Extensions.Http");
		}
	}
}
