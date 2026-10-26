using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MoreLinq.Extensions;
using SkrinshoterApp.Common;

namespace SkrinshoterApp;

internal class ScreenshotEditingPanelPlacer
{
	private class PlacementResult
	{
		public Rect DrawingPanelRect { get; }

		public Rect ActionPanelRect { get; }

		public double SelectionRectIntersectionArea { get; }

		public double SelectionRectContactPerimeter { get; }

		public double MovingDistance { get; }

		public PlacementResult(Rect drawingPanelRect, Rect actionPanelRect, Rect originalDrawingPanelRect, Rect originalActionPanelRect, Rect selectionRect)
		{
			DrawingPanelRect = drawingPanelRect;
			ActionPanelRect = actionPanelRect;
			SelectionRectIntersectionArea = CalculateIntersectionArea(drawingPanelRect, selectionRect) + CalculateIntersectionArea(actionPanelRect, selectionRect);
			SelectionRectContactPerimeter = CalculateContactPerimeter(drawingPanelRect, selectionRect) + CalculateContactPerimeter(actionPanelRect, selectionRect);
			MovingDistance = CalculateMovingDistance(drawingPanelRect, originalDrawingPanelRect) + CalculateMovingDistance(actionPanelRect, originalActionPanelRect);
		}

		private static double CalculateIntersectionArea(Rect panelRect, Rect selectionRect)
		{
			panelRect.Intersect(selectionRect);
			if (!panelRect.IsEmpty)
			{
				return panelRect.Width * panelRect.Height;
			}
			return 0.0;
		}

		private static double CalculateContactPerimeter(Rect panelRect, Rect selectionRect)
		{
			double num = 0.0;
			Rect rect = panelRect;
			rect.X = selectionRect.X;
			rect.Intersect(selectionRect);
			if (!rect.IsEmpty)
			{
				num += rect.Height;
			}
			rect = panelRect;
			rect.Y = selectionRect.Y;
			rect.Intersect(selectionRect);
			if (!rect.IsEmpty)
			{
				num += rect.Width;
			}
			return num;
		}

		private static double CalculateMovingDistance(Rect panelRect, Rect originalPanelRect)
		{
			return Math.Abs(panelRect.X - originalPanelRect.X) + Math.Abs(panelRect.Y - originalPanelRect.Y);
		}
	}

	private const double PanelIndent = 5.0;

	private static readonly DoubleComparer Comparer = new DoubleComparer(0.001);

	public void CalculatePanelRects(Size drawingPanelSize, Size actionPanelSize, Rect selectionRect, Rect boundingRect, out Rect drawingPanelRect, out Rect actionPanelRect)
	{
		selectionRect.Inflate(5.0, 5.0);
		selectionRect.Intersect(boundingRect);
		drawingPanelRect = new Rect(boundingRect.Location, drawingPanelSize);
		actionPanelRect = new Rect(boundingRect.Location, actionPanelSize);
		List<PlacementResult> list = new List<PlacementResult>();
		drawingPanelRect.X = selectionRect.Right - drawingPanelRect.Width;
		drawingPanelRect.Y = selectionRect.Bottom;
		actionPanelRect.X = selectionRect.Right;
		actionPanelRect.Y = selectionRect.Bottom - actionPanelRect.Height;
		list.AddRange(FindAllPlacementResults(drawingPanelRect, actionPanelRect, selectionRect, boundingRect));
		drawingPanelRect.X = selectionRect.Left;
		drawingPanelRect.Y = selectionRect.Bottom;
		actionPanelRect.X = selectionRect.Left - actionPanelRect.Width;
		actionPanelRect.Y = selectionRect.Bottom - actionPanelRect.Height;
		list.AddRange(FindAllPlacementResults(drawingPanelRect, actionPanelRect, selectionRect, boundingRect));
		drawingPanelRect.X = selectionRect.Right - drawingPanelRect.Width;
		drawingPanelRect.Y = selectionRect.Top - drawingPanelRect.Height;
		actionPanelRect.X = selectionRect.Right;
		actionPanelRect.Y = selectionRect.Top;
		list.AddRange(FindAllPlacementResults(drawingPanelRect, actionPanelRect, selectionRect, boundingRect));
		drawingPanelRect.X = selectionRect.Left;
		drawingPanelRect.Y = selectionRect.Top - drawingPanelRect.Height;
		actionPanelRect.X = selectionRect.Left - actionPanelRect.Width;
		actionPanelRect.Y = selectionRect.Top;
		list.AddRange(FindAllPlacementResults(drawingPanelRect, actionPanelRect, selectionRect, boundingRect));
		PlacementResult placementResult = list.Minima((PlacementResult x) => x.SelectionRectIntersectionArea, Comparer).Maxima((PlacementResult x) => x.SelectionRectContactPerimeter, Comparer).Minima((PlacementResult x) => x.MovingDistance, Comparer)
			.First();
		drawingPanelRect = placementResult.DrawingPanelRect;
		actionPanelRect = placementResult.ActionPanelRect;
	}

	private IEnumerable<PlacementResult> FindAllPlacementResults(Rect drawingPanelRect, Rect actionPanelRect, Rect selectionRect, Rect boundingRect)
	{
		Rect originalDrawingPanelRect = drawingPanelRect;
		Rect originalActionPanelRect = actionPanelRect;
		drawingPanelRect = drawingPanelRect.MoveInsideRect(boundingRect);
		actionPanelRect = actionPanelRect.MoveInsideRect(boundingRect);
		foreach (Rect foundDrawingPanelRect in FindAllDrawingPanelPositions(drawingPanelRect, actionPanelRect, selectionRect))
		{
			foreach (Rect item in FindAllActionPanelPositions(foundDrawingPanelRect, actionPanelRect, selectionRect))
			{
				Rect rect = foundDrawingPanelRect.MoveInsideRect(boundingRect);
				Rect rect2 = item.MoveInsideRect(boundingRect);
				if (!rect.HasNonZeroIntersectionWith(rect2))
				{
					yield return new PlacementResult(rect, rect2, originalDrawingPanelRect, originalActionPanelRect, selectionRect);
				}
			}
		}
	}

	private IEnumerable<Rect> FindAllDrawingPanelPositions(Rect drawingPanelRect, Rect actionPanelRect, Rect selectionRect)
	{
		yield return drawingPanelRect;
		drawingPanelRect.X = selectionRect.Left - drawingPanelRect.Width;
		yield return drawingPanelRect;
		drawingPanelRect.X = selectionRect.Left;
		yield return drawingPanelRect;
		drawingPanelRect.X = selectionRect.Right - drawingPanelRect.Width;
		yield return drawingPanelRect;
		drawingPanelRect.X = selectionRect.Right;
		yield return drawingPanelRect;
		drawingPanelRect.X = actionPanelRect.Left - drawingPanelRect.Width;
		yield return drawingPanelRect;
		drawingPanelRect.X = actionPanelRect.Left;
		yield return drawingPanelRect;
		drawingPanelRect.X = actionPanelRect.Right - drawingPanelRect.Width;
		yield return drawingPanelRect;
		drawingPanelRect.X = actionPanelRect.Right;
		yield return drawingPanelRect;
	}

	private IEnumerable<Rect> FindAllActionPanelPositions(Rect drawingPanelRect, Rect actionPanelRect, Rect selectionRect)
	{
		yield return actionPanelRect;
		actionPanelRect.Y = selectionRect.Top - actionPanelRect.Height;
		yield return actionPanelRect;
		actionPanelRect.Y = selectionRect.Top;
		yield return actionPanelRect;
		actionPanelRect.Y = selectionRect.Bottom - actionPanelRect.Height;
		yield return actionPanelRect;
		actionPanelRect.Y = selectionRect.Bottom;
		yield return actionPanelRect;
		actionPanelRect.Y = drawingPanelRect.Top - actionPanelRect.Height;
		yield return actionPanelRect;
		actionPanelRect.Y = drawingPanelRect.Top;
		yield return actionPanelRect;
		actionPanelRect.Y = drawingPanelRect.Bottom - actionPanelRect.Height;
		yield return actionPanelRect;
		actionPanelRect.Y = drawingPanelRect.Bottom;
		yield return actionPanelRect;
	}
}
