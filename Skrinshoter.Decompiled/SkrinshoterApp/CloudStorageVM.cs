using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using SkrinshoterApp.BackgroundServices;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class CloudStorageVM : LocalizableObservableObject
{
	internal long usedBytes;

	internal long totalBytes;

	[ObservableProperty]
	private bool isVisible;

	[ObservableProperty]
	private string usedSizeText;

	[ObservableProperty]
	private string totalSizeText;

	[ObservableProperty]
	private double usagePercent;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsVisible
	{
		get
		{
			return isVisible;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isVisible, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsVisible);
				isVisible = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsVisible);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string UsedSizeText
	{
		get
		{
			return usedSizeText;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(usedSizeText, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.UsedSizeText);
				usedSizeText = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.UsedSizeText);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string TotalSizeText
	{
		get
		{
			return totalSizeText;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(totalSizeText, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.TotalSizeText);
				totalSizeText = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.TotalSizeText);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public double UsagePercent
	{
		get
		{
			return usagePercent;
		}
		set
		{
			if (!EqualityComparer<double>.Default.Equals(usagePercent, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.UsagePercent);
				usagePercent = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.UsagePercent);
			}
		}
	}

	public CloudStorageVM(UserQuotaService userQuotaService)
	{
		userQuotaService.Updated += UserQuotaService_Updated;
		UpdateProperties();
	}

	private void UserQuotaService_Updated(object sender, UserQuotaUpdatedEventArgs e)
	{
		usedBytes = e.Quota.CloudStorageUsed;
		totalBytes = e.Quota.CloudStorageTotal;
		UpdateProperties();
	}

	internal void UpdateProperties()
	{
		IsVisible = totalBytes > 0;
		if (IsVisible)
		{
			UsedSizeText = FormatSize(usedBytes);
			TotalSizeText = FormatSize(totalBytes);
			UsagePercent = MathEx.Clamp((double)usedBytes / (double)totalBytes * 100.0, 0.0, 100.0);
		}
	}

	private static string FormatSize(long bytes)
	{
		if (bytes < 0)
		{
			bytes = 0L;
		}
		double num = bytes;
		string arg = Resources.Unit_B;
		if (num >= 1024.0)
		{
			num /= 1024.0;
			arg = Resources.Unit_KB;
		}
		if (num >= 1024.0)
		{
			num /= 1024.0;
			arg = Resources.Unit_MB;
		}
		if (num >= 1024.0)
		{
			num /= 1024.0;
			arg = Resources.Unit_GB;
		}
		if (num >= 1024.0)
		{
			num /= 1024.0;
			arg = Resources.Unit_TB;
		}
		return $"{num:0.#} {arg}";
	}
}
