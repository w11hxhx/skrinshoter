using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;

namespace SkrinshoterApp;

public class PrintDialogService
{
	private const int SharedMemorySize = 2097152;

	private readonly ILogger logger;

	private readonly string helperProcessPath;

	public string PrinterName { get; private set; }

	public PrintTicket PrintTicket { get; private set; }

	public PrintDialogService(ILogger<PrintDialogService> logger)
	{
		this.logger = logger;
		helperProcessPath = Path.Combine(AppContext.BaseDirectory, "PrintHelper.exe");
	}

	public bool Show(Window owner)
	{
		bool result = false;
		Window window = new Window
		{
			Owner = owner,
			ShowInTaskbar = false,
			WindowStyle = WindowStyle.None,
			ResizeMode = ResizeMode.NoResize,
			Width = 1.0,
			Height = 1.0,
			WindowStartupLocation = WindowStartupLocation.CenterOwner,
			Topmost = true,
			AllowsTransparency = true,
			Background = System.Windows.Media.Brushes.Transparent
		};
		window.Loaded += async (object s, RoutedEventArgs e) =>
		{
			bool oldTopmost = owner.Topmost;
			try
			{
				owner.Topmost = false;
				result = await ShowAndProcessResult(owner.GetMonitor().GetRectangle());
			}
			catch (Exception exception)
			{
				logger.LogError(exception, "Failed to show print dialog");
				result = false;
			}
			finally
			{
				owner.Topmost = oldTopmost;
				window.Close();
			}
		};
		window.ShowDialog();
		return result;
	}

	private async Task<bool> ShowAndProcessResult(Rectangle monitorRectangle)
	{
		string text = "Skrinshoter_" + Guid.NewGuid().ToString();
		using MemoryMappedFile memoryMappedFile = MemoryMappedFile.CreateNew(text, 2097152L);
		ProcessStartInfo processStartInfo = new ProcessStartInfo();
		processStartInfo.FileName = helperProcessPath;
		processStartInfo.Arguments = $"--sharedMemoryName \"{text}\" --x {monitorRectangle.X} --y {monitorRectangle.Y} --width {monitorRectangle.Width} --height {monitorRectangle.Height}";
		processStartInfo.UseShellExecute = false;
		processStartInfo.CreateNoWindow = true;
		ProcessStartInfo startInfo = processStartInfo;
		using (Process process = Process.Start(startInfo))
		{
			await Task.Run((Action)process.WaitForExit);
			if (process.ExitCode != 0)
			{
				logger.LogError("Process {processName} exited with code {exitCode}", Path.GetFileName(helperProcessPath), process.ExitCode);
				return false;
			}
		}
		PrintSelectionDto printSelectionDto = ReadDtoFromSharedMemory(memoryMappedFile);
		if (!string.IsNullOrEmpty(printSelectionDto.Error))
		{
			logger.LogError(printSelectionDto.Error);
			return false;
		}
		if (string.IsNullOrEmpty(printSelectionDto.PrinterName) || string.IsNullOrEmpty(printSelectionDto.PrintTicketXml))
		{
			return false;
		}
		PrinterName = printSelectionDto.PrinterName;
		using MemoryStream xmlStream = new MemoryStream(Encoding.UTF8.GetBytes(printSelectionDto.PrintTicketXml));
		PrintTicket = new PrintTicket(xmlStream);
		logger.LogInformation("Printer name: {printerName}", PrinterName);
		return true;
	}

	private static PrintSelectionDto ReadDtoFromSharedMemory(MemoryMappedFile memoryMappedFile)
	{
		using MemoryMappedViewStream memoryMappedViewStream = memoryMappedFile.CreateViewStream(0L, 0L, MemoryMappedFileAccess.Read);
		byte[] array = new byte[2097152];
		int num = memoryMappedViewStream.Read(array, 0, array.Length);
		if (num <= 0)
		{
			throw new InvalidOperationException("Error reading from shared memory object.");
		}
		int num2 = Array.IndexOf(array, (byte)0, 0, num);
		if (num2 <= 0)
		{
			new InvalidOperationException("No NUL terminator found in shared memory object.");
		}
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(PrintSelectionDto));
		using MemoryStream input = new MemoryStream(array, 0, num2, writable: false);
		using XmlReader xmlReader = XmlReader.Create(input);
		return (PrintSelectionDto)xmlSerializer.Deserialize(xmlReader);
	}
}
