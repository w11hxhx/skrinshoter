using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com.StructuredStorage;

namespace Windows.Win32.UI.Shell.PropertiesSystem;

[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IPropertyStore
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, uint*, HRESULT> GetCount_4;

		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, uint, PROPERTYKEY*, HRESULT> GetAt_5;

		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, PROPERTYKEY*, PROPVARIANT*, HRESULT> GetValue_6;

		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, PROPERTYKEY*, PROPVARIANT*, HRESULT> SetValue_7;

		internal unsafe delegate* unmanaged[Stdcall]<IPropertyStore*, HRESULT> Commit_8;
	}

	[ComImport]
	[Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetCount(uint* cProps);

		[PreserveSig]
		unsafe HRESULT GetAt(uint iProp, PROPERTYKEY* pkey);

		[PreserveSig]
		unsafe HRESULT GetValue(PROPERTYKEY* key, PROPVARIANT* pv);

		[PreserveSig]
		unsafe HRESULT SetValue(PROPERTYKEY* key, PROPVARIANT* propvar);

		[PreserveSig]
		HRESULT Commit();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2288881387u, 36082, 17478, 141, 2, 205, 186, 29, 189, 207, 153);

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT QueryInterface(in Guid riid, out void* ppvObject)
	{
		fixed (void** ppvObject2 = &ppvObject)
		{
			fixed (Guid* riid2 = &riid)
			{
				return QueryInterface(riid2, ppvObject2);
			}
		}
	}

	public unsafe HRESULT QueryInterface(Guid* riid, void** ppvObject)
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, Guid*, void**, HRESULT>)(*lpVtbl))((IPropertyStore*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, uint>)lpVtbl[1])((IPropertyStore*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, uint>)lpVtbl[2])((IPropertyStore*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetCount(out uint cProps)
	{
		fixed (uint* cProps2 = &cProps)
		{
			return GetCount(cProps2);
		}
	}

	public unsafe HRESULT GetCount(uint* cProps)
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, uint*, HRESULT>)lpVtbl[3])((IPropertyStore*)Unsafe.AsPointer(ref this), cProps);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetAt(uint iProp, out PROPERTYKEY pkey)
	{
		fixed (PROPERTYKEY* pkey2 = &pkey)
		{
			return GetAt(iProp, pkey2);
		}
	}

	public unsafe HRESULT GetAt(uint iProp, PROPERTYKEY* pkey)
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, uint, PROPERTYKEY*, HRESULT>)lpVtbl[4])((IPropertyStore*)Unsafe.AsPointer(ref this), iProp, pkey);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetValue(in PROPERTYKEY key, out PROPVARIANT pv)
	{
		fixed (PROPVARIANT* pv2 = &pv)
		{
			fixed (PROPERTYKEY* key2 = &key)
			{
				return GetValue(key2, pv2);
			}
		}
	}

	public unsafe HRESULT GetValue(PROPERTYKEY* key, PROPVARIANT* pv)
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, PROPERTYKEY*, PROPVARIANT*, HRESULT>)lpVtbl[5])((IPropertyStore*)Unsafe.AsPointer(ref this), key, pv);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetValue(in PROPERTYKEY key, in PROPVARIANT propvar)
	{
		fixed (PROPVARIANT* propvar2 = &propvar)
		{
			fixed (PROPERTYKEY* key2 = &key)
			{
				return SetValue(key2, propvar2);
			}
		}
	}

	public unsafe HRESULT SetValue(PROPERTYKEY* key, PROPVARIANT* propvar)
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, PROPERTYKEY*, PROPVARIANT*, HRESULT>)lpVtbl[6])((IPropertyStore*)Unsafe.AsPointer(ref this), key, propvar);
	}

	public unsafe HRESULT Commit()
	{
		return ((delegate* unmanaged[Stdcall]<IPropertyStore*, HRESULT>)lpVtbl[7])((IPropertyStore*)Unsafe.AsPointer(ref this));
	}

	internal unsafe HRESULT QueryInterface<T>(out T* ppv) where T : unmanaged
	{
		Guid gUID = typeof(T).GUID;
		void* ptr = default;
		HRESULT result = QueryInterface(&gUID, &ptr);
		if (result.Succeeded)
		{
			ppv = (T*)ptr;
		}
		else
		{
			ppv = null;
		}
		return result;
	}
}
