using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;
using Windows.Win32.System.Com;

namespace Windows.Win32.Graphics.Direct3D11;

[Guid("6E8C49FB-A371-4770-B440-29086022B741")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct ID3D11Counter
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, ID3D11Device**, void> GetDevice_4;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, uint*, void*, HRESULT> GetPrivateData_5;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, uint, void*, HRESULT> SetPrivateData_6;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, IUnknown*, HRESULT> SetPrivateDataInterface_7;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, uint> GetDataSize_8;

		internal unsafe delegate* unmanaged[Stdcall]<ID3D11Counter*, D3D11_COUNTER_DESC*, void> GetDesc_9;
	}

	[ComImport]
	[Guid("6E8C49FB-A371-4770-B440-29086022B741")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : ID3D11Asynchronous.Interface, ID3D11DeviceChild.Interface
	{
		[PreserveSig]
		new unsafe void GetDevice(ID3D11Device** ppDevice);

		[PreserveSig]
		new unsafe HRESULT GetPrivateData(Guid* guid, uint* pDataSize, [Optional] void* pData);

		[PreserveSig]
		new unsafe HRESULT SetPrivateData(Guid* guid, uint DataSize, [Optional] void* pData);

		[PreserveSig]
		new unsafe HRESULT SetPrivateDataInterface(Guid* guid, [Optional] IUnknown* pData);

		[PreserveSig]
		new uint GetDataSize();

		[PreserveSig]
		unsafe void GetDesc(D3D11_COUNTER_DESC* pDesc);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(1854687739u, 41841, 18288, 180, 64, 41, 8, 96, 34, 183, 65);

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
		return ((delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, void**, HRESULT>)(*lpVtbl))((ID3D11Counter*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Counter*, uint>)lpVtbl[1])((ID3D11Counter*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Counter*, uint>)lpVtbl[2])((ID3D11Counter*)Unsafe.AsPointer(ref this));
	}

	public unsafe void GetDevice(ID3D11Device** ppDevice)
	{
		((delegate* unmanaged[Stdcall]<ID3D11Counter*, ID3D11Device**, void>)lpVtbl[3])((ID3D11Counter*)Unsafe.AsPointer(ref this), ppDevice);
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
		return ((delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, uint*, void*, HRESULT>)lpVtbl[4])((ID3D11Counter*)Unsafe.AsPointer(ref this), guid, pDataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, uint, void*, HRESULT>)lpVtbl[5])((ID3D11Counter*)Unsafe.AsPointer(ref this), guid, DataSize, pData);
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
		return ((delegate* unmanaged[Stdcall]<ID3D11Counter*, Guid*, IUnknown*, HRESULT>)lpVtbl[6])((ID3D11Counter*)Unsafe.AsPointer(ref this), guid, pData);
	}

	public unsafe uint GetDataSize()
	{
		return ((delegate* unmanaged[Stdcall]<ID3D11Counter*, uint>)lpVtbl[7])((ID3D11Counter*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe void GetDesc(out D3D11_COUNTER_DESC pDesc)
	{
		fixed (D3D11_COUNTER_DESC* pDesc2 = &pDesc)
		{
			GetDesc(pDesc2);
		}
	}

	public unsafe void GetDesc(D3D11_COUNTER_DESC* pDesc)
	{
		((delegate* unmanaged[Stdcall]<ID3D11Counter*, D3D11_COUNTER_DESC*, void>)lpVtbl[8])((ID3D11Counter*)Unsafe.AsPointer(ref this), pDesc);
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
