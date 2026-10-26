using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using CommunityToolkit.Mvvm.Input;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SkrinshoterApp.Commands;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class AppUpdateCheckWindowVM : LocalizableObservableObject, IDisposable
{
	private class VersionStringComparer
	{
		public int Compare(string version1, string version2)
		{
			return Version.Parse(version1).CompareTo(Version.Parse(version2));
		}
	}

	private readonly ILogger logger;

	private readonly Settings settings;

	private readonly IMediator mediator;

	private readonly IHttpClientFactory httpClientFactory;

	internal Task downloadUpdatesTask;

	internal readonly CancellationTokenSource cts = new CancellationTokenSource();

	private bool isDisposed;

	[ObservableProperty]
	private AppUpdateCheckWindowState state;

	[ObservableProperty]
	private string errorMessage;

	[ObservableProperty]
	private string flowDocument;

	[ObservableProperty]
	private string currentVersion;

	[ObservableProperty]
	private bool updatesAvailable;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand? downloadNewVersionCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private AsyncRelayCommand<Uri>? openUrlCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public AppUpdateCheckWindowState State
	{
		get
		{
			return state;
		}
		set
		{
			if (!EqualityComparer<AppUpdateCheckWindowState>.Default.Equals(state, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.State);
				state = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.State);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ErrorMessage
	{
		get
		{
			return errorMessage;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(errorMessage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ErrorMessage);
				errorMessage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ErrorMessage);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string FlowDocument
	{
		get
		{
			return flowDocument;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(flowDocument, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.FlowDocument);
				flowDocument = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.FlowDocument);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string CurrentVersion
	{
		get
		{
			return currentVersion;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(currentVersion, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.CurrentVersion);
				currentVersion = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.CurrentVersion);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool UpdatesAvailable
	{
		get
		{
			return updatesAvailable;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(updatesAvailable, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.UpdatesAvailable);
				updatesAvailable = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.UpdatesAvailable);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand DownloadNewVersionCommand => downloadNewVersionCommand ?? (downloadNewVersionCommand = new AsyncRelayCommand(DownloadNewVersion));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IAsyncRelayCommand<Uri> OpenUrlCommand => openUrlCommand ?? (openUrlCommand = new AsyncRelayCommand<Uri>(OpenUrl));

	public AppUpdateCheckWindowVM(Settings settings, AppInfoProvider appInfoProvider, IMediator mediator, IHttpClientFactory httpClientFactory, ILogger<AppUpdateCheckWindowVM> logger)
	{
		this.logger = (ILogger)(((object)logger) ?? ((object)NullLogger.Instance));
		this.settings = settings;
		this.mediator = mediator;
		this.httpClientFactory = httpClientFactory;
		CurrentVersion = appInfoProvider.Version.ToString();
		downloadUpdatesTask = DownloadUpdatesAsync(cts.Token);
	}

	[RelayCommand]
	private Task DownloadNewVersion()
	{
		string arg = DateTime.UtcNow.Ticks.ToString();
		return mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(string.Format(ProductUrl.Resolve(ProductResources.Url_DownloadNewVersion), arg)), default(CancellationToken));
	}

	[RelayCommand]
	private Task OpenUrl(Uri url)
	{
		return mediator.Send((IRequest<Unit>)new OpenUrlInBrowserCommand(url), default(CancellationToken));
	}

	private async Task DownloadUpdatesAsync(CancellationToken cancellationToken)
	{
		_ = 1;
		try
		{
			string text = DateTime.UtcNow.Ticks.ToString();
			using HttpClient httpClient = httpClientFactory.CreateClient("AppUpdateChecker");
			string text2 = (AppInfoProvider.IsFloomby ? ("." + settings.Language) : "");
			using HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "windows/version_history" + text2 + ".xml?rnd=" + text);
			using HttpResponseMessage response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			response.EnsureSuccessStatusCode();
			XDocument xDocument = XDocument.Load(await response.Content.ReadAsStreamAsync().ConfigureAwait(continueOnCapturedContext: false));
			SetFlowDocument(xDocument);
			State = AppUpdateCheckWindowState.Loaded;
		}
		catch (OperationCanceledException)
		{
		}
		catch (Exception exception)
		{
			logger.LogError(exception, "Error downloading app update list from server");
			ErrorMessage = Resources.Notify_FailedToDownloadDataFromServer;
			State = AppUpdateCheckWindowState.Error;
		}
	}

	internal void SetFlowDocument(XDocument updates)
	{
		XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(updates.CreateNavigator().NameTable);
		xmlNamespaceManager.AddNamespace("x", "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
		VersionStringComparer versionStringComparer = new VersionStringComparer();
		(from x in updates.XPathSelectElements("/x:FlowDocument/x:Section", xmlNamespaceManager)
			where versionStringComparer.Compare(x.XPathSelectElement(".//*[@Tag='VersionNumber']").Value, CurrentVersion) <= 0
			select x).Remove();
		UpdatesAvailable = updates.XPathSelectElement("/x:FlowDocument/x:Section", xmlNamespaceManager) != null;
		FlowDocument = updates.CreateNavigator().OuterXml;
	}

	public virtual void Dispose()
	{
		if (!isDisposed)
		{
			isDisposed = true;
			cts.Cancel();
			downloadUpdatesTask.ContinueWith((Task _) =>
			{
				cts.Dispose();
			});
		}
	}
}
