using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp;

public class AppUpdateCheckWindowVM : LocalizableObservableObject, IDisposable
{
    private readonly ForkUpdateService updates;
    private readonly SettingsManager settings;
    private readonly AppShutdownService shutdown;
    private readonly IForkUpdateInstaller installer;
    private readonly IOperationStateProvider operations;
    private readonly ILogger logger;
    private ForkRelease release;
    private CancellationTokenSource downloadCancellation;
    internal readonly CancellationTokenSource cts = new CancellationTokenSource();
    internal Task downloadUpdatesTask = Task.CompletedTask;
    private bool disposed, busy;
    private string notes = "Проверяем релизы форка на GitHub…", status = "";
    private int progress;
    public string CurrentVersion { get; set; }
    public string LatestVersion => release?.Version.ToString() ?? "—";
    public string ReleaseNotes { get => notes; private set => SetProperty(ref notes, value); }
    public string Status { get => status; private set => SetProperty(ref status, value); }
    public int Progress { get => progress; private set => SetProperty(ref progress, value); }
    public bool Busy { get => busy; private set { SetProperty(ref busy, value); DownloadNewVersionCommand.NotifyCanExecuteChanged(); SkipVersionCommand.NotifyCanExecuteChanged(); } }
    public bool UpdatesAvailable => release != null && release.Version > Version.Parse(CurrentVersion);
    public bool CheckForUpdates
    {
        get => settings.Settings.CheckForUpdates;
        set { settings.Settings.CheckForUpdates = value; settings.SaveSettings(); OnPropertyChanged(); }
    }
    public AsyncRelayCommand DownloadNewVersionCommand { get; }
    public RelayCommand CancelDownloadCommand { get; }
    public RelayCommand SkipVersionCommand { get; }
    public event EventHandler RequestClose;

    public AppUpdateCheckWindowVM(AppInfoProvider info, ForkUpdateService updates, SettingsManager settings,
        AppShutdownService shutdown, IOperationStateProvider operations, IForkUpdateInstaller installer, ILogger<AppUpdateCheckWindowVM> logger)
    {
        CurrentVersion = info.Version.ToString();
        this.updates = updates;
        this.settings = settings;
        this.shutdown = shutdown;
        this.installer = installer;
        this.operations = operations;
        this.logger = logger;
        DownloadNewVersionCommand = new AsyncRelayCommand(DownloadNewVersionAsync, () => UpdatesAvailable && !Busy);
        CancelDownloadCommand = new RelayCommand(() => downloadCancellation?.Cancel());
        SkipVersionCommand = new RelayCommand(() => {
            settings.Settings.SkippedForkUpdateVersion = LatestVersion;
            settings.SaveSettings();
            RequestClose?.Invoke(this, EventArgs.Empty);
        }, () => UpdatesAvailable && !Busy);
    }

    public void Initialize(ForkRelease availableRelease = null) => downloadUpdatesTask = LoadAsync(availableRelease);

    private async Task LoadAsync(ForkRelease availableRelease)
    {
        try
        {
            release = availableRelease ?? await updates.GetLatestAsync(cts.Token);
            if (disposed) return;
            ReleaseNotes = UpdatesAvailable ? release.Notes : "Установлена последняя версия форка.\n\n" + release.Notes;
            Status = UpdatesAvailable ? "Обновление устанавливается только с вашего согласия." : "Новых обновлений форка нет.";
            OnPropertyChanged(nameof(LatestVersion));
            OnPropertyChanged(nameof(UpdatesAvailable));
            DownloadNewVersionCommand.NotifyCanExecuteChanged();
            SkipVersionCommand.NotifyCanExecuteChanged();
        }
        catch (OperationCanceledException) { }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to load fork releases");
            if (!disposed) { ReleaseNotes = "Не удалось проверить обновления форка."; Status = exception.Message; }
        }
    }

    private async Task DownloadNewVersionAsync()
    {
        if (operations.CurrentOperation != OperationType.None)
        {
            Status = "Завершите захват или запись экрана перед обновлением.";
            return;
        }
        Busy = true;
        Progress = 0;
        Status = "Скачиваем установщик форка. Загрузку можно отменить.";
        downloadCancellation = CancellationTokenSource.CreateLinkedTokenSource(cts.Token);
        string path = null;
        bool started = false;
        try
        {
            path = await updates.DownloadAsync(release, new Progress<int>(value => { if (!disposed) Progress = value; }), downloadCancellation.Token);
            downloadCancellation.Token.ThrowIfCancellationRequested();
            if (operations.CurrentOperation != OperationType.None)
            {
                Status = "Завершите захват или запись и повторите обновление.";
                return;
            }
            // This command is the user's explicit consent. The installer stays interactive
            // and offers its own Cancel button; there is no silent installation.
            installer.Start(path, AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar));
            started = true;
            await shutdown.ShutdownAsync();
        }
        catch (OperationCanceledException) { if (!disposed) Status = "Обновление отменено. Текущая версия сохранена."; }
        catch (Exception exception)
        {
            logger.LogError(exception, "Fork update failed");
            if (!disposed) Status = "Не удалось обновить форк: " + exception.Message;
        }
        finally
        {
            if (!started && path != null)
            {
                try { File.Delete(path); Directory.Delete(Path.GetDirectoryName(path)); } catch (IOException) { } catch (UnauthorizedAccessException) { }
            }
            downloadCancellation.Dispose();
            downloadCancellation = null;
            if (!disposed) Busy = false;
        }
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        cts.Cancel();
        // The active download owns its linked source until its finally block runs.
        downloadCancellation?.Cancel();
        downloadUpdatesTask.ContinueWith(_ => cts.Dispose(), TaskScheduler.Default);
    }
}
