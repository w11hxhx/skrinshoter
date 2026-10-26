using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFCollection
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, uint*, HRESULT> GetElementCount_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, uint, IUnknown**, HRESULT> GetElement_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, IUnknown*, HRESULT> AddElement_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, uint, IUnknown**, HRESULT> RemoveElement_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, uint, IUnknown*, HRESULT> InsertElementAt_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMFCollection*, HRESULT> RemoveAllElements_9;
	}

	[ComImport]
	[Guid("5BC8A76B-869A-46A3-9B03-FA218A66AEBE")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetElementCount(uint* pcElements);

		[PreserveSig]
		unsafe HRESULT GetElement(uint dwElementIndex, IUnknown** ppUnkElement);

		[PreserveSig]
		unsafe HRESULT AddElement(IUnknown* pUnkElement);

		[PreserveSig]
		unsafe HRESULT RemoveElement(uint dwElementIndex, IUnknown** ppUnkElement);

		[PreserveSig]
		unsafe HRESULT InsertElementAt(uint dwIndex, IUnknown* pUnknown);

		[PreserveSig]
		HRESULT RemoveAllElements();
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(1539876715u, 34458, 18083, 155, 3, 250, 33, 138, 102, 174, 190);

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
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFCollection*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, uint>)lpVtbl[1])((IMFCollection*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, uint>)lpVtbl[2])((IMFCollection*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetElementCount(out uint pcElements)
	{
		fixed (uint* pcElements2 = &pcElements)
		{
			return GetElementCount(pcElements2);
		}
	}

	public unsafe HRESULT GetElementCount(uint* pcElements)
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, uint*, HRESULT>)lpVtbl[3])((IMFCollection*)Unsafe.AsPointer(ref this), pcElements);
	}

	public unsafe HRESULT GetElement(uint dwElementIndex, IUnknown** ppUnkElement)
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, uint, IUnknown**, HRESULT>)lpVtbl[4])((IMFCollection*)Unsafe.AsPointer(ref this), dwElementIndex, ppUnkElement);
	}

	public unsafe HRESULT AddElement(IUnknown* pUnkElement)
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, IUnknown*, HRESULT>)lpVtbl[5])((IMFCollection*)Unsafe.AsPointer(ref this), pUnkElement);
	}

	public unsafe HRESULT RemoveElement(uint dwElementIndex, IUnknown** ppUnkElement)
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, uint, IUnknown**, HRESULT>)lpVtbl[6])((IMFCollection*)Unsafe.AsPointer(ref this), dwElementIndex, ppUnkElement);
	}

	public unsafe HRESULT InsertElementAt(uint dwIndex, IUnknown* pUnknown)
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, uint, IUnknown*, HRESULT>)lpVtbl[7])((IMFCollection*)Unsafe.AsPointer(ref this), dwIndex, pUnknown);
	}

	public unsafe HRESULT RemoveAllElements()
	{
		return ((delegate* unmanaged[Stdcall]<IMFCollection*, HRESULT>)lpVtbl[8])((IMFCollection*)Unsafe.AsPointer(ref this));
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
