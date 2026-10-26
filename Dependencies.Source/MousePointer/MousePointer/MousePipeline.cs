using System;
using System.Collections.Generic;
using System.Drawing;
using AudioVideoCore;
using Common;

namespace MousePointer;

public sealed class MousePipeline : SafeDisposable, IMouseOverlay, IDisposable
{
	private readonly List<IMouseOverlay> stages = new List<IMouseOverlay>();

	public void AddStage(IMouseOverlay stage)
	{
		if (stage == null)
		{
			throw new ArgumentNullException("stage");
		}
		lock (InternalDisposeLockObject)
		{
			if (!IsDisposed)
			{
				stages.Add(stage);
			}
		}
	}

	public void Draw(RawImage image, Point imagePosition, MousePointerInfo mousePointerInfo)
	{
		if (image == null)
		{
			throw new ArgumentNullException("image");
		}
		if (mousePointerInfo == null)
		{
			throw new ArgumentNullException("mousePointerInfo");
		}
		lock (InternalDisposeLockObject)
		{
			if (IsDisposed)
			{
				return;
			}
			foreach (IMouseOverlay stage in stages)
			{
				stage.Draw(image, imagePosition, mousePointerInfo);
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposing)
		{
			return;
		}
		foreach (IMouseOverlay stage in stages)
		{
			stage.Dispose();
		}
		stages.Clear();
	}
}
