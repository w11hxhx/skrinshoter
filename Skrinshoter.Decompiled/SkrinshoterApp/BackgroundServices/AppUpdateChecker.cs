using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SkrinshoterApp.Commands;

namespace SkrinshoterApp.BackgroundServices;

public sealed class AppUpdateChecker : IHostedService, IDisposable
{
    private readonly AppInfoProvider info;
    private readonly SettingsManager settings;
    private readonly ForkUpdateService updates;
    private readonly NotificationService notifications;
    private readonly IMediator mediator;
    private readonly Dispatcher dispatcher;
    private readonly IOperationStateProvider operations;
    private readonly ILogger logger;
    private CancellationTokenSource cancellation;
    private Task loop = Task.CompletedTask;
    private string offeredVersion;

    public AppUpdateChecker(AppInfoProvider info, SettingsManager settings, ForkUpdateService updates,
        NotificationService notifications, IMediator mediator, Dispatcher dispatcher,
        IOperationStateProvider operations, ILogger<AppUpdateChecker> logger)
    {
        this.info = info; this.settings = settings; this.updates = updates;
        this.notifications = notifications; this.mediator = mediator; this.dispatcher = dispatcher;
        this.operations = operations; this.logger = logger;
        settings.SettingsChanged += SettingsChanged;
    }

    private async void SettingsChanged(object sender, SettingsChangedEventArgs e)
    {
        if (e.Old.CheckForUpdates == e.New.CheckForUpdates) return;
        if (e.New.CheckForUpdates) await StartAsync(CancellationToken.None);
        else await StopAsync(CancellationToken.None);
    }

    public Task StartAsync(CancellationToken token)
    {
        if (cancellation != null || !dispatcher.InvokeIfRequired(() => settings.Settings.CheckForUpdates)) return Task.CompletedTask;
        cancellation = new CancellationTokenSource();
        loop = RunAsync(cancellation.Token);
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken token)
    {
        var old = cancellation;
        if (old == null) return;
        cancellation = null;
        var pending = loop;
        old.Cancel();
        try { await pending.ConfigureAwait(false); } finally { old.Dispose(); }
    }

    private async Task RunAsync(CancellationToken token)
    {
        try
        {
            await Task.Delay(TimeSpan.FromSeconds(15), token).ConfigureAwait(false);
            while (!token.IsCancellationRequested)
            {
                try
                {
                    var release = await updates.GetLatestAsync(token).ConfigureAwait(false);
                    // All Settings access stays on the UI thread. No download or installer
                    // is started by this background checker, only a dismissible offer.
                    await dispatcher.InvokeAsync(async () => {
                        token.ThrowIfCancellationRequested();
                        var current = settings.Settings;
                        if (!current.CheckForUpdates || !release.ShouldOffer(info.Version, current.SkippedForkUpdateVersion) ||
                            offeredVersion == release.Version.ToString()) return;
                        offeredVersion = release.Version.ToString();
                        var command = new ShowAppUpdateCheckWindowCommand { Release = release };
                        if (!current.SilentMode && operations.CurrentOperation == OperationType.None)
                            await mediator.Send(command, token);
                        else
                            notifications.ShowMessage("Доступно обновление форка " + release.Version + ". Нажмите, чтобы посмотреть изменения.",
                                options => options.WithCommand(command).WithRespectSilentMode(true));
                    }).Task.Unwrap().ConfigureAwait(false);
                }
                catch (OperationCanceledException) when (token.IsCancellationRequested) { throw; }
                catch (Exception exception) { logger.LogWarning(exception, "Failed to check fork releases on GitHub"); }
                await Task.Delay(TimeSpan.FromHours(6), token).ConfigureAwait(false);
            }
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested) { }
    }

    public void Dispose()
    {
        settings.SettingsChanged -= SettingsChanged;
        cancellation?.Cancel();
        cancellation?.Dispose();
    }
}
