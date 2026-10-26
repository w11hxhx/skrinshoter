using CommunityToolkit.Mvvm.Input;

namespace SkrinshoterApp;

public static class RelayCommandExtensions
{
	public static void ExecuteIfCan<T>(this IRelayCommand<T> command, T parameter)
	{
		if (command != null && command.CanExecute(parameter))
		{
			command.Execute(parameter);
		}
	}

	public static void ExecuteIfCan(this IRelayCommand command)
	{
		if (command != null && command.CanExecute(null))
		{
			command.Execute(null);
		}
	}
}
