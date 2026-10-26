namespace SkrinshoterApp;

public interface IOperationStateProvider
{
	OperationType CurrentOperation { get; }

	bool IsRecording { get; }
}
