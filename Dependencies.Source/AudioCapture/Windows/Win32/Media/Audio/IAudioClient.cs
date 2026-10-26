using System;
using System.CodeDom.Compiler;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Windows.Win32.Foundation;

namespace Windows.Win32.Media.Audio;

[Guid("1CB9AD4C-DBFA-4C32-B178-C2F568A703B2")]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal struct IAudioClient
{
	internal struct Vtbl
	{
		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, Guid*, void**, HRESULT> QueryInterface_1;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, uint> AddRef_2;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, uint> Release_3;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, AUDCLNT_SHAREMODE, uint, long, long, WAVEFORMATEX*, Guid*, HRESULT> Initialize_4;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, uint*, HRESULT> GetBufferSize_5;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, long*, HRESULT> GetStreamLatency_6;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, uint*, HRESULT> GetCurrentPadding_7;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, AUDCLNT_SHAREMODE, WAVEFORMATEX*, WAVEFORMATEX**, HRESULT> IsFormatSupported_8;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, WAVEFORMATEX**, HRESULT> GetMixFormat_9;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, long*, long*, HRESULT> GetDevicePeriod_10;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, HRESULT> Start_11;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, HRESULT> Stop_12;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, HRESULT> Reset_13;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, HANDLE, HRESULT> SetEventHandle_14;

		internal unsafe delegate* unmanaged[Stdcall]<IAudioClient*, Guid*, void**, HRESULT> GetService_15;
	}

	[ComImport]
	[Guid("1CB9AD4C-DBFA-4C32-B178-C2F568A703B2")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface Interface
	{
		[PreserveSig]
		unsafe HRESULT Initialize(AUDCLNT_SHAREMODE ShareMode, uint StreamFlags, long hnsBufferDuration, long hnsPeriodicity, WAVEFORMATEX* pFormat, [Optional] Guid* AudioSessionGuid);

		[PreserveSig]
		unsafe HRESULT GetBufferSize(uint* pNumBufferFrames);

		[PreserveSig]
		unsafe HRESULT GetStreamLatency(long* phnsLatency);

		[PreserveSig]
		unsafe HRESULT GetCurrentPadding(uint* pNumPaddingFrames);

		[PreserveSig]
		unsafe HRESULT IsFormatSupported(AUDCLNT_SHAREMODE ShareMode, WAVEFORMATEX* pFormat, [Optional] WAVEFORMATEX** ppClosestMatch);

		[PreserveSig]
		unsafe HRESULT GetMixFormat(WAVEFORMATEX** ppDeviceFormat);

		[PreserveSig]
		unsafe HRESULT GetDevicePeriod([Optional] long* phnsDefaultDevicePeriod, [Optional] long* phnsMinimumDevicePeriod);

		[PreserveSig]
		HRESULT Start();

		[PreserveSig]
		HRESULT Stop();

		[PreserveSig]
		HRESULT Reset();

		[PreserveSig]
		HRESULT SetEventHandle(HANDLE eventHandle);

		[PreserveSig]
		unsafe HRESULT GetService(Guid* riid, void** ppv);
	}

	private unsafe void** lpVtbl;

	internal static readonly Guid IID_Guid = new Guid(481930572u, 56314, 19506, 177, 120, 194, 245, 104, 167, 3, 178);

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
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, Guid*, void**, HRESULT>)(*lpVtbl))((IAudioClient*)Unsafe.AsPointer(ref this), riid, ppvObject);
	}

	public unsafe uint AddRef()
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, uint>)lpVtbl[1])((IAudioClient*)Unsafe.AsPointer(ref this));
	}

	public unsafe uint Release()
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, uint>)lpVtbl[2])((IAudioClient*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT Initialize(AUDCLNT_SHAREMODE ShareMode, uint StreamFlags, long hnsBufferDuration, long hnsPeriodicity, in WAVEFORMATEX pFormat, Guid? AudioSessionGuid)
	{
		fixed (WAVEFORMATEX* pFormat2 = &pFormat)
		{
			Guid valueOrDefault = AudioSessionGuid.GetValueOrDefault();
			return Initialize(ShareMode, StreamFlags, hnsBufferDuration, hnsPeriodicity, pFormat2, AudioSessionGuid.HasValue ? (&valueOrDefault) : null);
		}
	}

	public unsafe HRESULT Initialize(AUDCLNT_SHAREMODE ShareMode, uint StreamFlags, long hnsBufferDuration, long hnsPeriodicity, WAVEFORMATEX* pFormat, [Optional] Guid* AudioSessionGuid)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, AUDCLNT_SHAREMODE, uint, long, long, WAVEFORMATEX*, Guid*, HRESULT>)lpVtbl[3])((IAudioClient*)Unsafe.AsPointer(ref this), ShareMode, StreamFlags, hnsBufferDuration, hnsPeriodicity, pFormat, AudioSessionGuid);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetBufferSize(out uint pNumBufferFrames)
	{
		fixed (uint* pNumBufferFrames2 = &pNumBufferFrames)
		{
			return GetBufferSize(pNumBufferFrames2);
		}
	}

	public unsafe HRESULT GetBufferSize(uint* pNumBufferFrames)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, uint*, HRESULT>)lpVtbl[4])((IAudioClient*)Unsafe.AsPointer(ref this), pNumBufferFrames);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetStreamLatency(out long phnsLatency)
	{
		fixed (long* phnsLatency2 = &phnsLatency)
		{
			return GetStreamLatency(phnsLatency2);
		}
	}

	public unsafe HRESULT GetStreamLatency(long* phnsLatency)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, long*, HRESULT>)lpVtbl[5])((IAudioClient*)Unsafe.AsPointer(ref this), phnsLatency);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetCurrentPadding(out uint pNumPaddingFrames)
	{
		fixed (uint* pNumPaddingFrames2 = &pNumPaddingFrames)
		{
			return GetCurrentPadding(pNumPaddingFrames2);
		}
	}

	public unsafe HRESULT GetCurrentPadding(uint* pNumPaddingFrames)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, uint*, HRESULT>)lpVtbl[6])((IAudioClient*)Unsafe.AsPointer(ref this), pNumPaddingFrames);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT IsFormatSupported(AUDCLNT_SHAREMODE ShareMode, in WAVEFORMATEX pFormat, WAVEFORMATEX** ppClosestMatch)
	{
		fixed (WAVEFORMATEX* pFormat2 = &pFormat)
		{
			return IsFormatSupported(ShareMode, pFormat2, ppClosestMatch);
		}
	}

	public unsafe HRESULT IsFormatSupported(AUDCLNT_SHAREMODE ShareMode, WAVEFORMATEX* pFormat, [Optional] WAVEFORMATEX** ppClosestMatch)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, AUDCLNT_SHAREMODE, WAVEFORMATEX*, WAVEFORMATEX**, HRESULT>)lpVtbl[7])((IAudioClient*)Unsafe.AsPointer(ref this), ShareMode, pFormat, ppClosestMatch);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetMixFormat(out WAVEFORMATEX* ppDeviceFormat)
	{
		fixed (WAVEFORMATEX** ppDeviceFormat2 = &ppDeviceFormat)
		{
			return GetMixFormat(ppDeviceFormat2);
		}
	}

	public unsafe HRESULT GetMixFormat(WAVEFORMATEX** ppDeviceFormat)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, WAVEFORMATEX**, HRESULT>)lpVtbl[8])((IAudioClient*)Unsafe.AsPointer(ref this), ppDeviceFormat);
	}

	public unsafe HRESULT GetDevicePeriod([Optional] long* phnsDefaultDevicePeriod, [Optional] long* phnsMinimumDevicePeriod)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, long*, long*, HRESULT>)lpVtbl[9])((IAudioClient*)Unsafe.AsPointer(ref this), phnsDefaultDevicePeriod, phnsMinimumDevicePeriod);
	}

	public unsafe HRESULT Start()
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, HRESULT>)lpVtbl[10])((IAudioClient*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT Stop()
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, HRESULT>)lpVtbl[11])((IAudioClient*)Unsafe.AsPointer(ref this));
	}

	public unsafe HRESULT Reset()
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, HRESULT>)lpVtbl[12])((IAudioClient*)Unsafe.AsPointer(ref this));
	}

	[OverloadResolutionPriority(1)]
	internal HRESULT SetEventHandle(SafeHandle eventHandle)
	{
		bool success = false;
		try
		{
			if (eventHandle != null)
			{
				eventHandle.DangerousAddRef(ref success);
				HANDLE eventHandle2 = (HANDLE)eventHandle.DangerousGetHandle();
				return SetEventHandle(eventHandle2);
			}
			throw new ArgumentNullException("eventHandle");
		}
		finally
		{
			if (success)
			{
				eventHandle.DangerousRelease();
			}
		}
	}

	public unsafe HRESULT SetEventHandle(HANDLE eventHandle)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, HANDLE, HRESULT>)lpVtbl[13])((IAudioClient*)Unsafe.AsPointer(ref this), eventHandle);
	}

	[OverloadResolutionPriority(1)]
	internal unsafe HRESULT GetService(in Guid riid, out void* ppv)
	{
		fixed (void** ppv2 = &ppv)
		{
			fixed (Guid* riid2 = &riid)
			{
				return GetService(riid2, ppv2);
			}
		}
	}

	public unsafe HRESULT GetService(Guid* riid, void** ppv)
	{
		return ((delegate* unmanaged[Stdcall]<IAudioClient*, Guid*, void**, HRESULT>)lpVtbl[14])((IAudioClient*)Unsafe.AsPointer(ref this), riid, ppv);
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
