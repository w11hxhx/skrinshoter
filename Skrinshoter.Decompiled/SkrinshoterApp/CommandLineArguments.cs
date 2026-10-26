using System;
using System.IO;

namespace SkrinshoterApp;

public sealed class CommandLineArguments
{
	public CommandLineAction Action { get; }

	public string Argument { get; }

	public CommandLineArguments()
	{
		string[] commandLineArgs = Environment.GetCommandLineArgs();
		if (commandLineArgs.Length < 2)
		{
			return;
		}
		switch (commandLineArgs[1])
		{
		case "-s":
			Action = CommandLineAction.SilentRun;
			break;
		case "-r":
			Action = CommandLineAction.MakeScreenshot;
			break;
		case "-v":
			Action = CommandLineAction.RecordVideo;
			break;
		case "-f":
			if (commandLineArgs.Length > 2)
			{
				try
				{
					Argument = Path.GetFullPath(commandLineArgs[2]);
					Action = CommandLineAction.UploadFile;
					break;
				}
				catch (Exception)
				{
					break;
				}
			}
			break;
		case "-u":
			Action = CommandLineAction.Shutdown;
			break;
		}
	}

	public CommandLineArguments(CommandLineAction action, string argument = null)
	{
		if (action == CommandLineAction.UploadFile && string.IsNullOrEmpty(argument))
		{
			action = CommandLineAction.None;
		}
		Action = action;
		Argument = ((action == CommandLineAction.UploadFile) ? argument : null);
	}

	public string Serialize()
	{
		if (Argument != null)
		{
			return $"{Action}|{Argument}";
		}
		return Action.ToString();
	}

	public static CommandLineArguments Deserialize(string message)
	{
		if (message == null)
		{
			throw new ArgumentNullException("message");
		}
		string[] array = message.Split(new char[1] { '|' }, 2);
		if (!Enum.TryParse<CommandLineAction>(array[0], out var result))
		{
			result = CommandLineAction.None;
		}
		return new CommandLineArguments(result, (array.Length > 1) ? array[1] : null);
	}
}
