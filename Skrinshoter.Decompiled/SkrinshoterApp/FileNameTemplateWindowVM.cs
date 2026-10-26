using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class FileNameTemplateWindowVM : LocalizableObservableObject
{
	private readonly DateTime Now = DateTime.Now;

	[ObservableProperty]
	private bool isScreenshotTemplate;

	[ObservableProperty]
	private FileNameTemplate template;

	[ObservableProperty]
	private string templateText;

	[ObservableProperty]
	private string preview;

	[ObservableProperty]
	private bool isTemplateValid;

	[ObservableProperty]
	private string errorMessage;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsScreenshotTemplate
	{
		get
		{
			return isScreenshotTemplate;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isScreenshotTemplate, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsScreenshotTemplate);
				isScreenshotTemplate = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsScreenshotTemplate);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public FileNameTemplate Template
	{
		get
		{
			return template;
		}
		set
		{
			if (!EqualityComparer<FileNameTemplate>.Default.Equals(template, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Template);
				template = value;
				OnTemplateChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Template);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string TemplateText
	{
		get
		{
			return templateText;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(templateText, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.TemplateText);
				templateText = value;
				OnTemplateTextChanged(value);
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.TemplateText);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string Preview
	{
		get
		{
			return preview;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(preview, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.Preview);
				preview = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.Preview);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public bool IsTemplateValid
	{
		get
		{
			return isTemplateValid;
		}
		set
		{
			if (!EqualityComparer<bool>.Default.Equals(isTemplateValid, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.IsTemplateValid);
				isTemplateValid = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.IsTemplateValid);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public string ErrorMessage
	{
		get
		{
			return errorMessage;
		}
		set
		{
			if (!EqualityComparer<string>.Default.Equals(errorMessage, value))
			{
				OnPropertyChanging(__KnownINotifyPropertyChangingArgs.ErrorMessage);
				errorMessage = value;
				OnPropertyChanged(__KnownINotifyPropertyChangedArgs.ErrorMessage);
			}
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnTemplateChanged(FileNameTemplate value)
	{
		IsTemplateValid = value != null;
		if (IsTemplateValid)
		{
			TemplateText = value.Text;
			Preview = value.Format(new FileNameTemplateArgs(Now, Resources.Settings_FileNameTemplate_Param_WindowTitle_PreviewValue), 1);
		}
	}

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator", "8.4.0.0")]
	private void OnTemplateTextChanged(string value)
	{
		if (Template?.Text != value)
		{
			try
			{
				Template = new FileNameTemplate(value);
				ErrorMessage = null;
			}
			catch (FileNameTemplateEmptyException)
			{
				Template = null;
				ErrorMessage = Resources.Settings_FileNameTemplate_Error_Empty;
			}
			catch (FileNameTemplateInvalidCharacterException ex2)
			{
				Template = null;
				ErrorMessage = string.Format(Resources.Settings_FileNameTemplate_Error_InvalidCharacter, ex2.InvalidCharacter);
			}
			catch
			{
				Template = null;
				ErrorMessage = Resources.Settings_FileNameTemplate_Error_General;
			}
		}
	}
}
