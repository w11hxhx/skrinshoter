using System;
using System.Threading.Tasks;

namespace SkrinshoterApp.SkrChildApp;

public interface IChildProcess
{
	string ProcessPath { get; }

	void StartProcess();

	Task WaitForProcessExited(TimeSpan timeout);
}
