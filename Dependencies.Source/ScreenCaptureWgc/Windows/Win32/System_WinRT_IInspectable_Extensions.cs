using System;
using System.CodeDom.Compiler;
using Windows.Win32.Foundation;
using Windows.Win32.System.WinRT;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class System_WinRT_IInspectable_Extensions
{
	internal unsafe static HRESULT GetIids(this IInspectable.Interface @this, out uint iidCount, out Guid* iids)
	{
		fixed (Guid** iids2 = &iids)
		{
			fixed (uint* iidCount2 = &iidCount)
			{
				return @this.GetIids(iidCount2, iids2);
			}
		}
	}

	internal unsafe static HRESULT GetRuntimeClassName(this IInspectable.Interface @this, out WindowsDeleteStringSafeHandle className)
	{
		HSTRING hSTRING = default;
		HRESULT runtimeClassName = @this.GetRuntimeClassName(&hSTRING);
		className = new WindowsDeleteStringSafeHandle(hSTRING);
		return runtimeClassName;
	}

	internal unsafe static HRESULT GetTrustLevel(this IInspectable.Interface @this, out TrustLevel trustLevel)
	{
		fixed (TrustLevel* trustLevel2 = &trustLevel)
		{
			return @this.GetTrustLevel(trustLevel2);
		}
	}
}
