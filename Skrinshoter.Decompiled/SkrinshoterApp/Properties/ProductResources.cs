using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace SkrinshoterApp.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
public class ProductResources
{
	private static ResourceManager resourceMan;

	private static CultureInfo resourceCulture;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static ResourceManager ResourceManager
	{
		get
		{
			if (resourceMan == null)
			{
				resourceMan = new ResourceManager("SkrinshoterApp.Resources.Skrinshoter.ProductResources", typeof(ProductResources).Assembly);
			}
			return resourceMan;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public static CultureInfo Culture
	{
		get
		{
			return resourceCulture;
		}
		set
		{
			resourceCulture = value;
		}
	}

	public static string AppFolderName => ResourceManager.GetString("AppFolderName", resourceCulture);

	public static Icon AppIcon => (Icon)ResourceManager.GetObject("AppIcon", resourceCulture);

	public static string AppInvariantName => ResourceManager.GetString("AppInvariantName", resourceCulture);

	public static string AppMutexName => ResourceManager.GetString("AppMutexName", resourceCulture);

	public static string AppName => ResourceManager.GetString("AppName", resourceCulture);

	public static string AppNameGenitive => ResourceManager.GetString("AppNameGenitive", resourceCulture);

	public static string BaseUrl_Cdn => ResourceManager.GetString("BaseUrl_Cdn", resourceCulture);

	public static string BaseUrl_MainApi => ResourceManager.GetString("BaseUrl_MainApi", resourceCulture);

	public static string BaseUrl_ShortLink => ResourceManager.GetString("BaseUrl_ShortLink", resourceCulture);

	public static string BaseUrl_StatsApi => ResourceManager.GetString("BaseUrl_StatsApi", resourceCulture);

	public static string CustomerSupportUrl => ResourceManager.GetString("CustomerSupportUrl", resourceCulture);

	public static string ProductId => ResourceManager.GetString("ProductId", resourceCulture);

	public static string ScreenshotBaseFileName => ResourceManager.GetString("ScreenshotBaseFileName", resourceCulture);

	public static string Settings_About_SiteUrl => ResourceManager.GetString("Settings_About_SiteUrl", resourceCulture);

	public static byte[] ShutterClick => (byte[])ResourceManager.GetObject("ShutterClick", resourceCulture);

	public static Icon StopVideo => (Icon)ResourceManager.GetObject("StopVideo", resourceCulture);

	public static string TrialState_AppData_Suffix => ResourceManager.GetString("TrialState_AppData_Suffix", resourceCulture);

	public static string TrialState_CredentialManager_Suffix => ResourceManager.GetString("TrialState_CredentialManager_Suffix", resourceCulture);

	public static string TrialState_Registry_Suffix => ResourceManager.GetString("TrialState_Registry_Suffix", resourceCulture);

	public static string Url_DownloadNewVersion => ResourceManager.GetString("Url_DownloadNewVersion", resourceCulture);

	public static string VideoBaseFileName => ResourceManager.GetString("VideoBaseFileName", resourceCulture);

	internal ProductResources()
	{
	}
}
