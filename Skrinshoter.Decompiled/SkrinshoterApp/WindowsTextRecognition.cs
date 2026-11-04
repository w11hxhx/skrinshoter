using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SkrinshoterApp;

public static class WindowsTextRecognition
{
    // Windows PowerShell supplies the WinRT projection on .NET Framework 4.8.
    private const string Script = @"
$ErrorActionPreference = 'Stop'
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
            if ($null -eq $engine) { throw 'Install an OCR language in Windows language settings.' }
            $result = Await ($engine.RecognizeAsync($bitmap)) ([Windows.Media.Ocr.OcrResult])
            $text = ($result.Lines | ForEach-Object { $_.Text }) -join [Environment]::NewLine
            [Console]::Write([Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes($text)))
        } finally { $bitmap.Dispose() }
    } finally { $stream.Dispose() }
} catch { [Console]::Error.Write($_.Exception.Message); exit 1 }
";

    public static Task<string> RecognizeAsync(string path) => Task.Run(() =>
    {
        var start = new ProcessStartInfo
        {
            FileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"WindowsPowerShell\v1.0\powershell.exe"),
            Arguments = "-NoProfile -NonInteractive -EncodedCommand " + Convert.ToBase64String(Encoding.Unicode.GetBytes(Script)),
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true
        };
        start.EnvironmentVariables["SKR_OCR_IMAGE"] = path;
        using var process = Process.Start(start);
        var output = process.StandardOutput.ReadToEndAsync();
        var error = process.StandardError.ReadToEndAsync();
        if (!process.WaitForExit(60000))
        {
            process.Kill();
            throw new TimeoutException("Распознавание текста заняло слишком много времени.");
        }
        if (process.ExitCode != 0) throw new InvalidOperationException(error.GetAwaiter().GetResult());
        return Encoding.UTF8.GetString(Convert.FromBase64String(output.GetAwaiter().GetResult().Trim()));
    });
}
