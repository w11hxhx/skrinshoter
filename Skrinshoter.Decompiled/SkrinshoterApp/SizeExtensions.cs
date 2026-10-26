using System;
using System.Windows;

namespace SkrinshoterApp;

public static class SizeExtensions
{
	public static bool AlmostEquals(this Size a, Size b)
	{
		if (Math.Abs(a.Width - b.Width) < 0.05)
		{
			return Math.Abs(a.Height - b.Height) < 0.05;
		}
		return false;
	}
}
