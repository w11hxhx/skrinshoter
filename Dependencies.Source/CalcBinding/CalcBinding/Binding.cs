using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using CalcBinding.ExpressionParsers;
using CalcBinding.PathAnalysis;

namespace CalcBinding;

public sealed class Binding : MarkupExtension
{
	private class PathAppearances
	{
		public PathTokenId PathId { get; private set; }

		public IEnumerable<PathToken> Pathes { get; private set; }

		public PathAppearances(PathTokenId id, List<PathToken> pathes)
		{
			PathId = id;
			Pathes = pathes;
		}
	}

	private static Lazy<IExpressionParser> _parser = new Lazy<IExpressionParser>(() => new ParserFactory().CreateCachedParser());

	public string Path { get; set; }

	public FalseToVisibility FalseToVisibility { get; set; } = FalseToVisibility.Collapsed;

	public bool SingleQuotes { get; set; }

	public object FallbackValue { get; set; } = DependencyProperty.UnsetValue;

	[DefaultValue("")]
	public IMultiValueConverter Converter { get; set; }

	[DefaultValue("")]
	[TypeConverter(typeof(CultureInfoIetfLanguageTagConverter))]
	public CultureInfo ConverterCulture { get; set; }

	[DefaultValue("")]
	public object ConverterParameter { get; set; }

	[DefaultValue(BindingMode.Default)]
	public BindingMode Mode { get; set; }

	[DefaultValue(false)]
	public bool NotifyOnSourceUpdated { get; set; }

	[DefaultValue(false)]
	public bool NotifyOnTargetUpdated { get; set; }

	[DefaultValue(false)]
	public bool NotifyOnValidationError { get; set; }

	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public UpdateSourceExceptionFilterCallback UpdateSourceExceptionFilter { get; set; }

	public UpdateSourceTrigger UpdateSourceTrigger { get; set; }

	[DefaultValue(false)]
	public bool ValidatesOnDataErrors { get; set; }

	[DefaultValue(false)]
	public bool ValidatesOnExceptions { get; set; }

	[DefaultValue(true)]
	public bool ValidatesOnNotifyDataErrors { get; set; }

	[DefaultValue("")]
	public RelativeSource RelativeSource { get; set; }

	public object Source { get; set; }

	[DefaultValue("")]
	public string ElementName { get; set; }

	[DefaultValue("")]
	public string StringFormat { get; set; }

	public Binding()
	{
		Mode = BindingMode.Default;
	}

	public Binding(string path)
		: this()
	{
		Path = path;
	}

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		Type propertyType = GetPropertyType(serviceProvider);
		IXamlTypeResolver typeResolver = (IXamlTypeResolver)serviceProvider.GetService(typeof(IXamlTypeResolver));
		ITypeDescriptorContext context = serviceProvider as ITypeDescriptorContext;
		string text = NormalizePath(Path);
		List<PathAppearances> sourcePathes = GetSourcePathes(text, typeResolver);
		string expressionTemplate = GetExpressionTemplate(text, sourcePathes, out var enumParameters);
		CalcConverter converter = new CalcConverter(_parser.Value, FallbackValue, enumParameters)
		{
			FalseToVisibility = FalseToVisibility,
			StringFormatDefined = (StringFormat != null)
		};
		List<PathAppearances> list = sourcePathes.Where((PathAppearances p) => p.PathId.PathType == PathTokenType.Property || p.PathId.PathType == PathTokenType.StaticProperty).ToList();
		BindingBase bindingBase;
		if (list.Count == 1)
		{
			System.Windows.Data.Binding binding = new System.Windows.Data.Binding
			{
				Mode = Mode,
				NotifyOnSourceUpdated = NotifyOnSourceUpdated,
				NotifyOnTargetUpdated = NotifyOnTargetUpdated,
				NotifyOnValidationError = NotifyOnValidationError,
				UpdateSourceExceptionFilter = UpdateSourceExceptionFilter,
				UpdateSourceTrigger = UpdateSourceTrigger,
				ValidatesOnDataErrors = ValidatesOnDataErrors,
				ValidatesOnExceptions = ValidatesOnExceptions,
				FallbackValue = FallbackValue,
				ValidatesOnNotifyDataErrors = ValidatesOnNotifyDataErrors
			};
			PathTokenId pathId = list.Single().PathId;
			string text2 = pathId.Value;
			if (pathId.PathType == PathTokenType.StaticProperty)
			{
				text2 = $"({text2})";
			}
			PropertyPath path = (PropertyPath)new PropertyPathConverter().ConvertFromString(context, text2);
			binding.Path = path;
			if (Source != null)
			{
				binding.Source = Source;
			}
			if (ElementName != null)
			{
				binding.ElementName = ElementName;
			}
			if (RelativeSource != null)
			{
				binding.RelativeSource = RelativeSource;
			}
			if (StringFormat != null)
			{
				binding.StringFormat = StringFormat;
			}
			if ((expressionTemplate != "{0}" && expressionTemplate != "({0})") || propertyType == typeof(Visibility))
			{
				binding.Converter = converter;
				binding.ConverterParameter = expressionTemplate;
				binding.ConverterCulture = ConverterCulture;
			}
			bindingBase = binding;
		}
		else
		{
			MultiBinding multiBinding = new MultiBinding
			{
				Converter = converter,
				ConverterParameter = expressionTemplate,
				ConverterCulture = ConverterCulture,
				Mode = BindingMode.OneWay,
				NotifyOnSourceUpdated = NotifyOnSourceUpdated,
				NotifyOnTargetUpdated = NotifyOnTargetUpdated,
				NotifyOnValidationError = NotifyOnValidationError,
				UpdateSourceExceptionFilter = UpdateSourceExceptionFilter,
				UpdateSourceTrigger = UpdateSourceTrigger,
				ValidatesOnDataErrors = ValidatesOnDataErrors,
				ValidatesOnExceptions = ValidatesOnExceptions,
				FallbackValue = FallbackValue,
				ValidatesOnNotifyDataErrors = ValidatesOnNotifyDataErrors
			};
			if (StringFormat != null)
			{
				multiBinding.StringFormat = StringFormat;
			}
			foreach (PathAppearances item in list)
			{
				System.Windows.Data.Binding binding2 = new System.Windows.Data.Binding();
				string text3 = item.PathId.Value;
				if (item.PathId.PathType == PathTokenType.StaticProperty)
				{
					text3 = $"({text3})";
				}
				PropertyPath path2 = (PropertyPath)new PropertyPathConverter().ConvertFromString(context, text3);
				binding2.Path = path2;
				if (Source != null)
				{
					binding2.Source = Source;
				}
				if (ElementName != null)
				{
					binding2.ElementName = ElementName;
				}
				if (RelativeSource != null)
				{
					binding2.RelativeSource = RelativeSource;
				}
				multiBinding.Bindings.Add(binding2);
			}
			bindingBase = multiBinding;
		}
		return bindingBase.ProvideValue(serviceProvider);
	}

	[EditorBrowsable(EditorBrowsableState.Never)]
	public static void ReplaceExpressionParser(IExpressionParser expressionParser)
	{
		if (expressionParser == null)
		{
			throw new ArgumentNullException("expressionParser");
		}
		_parser = new Lazy<IExpressionParser>(() => expressionParser);
	}

	private Type GetPropertyType(IServiceProvider serviceProvider)
	{
		IProvideValueTarget provideValueTarget = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));
		if (provideValueTarget.TargetProperty is DependencyProperty)
		{
			return ((DependencyProperty)provideValueTarget.TargetProperty).PropertyType;
		}
		return provideValueTarget.TargetProperty.GetType();
	}

	private string GetExpressionTemplate(string path, List<PathAppearances> properties, out Dictionary<string, Type> enumParameters)
	{
		string text = "";
		int sourceIndex = 0;
		Dictionary<PathTokenId, string> dictionary = new Dictionary<PathTokenId, string>();
		Dictionary<PathTokenId, string> dictionary2 = new Dictionary<PathTokenId, string>();
		enumParameters = new Dictionary<string, Type>();
		while (sourceIndex < path.Length)
		{
			bool flag = false;
			for (int i = 0; i < properties.Count(); i++)
			{
				PathAppearances pathAppearances = properties[i];
				PathTokenId pathId = pathAppearances.PathId;
				if (pathAppearances.Pathes.FirstOrDefault((PathToken token) => token.Start == sourceIndex) == null)
				{
					continue;
				}
				string value = pathId.Value;
				if (pathId.PathType == PathTokenType.Property || pathId.PathType == PathTokenType.StaticProperty)
				{
					string text2 = null;
					if (dictionary.ContainsKey(pathId))
					{
						text2 = dictionary[pathId];
					}
					else
					{
						text2 = dictionary.Count.ToString("{0}");
						dictionary.Add(pathId, text2);
					}
					text += text2;
					sourceIndex += value.Length;
					flag = true;
				}
				else if (pathId.PathType == PathTokenType.Enum)
				{
					EnumToken enumToken = pathAppearances.Pathes.First() as EnumToken;
					string text3 = null;
					if (dictionary2.ContainsKey(pathId))
					{
						text3 = dictionary2[pathId];
					}
					else
					{
						text3 = GetEnumName(dictionary2.Count);
						dictionary2.Add(pathId, text3);
						enumParameters.Add(text3, enumToken.Enum);
					}
					string text4 = string.Join(".", text3, enumToken.EnumMember);
					text += text4;
					sourceIndex += value.Length;
					flag = true;
				}
				if (flag)
				{
					break;
				}
			}
			if (!flag)
			{
				text += path[sourceIndex];
				sourceIndex++;
			}
		}
		return text;
	}

	private string GetEnumName(int i)
	{
		return $"Enum{++i}";
	}

	private List<PathAppearances> GetSourcePathes(string normPath, IXamlTypeResolver typeResolver)
	{
		return (from p in new PropertyPathAnalyzer().GetPathes(normPath, typeResolver)
			group p by p.Id into p
			select new PathAppearances(p.Key, p.ToList())).ToList();
	}

	private string NormalizePath(string path)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>
		{
			{ " and ", " && " },
			{ ")and ", ")&& " },
			{ " and(", " &&(" },
			{ ")and(", ")&&(" },
			{ " or ", " || " },
			{ ")or ", ")|| " },
			{ " or(", " ||(" },
			{ ")or(", ")||(" },
			{ " less ", " < " },
			{ ")less ", ")< " },
			{ " less(", " <(" },
			{ ")less(", ")<(" },
			{ " less=", " <=" },
			{ ")less=", ")<=" },
			{ "not ", "!" }
		};
		if (!SingleQuotes)
		{
			dictionary.Add("'", "\"");
		}
		else
		{
			dictionary.Add("\"", "'");
		}
		string text = path;
		foreach (KeyValuePair<string, string> item in dictionary)
		{
			text = text.Replace(item.Key, item.Value);
		}
		return text;
	}
}
