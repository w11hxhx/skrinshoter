using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.UI.WindowsAndMessaging;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum IMAGE_FLAGS : uint
{
	LR_CREATEDIBSECTION = 0x2000u,
	LR_DEFAULTCOLOR = 0u,
	LR_DEFAULTSIZE = 0x40u,
	LR_LOADFROMFILE = 0x10u,
	LR_LOADMAP3DCOLORS = 0x1000u,
	LR_LOADTRANSPARENT = 0x20u,
	LR_MONOCHROME = 1u,
	LR_SHARED = 0x8000u,
	LR_VGACOLOR = 0x80u,
	LR_COPYDELETEORG = 8u,
	LR_COPYFROMRESOURCE = 0x4000u,
	LR_COPYRETURNORG = 4u
}
