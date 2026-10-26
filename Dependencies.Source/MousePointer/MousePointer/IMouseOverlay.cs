using System;
using System.Drawing;
using AudioVideoCore;

namespace MousePointer;

public interface IMouseOverlay : IDisposable
{
	void Draw(RawImage image, Point imagePosition, MousePointerInfo mousePointerInfo);
}
