using System;
using System.IO;

namespace SkrinshoterApp.Common;

public class TempFile : IDisposable
{
	public string Path { get; }

	public TempFile(string fileExtension = ".tmp")
	{
		Path = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + fileExtension;
	}

	public void Dispose()
	{
		try
		{
			if (File.Exists(Path))
			{
				File.Delete(Path);
			}
		}
		catch
		{
		}
	}
}
