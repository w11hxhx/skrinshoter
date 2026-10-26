using System;
using System.Windows;

namespace SkrinshoterApp;

internal sealed class ScrollingCapturePanelPlacer
{
	private const double PreviewPanelMaxWidth = 192.0;

	private const double MinAvailableHeightForTopBottomPreviewPanelPlacement = 192.0;

	private const double PanelIndent = 8.0;

	private const double BoundingRectIndent = 7.0;

	private const double PanelShadowWidth = 3.0;

	private readonly double previewPanelMaxHeightPercent;

	private Rect selectionRect;

	private Rect boundingRect;

	private RectPlacement actionPanelPlacement;

	private RectPlacement previewPanelPlacement;

	public ScrollingCapturePanelPlacer(double previewPanelMaxHeightPercent)
	{
		this.previewPanelMaxHeightPercent = previewPanelMaxHeightPercent;
	}

	public void CalculatePanelPlacements(Size actionPanelSize, Rect selectionRect, Rect boundingRect)
	{
		actionPanelSize.Width -= 6.0;
		actionPanelSize.Height -= 6.0;
		double val = Math.Round((selectionRect.Height - 16.0) * previewPanelMaxHeightPercent / 100.0);
		val = Math.Max(val, 192.0);
		Size size = new Size(192.0, val);
		if (boundingRect.Right - selectionRect.Right >= size.Width + 8.0 + 7.0)
		{
			if (boundingRect.Bottom - selectionRect.Bottom >= actionPanelSize.Height + 8.0 + 7.0)
			{
				actionPanelPlacement = RectPlacement.OuterSideBottomCenter;
				previewPanelPlacement = RectPlacement.OuterSideRightBottom;
			}
			else if (selectionRect.Top - boundingRect.Top >= actionPanelSize.Height + 8.0 + 7.0)
			{
				actionPanelPlacement = RectPlacement.OuterSideTopCenter;
				previewPanelPlacement = RectPlacement.OuterSideRightTop;
			}
			else
			{
				actionPanelPlacement = RectPlacement.OuterSideRightBottom;
				previewPanelPlacement = RectPlacement.OuterSideRightBottom;
			}
		}
		else if (selectionRect.Left - boundingRect.Left >= size.Width + 8.0 + 7.0)
		{
			if (boundingRect.Bottom - selectionRect.Bottom >= actionPanelSize.Height + 8.0 + 7.0)
			{
				actionPanelPlacement = RectPlacement.OuterSideBottomCenter;
				previewPanelPlacement = RectPlacement.OuterSideLeftBottom;
			}
			else if (selectionRect.Top - boundingRect.Top >= actionPanelSize.Height + 8.0 + 7.0)
			{
				actionPanelPlacement = RectPlacement.OuterSideTopCenter;
				previewPanelPlacement = RectPlacement.OuterSideLeftTop;
			}
			else
			{
				actionPanelPlacement = RectPlacement.OuterSideLeftBottom;
				previewPanelPlacement = RectPlacement.OuterSideLeftBottom;
			}
		}
		else if (boundingRect.Bottom - selectionRect.Bottom >= size.Height + 8.0 + 7.0 || selectionRect.Top - boundingRect.Top >= size.Height + 8.0 + 7.0)
		{
			if (boundingRect.Bottom - selectionRect.Bottom >= selectionRect.Top - boundingRect.Top)
			{
				previewPanelPlacement = RectPlacement.OuterSideBottomRight;
				actionPanelPlacement = RectPlacement.OuterSideBottomCenter;
			}
			else
			{
				previewPanelPlacement = RectPlacement.OuterSideTopRight;
				actionPanelPlacement = RectPlacement.OuterSideTopCenter;
			}
		}
		else
		{
			previewPanelPlacement = RectPlacement.InnerCornerTopRight;
			if (boundingRect.Bottom - selectionRect.Bottom >= actionPanelSize.Height + 8.0 + 7.0)
			{
				actionPanelPlacement = RectPlacement.OuterSideBottomCenter;
			}
			else if (selectionRect.Top - boundingRect.Top >= actionPanelSize.Height + 8.0 + 7.0)
			{
				actionPanelPlacement = RectPlacement.OuterSideTopCenter;
			}
			else
			{
				actionPanelPlacement = RectPlacement.InnerSideTopCenter;
			}
		}
		this.selectionRect = selectionRect;
		this.boundingRect = boundingRect;
	}

	public void CalculatePanelRects(Size actionPanelSize, double previewPanelAspectRatio, out Rect actionPanelRect, out Rect previewPanelRect)
	{
		if (actionPanelPlacement == RectPlacement.None)
		{
			throw new InvalidOperationException("Call CalculatePanelPlacements method before calling this method.");
		}
		Rect rect = boundingRect;
		rect.Inflate(-7.0, -7.0);
		actionPanelSize.Width -= 6.0;
		actionPanelSize.Height -= 6.0;
		actionPanelRect = new Rect(actionPanelPlacement.PlaceRect(actionPanelSize, selectionRect, 8.0), actionPanelSize);
		actionPanelRect = actionPanelRect.MoveInsideRect(rect);
		Size size = CalculatePreviewPanelSize(actionPanelRect, previewPanelAspectRatio);
		previewPanelRect = new Rect(previewPanelPlacement.PlaceRect(size, selectionRect, 8.0), size);
		if ((actionPanelPlacement == RectPlacement.OuterSideRightBottom && previewPanelPlacement == RectPlacement.OuterSideRightBottom) || (actionPanelPlacement == RectPlacement.OuterSideLeftBottom && previewPanelPlacement == RectPlacement.OuterSideLeftBottom))
		{
			previewPanelRect.Offset(0.0, 0.0 - (selectionRect.Bottom - actionPanelRect.Top) - 8.0);
		}
		previewPanelRect = previewPanelRect.MoveInsideRect(rect);
		actionPanelRect.Inflate(3.0, 3.0);
		previewPanelRect.Inflate(3.0, 3.0);
	}

	public void CalculateMessagePanelRect(Size messagePanelSize, Rect selectionRect, Rect boundingRect, out Rect messagePanelRect)
	{
		double x = selectionRect.X + (selectionRect.Width - messagePanelSize.Width) / 2.0;
		double y = selectionRect.Y + (selectionRect.Height - messagePanelSize.Height) / 2.0;
		messagePanelRect = new Rect(new Point(x, y), messagePanelSize).MoveInsideRect(boundingRect);
	}

	private Size CalculatePreviewPanelSize(Rect actionPanelRect, double previewPanelAspectRatio)
	{
		double num = 192.0;
		double num2 = Math.Round((selectionRect.Height - 16.0) * previewPanelMaxHeightPercent / 100.0);
		switch (previewPanelPlacement)
		{
		case RectPlacement.OuterSideRightBottom:
			if (actionPanelPlacement == RectPlacement.OuterSideRightBottom)
			{
				goto IL_009b;
			}
			goto case RectPlacement.OuterSideRightTop;
		case RectPlacement.OuterSideLeftBottom:
			if (actionPanelPlacement == RectPlacement.OuterSideLeftBottom)
			{
				goto IL_009b;
			}
			goto case RectPlacement.OuterSideRightTop;
		case RectPlacement.OuterSideRightTop:
		case RectPlacement.OuterSideRightCenter:
		case RectPlacement.OuterSideLeftCenter:
		case RectPlacement.OuterSideLeftTop:
			num2 = boundingRect.Height - 14.0;
			break;
		case RectPlacement.OuterSideTopLeft:
		case RectPlacement.OuterSideTopCenter:
		case RectPlacement.OuterSideTopRight:
			num2 = selectionRect.Top - boundingRect.Top - 8.0 - 7.0;
			break;
		case RectPlacement.OuterSideBottomRight:
		case RectPlacement.OuterSideBottomCenter:
		case RectPlacement.OuterSideBottomLeft:
			{
				num2 = boundingRect.Bottom - selectionRect.Bottom - 8.0 - 7.0;
				break;
			}
			IL_009b:
			num2 = actionPanelRect.Top - boundingRect.Top - 8.0 - 7.0;
			break;
		}
		if (num > num2 * previewPanelAspectRatio)
		{
			num = num2 * previewPanelAspectRatio;
		}
		else if (num < num2 * previewPanelAspectRatio)
		{
			num2 = num / previewPanelAspectRatio;
		}
		return new Size(num, num2);
	}
}
