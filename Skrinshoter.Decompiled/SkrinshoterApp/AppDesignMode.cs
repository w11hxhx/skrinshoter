using System.ComponentModel;
using System.Windows;

namespace SkrinshoterApp;

internal static class AppDesignMode
{
	public static readonly bool Enabled;

	static AppDesignMode()
	{
		if (System.ComponentModel.LicenseManager.UsageMode == LicenseUsageMode.Designtime)
		{
			Enabled = true;
			return;
		}
		PropertyMetadata metadata = DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject));
		bool flag = default;
		int num;
		if (metadata != null)
		{
			object defaultValue = metadata.DefaultValue;
			if (defaultValue is bool)
			{
				flag = (bool)defaultValue;
				num = 1;
			}
			else
			{
				num = 0;
			}
		}
		else
		{
			num = 0;
		}
		Enabled = (byte)((uint)num & (flag ? 1u : 0u)) != 0;
	}
}
