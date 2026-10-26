using System.IO;

namespace SkrinshoterApp.Common;

public static class FileHelpers
{
	public static long GetFileSize(string filePath)
	{
		try
		{
			return new FileInfo(filePath).Length;
		}
		catch
		{
			return -1L;
		}
	}

	public static void DeleteFile(string filePath)
	{
		try
		{
			if (File.Exists(filePath))
			{
				File.Delete(filePath);
			}
		}
		catch
		{
		}
	}
}
