using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com.StructuredStorage;
using Windows.Win32.UI.Shell.PropertiesSystem;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class UI_Shell_PropertiesSystem_IPropertyStore_Extensions
{
	internal unsafe static HRESULT GetCount(this IPropertyStore.Interface @this, out uint cProps)
	{
		fixed (uint* cProps2 = &cProps)
		{
			return @this.GetCount(cProps2);
		}
	}

	internal unsafe static HRESULT GetAt(this IPropertyStore.Interface @this, uint iProp, out PROPERTYKEY pkey)
	{
		fixed (PROPERTYKEY* pkey2 = &pkey)
		{
			return @this.GetAt(iProp, pkey2);
		}
	}

	internal unsafe static HRESULT GetValue(this IPropertyStore.Interface @this, in PROPERTYKEY key, out PROPVARIANT pv)
	{
		fixed (PROPVARIANT* pv2 = &pv)
		{
			fixed (PROPERTYKEY* key2 = &key)
			{
				return @this.GetValue(key2, pv2);
			}
		}
	}

	internal unsafe static HRESULT SetValue(this IPropertyStore.Interface @this, in PROPERTYKEY key, in PROPVARIANT propvar)
	{
		fixed (PROPVARIANT* propvar2 = &propvar)
		{
			fixed (PROPERTYKEY* key2 = &key)
			{
				return @this.SetValue(key2, propvar2);
			}
		}
	}
}
