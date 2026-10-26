using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using CommunityToolkit.Mvvm.Input;
using SkrinshoterApp.Properties;

namespace SkrinshoterApp;

public class ScreenshotEditingActionWindowVM : LocalizableObservableObject
{
	private readonly Settings settings;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? uploadToCloudCommand;

	private RelayCommand? uploadToAllCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? copyToClipboardCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? printCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? saveToFileCommand;

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	private RelayCommand? closeCommand;

	public string UploadToCloudToolTip => "Загрузить в Скриншотер (Enter)";

	public string UploadToAllToolTip => "Загрузить во все выбранные сервисы" + (settings?.UploadScreenshotToAllHotkey.ToDisplaySuffix() ?? " (Ctrl+Enter)");

	public string CopyToClipboardToolTip => Resources.ScreenshotEditing_CopyToClipboardToolTip + " (Ctrl+C, Ctrl+Insert)";

	public string PrintToolTip => Resources.ScreenshotEditing_PrintToolTip + " (Ctrl+P)";

	public string SaveToFileToolTip => Resources.ScreenshotEditing_SaveToFileToolTip + " (Ctrl+S)";

	public string CloseToolTip => Resources.ScreenshotEditing_CloseToolTip + " (Esc)";

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand UploadToCloudCommand => uploadToCloudCommand ?? (uploadToCloudCommand = new RelayCommand(UploadToCloud));

	public IRelayCommand UploadToAllCommand => uploadToAllCommand ?? (uploadToAllCommand = new RelayCommand(UploadToAll));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CopyToClipboardCommand => copyToClipboardCommand ?? (copyToClipboardCommand = new RelayCommand(CopyToClipboard));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand PrintCommand => printCommand ?? (printCommand = new RelayCommand(Print));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand SaveToFileCommand => saveToFileCommand ?? (saveToFileCommand = new RelayCommand(SaveToFile));

	[GeneratedCode("CommunityToolkit.Mvvm.SourceGenerators.RelayCommandGenerator", "8.4.0.0")]
	[ExcludeFromCodeCoverage]
	public IRelayCommand CloseCommand => closeCommand ?? (closeCommand = new RelayCommand(Close));

	public event EventHandler<ScreenshotEditingActionSelectedEventArgs> ActionSelected;

	public ScreenshotEditingActionWindowVM(Settings settings = null)
	{
		this.settings = settings;
	}

	[RelayCommand]
	private void UploadToCloud()
	{
		ActionSelected?.Invoke(this, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.UploadToCloud));
	}

	private void UploadToAll()
	{
		ActionSelected?.Invoke(this, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.UploadToAll));
	}

	[RelayCommand]
	private void CopyToClipboard()
	{
		ActionSelected?.Invoke(this, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.CopyToClipboard));
	}

	[RelayCommand]
	private void Print()
	{
		ActionSelected?.Invoke(this, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.Print));
	}

	[RelayCommand]
	private void SaveToFile()
	{
		ActionSelected?.Invoke(this, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.SaveToFile));
	}

	[RelayCommand]
	private void Close()
	{
		ActionSelected?.Invoke(this, new ScreenshotEditingActionSelectedEventArgs(ScreenshotEditingAction.Close));
	}
}
