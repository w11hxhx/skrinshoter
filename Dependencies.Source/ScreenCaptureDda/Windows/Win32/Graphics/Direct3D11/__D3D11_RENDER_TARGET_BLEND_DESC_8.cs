using System;
using System.Runtime.CompilerServices;

namespace Windows.Win32.Graphics.Direct3D11;

internal struct __D3D11_RENDER_TARGET_BLEND_DESC_8
{
	private const int SpanLength = 8;

	internal D3D11_RENDER_TARGET_BLEND_DESC _0;

	internal D3D11_RENDER_TARGET_BLEND_DESC _1;

	internal D3D11_RENDER_TARGET_BLEND_DESC _2;

	internal D3D11_RENDER_TARGET_BLEND_DESC _3;

	internal D3D11_RENDER_TARGET_BLEND_DESC _4;

	internal D3D11_RENDER_TARGET_BLEND_DESC _5;

	internal D3D11_RENDER_TARGET_BLEND_DESC _6;

	internal D3D11_RENDER_TARGET_BLEND_DESC _7;

	internal readonly int Length => 8;

	internal unsafe readonly void CopyTo(Span<D3D11_RENDER_TARGET_BLEND_DESC> target, int length = 8)
	{
		fixed (D3D11_RENDER_TARGET_BLEND_DESC* pointer = &_0)
		{
			ReadOnlySpan<D3D11_RENDER_TARGET_BLEND_DESC> readOnlySpan = new ReadOnlySpan<D3D11_RENDER_TARGET_BLEND_DESC>(pointer, 8);
			readOnlySpan = readOnlySpan.Slice(0, length);
			readOnlySpan.CopyTo(target);
		}
	}

	internal unsafe readonly D3D11_RENDER_TARGET_BLEND_DESC[] ToArray(int length = 8)
	{
		fixed (D3D11_RENDER_TARGET_BLEND_DESC* pointer = &_0)
		{
			ReadOnlySpan<D3D11_RENDER_TARGET_BLEND_DESC> readOnlySpan = new ReadOnlySpan<D3D11_RENDER_TARGET_BLEND_DESC>(pointer, 8);
			readOnlySpan = readOnlySpan.Slice(0, length);
			return readOnlySpan.ToArray();
		}
	}

	public unsafe static implicit operator __D3D11_RENDER_TARGET_BLEND_DESC_8(ReadOnlySpan<D3D11_RENDER_TARGET_BLEND_DESC> value)
	{
		Unsafe.SkipInit<__D3D11_RENDER_TARGET_BLEND_DESC_8>(out var value2);
		Span<D3D11_RENDER_TARGET_BLEND_DESC> destination = new Span<D3D11_RENDER_TARGET_BLEND_DESC>(&value2._0, 8);
		value.CopyTo(destination);
		int length = value.Length;
		destination.Slice(length, 8 - length).Clear();
		return value2;
	}
}
