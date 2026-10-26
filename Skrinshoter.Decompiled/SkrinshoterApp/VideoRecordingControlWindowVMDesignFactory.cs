using System;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp;

public class VideoRecordingControlWindowVMDesignFactory : DesignFactory<VideoRecordingControlWindowVM>
{
	public override VideoRecordingControlWindowVM Create(IServiceProvider sp)
	{
		Dispatcher requiredService = sp.GetRequiredService<Dispatcher>();
		VideoRecordingControlWindowVM videoRecordingControlWindowVM = ActivatorUtilities.CreateInstance<VideoRecordingControlWindowVM>(sp, new object[3]
		{
			new AudioLevelMeter(requiredService),
			new AudioLevelMeter(requiredService),
			(Func<bool>)(() => false)
		});
		videoRecordingControlWindowVM.State = VideoRecordingControlWindowState.Selecting;
		videoRecordingControlWindowVM.Duration = TimeSpan.Parse("00:23:45");
		return videoRecordingControlWindowVM;
	}
}
