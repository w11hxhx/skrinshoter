namespace SkrinshoterApp;

public sealed class PrintSelectionDto
{
	public string PrinterName { get; set; } = string.Empty;

	public string PrintTicketXml { get; set; } = string.Empty;

	public string Error { get; set; } = string.Empty;
}
