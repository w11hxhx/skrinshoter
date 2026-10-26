using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SkrinshoterApp;

public class ComboBoxTemplateSelectorExtension : MarkupExtension
{
	public DataTemplate SelectedItemTemplate { get; set; }

	public DataTemplateSelector SelectedItemTemplateSelector { get; set; }

	public DataTemplate DropDownItemTemplate { get; set; }

	public DataTemplateSelector DropDownItemTemplateSelector { get; set; }

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return new ComboBoxTemplateSelector
		{
			SelectedItemTemplate = SelectedItemTemplate,
			SelectedItemTemplateSelector = SelectedItemTemplateSelector,
			DropDownItemTemplate = DropDownItemTemplate,
			DropDownItemTemplateSelector = DropDownItemTemplateSelector
		};
	}
}
