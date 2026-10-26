using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Markup;
using CalcBinding.Trace;

namespace CalcBinding.PathAnalysis;

public class PropertyPathAnalyzer
{
	private class Chunk
	{
		public string Value { get; private set; }

		public int Start { get; private set; }

		public int End { get; private set; }

		public Chunk(string value, int startPosition, int endPosition)
		{
			Value = value;
			Start = startPosition;
			End = endPosition;
		}
	}

	public static char[] UnknownDelimiters;

	public static char[] KnownDelimiters;

	public static string[] Keywords;

	public static char[] QuoteTerminals;

	private static char[] delimiters;

	private IXamlTypeResolver _typeResolver;

	private static readonly Tracer Tracer;

	static PropertyPathAnalyzer()
	{
		UnknownDelimiters = new char[17]
		{
			'(', ')', '+', '-', '*', '/', '%', '^', '&', '|',
			'?', '<', '>', '=', '!', ',', ' '
		};
		KnownDelimiters = new char[2] { '.', ':' };
		Keywords = new string[1] { "null" };
		QuoteTerminals = new char[2] { '\'', '"' };
		Tracer = new Tracer(TraceComponent.Parser);
		delimiters = KnownDelimiters.Concat(UnknownDelimiters).Concat(QuoteTerminals).ToArray();
	}

	public List<PathToken> GetPathes(string normPath, IXamlTypeResolver typeResolver)
	{
		_typeResolver = typeResolver;
		List<Chunk> chunks = GetChunks(normPath);
		return GetPathes(chunks);
	}

	private List<Chunk> GetChunks(string str)
	{
		int num = 0;
		bool flag = false;
		List<Chunk> list = new List<Chunk>();
		int num2 = 0;
		bool flag2 = false;
		char c = '\'';
		while (true)
		{
			char c2 = ((num2 < str.Length) ? str[num2] : '\0');
			if (flag2)
			{
				if (c2 == c)
				{
					flag2 = false;
				}
			}
			else
			{
				bool flag3 = UnknownDelimiters.Contains(c2) || QuoteTerminals.Contains(c2) || c2 == '\0';
				if (flag && flag3)
				{
					list.Add(new Chunk(SubStr(str, num, num2 - 1), num, num2 - 1));
					flag = false;
				}
				if (!flag)
				{
					if (flag3)
					{
						if (QuoteTerminals.Contains(c2))
						{
							c = c2;
							flag2 = true;
						}
					}
					else
					{
						num = num2;
						flag = true;
					}
				}
			}
			if (c2 == '\0')
			{
				break;
			}
			num2++;
		}
		return list;
	}

	private List<PathToken> GetPathes(List<Chunk> chunks)
	{
		List<PathToken> list = new List<PathToken>();
		foreach (Chunk chunk in chunks)
		{
			if (GetPath(chunk, out var pathToken))
			{
				TracePath(pathToken);
				list.Add(pathToken);
			}
		}
		return list;
	}

	private bool GetPath(Chunk chunk, out PathToken pathToken)
	{
		string value = chunk.Value;
		if (Keywords.Contains(value))
		{
			pathToken = null;
			return false;
		}
		int num = value.IndexOf(':');
		List<string> propChain2;
		if (num > 0)
		{
			string text = SubStr(value, 0, num - 1);
			if (IsIdentifier(text) && GetPropChain(SubStr(value, num + 1, value.Length - 1), out var propChain) && propChain.Count() > 1)
			{
				pathToken = GetEnumOrStaticProperty(chunk, text, propChain);
				return true;
			}
		}
		else if (GetPropChain(value, out propChain2))
		{
			pathToken = GetPropPathOrMath(chunk, propChain2);
			return true;
		}
		pathToken = null;
		return false;
	}

	private bool GetPropChain(string str, out List<string> propChain)
	{
		string[] source = str.Split(new char[1] { '.' }, StringSplitOptions.None);
		if (source.All(IsIdentifier) && source.Any())
		{
			propChain = source.ToList();
			return true;
		}
		propChain = null;
		return false;
	}

	private bool IsIdentifier(string str)
	{
		if (str.Length == 0)
		{
			return false;
		}
		char c = str[0];
		if (char.IsDigit(c) || delimiters.Contains(c))
		{
			return false;
		}
		for (int i = 1; i <= str.Length - 1; i++)
		{
			if (delimiters.Contains(str[i]))
			{
				return false;
			}
		}
		return true;
	}

	private PathToken GetPropPathOrMath(Chunk chunk, List<string> propChain)
	{
		PathToken pathToken = null;
		if (propChain.Count() == 2 && propChain[0] == "Math")
		{
			return new MathToken(chunk.Start, chunk.End, propChain[1]);
		}
		return new PropertyPathToken(chunk.Start, chunk.End, propChain);
	}

	private PathToken GetEnumOrStaticProperty(Chunk chunk, string @namespace, List<string> identifierChain)
	{
		PathToken pathToken = null;
		string text = identifierChain[0];
		string fullTypeName = $"{@namespace}:{text}";
		List<string> list = identifierChain.Skip(1).ToList();
		Type type;
		if (list.Count == 1 && (type = TakeEnum(fullTypeName)) != null)
		{
			string enumMember = list.Single();
			return new EnumToken(chunk.Start, chunk.End, @namespace, type, enumMember);
		}
		return new StaticPropertyPathToken(chunk.Start, chunk.End, @namespace, text, list);
	}

	private string SubStr(string str, int start, int end)
	{
		return str.Substring(start, end - start + 1);
	}

	private Type TakeEnum(string fullTypeName)
	{
		Type type = _typeResolver.Resolve(fullTypeName);
		if (type != null && type.IsEnum)
		{
			return type;
		}
		return null;
	}

	private void TracePath(PathToken path)
	{
	}
}
