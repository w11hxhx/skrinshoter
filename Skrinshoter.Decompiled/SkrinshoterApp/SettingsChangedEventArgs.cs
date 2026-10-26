using System;

namespace SkrinshoterApp;

public sealed class SettingsChangedEventArgs : EventArgs
{
	public Settings Old { get; }

	public Settings New { get; }

	public SettingsChangedEventArgs(Settings oldSettings, Settings newSettings)
	{
		Old = oldSettings;
		New = newSettings;
	}
}
