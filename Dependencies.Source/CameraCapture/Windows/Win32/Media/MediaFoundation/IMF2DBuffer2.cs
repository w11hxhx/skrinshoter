using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("33AE5EA6-4316-436F-8DDD-D73D22F829EC")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMF2DBuffer2
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte**, int*, HRESULT> Lock2D_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, HRESULT> Unlock2D_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte**, int*, HRESULT> GetScanline0AndPitch_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, BOOL*, HRESULT> IsContiguousFormat_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, uint*, HRESULT> GetContiguousLength_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte*, uint, HRESULT> ContiguousCopyTo_9;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte*, uint, HRESULT> ContiguousCopyFrom_10;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, MF2DBuffer_LockFlags, byte**, int*, byte**, uint*, HRESULT> Lock2DSize_11;

		internal unsafe delegate* unmanaged[Stdcall]<IMF2DBuffer2*, IMF2DBuffer2*, HRESULT> Copy2DTo_12;
	}

	[ComImport]
	[Guid("33AE5EA6-4316-436F-8DDD-D73D22F829EC")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface : IMF2DBuffer.Interface
	{
		[PreserveSig]
		new unsafe HRESULT Lock2D(byte** ppbScanline0, int* plPitch);

		[PreserveSig]
		new HRESULT Unlock2D();

		[PreserveSig]
		new unsafe HRESULT GetScanline0AndPitch(byte** pbScanline0, int* plPitch);

		[PreserveSig]
		new unsafe HRESULT IsContiguousFormat(BOOL* pfIsContiguous);

		[PreserveSig]
		new unsafe HRESULT GetContiguousLength(uint* pcbLength);

		[PreserveSig]
		new unsafe HRESULT ContiguousCopyTo(byte* pbDestBuffer, uint cbDestBuffer);

		[PreserveSig]
		new unsafe HRESULT ContiguousCopyFrom(byte* pbSrcBuffer, uint cbSrcBuffer);

		[PreserveSig]
		unsafe HRESULT Lock2DSize(MF2DBuffer_LockFlags lockFlags, byte** ppbScanline0, int* plPitch, byte** ppbBufferStart, uint* pcbBufferLength);

		[PreserveSig]
		unsafe HRESULT Copy2DTo(IMF2DBuffer2* pDestBuffer);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(867065510, 17174, 17263, 141, 221, 215, 61, 34, 248, 41, 236);

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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, Guid*, void**, HRESULT>)(*lpVtbl))((IMF2DBuffer2*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, uint>)lpVtbl[1])((IMF2DBuffer2*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, uint>)lpVtbl[2])((IMF2DBuffer2*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte**, int*, HRESULT>)lpVtbl[3])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), ppbScanline0, plPitch);
	}

	public unsafe HRESULT Unlock2D()
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, HRESULT>)lpVtbl[4])((IMF2DBuffer2*)Unsafe.AsPointer(ref this));
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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte**, int*, HRESULT>)lpVtbl[5])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), pbScanline0, plPitch);
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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, BOOL*, HRESULT>)lpVtbl[6])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), pfIsContiguous);
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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, uint*, HRESULT>)lpVtbl[7])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), pcbLength);
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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte*, uint, HRESULT>)lpVtbl[8])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), pbDestBuffer, cbDestBuffer);
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
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, byte*, uint, HRESULT>)lpVtbl[9])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), pbSrcBuffer, cbSrcBuffer);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Lock2DSize(MF2DBuffer_LockFlags lockFlags, out byte* ppbScanline0, out int plPitch, out byte* ppbBufferStart, out uint pcbBufferLength)
	{
		fixed (uint* pcbBufferLength2 = &pcbBufferLength)
		{
			fixed (byte** ppbBufferStart2 = &ppbBufferStart)
			{
				fixed (int* plPitch2 = &plPitch)
				{
					fixed (byte** ppbScanline1 = &ppbScanline0)
					{
						return Lock2DSize(lockFlags, ppbScanline1, plPitch2, ppbBufferStart2, pcbBufferLength2);
					}
				}
			}
		}
	}

	public unsafe HRESULT Lock2DSize(MF2DBuffer_LockFlags lockFlags, byte** ppbScanline0, int* plPitch, byte** ppbBufferStart, uint* pcbBufferLength)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, MF2DBuffer_LockFlags, byte**, int*, byte**, uint*, HRESULT>)lpVtbl[10])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), lockFlags, ppbScanline0, plPitch, ppbBufferStart, pcbBufferLength);
	}

	public unsafe HRESULT Copy2DTo(IMF2DBuffer2* pDestBuffer)
	{
		return ((delegate* unmanaged[Stdcall]<IMF2DBuffer2*, IMF2DBuffer2*, HRESULT>)lpVtbl[11])((IMF2DBuffer2*)Unsafe.AsPointer(ref this), pDestBuffer);
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
