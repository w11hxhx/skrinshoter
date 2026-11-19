using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace SkrinshoterApp;

public static class WindowsTextRecognition
{
    // Windows PowerShell supplies the WinRT projection on .NET Framework 4.8.
    private const string Script = @"
$ErrorActionPreference = 'Stop'
$ProgressPreference = 'SilentlyContinue'
[Console]::OutputEncoding = New-Object Text.UTF8Encoding($false)
try {
    Add-Type -AssemblyName System.Runtime.WindowsRuntime
    $null = [Windows.Storage.StorageFile, Windows.Storage, ContentType=WindowsRuntime]
    $null = [Windows.Graphics.Imaging.BitmapDecoder, Windows.Graphics.Imaging, ContentType=WindowsRuntime]
    $null = [Windows.Media.Ocr.OcrEngine, Windows.Foundation, ContentType=WindowsRuntime]
    $asTask = [System.WindowsRuntimeSystemExtensions].GetMethods() | Where-Object { $_.Name -eq 'AsTask' -and $_.IsGenericMethod -and $_.GetParameters().Count -eq 1 -and $_.GetParameters()[0].ParameterType.Name -eq 'IAsyncOperation`1' } | Select-Object -First 1
    function Await($operation, [Type]$type) {
        $task = $asTask.MakeGenericMethod($type).Invoke($null, @($operation))
        $task.GetAwaiter().GetResult()
    }
    $file = Await ([Windows.Storage.StorageFile]::GetFileFromPathAsync($env:SKR_OCR_IMAGE)) ([Windows.Storage.StorageFile])
    $stream = Await ($file.OpenAsync([Windows.Storage.FileAccessMode]::Read)) ([Windows.Storage.Streams.IRandomAccessStream])
    try {
        $decoder = Await ([Windows.Graphics.Imaging.BitmapDecoder]::CreateAsync($stream)) ([Windows.Graphics.Imaging.BitmapDecoder])
        $transform = New-Object Windows.Graphics.Imaging.BitmapTransform
        $limit = [Windows.Media.Ocr.OcrEngine]::MaxImageDimension
        $scale = [Math]::Min(1.0, $limit / [double][Math]::Max($decoder.PixelWidth, $decoder.PixelHeight))
        $transform.ScaledWidth = [uint32][Math]::Max(1, [Math]::Floor($decoder.PixelWidth * $scale))
        $transform.ScaledHeight = [uint32][Math]::Max(1, [Math]::Floor($decoder.PixelHeight * $scale))
        $bitmap = Await ($decoder.GetSoftwareBitmapAsync([Windows.Graphics.Imaging.BitmapPixelFormat]::Bgra8, [Windows.Graphics.Imaging.BitmapAlphaMode]::Premultiplied, $transform, [Windows.Graphics.Imaging.ExifOrientationMode]::IgnoreExifOrientation, [Windows.Graphics.Imaging.ColorManagementMode]::DoNotColorManage)) ([Windows.Graphics.Imaging.SoftwareBitmap])
        try {
            $engine = [Windows.Media.Ocr.OcrEngine]::TryCreateFromUserProfileLanguages()
            if ($null -eq $engine) {
                # A language pack may be installed without being in the user's preferred languages.
                foreach ($language in [Windows.Media.Ocr.OcrEngine]::AvailableRecognizerLanguages) {
                    $engine = [Windows.Media.Ocr.OcrEngine]::TryCreateFromLanguage($language)
                    if ($null -ne $engine) { break }
                }
            }
            if ($null -eq $engine) { throw 'Install an OCR language in Windows Settings > Time & language > Language & region > Language options.' }
            $result = Await ($engine.RecognizeAsync($bitmap)) ([Windows.Media.Ocr.OcrResult])
            $text = ($result.Lines | ForEach-Object { $_.Text }) -join [Environment]::NewLine
            [Console]::Write([Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes($text)))
        } finally { $bitmap.Dispose() }
    } finally { $stream.Dispose() }
} catch { [Console]::Error.Write($_.Exception.Message); exit 1 }
";

    public static Task<string> RecognizeAsync(string path, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentException("Укажите файл изображения.", nameof(path));
        string fullPath = Path.GetFullPath(path);
        if (!File.Exists(fullPath)) throw new FileNotFoundException("Изображение для распознавания не найдено.", fullPath);
        return RecognizeCoreAsync(fullPath, cancellationToken);
    }

    private static async Task<string> RecognizeCoreAsync(string path, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var start = new ProcessStartInfo
        {
            FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"WindowsPowerShell\v1.0\powershell.exe"),
            Arguments = "-NoLogo -NoProfile -NonInteractive -STA -EncodedCommand " + Convert.ToBase64String(Encoding.Unicode.GetBytes(Script)),
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8
        };
        start.EnvironmentVariables["SKR_OCR_IMAGE"] = path;
        using var process = new Process { StartInfo = start, EnableRaisingEvents = true };
        var exited = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        process.Exited += (_, __) => exited.TrySetResult(true);
        process.Start();
        var output = process.StandardOutput.ReadToEndAsync();
        var error = process.StandardError.ReadToEndAsync();
        using var timeout = new CancellationTokenSource(TimeSpan.FromSeconds(60));
        using var linked = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeout.Token);
        using (linked.Token.Register(() =>
        {
            try { if (!process.HasExited) process.Kill(); }
            catch (InvalidOperationException) { } // The process exited between checking and killing it.
        }))
        {
            await exited.Task.ConfigureAwait(false);
            string result = await output.ConfigureAwait(false);
            string diagnostic = await error.ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            if (timeout.IsCancellationRequested)
                throw new TimeoutException("Распознавание текста заняло слишком много времени.");
            if (process.ExitCode != 0)
                throw new InvalidOperationException(string.IsNullOrWhiteSpace(diagnostic)
                    ? "Не удалось запустить распознавание текста Windows." : diagnostic.Trim());
            return Encoding.UTF8.GetString(Convert.FromBase64String(result.Trim()));
        }
    }
}
