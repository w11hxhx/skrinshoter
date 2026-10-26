using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Direct3D11;

[Guid("E4819DDC-4CF0-4025-BD26-5DE82A3E07B7")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ID3D11InputLayout
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11InputLayout*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11InputLayout*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11InputLayout*, ID3D11Device**, void> GetDevice_4;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, uint*, void*, HRESULT> GetPrivateData_5;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, uint, void*, HRESULT> SetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_7;
	}

	[ComImport]
	[Guid("E4819DDC-4CF0-4025-BD26-5DE82A3E07B7")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : ID3D11DeviceChild.Interface
	{
		[PreserveSig]
		new unsafe void GetDevice(ID3D11Device** ppDevice);

		[PreserveSig]
		new unsafe HRESULT GetPrivateData(Guid* guid, uint* pDataSize, [Optional] void* pData);

		[PreserveSig]
		new unsafe HRESULT SetPrivateData(Guid* guid, uint DataSize, [Optional] void* pData);

		[PreserveSig]
		new unsafe HRESULT SetPrivateDataInterface(Guid* guid, [Optional] IUnknown* pData);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3833699804u, 19696, 16421, 189, 38, 93, 232, 42, 62, 7, 183);

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
		return ((delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, void**, HRESULT>)(*lpVtbl))((ID3D11InputLayout*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11InputLayout*, uint>)lpVtbl[1])((ID3D11InputLayout*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11InputLayout*, uint>)lpVtbl[2])((ID3D11InputLayout*)Unsafe.AsPointer(ref this));
	}

	public unsafe void GetDevice(ID3D11Device** ppDevice)
	{
		((delegate* unmanaged[Stdcall]<ID3D11InputLayout*, ID3D11Device**, void>)lpVtbl[3])((ID3D11InputLayout*)Unsafe.AsPointer(ref this), ppDevice);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetPrivateData(in Guid guid, ref uint pDataSize, void* pData)
	{
		fixed (uint* pDataSize2 = &pDataSize)
		{
			fixed (Guid* guid2 = &guid)
			{
				return GetPrivateData(guid2, pDataSize2, pData);
			}
		}
	}

	public unsafe HRESULT GetPrivateData(Guid* guid, uint* pDataSize, [Optional] void* pData)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, uint*, void*, HRESULT>)lpVtbl[4])((ID3D11InputLayout*)Unsafe.AsPointer(ref this), guid, pDataSize, pData);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetPrivateData(in Guid guid, uint DataSize, void* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return SetPrivateData(guid2, DataSize, pData);
		}
	}

	public unsafe HRESULT SetPrivateData(Guid* guid, uint DataSize, [Optional] void* pData)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, uint, void*, HRESULT>)lpVtbl[5])((ID3D11InputLayout*)Unsafe.AsPointer(ref this), guid, DataSize, pData);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT SetPrivateDataInterface(in Guid guid, IUnknown* pData)
	{
		fixed (Guid* guid2 = &guid)
		{
			return SetPrivateDataInterface(guid2, pData);
		}
	}

	public unsafe HRESULT SetPrivateDataInterface(Guid* guid, [Optional] IUnknown* pData)
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11InputLayout*, Guid*, IUnknown*, HRESULT>)lpVtbl[6])((ID3D11InputLayout*)Unsafe.AsPointer(ref this), guid, pData);
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
