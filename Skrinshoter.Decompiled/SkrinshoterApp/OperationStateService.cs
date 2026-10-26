namespace SkrinshoterApp;

public class OperationStateService : IOperationStateProvider
{
	public OperationType CurrentOperation { get; private set; }

	public bool IsRecording { get; private set; }

	public bool TryStartOperation(OperationType type)
	{
		if (CurrentOperation != OperationType.None || type == OperationType.None)
		{
			return false;
		}
		CurrentOperation = type;
		return true;
	}

	public void StopOperation()
	{
		CurrentOperation = OperationType.None;
		IsRecording = false;
	}

	public bool TryStartRecording()
	{
		if (CurrentOperation != OperationType.Video || IsRecording)
		{
			return false;
		}
		IsRecording = true;
		return true;
	}

	public void StopRecording()
	{
		IsRecording = false;
	}
}
