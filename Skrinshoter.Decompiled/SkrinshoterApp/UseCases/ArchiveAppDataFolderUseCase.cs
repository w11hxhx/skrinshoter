using System;
using System.IO;
using System.IO.Compression;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp.UseCases;

internal class ArchiveAppDataFolderUseCase
{
	private readonly string zipFileName;

	private readonly string targetPath;

	public ArchiveAppDataFolderUseCase(string zipName, string targetPath)
	{
		zipFileName = zipName;
		this.targetPath = targetPath;
	}

	public string Execute()
	{
		try
		{
			string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ProductResources.AppFolderName);
			string text2 = Path.Combine(targetPath, zipFileName + ".zip");
			string text3 = Path.Combine(targetPath, zipFileName ?? "");
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
			if (Directory.Exists(text))
			{
				CopyDirectory(text, text3, copySubDirs: true);
				ZipFile.CreateFromDirectory(text3, text2);
				try
				{
					Directory.Delete(text3, recursive: true);
				}
				catch
				{
					AppLogger.Logger.Warn("Failed delete zip folder: " + text3);
				}
				return text2;
			}
			AppLogger.Logger.Warn("Failed to zip working folder");
			return null;
		}
		catch (Exception value)
		{
			AppLogger.Logger.Error(value);
			return null;
		}
	}

	public static void CopyDirectory(string sourceDir, string destinationDir, bool copySubDirs)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(sourceDir);
		if (!directoryInfo.Exists)
		{
			throw new DirectoryNotFoundException("Source directory does not exist or could not be found: " + sourceDir);
		}
		DirectoryInfo[] directories = directoryInfo.GetDirectories();
		Directory.CreateDirectory(destinationDir);
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			if (!fileInfo.Name.Contains(".tmp"))
			{
				string destFileName = Path.Combine(destinationDir, fileInfo.Name);
				try
				{
					fileInfo.CopyTo(destFileName, overwrite: false);
				}
				catch (Exception arg)
				{
					AppLogger.Logger.Error($"Copy logs: {arg}");
				}
			}
		}
		if (copySubDirs)
		{
			DirectoryInfo[] array = directories;
			foreach (DirectoryInfo directoryInfo2 in array)
			{
				string destinationDir2 = Path.Combine(destinationDir, directoryInfo2.Name);
				CopyDirectory(directoryInfo2.FullName, destinationDir2, copySubDirs);
			}
		}
	}
}
