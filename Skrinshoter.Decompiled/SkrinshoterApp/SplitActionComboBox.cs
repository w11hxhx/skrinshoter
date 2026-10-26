using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace SkrinshoterApp;

public class SplitActionComboBox : ComboBox
{
	public static readonly DependencyProperty ButtonCommandProperty;

	public static readonly DependencyProperty ButtonCommandParameterProperty;

	public static readonly DependencyProperty ButtonIconProperty;

	public static readonly DependencyProperty ButtonContentProperty;

	public static readonly DependencyProperty ButtonContentTemplateProperty;

	public static readonly DependencyProperty ButtonToolTipProperty;

	public static readonly DependencyProperty ArrowToolTipProperty;

	private ButtonBase actionButton;

	private const double DropDownGap = 2.0;

	private Popup dropDownPopup;

	public ICommand ButtonCommand
	{
		get
		{
			return (ICommand)GetValue(ButtonCommandProperty);
		}
		set
		{
			SetValue(ButtonCommandProperty, value);
		}
	}

	public object ButtonCommandParameter
	{
		get
		{
			return GetValue(ButtonCommandParameterProperty);
		}
		set
		{
			SetValue(ButtonCommandParameterProperty, value);
		}
	}

	public ImageSource ButtonIcon
	{
		get
		{
			return (ImageSource)GetValue(ButtonIconProperty);
		}
		set
		{
			SetValue(ButtonIconProperty, value);
		}
	}

	public object ButtonContent
	{
		get
		{
			return GetValue(ButtonContentProperty);
		}
		set
		{
			SetValue(ButtonContentProperty, value);
		}
	}

	public DataTemplate ButtonContentTemplate
	{
		get
		{
			return (DataTemplate)GetValue(ButtonContentTemplateProperty);
		}
		set
		{
			SetValue(ButtonContentTemplateProperty, value);
		}
	}

	public object ButtonToolTip
	{
		get
		{
			return GetValue(ButtonToolTipProperty);
		}
		set
		{
			SetValue(ButtonToolTipProperty, value);
		}
	}

	public object ArrowToolTip
	{
		get
		{
			return GetValue(ArrowToolTipProperty);
		}
		set
		{
			SetValue(ArrowToolTipProperty, value);
		}
	}

	static SplitActionComboBox()
	{
		ButtonCommandProperty = DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(SplitActionComboBox), new PropertyMetadata(null));
		ButtonCommandParameterProperty = DependencyProperty.Register("ButtonCommandParameter", typeof(object), typeof(SplitActionComboBox), new PropertyMetadata(null));
		ButtonIconProperty = DependencyProperty.Register("ButtonIcon", typeof(ImageSource), typeof(SplitActionComboBox), new PropertyMetadata(null));
		ButtonContentProperty = DependencyProperty.Register("ButtonContent", typeof(object), typeof(SplitActionComboBox), new PropertyMetadata(null));
		ButtonContentTemplateProperty = DependencyProperty.Register("ButtonContentTemplate", typeof(DataTemplate), typeof(SplitActionComboBox), new PropertyMetadata(null));
		ButtonToolTipProperty = DependencyProperty.Register("ButtonToolTip", typeof(object), typeof(SplitActionComboBox), new PropertyMetadata(null));
		ArrowToolTipProperty = DependencyProperty.Register("ArrowToolTip", typeof(object), typeof(SplitActionComboBox), new PropertyMetadata(null));
		FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(SplitActionComboBox), new FrameworkPropertyMetadata(typeof(ComboBox)));
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		SetupActionButton();
		SetupDropDownPlacement();
	}

	private void SetupActionButton()
	{
		if (actionButton != null)
		{
			actionButton.Click -= ActionButtonClickHandler;
		}
		actionButton = GetTemplateChild("PART_ActionButton") as ButtonBase;
		if (actionButton != null)
		{
			actionButton.Click += ActionButtonClickHandler;
		}
	}

	private void ActionButtonClickHandler(object sender, RoutedEventArgs e)
	{
		IsDropDownOpen = false;
	}

	private void SetupDropDownPlacement()
	{
		SizeChanged -= SizeChangedHandler;
		dropDownPopup = GetTemplateChild("PART_Popup") as Popup;
		if (dropDownPopup != null)
		{
			SizeChanged += SizeChangedHandler;
			UpdateDropDownPlacementRectangle();
		}
	}

	private void UpdateDropDownPlacementRectangle()
	{
		if (dropDownPopup != null)
		{
			dropDownPopup.PlacementRectangle = new Rect(0.0, -2.0, ActualWidth, ActualHeight + 4.0);
		}
	}

	private void SizeChangedHandler(object sender, SizeChangedEventArgs e)
	{
		UpdateDropDownPlacementRectangle();
	}
}
