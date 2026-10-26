using System;
using System.Collections.Generic;
using ScreenRecorderCommon;

namespace ScreenRecorder.Commands;

internal class CommandFactory
{
	public Command CreateCommand(string commandName, IReadOnlyList<string> commandArgs)
	{
		if (commandName == null)
		{
			throw new ArgumentNullException("commandName");
		}
		if (commandArgs == null)
		{
			throw new ArgumentNullException("commandArgs");
		}
		switch (commandName)
		{
		case "start":
			return new StartCommand(commandArgs);
		case "stop":
			CommandArgumentReader.EnsureNoArguments(commandArgs);
			return new Command(VideoModuleCommand.stop);
		case "cancel":
			CommandArgumentReader.EnsureNoArguments(commandArgs);
			return new Command(VideoModuleCommand.cancel);
		case "togglePause":
			CommandArgumentReader.EnsureNoArguments(commandArgs);
			return new Command(VideoModuleCommand.togglePause);
		case "setAudioDevice":
			return new SetAudioDeviceCommand(commandArgs);
		case "audioLevelStart":
			return new AudioLevelStartCommand(commandArgs);
		case "audioLevelStop":
			CommandArgumentReader.EnsureNoArguments(commandArgs);
			return new Command(VideoModuleCommand.audioLevelStop);
		case "screenshot":
			return new ScreenshotCommand(commandArgs);
		case "captureStart":
			return new CaptureStartCommand(commandArgs);
		case "captureStop":
			CommandArgumentReader.EnsureNoArguments(commandArgs);
			return new Command(VideoModuleCommand.captureStop);
		case "captureGetFrame":
			return new CaptureGetFrameCommand(commandArgs);
		case "terminate":
			CommandArgumentReader.EnsureNoArguments(commandArgs);
			return new Command(VideoModuleCommand.terminate);
		default:
			throw new FormatException("Unknown command.");
		}
	}
}
