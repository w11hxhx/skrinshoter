using System.ComponentModel;

namespace SkrinshoterApp;

public class PanelWindow : FramelessWindow
{
	private bool forceClose;

	public void ForceClose()
	{
		forceClose = true;
		Close();
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		e.Cancel = !forceClose;
	}
}
