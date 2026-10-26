using NLog;

namespace SkrinshoterApp;

internal class AppLogger
{
	public static Logger Logger { get; } = LogManager.GetCurrentClassLogger();
}
