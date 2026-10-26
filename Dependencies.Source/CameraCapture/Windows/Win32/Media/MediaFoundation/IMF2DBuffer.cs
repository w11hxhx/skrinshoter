using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("7DC9D5F9-9ED9-44EC-9BBF-0600BB589FBB")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMF2DBuffer
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte**, int*, HRESULT> Lock2D_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, HRESULT> Unlock2D_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte**, int*, HRESULT> GetScanline0AndPitch_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, BOOL*, HRESULT> IsContiguousFormat_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, uint*, HRESULT> GetContiguousLength_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte*, uint, HRESULT> ContiguousCopyTo_9;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte*, uint, HRESULT> ContiguousCopyFrom_10;
	}

	[ComImport]
	[Guid("7DC9D5F9-9ED9-44EC-9BBF-0600BB589FBB")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT Lock2D(byte** ppbScanline0, int* plPitch);

		[PreserveSig]
		HRESULT Unlock2D();

		[PreserveSig]
		unsafe HRESULT GetScanline0AndPitch(byte** pbScanline0, int* plPitch);

		[PreserveSig]
		unsafe HRESULT IsContiguousFormat(BOOL* pfIsContiguous);

		[PreserveSig]
		unsafe HRESULT GetContiguousLength(uint* pcbLength);

		[PreserveSig]
		unsafe HRESULT ContiguousCopyTo(byte* pbDestBuffer, uint cbDestBuffer);

		[PreserveSig]
		unsafe HRESULT ContiguousCopyFrom(byte* pbSrcBuffer, uint cbSrcBuffer);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(2110379513u, 40665, 17644, 155, 191, 6, 0, 187, 88, 159, 187);

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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, Guid*, void**, HRESULT>)(*lpVtbl))((IMF2DBuffer*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, uint>)lpVtbl[1])((IMF2DBuffer*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, uint>)lpVtbl[2])((IMF2DBuffer*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Lock2D(out byte* ppbScanline0, out int plPitch)
	{
		fixed (int* plPitch2 = &plPitch)
		{
			fixed (byte** ppbScanline1 = &ppbScanline0)
			{
				return Lock2D(ppbScanline1, plPitch2);
			}
		}
	}

	public unsafe HRESULT Lock2D(byte** ppbScanline0, int* plPitch)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte**, int*, HRESULT>)lpVtbl[3])((IMF2DBuffer*)Unsafe.AsPointer(ref this), ppbScanline0, plPitch);
	}

	public unsafe HRESULT Unlock2D()
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, HRESULT>)lpVtbl[4])((IMF2DBuffer*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetScanline0AndPitch(out byte* pbScanline0, out int plPitch)
	{
		fixed (int* plPitch2 = &plPitch)
		{
			fixed (byte** pbScanline1 = &pbScanline0)
			{
				return GetScanline0AndPitch(pbScanline1, plPitch2);
			}
		}
	}

	public unsafe HRESULT GetScanline0AndPitch(byte** pbScanline0, int* plPitch)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte**, int*, HRESULT>)lpVtbl[5])((IMF2DBuffer*)Unsafe.AsPointer(ref this), pbScanline0, plPitch);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT IsContiguousFormat(out BOOL pfIsContiguous)
	{
		fixed (BOOL* pfIsContiguous2 = &pfIsContiguous)
		{
			return IsContiguousFormat(pfIsContiguous2);
		}
	}

	public unsafe HRESULT IsContiguousFormat(BOOL* pfIsContiguous)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, BOOL*, HRESULT>)lpVtbl[6])((IMF2DBuffer*)Unsafe.AsPointer(ref this), pfIsContiguous);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetContiguousLength(out uint pcbLength)
	{
		fixed (uint* pcbLength2 = &pcbLength)
		{
			return GetContiguousLength(pcbLength2);
		}
	}

	public unsafe HRESULT GetContiguousLength(uint* pcbLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, uint*, HRESULT>)lpVtbl[7])((IMF2DBuffer*)Unsafe.AsPointer(ref this), pcbLength);
	}

	internal unsafe HRESULT ContiguousCopyTo(Span<byte> pbDestBuffer)
	{
		fixed (byte* pbDestBuffer2 = pbDestBuffer)
		{
			return ContiguousCopyTo(pbDestBuffer2, (uint)pbDestBuffer.Length);
		}
	}

	public unsafe HRESULT ContiguousCopyTo(byte* pbDestBuffer, uint cbDestBuffer)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte*, uint, HRESULT>)lpVtbl[8])((IMF2DBuffer*)Unsafe.AsPointer(ref this), pbDestBuffer, cbDestBuffer);
	}

	internal unsafe HRESULT ContiguousCopyFrom(ReadOnlySpan<byte> pbSrcBuffer)
	{
		fixed (byte* pbSrcBuffer2 = pbSrcBuffer)
		{
			return ContiguousCopyFrom(pbSrcBuffer2, (uint)pbSrcBuffer.Length);
		}
	}

	public unsafe HRESULT ContiguousCopyFrom(byte* pbSrcBuffer, uint cbSrcBuffer)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer*, byte*, uint, HRESULT>)lpVtbl[9])((IMF2DBuffer*)Unsafe.AsPointer(ref this), pbSrcBuffer, cbSrcBuffer);
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
