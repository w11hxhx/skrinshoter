using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace Common;

public sealed class UnmanagedMemoryManager<T> : MemoryManager<T> where T : unmanaged
{
	private unsafe readonly T* _pointer;

	private readonly int _length;

	public unsafe UnmanagedMemoryManager(Span<T> span)
	{
		fixed (T* reference = &MemoryMarshal.GetReference(span))
		{
			_pointer = reference;
			_length = span.Length;
		}
	}

	public unsafe UnmanagedMemoryManager(T* pointer, int length)
	{
		if (length < 0)
		{
			throw new ArgumentOutOfRangeException("length");
		}
		_pointer = pointer;
		_length = length;
	}

	public unsafe UnmanagedMemoryManager(IntPtr pointer, int length)
		: this((T*)pointer.ToPointer(), length)
	{
	}

	public unsafe override Span<T> GetSpan()
	{
		return new Span<T>(_pointer, _length);
	}

	public unsafe override MemoryHandle Pin(int elementIndex = 0)
	{
		if (elementIndex < 0 || elementIndex >= _length)
		{
			throw new ArgumentOutOfRangeException("elementIndex");
		}
		return new MemoryHandle(_pointer + elementIndex);
	}

	public override void Unpin()
	{
	}

	protected override void Dispose(bool disposing)
	{
	}
}
