using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class ProgressWindowVM : LocalizableObservableObject
{
	private readonly ILogger logger;

	private readonly CancellationTokenSource cts;

	[ObservableProperty]
	private string message;

	[ObservableProperty]
	private int progress;

	[ObservableProperty]
	private string cancelButtonText = Resources.ProgressWindow_CancelButtonText;

	[ObservableProperty]
	private bool isIndeterminate;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? cancelCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Message
	{
		get
		{
			return message;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(message, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Message);
				message = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Message);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public int Progress
	{
		get
		{
			return progress;
		}
		set
		{
			if (!EqualityComparer<int>.Default.Equals(progress, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Progress);
				progress = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Progress);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CancelButtonText
	{
		get
		{
			return cancelButtonText;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(cancelButtonText, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CancelButtonText);
				cancelButtonText = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CancelButtonText);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsIndeterminate
	{
		get
		{
			return isIndeterminate;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isIndeterminate, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsIndeterminate);
				isIndeterminate = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsIndeterminate);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(Cancel));

	public ProgressWindowVM(CancellationTokenSource cts, string message, ILogger<ProgressWindowVM> logger)
	{
		this.logger = logger;
		this.cts = cts;
		this.message = message;
	}

	[RelayCommand]
	private void Cancel()
	{
		logger.LogInformation("User clicked Cancel on progress window");
		cts.Cancel();
	}
}
