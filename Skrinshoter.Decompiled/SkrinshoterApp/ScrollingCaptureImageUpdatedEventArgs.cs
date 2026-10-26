using System;

namespace SkrinshoterApp;

public sealed class ScrollingCaptureImageUpdatedEventArgs : EventArgs
{
	public int ModifiedFromRow { get; }

	public ScrollingCaptureImageUpdatedEventArgs(int modifiedFromRow)
	{
		ModifiedFromRow = modifiedFromRow;
	}
}
