using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SkrinshoterApp;

public class ComboBoxTemplateSelector : DataTemplateSelector
{
	public DataTemplate SelectedItemTemplate { get; set; }

	public DataTemplateSelector SelectedItemTemplateSelector { get; set; }

	public DataTemplate DropDownItemTemplate { get; set; }

	public DataTemplateSelector DropDownItemTemplateSelector { get; set; }

	public override DataTemplate SelectTemplate(object item, DependencyObject container)
	{
		DependencyObject dependencyObject = container;
		while (dependencyObject != null && !(dependencyObject is ComboBox) && !(dependencyObject is ComboBoxItem))
		{
			dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
		}
		DataTemplate dataTemplate;
		if (!(dependencyObject is ComboBoxItem))
		{
			dataTemplate = SelectedItemTemplate;
			if (dataTemplate == null)
			{
				return SelectedItemTemplateSelector?.SelectTemplate(item, container);
			}
		}
		else
		{
			dataTemplate = DropDownItemTemplate;
			if (dataTemplate == null)
			{
				DataTemplateSelector dropDownItemTemplateSelector = DropDownItemTemplateSelector;
				if (dropDownItemTemplateSelector == null)
				{
					return null;
				}
				dataTemplate = dropDownItemTemplateSelector.SelectTemplate(item, container);
			}
		}
		return dataTemplate;
	}
}
