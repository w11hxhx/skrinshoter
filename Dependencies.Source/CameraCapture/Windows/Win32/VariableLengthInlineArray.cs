using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Windows.Win32;

internal struct VariableLengthInlineArray<T, TBlittable> where T : unmanaged where TBlittable : unmanaged
{
	private TBlittable _e0;

	[UnscopedRef]
	internal ref T e0 => ref Unsafe.As<TBlittable, T>(ref _e0);

	internal ref T this[int index]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[UnscopedRef]
		get
		{
			return ref Unsafe.Add(ref e0, index);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[UnscopedRef]
	internal unsafe Span<T> AsSpan(int length)
	{
		fixed (T* ptr = &e0)
		{
			void* pointer = ptr;
			return new Span<T>(pointer, length);
		}
	}
}
internal struct VariableLengthInlineArray<T> where T : unmanaged
{
	internal T e0;

	internal ref T this[int index]
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[UnscopedRef]
		get
		{
			return ref Unsafe.Add(ref e0, index);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	[UnscopedRef]
	internal unsafe Span<T> AsSpan(int length)
	{
		fixed (T* ptr = &e0)
		{
			void* pointer = ptr;
			return new Span<T>(pointer, length);
		}
	}
}
