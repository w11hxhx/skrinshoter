using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class ScrollingCaptureStartWindowVM : LocalizableObservableObject
{
	private readonly OverlayWindowStateManager stateManager;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand<Window>? startCommand;

	public string StartToolTip => Resources.ScrollingCapture_StartToolTip;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand<Window> StartCommand => startCommand ?? (startCommand = new RelayCommand<Window>(Start));

	public ScrollingCaptureStartWindowVM(OverlayWindowStateManager stateManager)
	{
		this.stateManager = stateManager;
	}

	[RelayCommand]
	private void Start(Window window)
	{
		stateManager.SetState(OverlayWindowState.Scrolling, window.Owner.DataContext as OverlayWindowVM);
	}
}
