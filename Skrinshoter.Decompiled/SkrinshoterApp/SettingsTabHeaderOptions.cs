using System.Windows;
using System.Windows.Media;

namespace SkrinshoterApp;

public sealed class SettingsTabHeaderOptions
{
	public Geometry IconGeometry { get; set; }

	public Brush IconBrush { get; set; }

	public double IconWidth { get; set; }

	public double IconShiftX { get; set; }

	public Brush TextBrush { get; set; }

	public FontWeight TextFontWeight { get; set; }
}
