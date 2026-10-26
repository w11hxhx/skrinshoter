using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;

namespace SkrinshoterApp;

public class VideoRecordingLowDiskSpaceWindowVM : LocalizableObservableObject
{
	[ObservableProperty]
	public string diskName;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string DiskName
	{
		get
		{
			return diskName;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(diskName, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.DiskName);
				diskName = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.DiskName);
			}
		}
	}

	public VideoRecordingLowDiskSpaceWindowVM(string diskName)
	{
		DiskName = diskName;
	}
}
