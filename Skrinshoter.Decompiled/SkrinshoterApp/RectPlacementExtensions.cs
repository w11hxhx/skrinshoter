using System;
using System.Windows;

namespace SkrinshoterApp;

public static class RectPlacementExtensions
{
	public static Point PlaceRect(this RectPlacement placement, Size rectSize, Rect anchorRect, double indent)
	{
		return placement switch
		{
			RectPlacement.OuterCornerTopLeft => new Point(anchorRect.Left - rectSize.Width - indent, anchorRect.Top - rectSize.Height - indent), 
			RectPlacement.OuterSideTopLeft => new Point(anchorRect.Left, anchorRect.Top - rectSize.Height - indent), 
			RectPlacement.OuterSideTopCenter => new Point(anchorRect.Left + (anchorRect.Width - rectSize.Width) / 2.0, anchorRect.Top - rectSize.Height - indent), 
			RectPlacement.OuterSideTopRight => new Point(anchorRect.Right - rectSize.Width, anchorRect.Top - rectSize.Height - indent), 
			RectPlacement.OuterCornerTopRight => new Point(anchorRect.Right + indent, anchorRect.Top - rectSize.Height - indent), 
			RectPlacement.OuterSideRightTop => new Point(anchorRect.Right + indent, anchorRect.Top), 
			RectPlacement.OuterSideRightCenter => new Point(anchorRect.Right + indent, anchorRect.Top + (anchorRect.Height - rectSize.Height) / 2.0), 
			RectPlacement.OuterSideRightBottom => new Point(anchorRect.Right + indent, anchorRect.Bottom - rectSize.Height), 
			RectPlacement.OuterCornerBottomRight => new Point(anchorRect.Right + indent, anchorRect.Bottom + indent), 
			RectPlacement.OuterSideBottomRight => new Point(anchorRect.Right - rectSize.Width, anchorRect.Bottom + indent), 
			RectPlacement.OuterSideBottomCenter => new Point(anchorRect.Left + (anchorRect.Width - rectSize.Width) / 2.0, anchorRect.Bottom + indent), 
			RectPlacement.OuterSideBottomLeft => new Point(anchorRect.Left, anchorRect.Bottom + indent), 
			RectPlacement.OuterCornerBottomLeft => new Point(anchorRect.Left - rectSize.Width - indent, anchorRect.Bottom + indent), 
			RectPlacement.OuterSideLeftBottom => new Point(anchorRect.Left - rectSize.Width - indent, anchorRect.Bottom - rectSize.Height), 
			RectPlacement.OuterSideLeftCenter => new Point(anchorRect.Left - rectSize.Width - indent, anchorRect.Top + (anchorRect.Height - rectSize.Height) / 2.0), 
			RectPlacement.OuterSideLeftTop => new Point(anchorRect.Left - rectSize.Width - indent, anchorRect.Top), 
			RectPlacement.InnerCornerTopLeft => new Point(anchorRect.Left + indent, anchorRect.Top + indent), 
			RectPlacement.InnerSideTopCenter => new Point(anchorRect.Left + (anchorRect.Width - rectSize.Width) / 2.0, anchorRect.Top + indent), 
			RectPlacement.InnerCornerTopRight => new Point(anchorRect.Right - rectSize.Width - indent, anchorRect.Top + indent), 
			RectPlacement.InnerSideRightCenter => new Point(anchorRect.Right - rectSize.Width - indent, anchorRect.Top + (anchorRect.Height - rectSize.Height) / 2.0), 
			RectPlacement.InnerCornerBottomRight => new Point(anchorRect.Right - rectSize.Width - indent, anchorRect.Bottom - rectSize.Height - indent), 
			RectPlacement.InnerSideBottomCenter => new Point(anchorRect.Left + (anchorRect.Width - rectSize.Width) / 2.0, anchorRect.Bottom - rectSize.Height - indent), 
			RectPlacement.InnerCornerBottomLeft => new Point(anchorRect.Left + indent, anchorRect.Bottom - rectSize.Height - indent), 
			RectPlacement.InnerSideLeftCenter => new Point(anchorRect.Left + indent, anchorRect.Top + (anchorRect.Height - rectSize.Height) / 2.0), 
			_ => throw new InvalidOperationException($"Unknown rect placement {typeof(RectPlacement)}.{placement}."), 
		};
	}
}
