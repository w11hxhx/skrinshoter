using System;

namespace SkrinshoterApp;

public class FileNameTemplateEmptyException : Exception
{
	public FileNameTemplateEmptyException()
		: base("File name template is empty.")
	{
	}
}
