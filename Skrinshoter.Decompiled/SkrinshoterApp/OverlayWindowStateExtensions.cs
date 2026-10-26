namespace SkrinshoterApp;

public static class OverlayWindowStateExtensions
{
	public static bool IsProcess(this OverlayWindowState state)
	{
		if ((uint)(state - 1) <= 3u || (uint)(state - 6) <= 7u)
		{
			return true;
		}
		return false;
	}
}
