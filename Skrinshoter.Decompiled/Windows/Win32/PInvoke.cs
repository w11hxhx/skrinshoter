using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Dwm;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.Security.Credentials;
using Windows.Win32.Storage.FileSystem;
using Windows.Win32.System.Com;
using Windows.Win32.System.Memory;
using Windows.Win32.System.SystemInformation;
using Windows.Win32.UI.Accessibility;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

namespace Windows.Win32;

[GeneratedCode("Microsoft.Windows.CsWin32", "0.3.217+533aa1bddf.RR")]
internal static class PInvoke
{
	internal const uint WM_DDE_FIRST = 992u;

	internal const uint WM_DDE_INITIATE = 992u;

	internal const uint WM_DDE_TERMINATE = 993u;

	internal const uint WM_DDE_ADVISE = 994u;

	internal const uint WM_DDE_UNADVISE = 995u;

	internal const uint WM_DDE_ACK = 996u;

	internal const uint WM_DDE_DATA = 997u;

	internal const uint WM_DDE_REQUEST = 998u;

	internal const uint WM_DDE_POKE = 999u;

	internal const uint WM_DDE_EXECUTE = 1000u;

	internal const uint WM_DDE_LAST = 1000u;

	internal const uint WM_IME_REPORT = 640u;

	internal const uint WM_WNT_CONVERTREQUESTEX = 265u;

	internal const uint WM_CONVERTREQUEST = 266u;

	internal const uint WM_CONVERTRESULT = 267u;

	internal const uint WM_INTERIM = 268u;

	internal const uint WM_IMEKEYDOWN = 656u;

	internal const uint WM_IMEKEYUP = 657u;

	internal const uint WM_CTLCOLOR = 25u;

	internal const uint WM_MOUSEHOVER = 673u;

	internal const uint WM_MOUSELEAVE = 675u;

	internal const uint WM_CHOOSEFONT_GETLOGFONT = 1025u;

	internal const uint WM_CHOOSEFONT_SETLOGFONT = 1125u;

	internal const uint WM_CHOOSEFONT_SETFLAGS = 1126u;

	internal const uint WM_PSD_FULLPAGERECT = 1025u;

	internal const uint WM_PSD_MINMARGINRECT = 1026u;

	internal const uint WM_PSD_MARGINRECT = 1027u;

	internal const uint WM_PSD_GREEKTEXTRECT = 1028u;

	internal const uint WM_PSD_ENVSTAMPRECT = 1029u;

	internal const uint WM_PSD_YAFULLPAGERECT = 1030u;

	internal const uint WM_CPL_LAUNCH = 2024u;

	internal const uint WM_CPL_LAUNCHED = 2025u;

	internal const uint WM_TABLET_DEFBASE = 704u;

	internal const uint WM_TABLET_MAXOFFSET = 32u;

	internal const uint WM_TABLET_ADDED = 712u;

	internal const uint WM_TABLET_DELETED = 713u;

	internal const uint WM_TABLET_FLICK = 715u;

	internal const uint WM_TABLET_QUERYSYSTEMGESTURESTATUS = 716u;

	internal const uint WM_CONTEXTMENU = 123u;

	internal const uint WM_UNICHAR = 265u;

	internal const uint WM_PRINTCLIENT = 792u;

	internal const uint WM_NOTIFY = 78u;

	internal const uint WM_DEVICECHANGE = 537u;

	internal const uint WM_NULL = 0u;

	internal const uint WM_CREATE = 1u;

	internal const uint WM_DESTROY = 2u;

	internal const uint WM_MOVE = 3u;

	internal const uint WM_SIZE = 5u;

	internal const uint WM_ACTIVATE = 6u;

	internal const uint WM_SETFOCUS = 7u;

	internal const uint WM_KILLFOCUS = 8u;

	internal const uint WM_ENABLE = 10u;

	internal const uint WM_SETREDRAW = 11u;

	internal const uint WM_SETTEXT = 12u;

	internal const uint WM_GETTEXT = 13u;

	internal const uint WM_GETTEXTLENGTH = 14u;

	internal const uint WM_PAINT = 15u;

	internal const uint WM_CLOSE = 16u;

	internal const uint WM_QUERYENDSESSION = 17u;

	internal const uint WM_QUERYOPEN = 19u;

	internal const uint WM_ENDSESSION = 22u;

	internal const uint WM_QUIT = 18u;

	internal const uint WM_ERASEBKGND = 20u;

	internal const uint WM_SYSCOLORCHANGE = 21u;

	internal const uint WM_SHOWWINDOW = 24u;

	internal const uint WM_WININICHANGE = 26u;

	internal const uint WM_SETTINGCHANGE = 26u;

	internal const uint WM_DEVMODECHANGE = 27u;

	internal const uint WM_ACTIVATEAPP = 28u;

	internal const uint WM_FONTCHANGE = 29u;

	internal const uint WM_TIMECHANGE = 30u;

	internal const uint WM_CANCELMODE = 31u;

	internal const uint WM_SETCURSOR = 32u;

	internal const uint WM_MOUSEACTIVATE = 33u;

	internal const uint WM_CHILDACTIVATE = 34u;

	internal const uint WM_QUEUESYNC = 35u;

	internal const uint WM_GETMINMAXINFO = 36u;

	internal const uint WM_PAINTICON = 38u;

	internal const uint WM_ICONERASEBKGND = 39u;

	internal const uint WM_NEXTDLGCTL = 40u;

	internal const uint WM_SPOOLERSTATUS = 42u;

	internal const uint WM_DRAWITEM = 43u;

	internal const uint WM_MEASUREITEM = 44u;

	internal const uint WM_DELETEITEM = 45u;

	internal const uint WM_VKEYTOITEM = 46u;

	internal const uint WM_CHARTOITEM = 47u;

	internal const uint WM_SETFONT = 48u;

	internal const uint WM_GETFONT = 49u;

	internal const uint WM_SETHOTKEY = 50u;

	internal const uint WM_GETHOTKEY = 51u;

	internal const uint WM_QUERYDRAGICON = 55u;

	internal const uint WM_COMPAREITEM = 57u;

	internal const uint WM_GETOBJECT = 61u;

	internal const uint WM_COMPACTING = 65u;

	internal const uint WM_COMMNOTIFY = 68u;

	internal const uint WM_WINDOWPOSCHANGING = 70u;

	internal const uint WM_WINDOWPOSCHANGED = 71u;

	internal const uint WM_POWER = 72u;

	internal const uint WM_COPYDATA = 74u;

	internal const uint WM_CANCELJOURNAL = 75u;

	internal const uint WM_INPUTLANGCHANGEREQUEST = 80u;

	internal const uint WM_INPUTLANGCHANGE = 81u;

	internal const uint WM_TCARD = 82u;

	internal const uint WM_HELP = 83u;

	internal const uint WM_USERCHANGED = 84u;

	internal const uint WM_NOTIFYFORMAT = 85u;

	internal const uint WM_STYLECHANGING = 124u;

	internal const uint WM_STYLECHANGED = 125u;

	internal const uint WM_DISPLAYCHANGE = 126u;

	internal const uint WM_GETICON = 127u;

	internal const uint WM_SETICON = 128u;

	internal const uint WM_NCCREATE = 129u;

	internal const uint WM_NCDESTROY = 130u;

	internal const uint WM_NCCALCSIZE = 131u;

	internal const uint WM_NCHITTEST = 132u;

	internal const uint WM_NCPAINT = 133u;

	internal const uint WM_NCACTIVATE = 134u;

	internal const uint WM_GETDLGCODE = 135u;

	internal const uint WM_SYNCPAINT = 136u;

	internal const uint WM_NCMOUSEMOVE = 160u;

	internal const uint WM_NCLBUTTONDOWN = 161u;

	internal const uint WM_NCLBUTTONUP = 162u;

	internal const uint WM_NCLBUTTONDBLCLK = 163u;

	internal const uint WM_NCRBUTTONDOWN = 164u;

	internal const uint WM_NCRBUTTONUP = 165u;

	internal const uint WM_NCRBUTTONDBLCLK = 166u;

	internal const uint WM_NCMBUTTONDOWN = 167u;

	internal const uint WM_NCMBUTTONUP = 168u;

	internal const uint WM_NCMBUTTONDBLCLK = 169u;

	internal const uint WM_NCXBUTTONDOWN = 171u;

	internal const uint WM_NCXBUTTONUP = 172u;

	internal const uint WM_NCXBUTTONDBLCLK = 173u;

	internal const uint WM_INPUT_DEVICE_CHANGE = 254u;

	internal const uint WM_INPUT = 255u;

	internal const uint WM_KEYFIRST = 256u;

	internal const uint WM_KEYDOWN = 256u;

	internal const uint WM_KEYUP = 257u;

	internal const uint WM_CHAR = 258u;

	internal const uint WM_DEADCHAR = 259u;

	internal const uint WM_SYSKEYDOWN = 260u;

	internal const uint WM_SYSKEYUP = 261u;

	internal const uint WM_SYSCHAR = 262u;

	internal const uint WM_SYSDEADCHAR = 263u;

	internal const uint WM_KEYLAST = 265u;

	internal const uint WM_IME_STARTCOMPOSITION = 269u;

	internal const uint WM_IME_ENDCOMPOSITION = 270u;

	internal const uint WM_IME_COMPOSITION = 271u;

	internal const uint WM_IME_KEYLAST = 271u;

	internal const uint WM_INITDIALOG = 272u;

	internal const uint WM_COMMAND = 273u;

	internal const uint WM_SYSCOMMAND = 274u;

	internal const uint WM_TIMER = 275u;

	internal const uint WM_HSCROLL = 276u;

	internal const uint WM_VSCROLL = 277u;

	internal const uint WM_INITMENU = 278u;

	internal const uint WM_INITMENUPOPUP = 279u;

	internal const uint WM_GESTURE = 281u;

	internal const uint WM_GESTURENOTIFY = 282u;

	internal const uint WM_MENUSELECT = 287u;

	internal const uint WM_MENUCHAR = 288u;

	internal const uint WM_ENTERIDLE = 289u;

	internal const uint WM_MENURBUTTONUP = 290u;

	internal const uint WM_MENUDRAG = 291u;

	internal const uint WM_MENUGETOBJECT = 292u;

	internal const uint WM_UNINITMENUPOPUP = 293u;

	internal const uint WM_MENUCOMMAND = 294u;

	internal const uint WM_CHANGEUISTATE = 295u;

	internal const uint WM_UPDATEUISTATE = 296u;

	internal const uint WM_QUERYUISTATE = 297u;

	internal const uint WM_CTLCOLORMSGBOX = 306u;

	internal const uint WM_CTLCOLOREDIT = 307u;

	internal const uint WM_CTLCOLORLISTBOX = 308u;

	internal const uint WM_CTLCOLORBTN = 309u;

	internal const uint WM_CTLCOLORDLG = 310u;

	internal const uint WM_CTLCOLORSCROLLBAR = 311u;

	internal const uint WM_CTLCOLORSTATIC = 312u;

	internal const uint WM_MOUSEFIRST = 512u;

	internal const uint WM_MOUSEMOVE = 512u;

	internal const uint WM_LBUTTONDOWN = 513u;

	internal const uint WM_LBUTTONUP = 514u;

	internal const uint WM_LBUTTONDBLCLK = 515u;

	internal const uint WM_RBUTTONDOWN = 516u;

	internal const uint WM_RBUTTONUP = 517u;

	internal const uint WM_RBUTTONDBLCLK = 518u;

	internal const uint WM_MBUTTONDOWN = 519u;

	internal const uint WM_MBUTTONUP = 520u;

	internal const uint WM_MBUTTONDBLCLK = 521u;

	internal const uint WM_MOUSEWHEEL = 522u;

	internal const uint WM_XBUTTONDOWN = 523u;

	internal const uint WM_XBUTTONUP = 524u;

	internal const uint WM_XBUTTONDBLCLK = 525u;

	internal const uint WM_MOUSEHWHEEL = 526u;

	internal const uint WM_MOUSELAST = 526u;

	internal const uint WM_PARENTNOTIFY = 528u;

	internal const uint WM_ENTERMENULOOP = 529u;

	internal const uint WM_EXITMENULOOP = 530u;

	internal const uint WM_NEXTMENU = 531u;

	internal const uint WM_SIZING = 532u;

	internal const uint WM_CAPTURECHANGED = 533u;

	internal const uint WM_MOVING = 534u;

	internal const uint WM_POWERBROADCAST = 536u;

	internal const uint WM_MDICREATE = 544u;

	internal const uint WM_MDIDESTROY = 545u;

	internal const uint WM_MDIACTIVATE = 546u;

	internal const uint WM_MDIRESTORE = 547u;

	internal const uint WM_MDINEXT = 548u;

	internal const uint WM_MDIMAXIMIZE = 549u;

	internal const uint WM_MDITILE = 550u;

	internal const uint WM_MDICASCADE = 551u;

	internal const uint WM_MDIICONARRANGE = 552u;

	internal const uint WM_MDIGETACTIVE = 553u;

	internal const uint WM_MDISETMENU = 560u;

	internal const uint WM_ENTERSIZEMOVE = 561u;

	internal const uint WM_EXITSIZEMOVE = 562u;

	internal const uint WM_DROPFILES = 563u;

	internal const uint WM_MDIREFRESHMENU = 564u;

	internal const uint WM_POINTERDEVICECHANGE = 568u;

	internal const uint WM_POINTERDEVICEINRANGE = 569u;

	internal const uint WM_POINTERDEVICEOUTOFRANGE = 570u;

	internal const uint WM_TOUCH = 576u;

	internal const uint WM_NCPOINTERUPDATE = 577u;

	internal const uint WM_NCPOINTERDOWN = 578u;

	internal const uint WM_NCPOINTERUP = 579u;

	internal const uint WM_POINTERUPDATE = 581u;

	internal const uint WM_POINTERDOWN = 582u;

	internal const uint WM_POINTERUP = 583u;

	internal const uint WM_POINTERENTER = 585u;

	internal const uint WM_POINTERLEAVE = 586u;

	internal const uint WM_POINTERACTIVATE = 587u;

	internal const uint WM_POINTERCAPTURECHANGED = 588u;

	internal const uint WM_TOUCHHITTESTING = 589u;

	internal const uint WM_POINTERWHEEL = 590u;

	internal const uint WM_POINTERHWHEEL = 591u;

	internal const uint WM_POINTERROUTEDTO = 593u;

	internal const uint WM_POINTERROUTEDAWAY = 594u;

	internal const uint WM_POINTERROUTEDRELEASED = 595u;

	internal const uint WM_IME_SETCONTEXT = 641u;

	internal const uint WM_IME_NOTIFY = 642u;

	internal const uint WM_IME_CONTROL = 643u;

	internal const uint WM_IME_COMPOSITIONFULL = 644u;

	internal const uint WM_IME_SELECT = 645u;

	internal const uint WM_IME_CHAR = 646u;

	internal const uint WM_IME_REQUEST = 648u;

	internal const uint WM_IME_KEYDOWN = 656u;

	internal const uint WM_IME_KEYUP = 657u;

	internal const uint WM_NCMOUSEHOVER = 672u;

	internal const uint WM_NCMOUSELEAVE = 674u;

	internal const uint WM_WTSSESSION_CHANGE = 689u;

	internal const uint WM_TABLET_FIRST = 704u;

	internal const uint WM_TABLET_LAST = 735u;

	internal const uint WM_DPICHANGED = 736u;

	internal const uint WM_DPICHANGED_BEFOREPARENT = 738u;

	internal const uint WM_DPICHANGED_AFTERPARENT = 739u;

	internal const uint WM_GETDPISCALEDSIZE = 740u;

	internal const uint WM_CUT = 768u;

	internal const uint WM_COPY = 769u;

	internal const uint WM_PASTE = 770u;

	internal const uint WM_CLEAR = 771u;

	internal const uint WM_UNDO = 772u;

	internal const uint WM_RENDERFORMAT = 773u;

	internal const uint WM_RENDERALLFORMATS = 774u;

	internal const uint WM_DESTROYCLIPBOARD = 775u;

	internal const uint WM_DRAWCLIPBOARD = 776u;

	internal const uint WM_PAINTCLIPBOARD = 777u;

	internal const uint WM_VSCROLLCLIPBOARD = 778u;

	internal const uint WM_SIZECLIPBOARD = 779u;

	internal const uint WM_ASKCBFORMATNAME = 780u;

	internal const uint WM_CHANGECBCHAIN = 781u;

	internal const uint WM_HSCROLLCLIPBOARD = 782u;

	internal const uint WM_QUERYNEWPALETTE = 783u;

	internal const uint WM_PALETTEISCHANGING = 784u;

	internal const uint WM_PALETTECHANGED = 785u;

	internal const uint WM_HOTKEY = 786u;

	internal const uint WM_PRINT = 791u;

	internal const uint WM_APPCOMMAND = 793u;

	internal const uint WM_THEMECHANGED = 794u;

	internal const uint WM_CLIPBOARDUPDATE = 797u;

	internal const uint WM_DWMCOMPOSITIONCHANGED = 798u;

	internal const uint WM_DWMNCRENDERINGCHANGED = 799u;

	internal const uint WM_DWMCOLORIZATIONCOLORCHANGED = 800u;

	internal const uint WM_DWMWINDOWMAXIMIZEDCHANGE = 801u;

	internal const uint WM_DWMSENDICONICTHUMBNAIL = 803u;

	internal const uint WM_DWMSENDICONICLIVEPREVIEWBITMAP = 806u;

	internal const uint WM_GETTITLEBARINFOEX = 831u;

	internal const uint WM_HANDHELDFIRST = 856u;

	internal const uint WM_HANDHELDLAST = 863u;

	internal const uint WM_AFXFIRST = 864u;

	internal const uint WM_AFXLAST = 895u;

	internal const uint WM_PENWINFIRST = 896u;

	internal const uint WM_PENWINLAST = 911u;

	internal const uint WM_APP = 32768u;

	internal const uint WM_USER = 1024u;

	internal const uint WM_TOOLTIPDISMISS = 837u;

	internal const uint WM_FI_FILENAME = 900u;

	internal const uint WM_CODEC_ONEPASS_CBR = 1u;

	internal const uint WM_CODEC_ONEPASS_VBR = 2u;

	internal const uint WM_CODEC_TWOPASS_CBR = 4u;

	internal const uint WM_CODEC_TWOPASS_VBR_UNCONSTRAINED = 8u;

	internal const uint WM_CODEC_TWOPASS_VBR_PEAKCONSTRAINED = 16u;

	internal const uint WM_CAP_START = 1024u;

	internal const uint WM_CAP_UNICODE_START = 1124u;

	internal const uint WM_CAP_GET_CAPSTREAMPTR = 1025u;

	internal const uint WM_CAP_SET_CALLBACK_ERRORW = 1126u;

	internal const uint WM_CAP_SET_CALLBACK_STATUSW = 1127u;

	internal const uint WM_CAP_SET_CALLBACK_ERRORA = 1026u;

	internal const uint WM_CAP_SET_CALLBACK_STATUSA = 1027u;

	internal const uint WM_CAP_SET_CALLBACK_ERROR = 1126u;

	internal const uint WM_CAP_SET_CALLBACK_STATUS = 1127u;

	internal const uint WM_CAP_SET_CALLBACK_YIELD = 1028u;

	internal const uint WM_CAP_SET_CALLBACK_FRAME = 1029u;

	internal const uint WM_CAP_SET_CALLBACK_VIDEOSTREAM = 1030u;

	internal const uint WM_CAP_SET_CALLBACK_WAVESTREAM = 1031u;

	internal const uint WM_CAP_GET_USER_DATA = 1032u;

	internal const uint WM_CAP_SET_USER_DATA = 1033u;

	internal const uint WM_CAP_DRIVER_CONNECT = 1034u;

	internal const uint WM_CAP_DRIVER_DISCONNECT = 1035u;

	internal const uint WM_CAP_DRIVER_GET_NAMEA = 1036u;

	internal const uint WM_CAP_DRIVER_GET_VERSIONA = 1037u;

	internal const uint WM_CAP_DRIVER_GET_NAMEW = 1136u;

	internal const uint WM_CAP_DRIVER_GET_VERSIONW = 1137u;

	internal const uint WM_CAP_DRIVER_GET_NAME = 1136u;

	internal const uint WM_CAP_DRIVER_GET_VERSION = 1137u;

	internal const uint WM_CAP_DRIVER_GET_CAPS = 1038u;

	internal const uint WM_CAP_FILE_SET_CAPTURE_FILEA = 1044u;

	internal const uint WM_CAP_FILE_GET_CAPTURE_FILEA = 1045u;

	internal const uint WM_CAP_FILE_SAVEASA = 1047u;

	internal const uint WM_CAP_FILE_SAVEDIBA = 1049u;

	internal const uint WM_CAP_FILE_SET_CAPTURE_FILEW = 1144u;

	internal const uint WM_CAP_FILE_GET_CAPTURE_FILEW = 1145u;

	internal const uint WM_CAP_FILE_SAVEASW = 1147u;

	internal const uint WM_CAP_FILE_SAVEDIBW = 1149u;

	internal const uint WM_CAP_FILE_SET_CAPTURE_FILE = 1144u;

	internal const uint WM_CAP_FILE_GET_CAPTURE_FILE = 1145u;

	internal const uint WM_CAP_FILE_SAVEAS = 1147u;

	internal const uint WM_CAP_FILE_SAVEDIB = 1149u;

	internal const uint WM_CAP_FILE_ALLOCATE = 1046u;

	internal const uint WM_CAP_FILE_SET_INFOCHUNK = 1048u;

	internal const uint WM_CAP_EDIT_COPY = 1054u;

	internal const uint WM_CAP_SET_AUDIOFORMAT = 1059u;

	internal const uint WM_CAP_GET_AUDIOFORMAT = 1060u;

	internal const uint WM_CAP_DLG_VIDEOFORMAT = 1065u;

	internal const uint WM_CAP_DLG_VIDEOSOURCE = 1066u;

	internal const uint WM_CAP_DLG_VIDEODISPLAY = 1067u;

	internal const uint WM_CAP_GET_VIDEOFORMAT = 1068u;

	internal const uint WM_CAP_SET_VIDEOFORMAT = 1069u;

	internal const uint WM_CAP_DLG_VIDEOCOMPRESSION = 1070u;

	internal const uint WM_CAP_SET_PREVIEW = 1074u;

	internal const uint WM_CAP_SET_OVERLAY = 1075u;

	internal const uint WM_CAP_SET_PREVIEWRATE = 1076u;

	internal const uint WM_CAP_SET_SCALE = 1077u;

	internal const uint WM_CAP_GET_STATUS = 1078u;

	internal const uint WM_CAP_SET_SCROLL = 1079u;

	internal const uint WM_CAP_GRAB_FRAME = 1084u;

	internal const uint WM_CAP_GRAB_FRAME_NOSTOP = 1085u;

	internal const uint WM_CAP_SEQUENCE = 1086u;

	internal const uint WM_CAP_SEQUENCE_NOFILE = 1087u;

	internal const uint WM_CAP_SET_SEQUENCE_SETUP = 1088u;

	internal const uint WM_CAP_GET_SEQUENCE_SETUP = 1089u;

	internal const uint WM_CAP_SET_MCI_DEVICEA = 1090u;

	internal const uint WM_CAP_GET_MCI_DEVICEA = 1091u;

	internal const uint WM_CAP_SET_MCI_DEVICEW = 1190u;

	internal const uint WM_CAP_GET_MCI_DEVICEW = 1191u;

	internal const uint WM_CAP_SET_MCI_DEVICE = 1190u;

	internal const uint WM_CAP_GET_MCI_DEVICE = 1191u;

	internal const uint WM_CAP_STOP = 1092u;

	internal const uint WM_CAP_ABORT = 1093u;

	internal const uint WM_CAP_SINGLE_FRAME_OPEN = 1094u;

	internal const uint WM_CAP_SINGLE_FRAME_CLOSE = 1095u;

	internal const uint WM_CAP_SINGLE_FRAME = 1096u;

	internal const uint WM_CAP_PAL_OPENA = 1104u;

	internal const uint WM_CAP_PAL_SAVEA = 1105u;

	internal const uint WM_CAP_PAL_OPENW = 1204u;

	internal const uint WM_CAP_PAL_SAVEW = 1205u;

	internal const uint WM_CAP_PAL_OPEN = 1204u;

	internal const uint WM_CAP_PAL_SAVE = 1205u;

	internal const uint WM_CAP_PAL_PASTE = 1106u;

	internal const uint WM_CAP_PAL_AUTOCREATE = 1107u;

	internal const uint WM_CAP_PAL_MANUALCREATE = 1108u;

	internal const uint WM_CAP_SET_CALLBACK_CAPCONTROL = 1109u;

	internal const uint WM_CAP_UNICODE_END = 1205u;

	internal const uint WM_CAP_END = 1205u;

	internal const uint WM_SampleExtension_ContentType_Size = 1u;

	internal const uint WM_SampleExtension_PixelAspectRatio_Size = 2u;

	internal const uint WM_SampleExtension_Timecode_Size = 14u;

	internal const uint WM_SampleExtension_SampleDuration_Size = 2u;

	internal const uint WM_SampleExtension_ChromaLocation_Size = 1u;

	internal const uint WM_SampleExtension_ColorSpaceInfo_Size = 3u;

	internal const uint WM_CT_REPEAT_FIRST_FIELD = 16u;

	internal const uint WM_CT_BOTTOM_FIELD_FIRST = 32u;

	internal const uint WM_CT_TOP_FIELD_FIRST = 64u;

	internal const uint WM_CT_INTERLACED = 128u;

	internal const uint WM_CL_INTERLACED420 = 0u;

	internal const uint WM_CL_PROGRESSIVE420 = 1u;

	internal const uint WM_MAX_VIDEO_STREAMS = 63u;

	internal const uint WM_MAX_STREAMS = 63u;

	internal static readonly Guid WM_SampleExtensionGUID_OutputCleanPoint = new Guid(4146740335u, 28340, 20156, 177, 146, 9, 173, 151, 89, 232, 40);

	internal static readonly Guid WM_SampleExtensionGUID_Timecode = new Guid(966104556u, 34407, 20013, 143, 219, 152, 129, 76, 231, 108, 30);

	internal static readonly Guid WM_SampleExtensionGUID_ChromaLocation = new Guid(1281019040u, 37494, 19244, 158, 76, 160, 237, 239, 221, 33, 126);

	internal static readonly Guid WM_SampleExtensionGUID_ColorSpaceInfo = new Guid(4154120790u, 12523, 20267, 159, 122, 242, 75, 19, 154, 17, 87);

	internal static readonly Guid WM_SampleExtensionGUID_UserDataInfo = new Guid(1932244218, 30910, 17737, 153, 189, 2, 219, 26, 85, 183, 168);

	internal static readonly Guid WM_SampleExtensionGUID_FileName = new Guid(3781553166u, 6637, 17879, 180, 167, 37, 203, 209, 226, 142, 155);

	internal static readonly Guid WM_SampleExtensionGUID_ContentType = new Guid(3583040544u, 1980, 17260, 156, 247, 243, 187, 251, 241, 164, 220);

	internal static readonly Guid WM_SampleExtensionGUID_PixelAspectRatio = new Guid(455009620u, 63978, 19400, 130, 26, 55, 107, 116, 228, 196, 184);

	internal static readonly Guid WM_SampleExtensionGUID_SampleDuration = new Guid(3334313040u, 34431, 18695, 131, 163, 199, 121, 33, 183, 51, 173);

	internal static readonly Guid WM_SampleExtensionGUID_SampleProtectionSalt = new Guid(1409539822u, 47598, 17295, 170, 131, 56, 4, 153, 126, 86, 157);

	internal const uint WM_ADSPROP_NOTIFY_PAGEINIT = 2125u;

	internal const uint WM_ADSPROP_NOTIFY_PAGEHWND = 2126u;

	internal const uint WM_ADSPROP_NOTIFY_CHANGE = 2127u;

	internal const uint WM_ADSPROP_NOTIFY_APPLY = 2128u;

	internal const uint WM_ADSPROP_NOTIFY_SETFOCUS = 2129u;

	internal const uint WM_ADSPROP_NOTIFY_FOREGROUND = 2130u;

	internal const uint WM_ADSPROP_NOTIFY_EXIT = 2131u;

	internal const uint WM_ADSPROP_NOTIFY_ERROR = 2134u;

	internal const uint WM_RASDIALEVENT = 52429u;

	internal const uint MA_NOACTIVATE = 3u;

	internal const uint SC_SIZE = 61440u;

	internal const uint WMSZ_LEFT = 1u;

	internal const uint WMSZ_RIGHT = 2u;

	internal const uint WMSZ_TOP = 3u;

	internal const uint WMSZ_TOPLEFT = 4u;

	internal const uint WMSZ_TOPRIGHT = 5u;

	internal const uint WMSZ_BOTTOM = 6u;

	internal const uint WMSZ_BOTTOMLEFT = 7u;

	internal const uint WMSZ_BOTTOMRIGHT = 8u;

	internal const uint EVENT_SYSTEM_FOREGROUND = 3u;

	internal const uint EVENT_OBJECT_NAMECHANGE = 32780u;

	internal const uint WINEVENT_OUTOFCONTEXT = 0u;

	internal unsafe static BOOL CredRead(string TargetName, CRED_TYPE Type, out CREDENTIALW* Credential)
	{
		fixed (CREDENTIALW** credential = &Credential)
		{
			fixed (char* ptr = TargetName)
			{
				return CredRead(ptr, Type, 0u, credential);
			}
		}
	}

	[DllImport("ADVAPI32.dll", EntryPoint = "CredReadW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL CredRead(PCWSTR TargetName, CRED_TYPE Type, uint Flags, CREDENTIALW** Credential);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL CredWrite(in CREDENTIALW Credential, uint Flags)
	{
		fixed (CREDENTIALW* credential = &Credential)
		{
			return CredWrite(credential, Flags);
		}
	}

	[DllImport("ADVAPI32.dll", EntryPoint = "CredWriteW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL CredWrite(CREDENTIALW* Credential, uint Flags);

	[DllImport("ADVAPI32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern void CredFree(void* Buffer);

	internal unsafe static HRESULT DwmGetWindowAttribute(HWND hwnd, [MarshalAs(UnmanagedType.U4)] DWMWINDOWATTRIBUTE dwAttribute, void* pvAttribute, uint cbAttribute)
	{
		return LocalExternFunction(hwnd, dwAttribute, pvAttribute, cbAttribute);
		[DllImport("dwmapi.dll", EntryPoint = "DwmGetWindowAttribute", ExactSpelling = true)]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		unsafe static extern HRESULT LocalExternFunction(HWND hwnd, [MarshalAs(UnmanagedType.U4)] DWMWINDOWATTRIBUTE dwAttribute, void* pvAttribute, uint cbAttribute);
	}

	internal unsafe static HRESULT DwmSetWindowAttribute(HWND hwnd, [MarshalAs(UnmanagedType.U4)] DWMWINDOWATTRIBUTE dwAttribute, void* pvAttribute, uint cbAttribute)
	{
		return LocalExternFunction(hwnd, dwAttribute, pvAttribute, cbAttribute);
		[DllImport("dwmapi.dll", EntryPoint = "DwmSetWindowAttribute", ExactSpelling = true)]
		[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
		unsafe static extern HRESULT LocalExternFunction(HWND hwnd, [MarshalAs(UnmanagedType.U4)] DWMWINDOWATTRIBUTE dwAttribute, void* pvAttribute, uint cbAttribute);
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL CloseHandle(HANDLE hObject);

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL FreeLibrary(HMODULE hLibModule);

	[OverloadResolutionPriority(1)]
	internal unsafe static FreeLibrarySafeHandle LoadLibrary(string lpLibFileName)
	{
		fixed (char* ptr = lpLibFileName)
		{
			return new FreeLibrarySafeHandle(LoadLibrary(ptr));
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "LoadLibraryW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMODULE LoadLibrary(PCWSTR lpLibFileName);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL MoveFileEx(string lpExistingFileName, string lpNewFileName, MOVE_FILE_FLAGS dwFlags)
	{
		fixed (char* ptr = lpNewFileName)
		{
			fixed (char* ptr2 = lpExistingFileName)
			{
				return MoveFileEx(ptr2, ptr, dwFlags);
			}
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "MoveFileExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL MoveFileEx(PCWSTR lpExistingFileName, PCWSTR lpNewFileName, MOVE_FILE_FLAGS dwFlags);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetDiskFreeSpaceEx(string lpDirectoryName, ulong* lpFreeBytesAvailableToCaller, ulong* lpTotalNumberOfBytes, ulong* lpTotalNumberOfFreeBytes)
	{
		fixed (char* ptr = lpDirectoryName)
		{
			return GetDiskFreeSpaceEx(ptr, lpFreeBytesAvailableToCaller, lpTotalNumberOfBytes, lpTotalNumberOfFreeBytes);
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "GetDiskFreeSpaceExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetDiskFreeSpaceEx(PCWSTR lpDirectoryName, [Optional] ulong* lpFreeBytesAvailableToCaller, [Optional] ulong* lpTotalNumberOfBytes, [Optional] ulong* lpTotalNumberOfFreeBytes);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetPhysicallyInstalledSystemMemory(out ulong TotalMemoryInKilobytes)
	{
		fixed (ulong* totalMemoryInKilobytes = &TotalMemoryInKilobytes)
		{
			return GetPhysicallyInstalledSystemMemory(totalMemoryInKilobytes);
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetPhysicallyInstalledSystemMemory(ulong* TotalMemoryInKilobytes);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer)
	{
		fixed (MEMORYSTATUSEX* lpBuffer2 = &lpBuffer)
		{
			return GlobalMemoryStatusEx(lpBuffer2);
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GlobalMemoryStatusEx(MEMORYSTATUSEX* lpBuffer);

	[OverloadResolutionPriority(1)]
	internal static GlobalFreeSafeHandle GlobalFree_SafeHandle(HGLOBAL hMem)
	{
		return new GlobalFreeSafeHandle(GlobalFree(hMem));
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HGLOBAL GlobalFree(HGLOBAL hMem);

	[OverloadResolutionPriority(1)]
	internal static GlobalFreeSafeHandle GlobalAlloc_SafeHandle(GLOBAL_ALLOC_FLAGS uFlags, nuint dwBytes)
	{
		return new GlobalFreeSafeHandle(GlobalAlloc(uFlags, dwBytes));
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HGLOBAL GlobalAlloc(GLOBAL_ALLOC_FLAGS uFlags, nuint dwBytes);

	[OverloadResolutionPriority(1)]
	internal unsafe static void* GlobalLock(SafeHandle hMem)
	{
		bool success = false;
		try
		{
			if (hMem != null)
			{
				hMem.DangerousAddRef(ref success);
				HGLOBAL hMem2 = (HGLOBAL)hMem.DangerousGetHandle();
				return GlobalLock(hMem2);
			}
			throw new ArgumentNullException("hMem");
		}
		finally
		{
			if (success)
			{
				hMem.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern void* GlobalLock(HGLOBAL hMem);

	[OverloadResolutionPriority(1)]
	internal static BOOL GlobalUnlock(SafeHandle hMem)
	{
		bool success = false;
		try
		{
			if (hMem != null)
			{
				hMem.DangerousAddRef(ref success);
				HGLOBAL hMem2 = (HGLOBAL)hMem.DangerousGetHandle();
				return GlobalUnlock(hMem2);
			}
			throw new ArgumentNullException("hMem");
		}
		finally
		{
			if (success)
			{
				hMem.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL GlobalUnlock(HGLOBAL hMem);

	[OverloadResolutionPriority(1)]
	internal unsafe static FreeLibrarySafeHandle GetModuleHandle(string lpModuleName)
	{
		fixed (char* ptr = lpModuleName)
		{
			return new FreeLibrarySafeHandle(GetModuleHandle(ptr), ownsHandle: false);
		}
	}

	[DllImport("KERNEL32.dll", EntryPoint = "GetModuleHandleW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMODULE GetModuleHandle(PCWSTR lpModuleName);

	[OverloadResolutionPriority(1)]
	internal unsafe static FARPROC GetProcAddress(SafeHandle hModule, string lpProcName)
	{
		bool success = false;
		try
		{
			fixed (byte* value = ((lpProcName != null) ? Encoding.Default.GetBytes(lpProcName) : null))
			{
				if (hModule != null)
				{
					hModule.DangerousAddRef(ref success);
					HMODULE hModule2 = (HMODULE)hModule.DangerousGetHandle();
					return GetProcAddress(hModule2, new PCSTR(value));
				}
				throw new ArgumentNullException("hModule");
			}
		}
		finally
		{
			if (success)
			{
				hModule.DangerousRelease();
			}
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern FARPROC GetProcAddress(HMODULE hModule, PCSTR lpProcName);

	internal unsafe static int GetUserDefaultGeoName(Span<char> geoName)
	{
		fixed (char* ptr = geoName)
		{
			return GetUserDefaultGeoName(ptr, geoName.Length);
		}
	}

	[DllImport("KERNEL32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int GetUserDefaultGeoName(PWSTR geoName, int geoNameCount);

	internal static HRESULT HRESULT_FROM_WIN32(WIN32_ERROR error)
	{
		return new HRESULT((int)((error == WIN32_ERROR.NO_ERROR) ? error : ((error & (WIN32_ERROR)0xFFFFu) | (WIN32_ERROR)0x80070000u)));
	}

	internal static HRESULT CoCreateInstance<T>(in Guid rclsid, object pUnkOuter, CLSCTX dwClsContext, out T ppv) where T : class
	{
		HRESULT result = CoCreateInstance(in rclsid, pUnkOuter, dwClsContext, typeof(T).GUID, out var ppv2);
		ppv = (T)ppv2;
		return result;
	}

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT CoCreateInstance(in Guid rclsid, object pUnkOuter, CLSCTX dwClsContext, in Guid riid, out object ppv)
	{
		fixed (Guid* riid2 = &riid)
		{
			fixed (Guid* rclsid2 = &rclsid)
			{
				return CoCreateInstance(rclsid2, pUnkOuter, dwClsContext, riid2, out ppv);
			}
		}
	}

	[DllImport("OLE32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT CoCreateInstance(Guid* rclsid, [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, CLSCTX dwClsContext, Guid* riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[DllImport("OLE32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern void CoTaskMemFree([Optional] void* pv);

	[OverloadResolutionPriority(1)]
	internal unsafe static HRESULT SHCreateItemFromParsingName(string pszPath, IBindCtx pbc, in Guid riid, out object ppv)
	{
		fixed (Guid* riid2 = &riid)
		{
			fixed (char* ptr = pszPath)
			{
				return SHCreateItemFromParsingName(ptr, pbc, riid2, out ppv);
			}
		}
	}

	[DllImport("SHELL32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HRESULT SHCreateItemFromParsingName(PCWSTR pszPath, IBindCtx pbc, Guid* riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppv);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL EnumDisplaySettings(string lpszDeviceName, ENUM_DISPLAY_SETTINGS_MODE iModeNum, ref DEVMODEW lpDevMode)
	{
		fixed (DEVMODEW* lpDevMode2 = &lpDevMode)
		{
			fixed (char* ptr = lpszDeviceName)
			{
				return EnumDisplaySettings(ptr, iModeNum, lpDevMode2);
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "EnumDisplaySettingsW", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL EnumDisplaySettings(PCWSTR lpszDeviceName, ENUM_DISPLAY_SETTINGS_MODE iModeNum, DEVMODEW* lpDevMode);

	[DllImport("USER32.dll", EntryPoint = "GetWindowLongW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int GetWindowLong(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex);

	[DllImport("USER32.dll", EntryPoint = "SetWindowLongW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int SetWindowLong(HWND hWnd, WINDOW_LONG_PTR_INDEX nIndex, int dwNewLong);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL SetWindowDisplayAffinity(HWND hWnd, WINDOW_DISPLAY_AFFINITY dwAffinity);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL SetWindowPos(HWND hWnd, HWND hWndInsertAfter, int X, int Y, int cx, int cy, SET_WINDOW_POS_FLAGS uFlags);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetWindowRect(HWND hWnd, out RECT lpRect)
	{
		fixed (RECT* lpRect2 = &lpRect)
		{
			return GetWindowRect(hWnd, lpRect2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetWindowRect(HWND hWnd, RECT* lpRect);

	[DllImport("USER32.dll", EntryPoint = "SendMessageW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern LRESULT SendMessage(HWND hWnd, uint Msg, WPARAM wParam, LPARAM lParam);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL UnhookWindowsHookEx(HHOOK hhk);

	[OverloadResolutionPriority(1)]
	internal static UnhookWindowsHookExSafeHandle SetWindowsHookEx(WINDOWS_HOOK_ID idHook, HOOKPROC lpfn, SafeHandle hmod, uint dwThreadId)
	{
		bool success = false;
		try
		{
			HINSTANCE hmod2;
			if (hmod != null)
			{
				hmod.DangerousAddRef(ref success);
				hmod2 = (HINSTANCE)hmod.DangerousGetHandle();
			}
			else
			{
				hmod2 = (HINSTANCE)new IntPtr(0L);
			}
			return new UnhookWindowsHookExSafeHandle(SetWindowsHookEx(idHook, lpfn, hmod2, dwThreadId));
		}
		finally
		{
			if (success)
			{
				hmod.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", EntryPoint = "SetWindowsHookExW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HHOOK SetWindowsHookEx(WINDOWS_HOOK_ID idHook, [MarshalAs(UnmanagedType.FunctionPtr)] HOOKPROC lpfn, HINSTANCE hmod, uint dwThreadId);

	[OverloadResolutionPriority(1)]
	internal static LRESULT CallNextHookEx(SafeHandle hhk, int nCode, WPARAM wParam, LPARAM lParam)
	{
		bool success = false;
		try
		{
			HHOOK hhk2;
			if (hhk != null)
			{
				hhk.DangerousAddRef(ref success);
				hhk2 = (HHOOK)hhk.DangerousGetHandle();
			}
			else
			{
				hhk2 = (HHOOK)new IntPtr(0L);
			}
			return CallNextHookEx(hhk2, nCode, wParam, lParam);
		}
		finally
		{
			if (success)
			{
				hhk.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern LRESULT CallNextHookEx(HHOOK hhk, int nCode, WPARAM wParam, LPARAM lParam);

	[OverloadResolutionPriority(1)]
	internal unsafe static BOOL GetCursorPos(out Point lpPoint)
	{
		fixed (Point* lpPoint2 = &lpPoint)
		{
			return GetCursorPos(lpPoint2);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern BOOL GetCursorPos(Point* lpPoint);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern short GetAsyncKeyState(int vKey);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL UnhookWinEvent(HWINEVENTHOOK hWinEventHook);

	[OverloadResolutionPriority(1)]
	internal static UnhookWinEventSafeHandle SetWinEventHook(uint eventMin, uint eventMax, SafeHandle hmodWinEventProc, WINEVENTPROC pfnWinEventProc, uint idProcess, uint idThread, uint dwFlags)
	{
		bool success = false;
		try
		{
			HMODULE hmodWinEventProc2;
			if (hmodWinEventProc != null)
			{
				hmodWinEventProc.DangerousAddRef(ref success);
				hmodWinEventProc2 = (HMODULE)hmodWinEventProc.DangerousGetHandle();
			}
			else
			{
				hmodWinEventProc2 = (HMODULE)new IntPtr(0L);
			}
			return new UnhookWinEventSafeHandle(SetWinEventHook(eventMin, eventMax, hmodWinEventProc2, pfnWinEventProc, idProcess, idThread, dwFlags));
		}
		finally
		{
			if (success)
			{
				hmodWinEventProc.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HWINEVENTHOOK SetWinEventHook(uint eventMin, uint eventMax, HMODULE hmodWinEventProc, [MarshalAs(UnmanagedType.FunctionPtr)] WINEVENTPROC pfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HWND GetForegroundWindow();

	internal unsafe static int GetWindowText(HWND hWnd, Span<char> lpString)
	{
		fixed (char* ptr = lpString)
		{
			return GetWindowText(hWnd, ptr, lpString.Length);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetWindowTextW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int GetWindowText(HWND hWnd, PWSTR lpString, int nMaxCount);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMONITOR MonitorFromWindow(HWND hwnd, MONITOR_FROM_FLAGS dwFlags);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HMONITOR MonitorFromPoint(Point pt, MONITOR_FROM_FLAGS dwFlags);

	[OverloadResolutionPriority(1)]
	internal unsafe static HMONITOR MonitorFromRect(in RECT lprc, MONITOR_FROM_FLAGS dwFlags)
	{
		fixed (RECT* lprc2 = &lprc)
		{
			return MonitorFromRect(lprc2, dwFlags);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern HMONITOR MonitorFromRect(RECT* lprc, MONITOR_FROM_FLAGS dwFlags);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HWND WindowFromPoint(Point Point);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL OpenClipboard(HWND hWndNewOwner);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL EmptyClipboard();

	[OverloadResolutionPriority(1)]
	internal static SafeFileHandle SetClipboardData(uint uFormat, SafeHandle hMem)
	{
		bool success = false;
		try
		{
			HANDLE hMem2;
			if (hMem != null)
			{
				hMem.DangerousAddRef(ref success);
				hMem2 = (HANDLE)hMem.DangerousGetHandle();
			}
			else
			{
				hMem2 = (HANDLE)new IntPtr(0L);
			}
			return new SafeFileHandle(SetClipboardData(uFormat, hMem2), ownsHandle: true);
		}
		finally
		{
			if (success)
			{
				hMem.DangerousRelease();
			}
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HANDLE SetClipboardData(uint uFormat, HANDLE hMem);

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern BOOL CloseClipboard();

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HWND GetOpenClipboardWindow();

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern uint GetWindowThreadProcessId(HWND hWnd, [Optional] uint* lpdwProcessId);

	internal unsafe static uint SendInput(ReadOnlySpan<INPUT> pInputs, int cbSize)
	{
		fixed (INPUT* pInputs2 = pInputs)
		{
			return SendInput((uint)pInputs.Length, pInputs2, cbSize);
		}
	}

	[DllImport("USER32.dll", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal unsafe static extern uint SendInput(uint cInputs, INPUT* pInputs, int cbSize);

	[DllImport("USER32.dll", ExactSpelling = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern HWND GetShellWindow();

	internal unsafe static int GetClassName(HWND hWnd, Span<char> lpClassName)
	{
		fixed (char* ptr = lpClassName)
		{
			return GetClassName(hWnd, ptr, lpClassName.Length);
		}
	}

	[DllImport("USER32.dll", EntryPoint = "GetClassNameW", ExactSpelling = true, SetLastError = true)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
	internal static extern int GetClassName(HWND hWnd, PWSTR lpClassName, int nMaxCount);
}
