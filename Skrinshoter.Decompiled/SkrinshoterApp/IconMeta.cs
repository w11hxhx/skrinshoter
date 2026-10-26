using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public static class IconMeta
{
	public static readonly DependencyProperty RenderModeProperty = DependencyProperty.RegisterAttached("RenderMode", typeof(IconRenderMode), typeof(IconMeta), new FrameworkPropertyMetadata(IconRenderMode.Fill));

	public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.RegisterAttached("StrokeThickness", typeof(double), typeof(IconMeta), new FrameworkPropertyMetadata(1.0));

	public static readonly DependencyProperty LineCapProperty = DependencyProperty.RegisterAttached("LineCap", typeof(PenLineCap), typeof(IconMeta), new FrameworkPropertyMetadata(PenLineCap.Round));

	public static readonly DependencyProperty LineJoinProperty = DependencyProperty.RegisterAttached("LineJoin", typeof(PenLineJoin), typeof(IconMeta), new FrameworkPropertyMetadata(PenLineJoin.Round));

	public static readonly DependencyProperty MiterLimitProperty = DependencyProperty.RegisterAttached("MiterLimit", typeof(double), typeof(IconMeta), new FrameworkPropertyMetadata(10.0));

	public static void SetRenderMode(DependencyObject obj, IconRenderMode value)
	{
		obj.SetValue(RenderModeProperty, value);
	}

	public static IconRenderMode GetRenderMode(DependencyObject obj)
	{
		return (IconRenderMode)obj.GetValue(RenderModeProperty);
	}

	public static void SetStrokeThickness(DependencyObject obj, double value)
	{
		obj.SetValue(StrokeThicknessProperty, value);
	}

	public static double GetStrokeThickness(DependencyObject obj)
	{
		return (double)obj.GetValue(StrokeThicknessProperty);
	}

	public static void SetLineCap(DependencyObject obj, PenLineCap value)
	{
		obj.SetValue(LineCapProperty, value);
	}

	public static PenLineCap GetLineCap(DependencyObject obj)
	{
		return (PenLineCap)obj.GetValue(LineCapProperty);
	}

	public static void SetLineJoin(DependencyObject obj, PenLineJoin value)
	{
		obj.SetValue(LineJoinProperty, value);
	}

	public static PenLineJoin GetLineJoin(DependencyObject obj)
	{
		return (PenLineJoin)obj.GetValue(LineJoinProperty);
	}

	public static void SetMiterLimit(DependencyObject obj, double value)
	{
		obj.SetValue(MiterLimitProperty, value);
	}

	public static double GetMiterLimit(DependencyObject obj)
	{
		return (double)obj.GetValue(MiterLimitProperty);
	}
}
