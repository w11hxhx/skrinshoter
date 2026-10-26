using System;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp;

public sealed class ScrollingCaptureStartWindowVMDesignFactory : DesignFactory<ScrollingCaptureStartWindowVM>
{
	public override ScrollingCaptureStartWindowVM Create(IServiceProvider sp)
	{
		return ActivatorUtilities.CreateInstance<ScrollingCaptureStartWindowVM>(sp, new object[1]
		{
			new OverlayWindowStateManager()
		});
	}
}
