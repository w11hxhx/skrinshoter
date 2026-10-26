using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace SkrinshoterApp;

public class Panel : ContentControl
{
	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		Border border = GetTemplateChild("MaskBorder") as Border;
		Grid grid = GetTemplateChild("ContentGrid") as Grid;
		if (border != null && grid != null)
		{
			RectangleGeometry rectangleGeometry = new RectangleGeometry
			{
				RadiusX = border.CornerRadius.TopLeft,
				RadiusY = border.CornerRadius.TopLeft
			};
			MultiBinding multiBinding = new MultiBinding
			{
				Converter = new RectConverter()
			};
			multiBinding.Bindings.Add(new Binding("ActualWidth")
			{
				Source = grid
			});
			multiBinding.Bindings.Add(new Binding("ActualHeight")
			{
				Source = grid
			});
			BindingOperations.SetBinding(rectangleGeometry, RectangleGeometry.RectProperty, multiBinding);
			grid.Clip = rectangleGeometry;
		}
	}
}
