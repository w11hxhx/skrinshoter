using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SkrinshoterApp;

internal class SkrFileUids
{
	private Settings _settings;

	private static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

	public SkrFileUids(Settings settings)
	{
		_settings = settings;
	}

	public async Task<string> PopNext()
	{
		await semaphore.WaitAsync();
		try
		{
			await UpdateAsync();
			List<string> fileUids = _settings.FileUids;
			if (fileUids.Count() > 0)
			{
				string text = fileUids[0];
				_settings.FileUids.Remove(text);
				return text;
			}
			throw new Exception("Failed to receive file UID from the server.");
		}
		finally
		{
			semaphore.Release();
		}
	}

	private async Task<bool> UpdateAsync()
	{
		bool result = false;
		try
		{
			List<string> fileUids = _settings.FileUids;
			if (fileUids.Count < 3)
			{
				List<string> list = fileUids;
				list.AddRange(await new SkrApiFileUids().GetFileUids());
				result = true;
			}
		}
		catch (Exception ex)
		{
			AppLogger.Logger.Error("SkrFileUids: failed to update: " + ex.Message);
			result = false;
		}
		return result;
	}

	internal async Task Drop(string uid)
	{
		await semaphore.WaitAsync();
		try
		{
			await UpdateAsync();
			_settings.FileUids.Remove(uid);
		}
		finally
		{
			semaphore.Release();
		}
	}
}
