using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace SkrinshoterApp;

public class LanguagesProvider
{
	public static IReadOnlyList<string> SupportedLanguages { get; } = DiscoverLanguages();

	public static string Default { get; } = GetDefaultLanguage();

	public string GetDisplayName(string lang)
	{
		CultureInfo cultureInfo = CultureInfo.GetCultureInfo(lang);
		string nativeName = cultureInfo.NativeName;
		return char.ToUpper(nativeName[0], cultureInfo) + nativeName.Substring(1);
	}

	private static IReadOnlyList<string> DiscoverLanguages()
	{
		if (AppInfoProvider.IsSkrinshoter)
		{
			return new _003C_003Ez__ReadOnlySingleElementList<string>("ru");
		}
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		string name = executingAssembly.GetName().Name;
		string directoryName = Path.GetDirectoryName(executingAssembly.Location);
		List<string> list = new List<string>();
		string text = executingAssembly.GetCustomAttribute<NeutralResourcesLanguageAttribute>()?.CultureName;
		if (!string.IsNullOrEmpty(text))
		{
			list.Add(text);
		}
		string[] directories = Directory.GetDirectories(directoryName);
		foreach (string obj in directories)
		{
			string fileName = Path.GetFileName(obj);
			if (File.Exists(Path.Combine(obj, name + ".resources.dll")))
			{
				CultureInfo cultureInfo;
				try
				{
					cultureInfo = CultureInfo.GetCultureInfo(fileName);
				}
				catch (CultureNotFoundException)
				{
					continue;
				}
				if (!(cultureInfo.Name == text))
				{
					list.Add(cultureInfo.Name);
				}
			}
		}
		LanguagesProvider provider = new LanguagesProvider();
		return list.OrderBy((string lang) => provider.GetDisplayName(lang), StringComparer.InvariantCulture).ToList().AsReadOnly();
	}

	private static string GetDefaultLanguage()
	{
		if (AppInfoProvider.IsSkrinshoter)
		{
			return "ru";
		}
		string twoLetterISOLanguageName = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		if (SupportedLanguages.Contains(twoLetterISOLanguageName))
		{
			return twoLetterISOLanguageName;
		}
		return "en";
	}
}
