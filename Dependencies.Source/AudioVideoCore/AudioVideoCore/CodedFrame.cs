using System;
using Common;

namespace AudioVideoCore;

public class CodedFrame : BaseDisposable
{
	public Memory<byte> Data { get; }

	public FrameTimestamp Timestamp { get; set; }

	public bool IsKey { get; }

	public CodedFrame(Memory<byte> data, FrameTimestamp timestamp, bool isKey = false)
	{
		Data = data;
		Timestamp = timestamp;
		IsKey = isKey;
	}
}
