using System;
using System.Windows;

namespace SkrinshoterApp;

public class AreaSelectionManager
{
	public Rect Area { get; private set; }

	public OverlayWindowVM Owner { get; private set; }

	public bool HasArea
	{
		get
		{
			if (Area.Width > 0.0)
			{
				return Area.Height > 0.0;
			}
			return false;
		}
	}

	public event EventHandler AreaChanged;

	public void SetArea(Rect area, OverlayWindowVM owner)
	{
		if (area == Rect.Empty)
		{
			throw new ArgumentException("Rect.Empty", "area");
		}
		if (owner == null && area.Width > 0.0 && area.Height > 0.0)
		{
			throw new ArgumentNullException("owner");
		}
		if (!(area == Area) || owner != Owner)
		{
			Area = area;
			Owner = owner;
			AreaChanged?.Invoke(this, EventArgs.Empty);
		}
	}

	public void Clear()
	{
		SetArea(default, null);
	}
}
