using System;
using System.CodeDom.Compiler;

namespace Windows.Win32.UI.WindowsAndMessaging;

[Flags]
[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal enum REGISTER_NOTIFICATION_FLAGS : uint
{
	DEVICE_NOTIFY_SERVICE_HANDLE = 1u,
	DEVICE_NOTIFY_CALLBACK = 2u,
	DEVICE_NOTIFY_WINDOW_HANDLE = 0u,
	DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = 4u
}
