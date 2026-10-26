using System;

namespace AudioCapture;

[Flags]
internal enum BufferFlags
{
	DATA_DISCONTINUITY = 1,
	SILENT = 2,
	TIMESTAMP_ERROR = 4
}
