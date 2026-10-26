using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.MediaFoundation;

[Guid("BF94C121-5B05-4E6F-8000-BA598961414D")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IMFTransform
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint*, uint*, uint*, uint*, HRESULT> GetStreamLimits_4;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint*, uint*, HRESULT> GetStreamCount_5;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint*, uint, uint*, HRESULT> GetStreamIDs_6;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, MFT_INPUT_STREAM_INFO*, HRESULT> GetInputStreamInfo_7;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, MFT_OUTPUT_STREAM_INFO*, HRESULT> GetOutputStreamInfo_8;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, IMFAttributes**, HRESULT> GetAttributes_9;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFAttributes**, HRESULT> GetInputStreamAttributes_10;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFAttributes**, HRESULT> GetOutputStreamAttributes_11;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, HRESULT> DeleteInputStream_12;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint*, HRESULT> AddInputStreams_13;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint, IMFMediaType**, HRESULT> GetInputAvailableType_14;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint, IMFMediaType**, HRESULT> GetOutputAvailableType_15;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType*, uint, HRESULT> SetInputType_16;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType*, uint, HRESULT> SetOutputType_17;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType**, HRESULT> GetInputCurrentType_18;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType**, HRESULT> GetOutputCurrentType_19;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint*, HRESULT> GetInputStatus_20;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint*, HRESULT> GetOutputStatus_21;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, long, long, HRESULT> SetOutputBounds_22;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaEvent*, HRESULT> ProcessEvent_23;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, MFT_MESSAGE_TYPE, nuint, HRESULT> ProcessMessage_24;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFSample*, uint, HRESULT> ProcessInput_25;

		internal unsafe delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint, MFT_OUTPUT_DATA_BUFFER*, uint*, HRESULT> ProcessOutput_26;
	}

	[ComImport]
	[Guid("BF94C121-5B05-4E6F-8000-BA598961414D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT GetStreamLimits(uint* pdwInputMinimum, uint* pdwInputMaximum, uint* pdwOutputMinimum, uint* pdwOutputMaximum);

		[PreserveSig]
		unsafe HRESULT GetStreamCount(uint* pcInputStreams, uint* pcOutputStreams);

		[PreserveSig]
		unsafe HRESULT GetStreamIDs(uint dwInputIDArraySize, uint* pdwInputIDs, uint dwOutputIDArraySize, uint* pdwOutputIDs);

		[PreserveSig]
		unsafe HRESULT GetInputStreamInfo(uint dwInputStreamID, MFT_INPUT_STREAM_INFO* pStreamInfo);

		[PreserveSig]
		unsafe HRESULT GetOutputStreamInfo(uint dwOutputStreamID, MFT_OUTPUT_STREAM_INFO* pStreamInfo);

		[PreserveSig]
		unsafe HRESULT GetAttributes(IMFAttributes** pAttributes);

		[PreserveSig]
		unsafe HRESULT GetInputStreamAttributes(uint dwInputStreamID, IMFAttributes** pAttributes);

		[PreserveSig]
		unsafe HRESULT GetOutputStreamAttributes(uint dwOutputStreamID, IMFAttributes** pAttributes);

		[PreserveSig]
		HRESULT DeleteInputStream(uint dwStreamID);

		[PreserveSig]
		unsafe HRESULT AddInputStreams(uint cStreams, uint* adwStreamIDs);

		[PreserveSig]
		unsafe HRESULT GetInputAvailableType(uint dwInputStreamID, uint dwTypeIndex, IMFMediaType** ppType);

		[PreserveSig]
		unsafe HRESULT GetOutputAvailableType(uint dwOutputStreamID, uint dwTypeIndex, IMFMediaType** ppType);

		[PreserveSig]
		unsafe HRESULT SetInputType(uint dwInputStreamID, IMFMediaType* pType, uint dwFlags);

		[PreserveSig]
		unsafe HRESULT SetOutputType(uint dwOutputStreamID, IMFMediaType* pType, uint dwFlags);

		[PreserveSig]
		unsafe HRESULT GetInputCurrentType(uint dwInputStreamID, IMFMediaType** ppType);

		[PreserveSig]
		unsafe HRESULT GetOutputCurrentType(uint dwOutputStreamID, IMFMediaType** ppType);

		[PreserveSig]
		unsafe HRESULT GetInputStatus(uint dwInputStreamID, uint* pdwFlags);

		[PreserveSig]
		unsafe HRESULT GetOutputStatus(uint* pdwFlags);

		[PreserveSig]
		HRESULT SetOutputBounds(long hnsLowerBound, long hnsUpperBound);

		[PreserveSig]
		unsafe HRESULT ProcessEvent(uint dwInputStreamID, IMFMediaEvent* pEvent);

		[PreserveSig]
		HRESULT ProcessMessage(MFT_MESSAGE_TYPE eMessage, nuint ulParam);

		[PreserveSig]
		unsafe HRESULT ProcessInput(uint dwInputStreamID, IMFSample* pSample, uint dwFlags);

		[PreserveSig]
		unsafe HRESULT ProcessOutput(uint dwFlags, uint cOutputBufferCount, MFT_OUTPUT_DATA_BUFFER* pOutputSamples, uint* pdwStatus);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(3214197025u, 23301, 20079, 128, 0, 186, 89, 137, 97, 65, 77);

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
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, Guid*, void**, HRESULT>)(*lpVtbl))((IMFTransform*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint>)lpVtbl[1])((IMFTransform*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint>)lpVtbl[2])((IMFTransform*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetStreamLimits(out uint pdwInputMinimum, out uint pdwInputMaximum, out uint pdwOutputMinimum, out uint pdwOutputMaximum)
	{
		fixed (uint* pdwOutputMaximum2 = &pdwOutputMaximum)
		{
			fixed (uint* pdwOutputMinimum2 = &pdwOutputMinimum)
			{
				fixed (uint* pdwInputMaximum2 = &pdwInputMaximum)
				{
					fixed (uint* pdwInputMinimum2 = &pdwInputMinimum)
					{
						return GetStreamLimits(pdwInputMinimum2, pdwInputMaximum2, pdwOutputMinimum2, pdwOutputMaximum2);
					}
				}
			}
		}
	}

	public unsafe HRESULT GetStreamLimits(uint* pdwInputMinimum, uint* pdwInputMaximum, uint* pdwOutputMinimum, uint* pdwOutputMaximum)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint*, uint*, uint*, uint*, HRESULT>)lpVtbl[3])((IMFTransform*)Unsafe.AsPointer(ref this), pdwInputMinimum, pdwInputMaximum, pdwOutputMinimum, pdwOutputMaximum);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetStreamCount(out uint pcInputStreams, out uint pcOutputStreams)
	{
		fixed (uint* pcOutputStreams2 = &pcOutputStreams)
		{
			fixed (uint* pcInputStreams2 = &pcInputStreams)
			{
				return GetStreamCount(pcInputStreams2, pcOutputStreams2);
			}
		}
	}

	public unsafe HRESULT GetStreamCount(uint* pcInputStreams, uint* pcOutputStreams)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint*, uint*, HRESULT>)lpVtbl[4])((IMFTransform*)Unsafe.AsPointer(ref this), pcInputStreams, pcOutputStreams);
	}

	internal unsafe HRESULT GetStreamIDs(Span<uint> pdwInputIDs, Span<uint> pdwOutputIDs)
	{
		fixed (uint* pdwOutputIDs2 = pdwOutputIDs)
		{
			fixed (uint* pdwInputIDs2 = pdwInputIDs)
			{
				return GetStreamIDs((uint)pdwInputIDs.Length, pdwInputIDs2, (uint)pdwOutputIDs.Length, pdwOutputIDs2);
			}
		}
	}

	public unsafe HRESULT GetStreamIDs(uint dwInputIDArraySize, uint* pdwInputIDs, uint dwOutputIDArraySize, uint* pdwOutputIDs)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint*, uint, uint*, HRESULT>)lpVtbl[5])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputIDArraySize, pdwInputIDs, dwOutputIDArraySize, pdwOutputIDs);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetInputStreamInfo(uint dwInputStreamID, out MFT_INPUT_STREAM_INFO pStreamInfo)
	{
		fixed (MFT_INPUT_STREAM_INFO* pStreamInfo2 = &pStreamInfo)
		{
			return GetInputStreamInfo(dwInputStreamID, pStreamInfo2);
		}
	}

	public unsafe HRESULT GetInputStreamInfo(uint dwInputStreamID, MFT_INPUT_STREAM_INFO* pStreamInfo)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, MFT_INPUT_STREAM_INFO*, HRESULT>)lpVtbl[6])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, pStreamInfo);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetOutputStreamInfo(uint dwOutputStreamID, out MFT_OUTPUT_STREAM_INFO pStreamInfo)
	{
		fixed (MFT_OUTPUT_STREAM_INFO* pStreamInfo2 = &pStreamInfo)
		{
			return GetOutputStreamInfo(dwOutputStreamID, pStreamInfo2);
		}
	}

	public unsafe HRESULT GetOutputStreamInfo(uint dwOutputStreamID, MFT_OUTPUT_STREAM_INFO* pStreamInfo)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, MFT_OUTPUT_STREAM_INFO*, HRESULT>)lpVtbl[7])((IMFTransform*)Unsafe.AsPointer(ref this), dwOutputStreamID, pStreamInfo);
	}

	public unsafe HRESULT GetAttributes(IMFAttributes** pAttributes)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, IMFAttributes**, HRESULT>)lpVtbl[8])((IMFTransform*)Unsafe.AsPointer(ref this), pAttributes);
	}

	public unsafe HRESULT GetInputStreamAttributes(uint dwInputStreamID, IMFAttributes** pAttributes)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFAttributes**, HRESULT>)lpVtbl[9])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, pAttributes);
	}

	public unsafe HRESULT GetOutputStreamAttributes(uint dwOutputStreamID, IMFAttributes** pAttributes)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFAttributes**, HRESULT>)lpVtbl[10])((IMFTransform*)Unsafe.AsPointer(ref this), dwOutputStreamID, pAttributes);
	}

	public unsafe HRESULT DeleteInputStream(uint dwStreamID)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, HRESULT>)lpVtbl[11])((IMFTransform*)Unsafe.AsPointer(ref this), dwStreamID);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT AddInputStreams(uint cStreams, in uint adwStreamIDs)
	{
		fixed (uint* adwStreamIDs2 = &adwStreamIDs)
		{
			return AddInputStreams(cStreams, adwStreamIDs2);
		}
	}

	public unsafe HRESULT AddInputStreams(uint cStreams, uint* adwStreamIDs)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint*, HRESULT>)lpVtbl[12])((IMFTransform*)Unsafe.AsPointer(ref this), cStreams, adwStreamIDs);
	}

	public unsafe HRESULT GetInputAvailableType(uint dwInputStreamID, uint dwTypeIndex, IMFMediaType** ppType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint, IMFMediaType**, HRESULT>)lpVtbl[13])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, dwTypeIndex, ppType);
	}

	public unsafe HRESULT GetOutputAvailableType(uint dwOutputStreamID, uint dwTypeIndex, IMFMediaType** ppType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint, IMFMediaType**, HRESULT>)lpVtbl[14])((IMFTransform*)Unsafe.AsPointer(ref this), dwOutputStreamID, dwTypeIndex, ppType);
	}

	public unsafe HRESULT SetInputType(uint dwInputStreamID, IMFMediaType* pType, uint dwFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType*, uint, HRESULT>)lpVtbl[15])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, pType, dwFlags);
	}

	public unsafe HRESULT SetOutputType(uint dwOutputStreamID, IMFMediaType* pType, uint dwFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType*, uint, HRESULT>)lpVtbl[16])((IMFTransform*)Unsafe.AsPointer(ref this), dwOutputStreamID, pType, dwFlags);
	}

	public unsafe HRESULT GetInputCurrentType(uint dwInputStreamID, IMFMediaType** ppType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType**, HRESULT>)lpVtbl[17])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, ppType);
	}

	public unsafe HRESULT GetOutputCurrentType(uint dwOutputStreamID, IMFMediaType** ppType)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaType**, HRESULT>)lpVtbl[18])((IMFTransform*)Unsafe.AsPointer(ref this), dwOutputStreamID, ppType);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetInputStatus(uint dwInputStreamID, out uint pdwFlags)
	{
		fixed (uint* pdwFlags2 = &pdwFlags)
		{
			return GetInputStatus(dwInputStreamID, pdwFlags2);
		}
	}

	public unsafe HRESULT GetInputStatus(uint dwInputStreamID, uint* pdwFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint*, HRESULT>)lpVtbl[19])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, pdwFlags);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetOutputStatus(out uint pdwFlags)
	{
		fixed (uint* pdwFlags2 = &pdwFlags)
		{
			return GetOutputStatus(pdwFlags2);
		}
	}

	public unsafe HRESULT GetOutputStatus(uint* pdwFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint*, HRESULT>)lpVtbl[20])((IMFTransform*)Unsafe.AsPointer(ref this), pdwFlags);
	}

	public unsafe HRESULT SetOutputBounds(long hnsLowerBound, long hnsUpperBound)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, long, long, HRESULT>)lpVtbl[21])((IMFTransform*)Unsafe.AsPointer(ref this), hnsLowerBound, hnsUpperBound);
	}

	public unsafe HRESULT ProcessEvent(uint dwInputStreamID, IMFMediaEvent* pEvent)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFMediaEvent*, HRESULT>)lpVtbl[22])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, pEvent);
	}

	public unsafe HRESULT ProcessMessage(MFT_MESSAGE_TYPE eMessage, nuint ulParam)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, MFT_MESSAGE_TYPE, UIntPtr, HRESULT>)lpVtbl[23])((IMFTransform*)Unsafe.AsPointer(ref this), eMessage, ulParam);
	}

	public unsafe HRESULT ProcessInput(uint dwInputStreamID, IMFSample* pSample, uint dwFlags)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, IMFSample*, uint, HRESULT>)lpVtbl[24])((IMFTransform*)Unsafe.AsPointer(ref this), dwInputStreamID, pSample, dwFlags);
	}

	internal unsafe HRESULT ProcessOutput(uint dwFlags, ReadOnlySpan<MFT_OUTPUT_DATA_BUFFER> pOutputSamples, out uint pdwStatus)
	{
		fixed (uint* pdwStatus2 = &pdwStatus)
		{
			fixed (MFT_OUTPUT_DATA_BUFFER* pOutputSamples2 = pOutputSamples)
			{
				return ProcessOutput(dwFlags, (uint)pOutputSamples.Length, pOutputSamples2, pdwStatus2);
			}
		}
	}

	public unsafe HRESULT ProcessOutput(uint dwFlags, uint cOutputBufferCount, MFT_OUTPUT_DATA_BUFFER* pOutputSamples, uint* pdwStatus)
	{
		return ((delegate* unmanaged[Stdcall]<IMFTransform*, uint, uint, MFT_OUTPUT_DATA_BUFFER*, uint*, HRESULT>)lpVtbl[25])((IMFTransform*)Unsafe.AsPointer(ref this), dwFlags, cOutputBufferCount, pOutputSamples, pdwStatus);
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
