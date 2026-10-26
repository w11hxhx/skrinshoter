using System;
using System.Buffers;
using System.Collections.Generic;

namespace Common;

public class MemoryHandleCollection : IDisposable
{
	private readonly IEnumerable<MemoryHandle> memoryHandles;

	public MemoryHandleCollection(IEnumerable<MemoryHandle> memoryHandles)
	{
		this.memoryHandles = memoryHandles ?? throw new ArgumentNullException("memoryHandles");
	}

	public void Dispose()
	{
		foreach (MemoryHandle memoryHandle in memoryHandles)
		{
			memoryHandle.Dispose();
		}
	}
}
