using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Common;

namespace SkrinshoterApp;

public class ScreenshotEditor
{
	private readonly ScreenshotEditingDrawingWindowVM drawingWindowVM;

	private readonly OverlayWindowManager overlayWindowManager;

	private readonly OverlayWindowStateManager stateManager;

	private readonly SelectionManager selectionManager = new SelectionManager();

	private readonly UndoManager undoManager;

	private VisualObjectVM lastAddedVisualObjectVM;

	private readonly BlurredImageProvider blurredImageProvider = new BlurredImageProvider();

	public double LineThickness { get; private set; }

	public Color Color { get; private set; }

	public ScreenshotEditor(ScreenshotEditingDrawingWindowVM drawingWindowVM, OverlayWindowManager overlayWindowManager, OverlayWindowStateManager stateManager, UndoManager undoManager)
	{
		this.drawingWindowVM = drawingWindowVM;
		this.overlayWindowManager = overlayWindowManager;
		this.stateManager = stateManager;
		this.undoManager = undoManager;
		LineThickness = drawingWindowVM.SelectedLineThickness;
		Color = drawingWindowVM.SelectedColor;
		drawingWindowVM.PropertyChanged += DrawingWindowVM_PropertyChanged;
		drawingWindowVM.SelectedLineThicknessChanged += DrawingWindowVM_SelectedLineThicknessChanged;
		drawingWindowVM.SelectedColorChanged += DrawingWindowVM_SelectedColorChanged;
		stateManager.StateChanged += StateManager_StateChanged;
		selectionManager.SelectedObjectChanged += SelectionManager_SelectedObjectChanged;
		undoManager.ObjectAdded += UndoManager_ObjectAdded;
		foreach (OverlayWindow window in overlayWindowManager.Windows)
		{
			window.MouseLeftButtonDown += OverlayWindow_MouseLeftButtonDown;
		}
		foreach (OverlayWindowVM windowVM in overlayWindowManager.WindowVMs)
		{
			windowVM.VisualObjects.CollectionChanged += VisualObjects_CollectionChanged;
		}
	}

	private void DrawingWindowVM_PropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName == "SelectedTool")
		{
			bool flag = drawingWindowVM.SelectedTool != DrawingTool.None;
			stateManager.SetState(flag ? OverlayWindowState.DrawingToolSelected : OverlayWindowState.Selected);
			OverlayWindowVM selectedWindowVM = overlayWindowManager.GetSelectedWindowVM();
			if (drawingWindowVM.SelectedTool == DrawingTool.Blur && selectedWindowVM != null)
			{
				blurredImageProvider.GetBlurredImageAsync(selectedWindowVM);
			}
			RecreateFloatingObjects();
			selectionManager.Select(null);
			lastAddedVisualObjectVM = null;
		}
	}

	private void DrawingWindowVM_SelectedLineThicknessChanged(object sender, PropertyChangeSourceEventArgs e)
	{
		if (e.IsProgrammaticChange)
		{
			return;
		}
		if (selectionManager.SelectedObject != null)
		{
			selectionManager.SelectedObject.LineThickness = drawingWindowVM.SelectedLineThickness;
		}
		if (lastAddedVisualObjectVM != null)
		{
			lastAddedVisualObjectVM.LineThickness = drawingWindowVM.SelectedLineThickness;
		}
		LineThickness = drawingWindowVM.SelectedLineThickness;
		foreach (OverlayWindowVM windowVM in overlayWindowManager.WindowVMs)
		{
			if (windowVM.FloatingObject is VisualObjectVM visualObjectVM)
			{
				visualObjectVM.LineThickness = LineThickness;
			}
		}
	}

	private void DrawingWindowVM_SelectedColorChanged(object sender, PropertyChangeSourceEventArgs e)
	{
		if (e.IsProgrammaticChange)
		{
			return;
		}
		if (selectionManager.SelectedObject != null)
		{
			selectionManager.SelectedObject.Color = drawingWindowVM.SelectedColor;
		}
		if (lastAddedVisualObjectVM != null)
		{
			lastAddedVisualObjectVM.Color = drawingWindowVM.SelectedColor;
		}
		Color = drawingWindowVM.SelectedColor;
		foreach (OverlayWindowVM windowVM in overlayWindowManager.WindowVMs)
		{
			if (windowVM.FloatingObject is VisualObjectVM visualObjectVM)
			{
				visualObjectVM.Color = Color;
			}
		}
	}

	private void StateManager_StateChanged(object sender, EventArgs e)
	{
		if (stateManager.State == OverlayWindowState.DrawingToolSelected)
		{
			RecreateFloatingObjects();
		}
		else if (stateManager.State == OverlayWindowState.Selected)
		{
			lastAddedVisualObjectVM = null;
		}
	}

	private void SelectionManager_SelectedObjectChanged(object sender, EventArgs e)
	{
		if (selectionManager.SelectedObject != null)
		{
			drawingWindowVM.SetSelectedLineThickness(selectionManager.SelectedObject.LineThickness);
			drawingWindowVM.SetSelectedColor(selectionManager.SelectedObject.Color);
		}
		else
		{
			drawingWindowVM.SetSelectedLineThickness(LineThickness);
			drawingWindowVM.SetSelectedColor(Color);
		}
		lastAddedVisualObjectVM = null;
	}

	private void UndoManager_ObjectAdded(object sender, ObjectAddedEventArgs e)
	{
		lastAddedVisualObjectVM = e.VisualObject;
	}

	private void OverlayWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		OverlayWindowVM overlayWindowVM = (sender as OverlayWindow).DataContext as OverlayWindowVM;
		if (stateManager.State == OverlayWindowState.DrawingToolSelected && overlayWindowVM.FloatingObject is VisualObjectVM visualObjectVM)
		{
			overlayWindowVM.FloatingObject = null;
			visualObjectVM.NeedsInteractiveDrawing = true;
			overlayWindowVM.VisualObjects.Insert(visualObjectVM.GetInsertZIndex(), visualObjectVM);
			e.Handled = true;
		}
	}

	internal void OverlayWindow_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (overlayWindowManager.GetSelectedWindowVM() != null && stateManager.State.In(OverlayWindowState.Selected, OverlayWindowState.DrawingToolSelected, OverlayWindowState.EditingText) && selectionManager.SelectedObject != null)
		{
			selectionManager.Select(null);
			e.Handled = true;
		}
	}

	private void VisualObjects_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		RecreateFloatingObjects();
	}

	public void DeleteSelectedObject()
	{
		if (selectionManager.SelectedObject != null)
		{
			VisualObjectVM selectedObject = selectionManager.SelectedObject;
			OverlayWindowVM overlayWindowVM = selectedObject.OverlayWindowVM;
			if (stateManager.State == OverlayWindowState.DrawingToolSelected)
			{
				selectionManager.Select(null);
				undoManager.RegisterAction(new DeleteObjectAction(selectedObject, selectionManager));
				overlayWindowVM.VisualObjects.Remove(selectedObject);
			}
		}
	}

	private VisualObjectVM CreateVisualObject(DrawingTool tool, OverlayWindowVM overlayWindowVM)
	{
		return tool switch
		{
			DrawingTool.Arrow => (VisualObjectVM)new ArrowObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color), 
			DrawingTool.Line => new LineObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color), 
			DrawingTool.Pencil => new PencilObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color), 
			DrawingTool.Marker => new MarkerObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color), 
			DrawingTool.Rectangle => new RectangleObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color), 
			DrawingTool.Oval => new OvalObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color), 
			DrawingTool.Text => new TextObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color), 
			DrawingTool.Number => new NumberObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color, GetNextNumberValue()), 
			DrawingTool.Blur => new BlurObjectVM(overlayWindowVM, stateManager, selectionManager, undoManager, LineThickness, Color, blurredImageProvider), 
			_ => null, 
		};
	}

	private void RecreateFloatingObjects()
	{
		if (drawingWindowVM.SelectedTool == DrawingTool.None)
		{
			return;
		}
		foreach (OverlayWindow window in overlayWindowManager.Windows)
		{
			OverlayWindowVM overlayWindowVM = window.DataContext as OverlayWindowVM;
			VisualObjectVM visualObjectVM = CreateVisualObject(drawingWindowVM.SelectedTool, overlayWindowVM);
			visualObjectVM.NeedsInteractiveDrawing = false;
			Point position = Mouse.GetPosition(window);
			visualObjectVM.UpdateFloatingObjectPosition(position);
			overlayWindowVM.FloatingObject = visualObjectVM;
		}
	}

	private int GetNextNumberValue()
	{
		HashSet<int> hashSet = new HashSet<int>();
		foreach (OverlayWindow window in overlayWindowManager.Windows)
		{
			OverlayWindowVM overlayWindowVM = window.DataContext as OverlayWindowVM;
			hashSet.UnionWith(from x in overlayWindowVM.VisualObjects.OfType<NumberObjectVM>()
				select x.Number);
		}
		int num;
		for (num = 1; hashSet.Contains(num); num++)
		{
		}
		return num;
	}
}
