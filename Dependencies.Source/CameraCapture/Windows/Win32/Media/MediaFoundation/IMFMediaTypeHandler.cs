using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("E93DCF6C-4B07-4E1E-8123-AA16ED6EADF5")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFMediaTypeHandler
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, IMFMediaType*, IMFMediaType**, HRESULT> IsMediaTypeSupported_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint*, HRESULT> GetMediaTypeCount_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint, IMFMediaType**, HRESULT> GetMediaTypeByIndex_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, IMFMediaType*, HRESULT> SetCurrentMediaType_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, IMFMediaType**, HRESULT> GetCurrentMediaType_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, Guid*, HRESULT> GetMajorType_9;
	}

	[ComImport]
	[Guid("E93DCF6C-4B07-4E1E-8123-AA16ED6EADF5")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT IsMediaTypeSupported(IMFMediaType* pMediaType, [Optional] IMFMediaType** ppMediaType);

		[PreserveSig]
		unsafe HRESULT GetMediaTypeCount(uint* pdwTypeCount);

		[PreserveSig]
		unsafe HRESULT GetMediaTypeByIndex(uint dwIndex, IMFMediaType** ppType);

		[PreserveSig]
		unsafe HRESULT SetCurrentMediaType(IMFMediaType* pMediaType);

		[PreserveSig]
		unsafe HRESULT GetCurrentMediaType(IMFMediaType** ppMediaType);

		[PreserveSig]
		unsafe HRESULT GetMajorType(Guid* pguidMajorType);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3913142124u, 19207, 19998, 129, 35, 170, 22, 237, 110, 173, 245);

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
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint>)lpVtbl[1])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint>)lpVtbl[2])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT IsMediaTypeSupported(IMFMediaType* pMediaType, [Optional] IMFMediaType** ppMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, IMFMediaType*, IMFMediaType**, HRESULT>)lpVtbl[3])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this), pMediaType, ppMediaType);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetMediaTypeCount(out uint pdwTypeCount)
	{
		fixed (uint* pdwTypeCount2 = &pdwTypeCount)
		{
			return GetMediaTypeCount(pdwTypeCount2);
		}
	}

	public unsafe HRESULT GetMediaTypeCount(uint* pdwTypeCount)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint*, HRESULT>)lpVtbl[4])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this), pdwTypeCount);
	}

	public unsafe HRESULT GetMediaTypeByIndex(uint dwIndex, IMFMediaType** ppType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, uint, IMFMediaType**, HRESULT>)lpVtbl[5])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this), dwIndex, ppType);
	}

	public unsafe HRESULT SetCurrentMediaType(IMFMediaType* pMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, IMFMediaType*, HRESULT>)lpVtbl[6])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this), pMediaType);
	}

	public unsafe HRESULT GetCurrentMediaType(IMFMediaType** ppMediaType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, IMFMediaType**, HRESULT>)lpVtbl[7])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this), ppMediaType);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetMajorType(out Guid pguidMajorType)
	{
		fixed (Guid* pguidMajorType2 = &pguidMajorType)
		{
			return GetMajorType(pguidMajorType2);
		}
	}

	public unsafe HRESULT GetMajorType(Guid* pguidMajorType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFMediaTypeHandler*, Guid*, HRESULT>)lpVtbl[8])((IMFMediaTypeHandler*)Unsafe.AsPointer(ref this), pguidMajorType);
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
