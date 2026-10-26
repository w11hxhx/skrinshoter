using System;

namespace SkrinshoterApp;

public class SelectionHintObjectVMDesignFactory : DesignFactory<SelectionHintObjectVM>
{
	public override SelectionHintObjectVM Create(IServiceProvider sp)
	{
		return new SelectionHintObjectVM(new OverlayWindowVM(new OverlayWindowStateManager(), new AreaSelectionManager(), null, 1.0), "Icon.Screenshot");
	}
}
