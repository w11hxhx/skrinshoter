using System;

namespace SkrinshoterApp;

public class ScreenshotEditingDrawingWindowVMDesignFactory : DesignFactory<ScreenshotEditingDrawingWindowVM>
{
	public override ScreenshotEditingDrawingWindowVM Create(IServiceProvider sp)
	{
		return new ScreenshotEditingDrawingWindowVM(new OverlayWindowStateManager(), new UndoManager());
	}
}
