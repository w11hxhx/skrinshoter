using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.Audio;

[Guid("C8ADBD64-E71E-48A0-A4DE-185C395CD317")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IAudioCaptureClient
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IAudioCaptureClient*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioCaptureClient*, byte**, uint*, uint*, ulong*, ulong*, HRESULT> GetBuffer_4;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint, HRESULT> ReleaseBuffer_5;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint*, HRESULT> GetNextPacketSize_6;
	}

	[ComImport]
	[Guid("C8ADBD64-E71E-48A0-A4DE-185C395CD317")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetBuffer(byte** ppData, uint* pNumFramesToRead, uint* pdwFlags, [Optional] ulong* pu64DevicePosition, [Optional] ulong* pu64QPCPosition);

		[PreserveSig]
		HRESULT ReleaseBuffer(uint NumFramesRead);

		[PreserveSig]
		unsafe HRESULT GetNextPacketSize(uint* pNumFramesInNextPacket);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3366829412u, 59166, 18592, 164, 222, 24, 92, 57, 92, 211, 23);

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
		return ((delegate* unmanaged[Stdcall]<IAudioCaptureClient*, Guid*, void**, HRESULT>)(*lpVtbl))((IAudioCaptureClient*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint>)lpVtbl[1])((IAudioCaptureClient*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint>)lpVtbl[2])((IAudioCaptureClient*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetBuffer(out byte* ppData, out uint pNumFramesToRead, out uint pdwFlags, ulong* pu64DevicePosition, ulong* pu64QPCPosition)
	{
		fixed (uint* pdwFlags2 = &pdwFlags)
		{
			fixed (uint* pNumFramesToRead2 = &pNumFramesToRead)
			{
				fixed (byte** ppData2 = &ppData)
				{
					return GetBuffer(ppData2, pNumFramesToRead2, pdwFlags2, pu64DevicePosition, pu64QPCPosition);
				}
			}
		}
	}

	public unsafe HRESULT GetBuffer(byte** ppData, uint* pNumFramesToRead, uint* pdwFlags, [Optional] ulong* pu64DevicePosition, [Optional] ulong* pu64QPCPosition)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioCaptureClient*, byte**, uint*, uint*, ulong*, ulong*, HRESULT>)lpVtbl[3])((IAudioCaptureClient*)Unsafe.AsPointer(ref this), ppData, pNumFramesToRead, pdwFlags, pu64DevicePosition, pu64QPCPosition);
	}

	public unsafe HRESULT ReleaseBuffer(uint NumFramesRead)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint, HRESULT>)lpVtbl[4])((IAudioCaptureClient*)Unsafe.AsPointer(ref this), NumFramesRead);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetNextPacketSize(out uint pNumFramesInNextPacket)
	{
		fixed (uint* pNumFramesInNextPacket2 = &pNumFramesInNextPacket)
		{
			return GetNextPacketSize(pNumFramesInNextPacket2);
		}
	}

	public unsafe HRESULT GetNextPacketSize(uint* pNumFramesInNextPacket)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioCaptureClient*, uint*, HRESULT>)lpVtbl[5])((IAudioCaptureClient*)Unsafe.AsPointer(ref this), pNumFramesInNextPacket);
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
