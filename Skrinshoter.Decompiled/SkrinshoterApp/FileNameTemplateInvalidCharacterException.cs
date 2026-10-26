using System;

namespace SkrinshoterApp;

public class FileNameTemplateInvalidCharacterException : Exception
{
	public char InvalidCharacter { get; }

	public FileNameTemplateInvalidCharacterException(char invalidCharacter)
		: base($"Invalid character in file name template: '{invalidCharacter}'.")
	{
		InvalidCharacter = invalidCharacter;
	}
}
