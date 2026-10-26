using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using CommunityToolkit.Mvvm.Input;

namespace SkrinshoterApp;

public class ResizableObjectVM<THandle> : VisualObjectVM
{
	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseEventArgs>? resizeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<MouseButtonEventArgs>? endResizeCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseEventArgs> ResizeCommand => resizeCommand ?? (resizeCommand = new RelayCommand<MouseEventArgs>(Resize));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<MouseButtonEventArgs> EndResizeCommand => endResizeCommand ?? (endResizeCommand = new RelayCommand<MouseButtonEventArgs>(EndResize));

	protected ResizableObjectVM(OverlayWindowVM overlayWindowVM, OverlayWindowStateManager stateManager, SelectionManager selectionManager, UndoManager undoManager, double lineThickness, Color color)
		: base(overlayWindowVM, stateManager, selectionManager, undoManager, lineThickness, color)
	{
	}

	[RelayCommand]
	private void Resize(MouseEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.ResizingObject && isDragging)
		{
			Resize(e.GetPosition(null));
			e.Handled = true;
		}
	}

	[RelayCommand]
	private void EndResize(MouseButtonEventArgs e)
	{
		if (stateManager.State == OverlayWindowState.ResizingObject && isDragging)
		{
			isDragging = false;
			EndResize();
			stateManager.SetState(OverlayWindowState.DrawingToolSelected);
			(e.Source as UIElement).ReleaseMouseCapture();
			e.Handled = true;
		}
	}

	protected virtual void StartResize(Point mousePosition, THandle resizeHandle)
	{
	}

	protected virtual void Resize(Point mousePosition)
	{
	}

	protected virtual void EndResize()
	{
	}

	protected void StartResize(MouseButtonEventArgs e, THandle resizeHandle)
	{
		if (stateManager.State == OverlayWindowState.DrawingToolSelected)
		{
			CaptureMouse(e.Source as UIElement);
			isDragging = true;
			stateManager.SetState(OverlayWindowState.ResizingObject, OverlayWindowVM);
			selectionManager.Select(this);
			StartResize(e.GetPosition(null), resizeHandle);
			e.Handled = true;
		}
	}
}
