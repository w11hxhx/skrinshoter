using System.CodeDom.Compiler;

namespace Windows.Win32.Media.MediaFoundation;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum MF2DBuffer_LockFlags
{
	MF2DBuffer_LockFlags_LockTypeMask = 3,
	MF2DBuffer_LockFlags_Read = 1,
	MF2DBuffer_LockFlags_Write = 2,
	MF2DBuffer_LockFlags_ReadWrite = MF2DBuffer_LockFlags_LockTypeMask,
	MF2DBuffer_LockFlags_ForceDWORD = int.MaxValue
}
