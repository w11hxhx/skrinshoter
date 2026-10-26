using System;

namespace SkrinshoterApp;

public class VideoRecordingLowDiskSpaceWindowVMDesignFactory : DesignFactory<VideoRecordingLowDiskSpaceWindowVM>
{
	public override VideoRecordingLowDiskSpaceWindowVM Create(IServiceProvider sp)
	{
		return new VideoRecordingLowDiskSpaceWindowVM("C:\\");
	}
}
