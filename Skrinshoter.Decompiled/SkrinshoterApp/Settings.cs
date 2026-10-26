using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

[UIThread]
public sealed class Settings
{
	public const string DefaultDeviceId = "Default";

	[JsonProperty("app_id")]
	[Obsolete("Для доступа к данному свойству предназначен класс AppInfoProvider.", false)]
	public string AppId
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("language")]
	public string Language
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("autostart_on")]
	public bool Autostart
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("camera_window_relative_rect")]
	public Rect CameraWindowRelativeRect
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Rect result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("camera_window_shape")]
	[JsonConverter(typeof(SafeStringEnumConverter), new object[] { 0 })]
	public VideoRecordingCameraWindowShape CameraWindowShape
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				VideoRecordingCameraWindowShape result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("capture_cursor")]
	public bool DrawCursorOnScreenshot
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("capture_cursor_video")]
	public bool DrawCursorOnVideo
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("capture_camera_device_id")]
	public string CaptureCameraDeviceId
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("is_capture_camera_device_off")]
	public bool? IsCaptureCameraDeviceOff
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool? result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("capture_input_audio_device_id")]
	public string CaptureInputAudioDeviceId
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("is_capture_input_audio_device_muted")]
	public bool? IsCaptureInputAudioDeviceMuted
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool? result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("capture_output_audio_device_id")]
	public string CaptureOutputAudioDeviceId
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("is_capture_output_audio_device_muted")]
	public bool? IsCaptureOutputAudioDeviceMuted
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool? result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("check_for_updates")]
	public bool CheckForUpdates
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("editor_default_element")]
	[JsonConverter(typeof(SafeStringEnumConverter), new object[] { 0 })]
	public EditorDefaultTool EditorDefaultTool
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				EditorDefaultTool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("editor_selected_tool")]
	[JsonConverter(typeof(SafeStringEnumConverter), new object[] { 0 })]
	public DrawingTool EditorSelectedTool
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				DrawingTool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("editor_pen_radius")]
	public int EditorSelectedLineThickness
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				int result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("editor_color")]
	public System.Windows.Media.Color EditorSelectedColor
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				System.Windows.Media.Color result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("editor_available_colors")]
	public List<System.Windows.Media.Color> EditorAvailableColors
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				List<System.Windows.Media.Color> result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("file_uids")]
	public List<string> FileUids
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				List<string> result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("highlight_mouse_pointer")]
	public bool HighlightMousePointer
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("highlight_mouse_clicks")]
	public bool HighlightMouseClicks
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("hotkey_region_screenshot")]
	public Hotkey AreaScreenshotHotkey
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Hotkey result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("hotkey_active_window_screenshot")]
	public Hotkey ActiveWindowScreenshotHotkey
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Hotkey result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("hotkey_full_screenshot")]
	public Hotkey ActiveMonitorScreenshotHotkey
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Hotkey result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("hotkey_desktop_screenshot")]
	public Hotkey DesktopScreenshotHotkey
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Hotkey result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("hotkey_video_recoding_activation")]
	public Hotkey RecordVideoHotkey
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Hotkey result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("hotkey_video_recoding_pause")]
	public Hotkey PauseVideoHotkey
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Hotkey result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("jpeg_quality")]
	public int JpegQualityLevel
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				int result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("open_screenshot_in_browser")]
	public bool OpenScreenshotInBrowser
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("open_video_in_browser")]
	public bool OpenVideoInBrowser
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("hotkey_upload_screenshot_to_all")]
	public Hotkey UploadScreenshotToAllHotkey { get; set; }

	[JsonProperty("screenshot_upload_destinations")]
	public ScreenshotUploadDestination ScreenshotUploadDestinations { get; set; }

	[JsonProperty("copy_screenshot_links_to_clipboard")]
	public bool CopyScreenshotLinksToClipboard { get; set; }

	[JsonProperty("screenshot_upload_defaults_initialized")]
	public bool ScreenshotUploadDefaultsInitialized { get; set; }

	[JsonProperty("play_sound")]
	public bool PlaySound
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("screenshot_file_type")]
	[JsonConverter(typeof(SafeStringEnumConverter), new object[] { 1 })]
	public ScreenshotFileFormat ScreenshotFileFormat
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				ScreenshotFileFormat result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("screenshots_path")]
	public string ScreenshotSavePath
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("screenshot_file_name_template")]
	[JsonConverter(typeof(FileNameTemplateJsonConverter))]
	public FileNameTemplate ScreenshotFileNameTemplate
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				FileNameTemplate result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("screenshot_previously_selected_region")]
	public Rectangle ScreenshotLastSelectionArea
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				Rectangle result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("select_and_draw_mode")]
	public bool ShouldShowEditor
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("draw_object_shadows")]
	public bool DrawObjectShadows
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("show_video_control_panel")]
	public bool ShowVideoControlPanel
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("show_muted_microphone_warning")]
	public bool ShowMutedMicrophoneWarning
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("silent_mode")]
	public bool SilentMode
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("start_recording_video_immediately_after_selection")]
	public bool StartRecordingVideoImmediatelyAfterSelection
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("storage_type")]
	public ScreenshotSaveType ScreenshotSaveType
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				ScreenshotSaveType result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("tray_left_click_action")]
	[JsonConverter(typeof(SafeStringEnumConverter), new object[] { 0 })]
	public TrayClickAction TrayLeftClickAction
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				TrayClickAction result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("upload_window_title")]
	public bool UploadWindowTitle
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("use_lr_mouse_for_regio_screenshot")]
	public bool UseMouseForScreenshot
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("use_lr_mouse_for_video_recording")]
	public bool UseMouseForVideo
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("use_previously_selected_region")]
	public bool UseLastSelectionArea
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("use_screenshot_direct_file_link")]
	public bool UseDirectLinkForScreenshot
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("use_video_direct_file_link")]
	public bool UseDirectLinkForVideo
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				bool result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("video_quality_level")]
	public VideoQualityLevel VideoQualityLevel
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				VideoQualityLevel result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("video_save_path")]
	public string VideoSavePath
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				string result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("video_file_name_template")]
	[JsonConverter(typeof(FileNameTemplateJsonConverter))]
	public FileNameTemplate VideoFileNameTemplate
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				FileNameTemplate result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("video_storage_type")]
	public VideoSaveType VideoSaveType
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				VideoSaveType result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	[JsonProperty("last_stats_send_date")]
	public long LastStatsSendDate
	{
		[CompilerGenerated]
		get
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			object[] args = new object[0];
			uIThreadAttribute.Init(this, methodFromHandle, args);
			uIThreadAttribute.OnEntry();
			try
			{
				long result = field;
				uIThreadAttribute.OnExit();
				return result;
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
		[CompilerGenerated]
		set
		{
			MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
			Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
			UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
			uIThreadAttribute.Init(this, methodFromHandle, new object[1] { value });
			uIThreadAttribute.OnEntry();
			try
			{
				field = value;
				uIThreadAttribute.OnExit();
			}
			catch (Exception exception)
			{
				uIThreadAttribute.OnException(exception);
				throw;
			}
		}
	}

	public Settings()
	{
		AppId = Guid.NewGuid().ToString("B").ToUpperInvariant();
		Language = LanguagesProvider.Default;
		Autostart = true;
		CameraWindowRelativeRect = new Rect(0.05, 0.7, 0.15, 0.25);
		DrawCursorOnVideo = true;
		CaptureCameraDeviceId = "";
		CaptureInputAudioDeviceId = "Default";
		CaptureOutputAudioDeviceId = "Default";
		CheckForUpdates = true;
		EditorSelectedLineThickness = 6;
		EditorSelectedColor = System.Windows.Media.Color.FromRgb(byte.MaxValue, 59, 48);
		EditorAvailableColors = new List<System.Windows.Media.Color>();
		FileUids = new List<string>();
		HighlightMouseClicks = true;
		AreaScreenshotHotkey = new Hotkey(Key.Snapshot);
		ActiveWindowScreenshotHotkey = new Hotkey(ModifierKeys.Alt, Key.Snapshot);
		ActiveMonitorScreenshotHotkey = new Hotkey(ModifierKeys.Shift, Key.Snapshot);
		DesktopScreenshotHotkey = new Hotkey(ModifierKeys.Control, Key.Snapshot);
		RecordVideoHotkey = new Hotkey(ModifierKeys.Control | ModifierKeys.Shift, Key.Snapshot);
		PauseVideoHotkey = new Hotkey(ModifierKeys.Shift, Key.Pause);
		UploadScreenshotToAllHotkey = new Hotkey(ModifierKeys.Control, Key.Return);
		JpegQualityLevel = 80;
		OpenScreenshotInBrowser = true;
		ScreenshotUploadDestinations = ScreenshotUploadDestination.All;
		CopyScreenshotLinksToClipboard = true;
		OpenVideoInBrowser = true;
		PlaySound = true;
		ScreenshotFileFormat = ScreenshotFileFormat.Jpeg;
		ScreenshotSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		ScreenshotFileNameTemplate = new FileNameTemplate(Resources.Settings_Screenshot_FileNameTemplateDefaultValue);
		ShouldShowEditor = true;
		DrawObjectShadows = true;
		ShowVideoControlPanel = true;
		ShowMutedMicrophoneWarning = true;
		ScreenshotSaveType = ScreenshotSaveType.Cloud;
		UploadWindowTitle = AppInfoProvider.IsSkrinshoter;
		VideoQualityLevel = VideoQualityLevel.HD;
		VideoSavePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		VideoFileNameTemplate = new FileNameTemplate(Resources.Settings_Video_FileNameTemplateDefaultValue);
		MethodBase methodFromHandle = MethodBase.GetCurrentMethod();
		Attribute[] customAttributes = Attribute.GetCustomAttributes(typeof(Settings), typeof(UIThreadAttribute));
		UIThreadAttribute uIThreadAttribute = (UIThreadAttribute)customAttributes[(nint)customAttributes.LongLength - 1];
		object[] args = new object[0];
		uIThreadAttribute.Init(null, methodFromHandle, args);
		uIThreadAttribute.OnEntry();
		try
		{
			uIThreadAttribute.OnExit();
		}
		catch (Exception exception)
		{
			uIThreadAttribute.OnException(exception);
			throw;
		}
	}
}
