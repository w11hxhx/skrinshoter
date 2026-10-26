using System;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace SkrinshoterApp.Services.Progress;

public class ProgressWindowService : IProgressWindowService
{
	private readonly SettingsWindow settingsWindow;

	private readonly Dispatcher dispatcher;

	private readonly IServiceProvider serviceProvider;

	public ProgressWindowService(SettingsWindow settingsWindow, Dispatcher dispatcher, IServiceProvider serviceProvider)
	{
		this.settingsWindow = settingsWindow;
		this.dispatcher = dispatcher;
		this.serviceProvider = serviceProvider;
	}

	public IProgressSession TryShow(string message, bool showUi, Window owner = null, CancellationToken externalToken = default(CancellationToken))
	{
		if (!showUi)
		{
			return new NullProgressSession(externalToken);
		}
		return dispatcher.InvokeIfRequired(() => CreateSession(message, externalToken, (owner != null) ? ((Action<Window>)((Window w) =>
		{
			w.Owner = owner;
		})) : null));
	}

	public IProgressSession TryShow(string message, bool showUi, IntPtr ownerHwnd, CancellationToken externalToken = default(CancellationToken))
	{
		if (!showUi)
		{
			return new NullProgressSession(externalToken);
		}
		return dispatcher.InvokeIfRequired(() => CreateSession(message, externalToken, (ownerHwnd != IntPtr.Zero) ? ((Action<Window>)((Window w) =>
		{
			new WindowInteropHelper(w).Owner = ownerHwnd;
		})) : null));
	}

	private IProgressSession CreateSession(string message, CancellationToken externalToken, Action<Window> applyOwner)
	{
		CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(externalToken);
		ProgressWindowVM progressWindowVM = ActivatorUtilities.CreateInstance<ProgressWindowVM>(serviceProvider, new object[2] { cancellationTokenSource, message });
		ProgressWindow requiredService = serviceProvider.GetRequiredService<ProgressWindow>();
		requiredService.DataContext = progressWindowVM;
		if (applyOwner != null)
		{
			applyOwner(requiredService);
		}
		else
		{
			requiredService.Owner = settingsWindow;
		}
		requiredService.Show();
		return new RealProgressSession(requiredService, progressWindowVM, cancellationTokenSource);
	}
}
