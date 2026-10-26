using System;

namespace SkrinshoterApp.SkrApi.RealTimeFileUpload;

internal class FileChunk
{
	public int Index { get; }

	public long Offset { get; }

	public ReadOnlyMemory<byte> Data { get; }

	public FileChunk(int index, long offset, ReadOnlyMemory<byte> data)
	{
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException("index");
		}
		if (offset < 0)
		{
			throw new ArgumentOutOfRangeException("offset");
		}
		Index = index;
		Offset = offset;
		Data = data;
	}
}
