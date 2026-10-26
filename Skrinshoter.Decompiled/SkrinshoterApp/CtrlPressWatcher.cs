using System;
using System.Windows.Input;

namespace SkrinshoterApp;

public class CtrlPressWatcher : IDisposable
{
	private bool isLeftDown;

	private bool isRightDown;

	public bool IsDown
	{
		get
		{
			if (!isLeftDown)
			{
				return isRightDown;
			}
			return true;
		}
	}

	public event EventHandler Pressed;

	public CtrlPressWatcher()
	{
		InputManager.Current.PreProcessInput += OnPreProcessInput;
		isLeftDown = Keyboard.IsKeyDown(Key.LeftCtrl);
		isRightDown = Keyboard.IsKeyDown(Key.RightCtrl);
	}

	private void OnPreProcessInput(object sender, PreProcessInputEventArgs e)
	{
		if (e.StagingItem.Input is KeyEventArgs e2)
		{
			bool isDown = IsDown;
			switch (e2.Key)
			{
			default:
				return;
			case Key.LeftCtrl:
				isLeftDown = (e2.KeyStates & KeyStates.Down) != 0;
				break;
			case Key.RightCtrl:
				isRightDown = (e2.KeyStates & KeyStates.Down) != 0;
				break;
			}
			if (!isDown && IsDown)
			{
				Pressed?.Invoke(this, EventArgs.Empty);
			}
		}
	}

	public void Dispose()
	{
		InputManager.Current.PreProcessInput -= OnPreProcessInput;
	}
}
