using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;

namespace SkrinshoterApp;

public class FocusablePopupComboBox : ComboBox
{
	protected override void OnDropDownOpened(EventArgs e)
	{
		base.OnDropDownOpened(e);
		Dispatcher.BeginInvoke((Action)(() =>
		{
			if (Template.FindName("PART_Popup", this) is Popup { Child: { } child })
			{
				child.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
			}
		}), DispatcherPriority.Input);
	}
}
