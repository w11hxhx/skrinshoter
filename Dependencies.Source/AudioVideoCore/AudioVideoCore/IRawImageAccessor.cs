using System;

namespace AudioVideoCore;

public interface IRawImageAccessor : IDisposable
{
	RawImage GetImage();
}
