namespace SkrinshoterApp;

public interface IMergeableAction : IUndoableAction
{
	bool TryMerge(IMergeableAction action);
}
