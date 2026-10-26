using System;
using System.Windows.Media;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp;

public class VideoRecordingCameraWindowVMDesignFactory : DesignFactory<VideoRecordingCameraWindowVM>
{
	public override VideoRecordingCameraWindowVM Create(IServiceProvider sp)
	{
		return ActivatorUtilities.CreateInstance<VideoRecordingCameraWindowVM>(sp, new object[3]
		{
			new DrawingVisual(),
			"",
			30
		});
	}
}
