using System;
using System.Windows;

namespace SkrinshoterApp;

internal sealed class VideoRecordingPanelPlacer
{
	private const double PanelIndent = 8.0;

	private const double BoundingRectIndent = 7.0;

	private const double PanelShadowWidth = 3.0;

	private Rect selectionRect;

	private Rect boundingRect;

	private RectPlacement panelPlacement;

	public bool IsManual { get; private set; }

	public HorizontalAlignment HorizontalAnchor
	{
		get
		{
			if (IsManual)
			{
				return HorizontalAlignment.Center;
			}
			switch (panelPlacement)
			{
			case RectPlacement.OuterSideRightBottom:
				return HorizontalAlignment.Left;
			case RectPlacement.OuterSideLeftBottom:
				return HorizontalAlignment.Right;
			case RectPlacement.OuterSideTopCenter:
			case RectPlacement.OuterSideBottomCenter:
			case RectPlacement.InnerSideBottomCenter:
				return HorizontalAlignment.Center;
			default:
				return HorizontalAlignment.Left;
			}
		}
	}

	public VerticalAlignment VerticalAnchor
	{
		get
		{
			if (IsManual)
			{
				return VerticalAlignment.Center;
			}
			switch (panelPlacement)
			{
			case RectPlacement.OuterSideBottomCenter:
				return VerticalAlignment.Top;
			case RectPlacement.OuterSideTopCenter:
			case RectPlacement.OuterSideRightBottom:
			case RectPlacement.OuterSideLeftBottom:
			case RectPlacement.InnerSideBottomCenter:
				return VerticalAlignment.Bottom;
			default:
				return VerticalAlignment.Top;
			}
		}
	}

	public void CalculatePanelPlacement(Size maxPanelSize, Rect selectionRect, Rect boundingRect)
	{
		this.selectionRect = selectionRect;
		this.boundingRect = boundingRect;
		IsManual = false;
		maxPanelSize.Width -= 6.0;
		maxPanelSize.Height -= 6.0;
		if (boundingRect.Bottom - selectionRect.Bottom >= maxPanelSize.Height + 8.0 + 7.0)
		{
			panelPlacement = RectPlacement.OuterSideBottomCenter;
		}
		else if (selectionRect.Top - boundingRect.Top >= maxPanelSize.Height + 8.0 + 7.0)
		{
			panelPlacement = RectPlacement.OuterSideTopCenter;
		}
		else if (boundingRect.Right - selectionRect.Right >= maxPanelSize.Width + 8.0 + 7.0)
		{
			panelPlacement = RectPlacement.OuterSideRightBottom;
		}
		else if (selectionRect.Left - boundingRect.Left >= maxPanelSize.Width + 8.0 + 7.0)
		{
			panelPlacement = RectPlacement.OuterSideLeftBottom;
		}
		else
		{
			panelPlacement = RectPlacement.InnerSideBottomCenter;
		}
	}

	public void CalculatePanelRect(Size panelSize, out Rect panelRect)
	{
		if (panelPlacement == RectPlacement.None)
		{
			throw new InvalidOperationException("Call CalculatePanelPlacement method before calling this method.");
		}
		if (IsManual)
		{
			throw new InvalidOperationException("The panel is in manual mode.");
		}
		panelSize.Width -= 6.0;
		panelSize.Height -= 6.0;
		Rect rect = boundingRect;
		rect.Inflate(-7.0, -7.0);
		panelRect = new Rect(panelPlacement.PlaceRect(panelSize, selectionRect, 8.0), panelSize);
		panelRect = panelRect.MoveInsideRect(rect);
		panelRect.Inflate(3.0, 3.0);
	}

	public void SetManualPlacement()
	{
		IsManual = true;
	}

	public void CalculateMutedMicrophoneWarningRect(Size warningWindowSize, Rect selectionRect, Rect boundingRect, Rect? panelRect, out Rect warningWindowRect)
	{
		bool flag = panelRect.HasValue && panelRect.Value.Top + panelRect.Value.Height / 2.0 < selectionRect.Y + selectionRect.Height / 2.0;
		double x = selectionRect.X + (selectionRect.Width - warningWindowSize.Width) / 2.0;
		double y = (flag ? (selectionRect.Top + 8.0) : (selectionRect.Bottom - 8.0 - warningWindowSize.Height));
		warningWindowRect = new Rect(new Point(x, y), warningWindowSize).MoveInsideRect(boundingRect);
		if (panelRect.HasValue && warningWindowRect.IntersectsWith(panelRect.Value))
		{
			double y2 = (flag ? (panelRect.Value.Bottom + 8.0) : (panelRect.Value.Top - 8.0 - warningWindowSize.Height));
			warningWindowRect = new Rect(new Point(warningWindowRect.X, y2), warningWindowSize).MoveInsideRect(boundingRect);
		}
	}
}
