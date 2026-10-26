using System;
using Microsoft.Extensions.DependencyInjection;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public sealed class FileNameTemplateWindowVMDesignFactory : DesignFactory<FileNameTemplateWindowVM>
{
	public override FileNameTemplateWindowVM Create(IServiceProvider sp)
	{
		FileNameTemplateWindowVM fileNameTemplateWindowVM = ActivatorUtilities.CreateInstance<FileNameTemplateWindowVM>(sp, Array.Empty<object>());
		fileNameTemplateWindowVM.TemplateText = Resources.Settings_Screenshot_FileNameTemplateDefaultValue;
		fileNameTemplateWindowVM.IsScreenshotTemplate = true;
		fileNameTemplateWindowVM.ErrorMessage = string.Format(Resources.Settings_FileNameTemplate_Error_InvalidCharacter, ':');
		return fileNameTemplateWindowVM;
	}
}
