using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace SkrinshoterApp;

public class ScreenshotSaveFileDialog
{
	private readonly Settings settings;

	private readonly ScreenshotFileFormatsProvider screenshotFileFormatsProvider;

	public ScreenshotSaveFileDialog(Settings settings, ScreenshotFileFormatsProvider screenshotFileFormatsProvider)
	{
		this.settings = settings;
		this.screenshotFileFormatsProvider = screenshotFileFormatsProvider;
	}

	public string Show(string baseFileName)
	{
		if (baseFileName == null)
		{
			throw new ArgumentNullException("baseFileName");
		}
		string text = "";
		int num = 1;
		int filterIndex = 1;
		IReadOnlyList<ScreenshotFileFormat> supportedFormats = ScreenshotFileFormatsProvider.SupportedFormats;
		List<string> list = supportedFormats.Select((ScreenshotFileFormat x) => screenshotFileFormatsProvider.GetFileExtension(x)).ToList();
		foreach (ScreenshotFileFormat item in supportedFormats)
		{
			if (text != "")
			{
				text += "|";
			}
			text = text + screenshotFileFormatsProvider.GetDisplayName(item) + "|*" + screenshotFileFormatsProvider.GetFileExtension(item);
			if (item == settings.ScreenshotFileFormat)
			{
				filterIndex = num;
			}
			num++;
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog
		{
			Filter = text,
			FilterIndex = filterIndex,
			FileName = baseFileName
		};
		if (saveFileDialog.ShowDialog() != true)
		{
			return null;
		}
		string fileName = saveFileDialog.FileName;
		if (list.Contains(Path.GetExtension(fileName).ToLowerInvariant()))
		{
			return fileName;
		}
		string extension = list[saveFileDialog.FilterIndex - 1];
		return Path.ChangeExtension(fileName, extension);
	}
}
