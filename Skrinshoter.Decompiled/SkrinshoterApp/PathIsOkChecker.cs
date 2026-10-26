using System;
using System.IO;

namespace SkrinshoterApp;

internal class PathIsOkChecker
{
	private string path;

	public PathIsOkChecker(string newPath)
	{
		path = newPath;
	}

	internal bool Check()
	{
		try
		{
			using (File.Create(Path.Combine(path, Path.GetRandomFileName()), 1, FileOptions.DeleteOnClose))
			{
				return true;
			}
		}
		catch (Exception value)
		{
			AppLogger.Logger.Error(value);
		}
		return false;
	}
}
