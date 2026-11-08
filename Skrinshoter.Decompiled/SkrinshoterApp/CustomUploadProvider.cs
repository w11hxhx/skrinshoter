using System.Collections.Generic;
namespace SkrinshoterApp;
public sealed class CustomUploadProvider
{
 public string Name { get; set; } = "Мой сервис";
 public string Url { get; set; } = "";
 public string Method { get; set; } = "POST";
 public string FileField { get; set; } = "file";
 public string ImageLink { get; set; } = "";
 public string DeletionLink { get; set; } = "";
 public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
 public Dictionary<string, string> FormFields { get; set; } = new Dictionary<string, string>();
 public bool Enabled { get; set; } = true;
}
