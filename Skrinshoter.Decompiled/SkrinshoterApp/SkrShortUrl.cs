using System;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

internal class SkrShortUrl
{
	private struct Date
	{
		public int d;

		public int m;

		public int y;
	}

	private int[] monthDays = new int[12]
	{
		31, 28, 31, 30, 31, 30, 31, 31, 30, 31,
		30, 31
	};

	private char[] EncodingAlphabet = new char[62]
	{
		'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
		'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
		'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
		'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
		'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n',
		'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x',
		'y', 'z'
	};

	private string _fileUid;

	private CloudFileUploadType _fileType;

	private Settings _settings;

	public SkrShortUrl(string fileUid, CloudFileUploadType fileType, Settings settings)
	{
		_fileUid = fileUid;
		_fileType = fileType;
		_settings = settings;
	}

	public string Generate()
	{
		Date dt = new Date
		{
			d = 1,
			m = 1,
			y = 2020
		};
		Date dt2 = new Date
		{
			d = DateTime.Now.Day,
			m = DateTime.Now.Month,
			y = DateTime.Now.Year
		};
		_ = _fileUid;
		string text = ProductUrl.Resolve(ProductResources.BaseUrl_ShortLink);
		return text + "/" + GetServerFolderByFileType((int)_fileType) + EncodeDays(GetDifference(dt, dt2)) + _fileUid;
	}

	public string GetServerFolderByFileType(int fileType)
	{
		return fileType switch
		{
			0 => "s", 
			1 => "f", 
			2 => "v", 
			_ => throw new ArgumentException($"Invalid file type: {fileType}"), 
		};
	}

	private int CountLeapYears(Date d)
	{
		int num = d.y;
		if (d.m <= 2)
		{
			num--;
		}
		return num / 4 - num / 100 + num / 400;
	}

	private int GetDifference(Date dt1, Date dt2)
	{
		int num = dt1.y * 365 + dt1.d;
		for (int i = 0; i < dt1.m - 1; i++)
		{
			num += monthDays[i];
		}
		num += CountLeapYears(dt1);
		int num2 = dt2.y * 365 + dt2.d;
		for (int j = 0; j < dt2.m - 1; j++)
		{
			num2 += monthDays[j];
		}
		num2 += CountLeapYears(dt2);
		return num2 - num;
	}

	private string EncodeDays(int days)
	{
		int num = EncodingAlphabet.Length;
		int num2 = days / num;
		int num3 = days % num;
		return string.Concat("" + EncodingAlphabet[num2], EncodingAlphabet[num3].ToString());
	}
}
