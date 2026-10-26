using System;
using System.Buffers;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Interop;
using System.Windows.Threading;
using AudioVideoCore;
using Common;
using Microsoft.Extensions.Logging;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.System.Memory;
using Windows.Win32.System.Ole;
using Windows.Win32.UI.Shell;

namespace SkrinshoterApp;

public class ClipboardService
{
	private sealed class MoveableGlobalMemory : IDisposable
	{
		private bool ownershipTransferred;

		public HGLOBAL Handle { get; private set; }

		public MoveableGlobalMemory(nuint bytes)
		{
			Handle = PInvoke.GlobalAlloc(GLOBAL_ALLOC_FLAGS.GMEM_MOVEABLE, bytes);
			if (Handle.IsNull)
			{
				throw new WinApiException($"GlobalAlloc(GMEM_MOVEABLE, {bytes})", new Win32Exception());
			}
		}

		public void TransferOwnership()
		{
			ownershipTransferred = true;
		}

		public GlobalLockScope Lock()
		{
			return new GlobalLockScope(Handle);
		}

		public void Dispose()
		{
			if (!Handle.IsNull)
			{
				if (!ownershipTransferred)
				{
					PInvoke.GlobalFree(Handle);
				}
				Handle = default;
			}
		}
	}

	private sealed class GlobalLockScope : IDisposable
	{
		private HGLOBAL handle;

		public unsafe void* Pointer { get; private set; }

		public unsafe GlobalLockScope(HGLOBAL handle)
		{
			if (handle.IsNull)
			{
				throw new ArgumentNullException("handle");
			}
			this.handle = handle;
			Pointer = PInvoke.GlobalLock(handle);
			if (Pointer == null)
			{
				throw new WinApiException("GlobalLock()", new Win32Exception());
			}
		}

		public unsafe void Dispose()
		{
			if (!handle.IsNull)
			{
				PInvoke.GlobalUnlock(handle);
				handle = default;
				Pointer = null;
			}
		}
	}

	private sealed class OpenClipboardScope : IDisposable
	{
		private const int StartDelayMs = 5;

		private const int MaxDelayMs = 100;

		private const int TimeoutMs = 1000;

		public OpenClipboardScope(HWND owner)
		{
			if (owner.IsNull)
			{
				throw new ArgumentNullException("owner");
			}
			OpenClipboard(owner);
			try
			{
				if (!PInvoke.EmptyClipboard())
				{
					throw new WinApiException("EmptyClipboard()", new Win32Exception());
				}
			}
			catch
			{
				Dispose();
				throw;
			}
		}

		private static void OpenClipboard(HWND owner)
		{
			if ((bool)PInvoke.OpenClipboard(owner))
			{
				return;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			int num = 5;
			while (stopwatch.ElapsedMilliseconds < 1000)
			{
				Thread.Sleep(num);
				if ((bool)PInvoke.OpenClipboard(owner))
				{
					return;
				}
				if (num < 100)
				{
					num = Math.Min(100, num * 2);
				}
			}
			Win32Exception inner = new Win32Exception();
			string clipboardHolder = GetClipboardHolder();
			throw new WinApiException("OpenClipboard() failed. Holder=\"" + clipboardHolder + "\".", inner);
		}

		private unsafe static string GetClipboardHolder()
		{
			try
			{
				HWND openClipboardWindow = PInvoke.GetOpenClipboardWindow();
				if (openClipboardWindow.IsNull)
				{
					return "<none>";
				}
				uint num = 0u;
				PInvoke.GetWindowThreadProcessId(openClipboardWindow, &num);
				if (num == 0)
				{
					return "<unknown>";
				}
				return Process.GetProcessById((int)num).ProcessName;
			}
			catch
			{
				return "<unknown>";
			}
		}

		public void Dispose()
		{
			PInvoke.CloseClipboard();
		}
	}

	private readonly Dispatcher dispatcher;

	private readonly ILogger logger;

	private readonly HwndSource clipboardOwnerSource;

	public ClipboardService(Dispatcher dispatcher, ILogger<ClipboardService> logger)
	{
		this.dispatcher = dispatcher;
		this.logger = logger;
		clipboardOwnerSource = dispatcher.InvokeIfRequired((Func<HwndSource>)CreateClipboardOwnerSource);
	}

	public void SetText(string text)
	{
		if (text == null)
		{
			throw new ArgumentNullException("text");
		}
		try
		{
			MoveableGlobalMemory memory = CreateUnicodeTextMemory(text);
			try
			{
				dispatcher.InvokeIfRequired(() =>
				{
					using (new OpenClipboardScope((HWND)clipboardOwnerSource.Handle))
					{
						SetClipboardData(CLIPBOARD_FORMAT.CF_UNICODETEXT, memory);
						memory.TransferOwnership();
					}
				});
			}
			finally
			{
				if (memory != null)
				{
					((IDisposable)memory).Dispose();
				}
			}
		}
		catch (Exception innerException)
		{
			throw new ClipboardException("Failed to copy text to the clipboard.", innerException);
		}
	}

	public void SetImage(RawImage image)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (!image.Format.PixelFormat.In(PixelFormat.BGRA, PixelFormat.BGRX))
		{
			throw new ArgumentException($"Unsupported pixel format {image.Format.PixelFormat}.", "image");
		}
		try
		{
			MoveableGlobalMemory memory = CreateDibMemory(image);
			try
			{
				dispatcher.InvokeIfRequired(() =>
				{
					using (new OpenClipboardScope((HWND)clipboardOwnerSource.Handle))
					{
						SetClipboardData(CLIPBOARD_FORMAT.CF_DIB, memory);
						memory.TransferOwnership();
					}
				});
			}
			finally
			{
				if (memory != null)
				{
					((IDisposable)memory).Dispose();
				}
			}
		}
		catch (Exception innerException)
		{
			throw new ClipboardException("Failed to copy image to the clipboard.", innerException);
		}
	}

	public void SetFile(string path)
	{
		if (path == null)
		{
			throw new ArgumentNullException("path");
		}
		try
		{
			path = Path.GetFullPath(path);
			MoveableGlobalMemory memory = CreateHDropMemory(path);
			try
			{
				dispatcher.InvokeIfRequired(() =>
				{
					using (new OpenClipboardScope((HWND)clipboardOwnerSource.Handle))
					{
						SetClipboardData(CLIPBOARD_FORMAT.CF_HDROP, memory);
						memory.TransferOwnership();
					}
				});
			}
			finally
			{
				if (memory != null)
				{
					((IDisposable)memory).Dispose();
				}
			}
		}
		catch (Exception innerException)
		{
			throw new ClipboardException("Failed to copy file to the clipboard.", innerException);
		}
	}

	private static HwndSource CreateClipboardOwnerSource()
	{
		try
		{
			HwndSourceParameters parameters = new HwndSourceParameters("Skrinshoter.ClipboardOwner");
			parameters.ParentWindow = HWND.HWND_MESSAGE;
			parameters.WindowStyle = 0;
			parameters.Width = 0;
			parameters.Height = 0;
			HwndSource hwndSource = new HwndSource(parameters);
			if (hwndSource.Handle == IntPtr.Zero)
			{
				hwndSource.Dispose();
				throw new InvalidOperationException("HwndSource.Handle is zero.");
			}
			return hwndSource;
		}
		catch (Exception innerException)
		{
			throw new ClipboardException("Failed to create clipboard owner source.", innerException);
		}
	}

	private unsafe MoveableGlobalMemory CreateUnicodeTextMemory(string text)
	{
		nuint num = (nuint)((text.Length + 1) * 2);
		MoveableGlobalMemory moveableGlobalMemory = new MoveableGlobalMemory(num);
		try
		{
			using (GlobalLockScope globalLockScope = moveableGlobalMemory.Lock())
			{
				char* pointer = (char*)globalLockScope.Pointer;
				fixed (char* source = text)
				{
					long sourceBytesToCopy = (long)text.Length * 2L;
					Buffer.MemoryCopy(source, pointer, (long)num, sourceBytesToCopy);
				}
				pointer[text.Length] = '\0';
			}
			return moveableGlobalMemory;
		}
		catch
		{
			moveableGlobalMemory.Dispose();
			throw;
		}
	}

	private unsafe MoveableGlobalMemory CreateDibMemory(RawImage image)
	{
		int width = image.Format.Width;
		int height = image.Format.Height;
		int num = image.Stride[0];
		int num2 = width * 4;
		int num3 = -num2;
		int num4 = sizeof(BITMAPINFOHEADER);
		int num5 = num2 * height;
		int num6 = num4 + num5;
		MoveableGlobalMemory moveableGlobalMemory = new MoveableGlobalMemory((nuint)num6);
		try
		{
			using (GlobalLockScope globalLockScope = moveableGlobalMemory.Lock())
			{
				BITMAPINFOHEADER* pointer = (BITMAPINFOHEADER*)globalLockScope.Pointer;
				*pointer = default;
				pointer->biSize = (uint)num4;
				pointer->biWidth = width;
				pointer->biHeight = height;
				pointer->biPlanes = 1;
				pointer->biBitCount = 32;
				pointer->biCompression = 0u;
				pointer->biSizeImage = (uint)num5;
				using MemoryHandle memoryHandle = image.Data[0].Pin();
				byte* ptr = (byte*)memoryHandle.Pointer;
				byte* ptr2 = (byte*)globalLockScope.Pointer + num6 + num3;
				for (int i = 0; i < height; i++)
				{
					Buffer.MemoryCopy(ptr, ptr2, num2, num2);
					ptr += num;
					ptr2 += num3;
				}
			}
			return moveableGlobalMemory;
		}
		catch
		{
			moveableGlobalMemory.Dispose();
			throw;
		}
	}

	private unsafe MoveableGlobalMemory CreateHDropMemory(string path)
	{
		int num = sizeof(DROPFILES);
		int num2 = (path.Length + 2) * 2;
		MoveableGlobalMemory moveableGlobalMemory = new MoveableGlobalMemory((nuint)(num + num2));
		try
		{
			using (GlobalLockScope globalLockScope = moveableGlobalMemory.Lock())
			{
				DROPFILES* pointer = (DROPFILES*)globalLockScope.Pointer;
				*pointer = default;
				pointer->pFiles = (uint)num;
				pointer->fWide = true;
				char* ptr = (char*)((byte*)globalLockScope.Pointer + num);
				fixed (char* source = path)
				{
					long sourceBytesToCopy = (long)path.Length * 2L;
					Buffer.MemoryCopy(source, ptr, num2, sourceBytesToCopy);
				}
				ptr[path.Length] = '\0';
				ptr[path.Length + 1] = '\0';
			}
			return moveableGlobalMemory;
		}
		catch
		{
			moveableGlobalMemory.Dispose();
			throw;
		}
	}

	private unsafe void SetClipboardData(CLIPBOARD_FORMAT format, MoveableGlobalMemory memory)
	{
		if (PInvoke.SetClipboardData((uint)format, (HANDLE)memory.Handle.Value).IsNull)
		{
			throw new WinApiException($"SetClipboardData({format})", new Win32Exception());
		}
	}
}
