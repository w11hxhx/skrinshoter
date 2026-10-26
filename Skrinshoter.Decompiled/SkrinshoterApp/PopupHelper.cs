using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Interop;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SkrinshoterApp;

public class PopupHelper : DependencyObject
{
	public static readonly DependencyProperty ExcludeFromCaptureProperty = DependencyProperty.RegisterAttached("ExcludeFromCapture", typeof(bool), typeof(PopupHelper), new PropertyMetadata(false, ExcludeFromCaptureChanged));

	private static readonly DependencyProperty ResolvedPopupProperty = DependencyProperty.RegisterAttached("ResolvedPopup", typeof(Popup), typeof(PopupHelper), new PropertyMetadata(null));

	public static bool GetExcludeFromCapture(DependencyObject obj)
	{
		return (bool)obj.GetValue(ExcludeFromCaptureProperty);
	}

	public static void SetExcludeFromCapture(DependencyObject obj, bool value)
	{
		obj.SetValue(ExcludeFromCaptureProperty, value);
	}

	private static void ExcludeFromCaptureChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		bool flag = (bool)e.NewValue;
		if (d is ComboBox comboBox)
		{
			UpdateComboBox(comboBox, flag);
			return;
		}
		Popup popup = ResolvePopup(d);
		if (popup != null)
		{
			popup.Opened -= PopupOpened;
			if (flag)
			{
				popup.Opened += PopupOpened;
			}
		}
	}

	private static Popup ResolvePopup(DependencyObject d)
	{
		if (d is Popup result)
		{
			return result;
		}
		if (d is ToolTip current)
		{
			return LogicalTreeHelper.GetParent(current) as Popup;
		}
		return null;
	}

	private static void UpdateComboBox(ComboBox comboBox, bool exclude)
	{
		comboBox.Loaded -= ComboBoxLoaded;
		if (!exclude)
		{
			DetachComboBoxPopup(comboBox);
		}
		else if (comboBox.IsLoaded)
		{
			AttachComboBoxPopup(comboBox);
		}
		else
		{
			comboBox.Loaded += ComboBoxLoaded;
		}
	}

	private static void ComboBoxLoaded(object sender, RoutedEventArgs e)
	{
		ComboBox comboBox = (ComboBox)sender;
		comboBox.Loaded -= ComboBoxLoaded;
		AttachComboBoxPopup(comboBox);
	}

	private static void AttachComboBoxPopup(ComboBox comboBox)
	{
		Popup popup = comboBox.FindChild<Popup>("PART_Popup");
		if (popup != null)
		{
			comboBox.SetValue(ResolvedPopupProperty, popup);
			popup.Opened -= PopupOpened;
			popup.Opened += PopupOpened;
		}
	}

	private static void DetachComboBoxPopup(ComboBox comboBox)
	{
		if (comboBox.GetValue(ResolvedPopupProperty) is Popup popup)
		{
			popup.Opened -= PopupOpened;
			comboBox.ClearValue(ResolvedPopupProperty);
		}
	}

	private static void PopupOpened(object sender, EventArgs e)
	{
		Popup popup = sender as Popup;
		if (popup?.Child != null && PresentationSource.FromVisual(popup.Child) is HwndSource hwndSource)
		{
			PInvoke.SetWindowDisplayAffinity(new HWND(hwndSource.Handle), WINDOW_DISPLAY_AFFINITY.WDA_EXCLUDEFROMCAPTURE);
		}
	}
}
