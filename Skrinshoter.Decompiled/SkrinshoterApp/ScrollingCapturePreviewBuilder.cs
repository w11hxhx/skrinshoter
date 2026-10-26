using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SkrinshoterApp;

public sealed class ScrollingCapturePreviewBuilder : IDisposable
{
	public readonly struct PreviewSurfaceInfo
	{
		public int Level { get; }

		public int Width { get; }

		public int CapacityHeight { get; }

		public int ContentHeight { get; }

		public int Version { get; }

		public PreviewSurfaceInfo(int level, int width, int capacityHeight, int contentHeight, int version)
		{
			Level = level;
			Width = width;
			CapacityHeight = capacityHeight;
			ContentHeight = contentHeight;
			Version = version;
		}
	}

	private interface IPreviewLevel
	{
		int Width { get; }

		int Height { get; }

		int MaxHeight { get; }

		object ReadSyncRoot { get; }

		unsafe byte* GetRowPointerUnchecked(int y);
	}

	private sealed class SourcePreviewLevel : IPreviewLevel
	{
		private readonly ScrollingCaptureImage source;

		public int Width => source.Width;

		public int Height => source.Height;

		public int MaxHeight => 32768;

		public object ReadSyncRoot => source.SyncRoot;

		public SourcePreviewLevel(ScrollingCaptureImage source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			this.source = source;
		}

		public unsafe byte* GetRowPointerUnchecked(int y)
		{
			return source.GetRowPointerUnchecked(y);
		}
	}

	private sealed class DownsampleStage : IDisposable
	{
		private readonly int inWidth;

		private readonly int outWidth;

		private readonly int inStride;

		private readonly int outStride;

		private IntPtr pendingRow;

		private int pendingInputRowIndex;

		private bool hasPendingRow;

		private bool pendingFlushed;

		private IntPtr tempOutRow;

		private PyramidLevel storage;

		public DownsampleStage(int inWidth, int outWidth, PyramidLevel storage)
		{
			if (inWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("inWidth");
			}
			if (outWidth <= 0)
			{
				throw new ArgumentOutOfRangeException("outWidth");
			}
			this.inWidth = inWidth;
			this.outWidth = outWidth;
			inStride = inWidth * 4;
			outStride = outWidth * 4;
			pendingRow = Marshal.AllocHGlobal(inStride);
			tempOutRow = Marshal.AllocHGlobal(outStride);
			this.storage = storage;
			pendingInputRowIndex = -1;
			hasPendingRow = false;
			pendingFlushed = false;
		}

		public void Dispose()
		{
			if (pendingRow != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(pendingRow);
			}
			if (tempOutRow != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(tempOutRow);
			}
			pendingRow = IntPtr.Zero;
			tempOutRow = IntPtr.Zero;
			storage = null;
		}

		public void SetStorage(PyramidLevel storage)
		{
			this.storage = storage;
		}

		public void ResetState()
		{
			pendingInputRowIndex = -1;
			hasPendingRow = false;
			pendingFlushed = false;
		}

		public unsafe bool TryProcessRow(int inputRowIndex, byte* inputRow, out int outputRowIndex, out byte* outputRow)
		{
			if (inputRow == null)
			{
				throw new ArgumentNullException("inputRow");
			}
			if (inputRowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("inputRowIndex");
			}
			outputRowIndex = 0;
			outputRow = null;
			if ((inputRowIndex & 1) == 0)
			{
				Buffer.MemoryCopy(inputRow, (void*)pendingRow, inStride, inStride);
				pendingInputRowIndex = inputRowIndex;
				hasPendingRow = true;
				pendingFlushed = false;
				return false;
			}
			if (!hasPendingRow || pendingInputRowIndex != inputRowIndex - 1)
			{
				Buffer.MemoryCopy(inputRow, (void*)pendingRow, inStride, inStride);
				pendingInputRowIndex = inputRowIndex;
				hasPendingRow = true;
				pendingFlushed = false;
				return false;
			}
			outputRowIndex = inputRowIndex / 2;
			byte* ptr = (byte*)((storage == null) ? ((void*)tempOutRow) : storage.GetRowPointerForWrite(outputRowIndex));
			byte* row = (byte*)(void*)pendingRow;
			DownsampleRow2x2(row, inputRow, inWidth, ptr, outWidth);
			pendingInputRowIndex = -1;
			hasPendingRow = false;
			pendingFlushed = false;
			outputRow = ptr;
			return true;
		}

		public unsafe bool TryFlushPending(int lastInputRowIndex, out int outputRowIndex, out byte* outputRow)
		{
			if (lastInputRowIndex < 0)
			{
				throw new ArgumentOutOfRangeException("lastInputRowIndex");
			}
			outputRowIndex = 0;
			outputRow = null;
			if (!hasPendingRow)
			{
				return false;
			}
			if (pendingInputRowIndex != lastInputRowIndex)
			{
				return false;
			}
			if (pendingFlushed)
			{
				return false;
			}
			outputRowIndex = lastInputRowIndex / 2;
			byte* ptr = (byte*)((storage == null) ? ((void*)tempOutRow) : storage.GetRowPointerForWrite(outputRowIndex));
			byte* ptr2 = (byte*)(void*)pendingRow;
			DownsampleRow2x2(ptr2, ptr2, inWidth, ptr, outWidth);
			pendingFlushed = true;
			outputRow = ptr;
			return true;
		}

		private unsafe static void DownsampleRow2x2(byte* row0, byte* row1, int inWidth, byte* dstRow, int outWidth)
		{
			for (int i = 0; i < outWidth; i++)
			{
				int num = i * 2;
				int num2 = num + 1;
				if (num >= inWidth)
				{
					num = inWidth - 1;
				}
				if (num2 >= inWidth)
				{
					num2 = num;
				}
				uint num3 = *(uint*)(row0 + num * 4);
				uint num4 = *(uint*)(row0 + num2 * 4);
				uint num5 = *(uint*)(row1 + num * 4);
				uint num6 = *(uint*)(row1 + num2 * 4);
				int num7 = (int)((num3 & 0xFF) + (num4 & 0xFF) + (num5 & 0xFF) + (num6 & 0xFF) + 2) >> 2;
				int num8 = (int)(((num3 >> 8) & 0xFF) + ((num4 >> 8) & 0xFF) + ((num5 >> 8) & 0xFF) + ((num6 >> 8) & 0xFF) + 2) >> 2;
				int num9 = (int)(((num3 >> 16) & 0xFF) + ((num4 >> 16) & 0xFF) + ((num5 >> 16) & 0xFF) + ((num6 >> 16) & 0xFF) + 2) >> 2;
				byte* num10 = dstRow + i * 4;
				*num10 = (byte)num7;
				num10[1] = (byte)num8;
				num10[2] = (byte)num9;
				num10[3] = byte.MaxValue;
			}
		}
	}

	private sealed class PyramidLevel : IPreviewLevel, IDisposable
	{
		private readonly int stride;

		private readonly int blockHeight;

		private readonly List<IntPtr> blocks;

		private int height;

		public int Width { get; }

		public int Height => height;

		public int MaxHeight { get; }

		public object ReadSyncRoot => null;

		public PyramidLevel(int width, int maxHeight, int blockHeight)
		{
			if (width <= 0)
			{
				throw new ArgumentOutOfRangeException("width");
			}
			if (maxHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("maxHeight");
			}
			if (blockHeight <= 0)
			{
				throw new ArgumentOutOfRangeException("blockHeight");
			}
			Width = width;
			MaxHeight = maxHeight;
			stride = width * 4;
			this.blockHeight = blockHeight;
			blocks = new List<IntPtr>();
			height = 0;
		}

		public void Dispose()
		{
			for (int i = 0; i < blocks.Count; i++)
			{
				if (blocks[i] != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(blocks[i]);
				}
			}
			blocks.Clear();
		}

		public unsafe byte* GetRowPointerForWrite(int y)
		{
			if (y < 0)
			{
				throw new ArgumentOutOfRangeException("y");
			}
			if (y >= MaxHeight)
			{
				throw new ArgumentOutOfRangeException("y", "Level height exceeds MaxHeight.");
			}
			EnsureBlocksForRow(y);
			if (y >= height)
			{
				height = y + 1;
			}
			return GetRowPointerUnchecked(y);
		}

		public unsafe byte* GetRowPointerUnchecked(int y)
		{
			int num = y / blockHeight;
			int num2 = y - num * blockHeight;
			return (byte*)(void*)blocks[num] + num2 * stride;
		}

		private void EnsureBlocksForRow(int y)
		{
			int num = y / blockHeight;
			while (blocks.Count <= num)
			{
				blocks.Add(Marshal.AllocHGlobal(blockHeight * stride));
			}
		}
	}

	private const int LevelBlockHeight = 1024;

	private readonly object sync = new object();

	private readonly ScrollingCaptureImage source;

	private int currentLevel;

	private readonly int maxLevel;

	private readonly int[] levelWidths;

	private readonly int[] levelMaxHeights;

	private readonly DownsampleStage[] stages;

	private IPreviewLevel currentStorage;

	private int processedSourceHeight;

	private int sourceHeight;

	private int dirtyTop;

	private int dirtyBottom;

	private int version;

	public int Version
	{
		get
		{
			lock (sync)
			{
				return version;
			}
		}
	}

	public ScrollingCapturePreviewBuilder(ScrollingCaptureImage source)
	{
		if (source == null)
		{
			throw new ArgumentNullException("source");
		}
		this.source = source;
		maxLevel = CalculateMaxLevel(source.Width, 32768);
		levelWidths = new int[maxLevel + 1];
		levelMaxHeights = new int[maxLevel + 1];
		stages = new DownsampleStage[maxLevel + 1];
		levelWidths[0] = source.Width;
		levelMaxHeights[0] = 32768;
		for (int i = 1; i <= maxLevel; i++)
		{
			levelWidths[i] = CeilDiv2(levelWidths[i - 1]);
			levelMaxHeights[i] = CeilDiv2(levelMaxHeights[i - 1]);
		}
		currentLevel = 0;
		currentStorage = new SourcePreviewLevel(source);
		processedSourceHeight = 0;
		sourceHeight = 0;
		ClearDirty();
		version = 1;
	}

	public void Dispose()
	{
		lock (sync)
		{
			for (int i = 1; i <= maxLevel; i++)
			{
				stages[i]?.Dispose();
			}
			if (currentStorage is PyramidLevel pyramidLevel)
			{
				pyramidLevel.Dispose();
			}
			currentStorage = null;
		}
	}

	public void OnSourceUpdated(int modifiedFromRow)
	{
		lock (sync)
		{
			lock (source.SyncRoot)
			{
				int height = source.Height;
				bool flag = modifiedFromRow < processedSourceHeight;
				int num = (flag ? modifiedFromRow : processedSourceHeight);
				if (height <= num)
				{
					return;
				}
				sourceHeight = height;
				processedSourceHeight = height;
				if (currentLevel == 0)
				{
					MarkDirty(num, height - num);
					return;
				}
				if (flag)
				{
					int num2 = 1 << currentLevel;
					num = num / num2 * num2;
					for (int i = 1; i <= currentLevel; i++)
					{
						stages[i]?.ResetState();
					}
				}
				ProcessNewSourceRows(num, height);
				FlushTailToCurrentLevel();
			}
		}
	}

	public PreviewSurfaceInfo EnsureSurfaceForTarget(int targetWidth, int targetHeight)
	{
		if (targetWidth <= 0 || targetHeight <= 0)
		{
			return default;
		}
		lock (sync)
		{
			EnsureLowerLevelIfNeeded(targetWidth, targetHeight);
			int width = currentStorage.Width;
			int capacityHeight = levelMaxHeights[currentLevel];
			int currentContentHeight = GetCurrentContentHeight();
			return new PreviewSurfaceInfo(currentLevel, width, capacityHeight, currentContentHeight, version);
		}
	}

	public unsafe bool TryCopyDirtyRows(byte* dstBase, int dstStride, int dstHeight, out int dirtyTopResult, out int dirtyHeightResult, out int contentHeight, out int contentWidth)
	{
		if (dstBase == null)
		{
			throw new ArgumentNullException("dstBase");
		}
		if (dstStride <= 0)
		{
			throw new ArgumentOutOfRangeException("dstStride");
		}
		if (dstHeight <= 0)
		{
			throw new ArgumentOutOfRangeException("dstHeight");
		}
		lock (sync)
		{
			IPreviewLevel previewLevel = currentStorage;
			contentWidth = previewLevel.Width;
			int num = dirtyTop;
			int num2 = dirtyBottom;
			if (num2 <= num)
			{
				dirtyTopResult = 0;
				dirtyHeightResult = 0;
				contentHeight = GetCurrentContentHeight();
				ClearDirty();
				return true;
			}
			object readSyncRoot = previewLevel.ReadSyncRoot;
			if (readSyncRoot != null)
			{
				lock (readSyncRoot)
				{
					contentHeight = previewLevel.Height;
					return CopyDirtyRowsCore(previewLevel, dstBase, dstStride, dstHeight, contentHeight, num, num2, out dirtyTopResult, out dirtyHeightResult);
				}
			}
			contentHeight = previewLevel.Height;
			return CopyDirtyRowsCore(previewLevel, dstBase, dstStride, dstHeight, contentHeight, num, num2, out dirtyTopResult, out dirtyHeightResult);
		}
	}

	public unsafe bool TryCopyAllRows(byte* dstBase, int dstStride, int dstHeight, out int contentHeight, out int contentWidth)
	{
		if (dstBase == null)
		{
			throw new ArgumentNullException("dstBase");
		}
		if (dstStride <= 0)
		{
			throw new ArgumentOutOfRangeException("dstStride");
		}
		if (dstHeight <= 0)
		{
			throw new ArgumentOutOfRangeException("dstHeight");
		}
		lock (sync)
		{
			IPreviewLevel previewLevel = currentStorage;
			contentWidth = previewLevel.Width;
			object readSyncRoot = previewLevel.ReadSyncRoot;
			if (readSyncRoot != null)
			{
				lock (readSyncRoot)
				{
					contentHeight = previewLevel.Height;
					CopyAllRowsCore(previewLevel, dstBase, dstStride, dstHeight, contentHeight);
					return true;
				}
			}
			contentHeight = previewLevel.Height;
			CopyAllRowsCore(previewLevel, dstBase, dstStride, dstHeight, contentHeight);
			return true;
		}
	}

	private void EnsureLowerLevelIfNeeded(int targetWidth, int targetHeight)
	{
		while (currentLevel < maxLevel)
		{
			int num = currentLevel + 1;
			int num2 = levelWidths[num];
			int num3 = CeilDiv2(GetCurrentContentHeight());
			if (num2 >= targetWidth && num3 >= targetHeight)
			{
				SwitchDownOneLevel();
				continue;
			}
			break;
		}
	}

	private void SwitchDownOneLevel()
	{
		int num = currentLevel + 1;
		if (num <= maxLevel)
		{
			PyramidLevel storage = new PyramidLevel(levelWidths[num], levelMaxHeights[num], 1024);
			DownsampleStage downsampleStage = stages[num];
			if (downsampleStage == null)
			{
				downsampleStage = new DownsampleStage(levelWidths[num - 1], levelWidths[num], storage);
				stages[num] = downsampleStage;
			}
			else
			{
				downsampleStage.SetStorage(storage);
			}
			ReplayFromLevelToStage(currentStorage, downsampleStage);
			if (currentStorage is PyramidLevel pyramidLevel)
			{
				stages[currentLevel]?.SetStorage(null);
				pyramidLevel.Dispose();
			}
			currentLevel = num;
			currentStorage = storage;
			version++;
			ClearDirty();
			int currentContentHeight = GetCurrentContentHeight();
			if (currentContentHeight > 0)
			{
				MarkDirty(0, currentContentHeight);
			}
		}
	}

	private void ReplayFromLevelToStage(IPreviewLevel inputLevel, DownsampleStage stage)
	{
		stage.ResetState();
		int height = inputLevel.Height;
		if (height <= 0)
		{
			return;
		}
		object readSyncRoot = inputLevel.ReadSyncRoot;
		if (readSyncRoot != null)
		{
			lock (readSyncRoot)
			{
				ReplayRowsCore(inputLevel, stage, height);
				return;
			}
		}
		ReplayRowsCore(inputLevel, stage, height);
	}

	private unsafe static void ReplayRowsCore(IPreviewLevel inputLevel, DownsampleStage stage, int height)
	{
		int outputRowIndex;
		byte* outputRow;
		for (int i = 0; i < height; i++)
		{
			byte* rowPointerUnchecked = inputLevel.GetRowPointerUnchecked(i);
			stage.TryProcessRow(i, rowPointerUnchecked, out outputRowIndex, out outputRow);
		}
		stage.TryFlushPending(height - 1, out outputRowIndex, out outputRow);
	}

	private unsafe void ProcessNewSourceRows(int startY, int endY)
	{
		for (int i = startY; i < endY; i++)
		{
			byte* outputRow = source.GetRowPointerUnchecked(i);
			int outputRowIndex = i;
			bool flag = true;
			for (int j = 1; j <= currentLevel; j++)
			{
				DownsampleStage downsampleStage = stages[j];
				if (downsampleStage == null)
				{
					flag = false;
					break;
				}
				if (!downsampleStage.TryProcessRow(outputRowIndex, outputRow, out outputRowIndex, out outputRow))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				MarkDirty(outputRowIndex, 1);
			}
		}
	}

	private unsafe void FlushTailToCurrentLevel()
	{
		if (currentLevel <= 0)
		{
			return;
		}
		for (int i = 1; i <= currentLevel; i++)
		{
			int heightAtLevel = GetHeightAtLevel(sourceHeight, i - 1);
			if ((heightAtLevel & 1) == 0)
			{
				continue;
			}
			int num = heightAtLevel - 1;
			if (num < 0)
			{
				continue;
			}
			DownsampleStage downsampleStage = stages[i];
			if (downsampleStage == null)
			{
				break;
			}
			if (!downsampleStage.TryFlushPending(num, out var outputRowIndex, out var outputRow))
			{
				continue;
			}
			bool flag = true;
			for (int j = i + 1; j <= currentLevel; j++)
			{
				DownsampleStage downsampleStage2 = stages[j];
				if (downsampleStage2 == null)
				{
					flag = false;
					break;
				}
				if (!downsampleStage2.TryProcessRow(outputRowIndex, outputRow, out outputRowIndex, out outputRow))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				MarkDirty(outputRowIndex, 1);
			}
		}
	}

	private unsafe bool CopyDirtyRowsCore(IPreviewLevel storage, byte* dstBase, int dstStride, int dstHeight, int contentHeight, int top, int bottom, out int dirtyTopResult, out int dirtyHeightResult)
	{
		if (top < 0)
		{
			top = 0;
		}
		if (bottom > contentHeight)
		{
			bottom = contentHeight;
		}
		if (bottom > dstHeight)
		{
			bottom = dstHeight;
		}
		if (bottom <= top)
		{
			dirtyTopResult = 0;
			dirtyHeightResult = 0;
			ClearDirty();
			return true;
		}
		CopyRowsFromLevelStorage(storage, top, bottom - top, dstBase, dstStride);
		dirtyTopResult = top;
		dirtyHeightResult = bottom - top;
		ClearDirty();
		return true;
	}

	private unsafe void CopyAllRowsCore(IPreviewLevel storage, byte* dstBase, int dstStride, int dstHeight, int contentHeight)
	{
		int num = contentHeight;
		if (num > dstHeight)
		{
			num = dstHeight;
		}
		if (num <= 0)
		{
			ClearDirty();
			return;
		}
		CopyRowsFromLevelStorage(storage, 0, num, dstBase, dstStride);
		ClearDirty();
	}

	private unsafe static void CopyRowsFromLevelStorage(IPreviewLevel storage, int startRow, int rowCount, byte* dstBase, int dstStride)
	{
		int num = storage.Width * 4;
		for (int i = 0; i < rowCount; i++)
		{
			int num2 = startRow + i;
			byte* rowPointerUnchecked = storage.GetRowPointerUnchecked(num2);
			byte* destination = dstBase + (long)num2 * (long)dstStride;
			int num3 = num;
			if (num3 > dstStride)
			{
				num3 = dstStride;
			}
			Buffer.MemoryCopy(rowPointerUnchecked, destination, dstStride, num3);
		}
	}

	private void MarkDirty(int startRow, int rowCount)
	{
		if (rowCount > 0)
		{
			int num = startRow + rowCount;
			if (startRow < dirtyTop)
			{
				dirtyTop = startRow;
			}
			if (num > dirtyBottom)
			{
				dirtyBottom = num;
			}
		}
	}

	private void ClearDirty()
	{
		dirtyTop = int.MaxValue;
		dirtyBottom = 0;
	}

	private int GetCurrentContentHeight()
	{
		IPreviewLevel previewLevel = currentStorage;
		object readSyncRoot = previewLevel.ReadSyncRoot;
		if (readSyncRoot == null)
		{
			return previewLevel.Height;
		}
		lock (readSyncRoot)
		{
			return previewLevel.Height;
		}
	}

	private static int CalculateMaxLevel(int width, int height)
	{
		int num = 0;
		while (width > 1 || height > 1)
		{
			width = CeilDiv2(width);
			height = CeilDiv2(height);
			num++;
		}
		if (num < 1)
		{
			num = 1;
		}
		return num;
	}

	private static int CeilDiv2(int value)
	{
		if (value <= 0)
		{
			return 0;
		}
		return (value + 1) / 2;
	}

	private static int GetHeightAtLevel(int height, int level)
	{
		for (int i = 0; i < level; i++)
		{
			height = CeilDiv2(height);
		}
		return height;
	}
}
