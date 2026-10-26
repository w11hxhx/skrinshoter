namespace SkrinshoterApp;

public interface IUndoableAction
{
	void Undo();

	void Redo();
}
