using System.ComponentModel;

namespace SkrinshoterApp;

public enum UndoActionKind
{
	[Description("Add")]
	Add,
	[Description("Delete")]
	Delete,
	[Description("Move")]
	Move,
	[Description("Resize")]
	Resize,
	[Description("Change line thickness of")]
	ChangeLineThickness,
	[Description("Change color of")]
	ChangeColor,
	[Description("Change text of")]
	ChangeText
}
