using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
namespace SkrinshoterApp;
public sealed class UploadProviderWindow : Window
{
 public UploadProviderWindow(CustomUploadProvider provider)
 {
  Title = "Настройка сервиса загрузки"; Width = 520; Height = 650; WindowStartupLocation = WindowStartupLocation.CenterOwner;
  var panel = new StackPanel { Margin = new Thickness(20) };
  Content = new ScrollViewer { Content = panel, VerticalScrollBarVisibility = ScrollBarVisibility.Auto };
  TextBox Field(string label, string value, bool multiline = false)
  {
   panel.Children.Add(new TextBlock { Text = label, Margin = new Thickness(0,10,0,4), TextWrapping = TextWrapping.Wrap });
   var box = new TextBox { Text = value, MinHeight = multiline ? 80 : 28, AcceptsReturn = multiline, TextWrapping = TextWrapping.Wrap };
   panel.Children.Add(box); return box;
  }
  var name = Field("Название", provider.Name);
  var url = Field("URL загрузки (https://…)", provider.Url);
  var method = Field("Метод запроса", provider.Method);
  var file = Field("Имя поля файла в multipart/form-data", provider.FileField);
  var link = Field("Путь к ссылке в JSON (например data.url). Пусто — определить автоматически", provider.ImageLink);
  var deletion = Field("Путь к ссылке удаления в JSON (необязательно)", provider.DeletionLink);
  var headers = Field("Заголовки, по одному на строку: Authorization: Bearer …", string.Join(Environment.NewLine, System.Linq.Enumerable.Select(provider.Headers, x => x.Key + ": " + x.Value)), true);
  var arguments = Field("Дополнительные поля формы, по одному на строку: имя: значение", string.Join(Environment.NewLine, System.Linq.Enumerable.Select(provider.FormFields, x => x.Key + ": " + x.Value)), true);
  var error = new TextBlock { Foreground = System.Windows.Media.Brushes.Firebrick, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(0,10,0,0) }; panel.Children.Add(error);
  var save = new Button { Content = "Сохранить сервис", Margin = new Thickness(0,16,0,0), MinHeight = 32, IsDefault = true }; panel.Children.Add(save);
  save.Click += (s,e) => {
   try {
    if (string.IsNullOrWhiteSpace(name.Text)) throw new Exception("Введите название.");
    if (!Uri.TryCreate(url.Text.Trim(), UriKind.Absolute, out var uri) || (uri.Scheme != "https" && uri.Scheme != "http")) throw new Exception("Введите полный HTTP(S) URL.");
    string verb = method.Text.Trim().ToUpperInvariant();
    if (verb != "POST" && verb != "PUT" && verb != "PATCH") throw new Exception("Выберите POST, PUT или PATCH.");
    if (string.IsNullOrWhiteSpace(file.Text)) throw new Exception("Введите имя поля файла.");
    var values = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
    foreach (string line in headers.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
     int colon = line.IndexOf(':'); if (colon <= 0) throw new Exception("Заголовок должен иметь вид Имя: значение.");
     string key = line.Substring(0,colon).Trim(), value = line.Substring(colon+1).Trim();
     using (var request = new System.Net.Http.HttpRequestMessage()) request.Headers.Add(key, value);
     values.Add(key, value);
    }
    var formValues = new Dictionary<string,string>(StringComparer.OrdinalIgnoreCase);
    foreach (string line in arguments.Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)) {
     int colon = line.IndexOf(':'); if (colon <= 0) throw new Exception("Поле формы должно иметь вид Имя: значение.");
     formValues.Add(line.Substring(0,colon).Trim(), line.Substring(colon+1).Trim());
    }
    provider.Name = name.Text.Trim(); provider.Url = uri.AbsoluteUri; provider.Method = verb; provider.FileField = file.Text.Trim(); provider.ImageLink = link.Text.Trim(); provider.DeletionLink = deletion.Text.Trim(); provider.Headers = values; provider.FormFields = formValues;
    DialogResult = true;
   } catch (Exception ex) { error.Text = ex.Message; }
  };
 }
}
