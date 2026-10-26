using System;
using System.Threading;
using System.Windows;

namespace SkrinshoterApp.Services.Progress;

public interface IProgressWindowService
{
	IProgressSession TryShow(string message, bool showUi, Window owner = null, CancellationToken externalToken = default(CancellationToken));

	IProgressSession TryShow(string message, bool showUi, IntPtr ownerHwnd, CancellationToken externalToken = default(CancellationToken));
}
