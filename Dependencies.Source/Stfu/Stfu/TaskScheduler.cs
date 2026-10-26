using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace Stfu;

public static class TaskScheduler
{
	private class XmlFixer
	{
		public readonly HashSet<string> Removes = new HashSet<string>();

		public readonly Dictionary<string, string> Renames = new Dictionary<string, string>();

		public readonly Dictionary<string, string> Replaces = new Dictionary<string, string>
		{
			{ "ExecutionTimeLimit", "PT0S" },
			{ "MultipleInstancesPolicy", "Parallel" },
			{ "DisallowStartIfOnBatteries", "false" },
			{ "StopIfGoingOnBatteries", "false" },
			{ "StopOnIdleEnd", "false" },
			{ "StartWhenAvailable", "false" },
			{ "RunOnlyIfNetworkAvailable", "false" }
		};

		public void FixElement(XmlElement node)
		{
			if (Renames.TryGetValue(node.Name, out var value))
			{
				XmlElement xmlElement = node.OwnerDocument.CreateElement(value, node.NamespaceURI);
				foreach (XmlNode childNode in node.ChildNodes)
				{
					xmlElement.AppendChild(childNode.CloneNode(deep: true));
				}
				node.ParentNode.InsertBefore(xmlElement, node);
				node.ParentNode.RemoveChild(node);
				node = xmlElement;
			}
			if (Replaces.TryGetValue(node.Name, out var value2))
			{
				node.InnerText = value2;
			}
			if (node.HasChildNodes && node.FirstChild is XmlElement node2)
			{
				FixElement(node2);
			}
			if (node.NextSibling is XmlElement node3)
			{
				FixElement(node3);
			}
			if (Removes.Contains(node.Name))
			{
				node.ParentNode.RemoveChild(node);
			}
		}
	}

	public static bool HasTask(string task_name)
	{
		return !string.IsNullOrEmpty(RunSchTasks("/query /tn " + task_name + " /xml"));
	}

	public static bool InstallTask(string task_name, string command, bool elevated = false, string author = null)
	{
		string filename = Environment.SystemDirectory + "\\Tasks\\" + task_name;
		string tempFileName = Path.GetTempFileName();
		try
		{
			RunSchTasks("/tn " + task_name + " /f /create /sc onlogon /tr " + command);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			XmlFixer xmlFixer = new XmlFixer();
			if (elevated)
			{
				xmlFixer.Renames.Add("UserId", "GroupId");
				xmlFixer.Replaces.Add("RunLevel", "HighestAvailable");
				xmlFixer.Replaces.Add("GroupId", GetLocalUserGroupName());
				xmlFixer.Removes.Add("LogonType");
			}
			if (author != null)
			{
				xmlFixer.Replaces.Add("Author", author);
			}
			xmlFixer.FixElement(xmlDocument.DocumentElement);
			xmlDocument.Save(tempFileName);
			RunSchTasks("/tn " + task_name + " /f /create /xml \"" + tempFileName + "\"");
			File.Delete(tempFileName);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private static string GetLocalUserGroupName()
	{
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder.EnsureCapacity(128);
		stringBuilder2.EnsureCapacity(128);
		uint cchName = (uint)stringBuilder.Capacity;
		uint cchReferencedDomainName = (uint)stringBuilder2.Capacity;
		byte[] sid = new byte[16]
		{
			1, 2, 0, 0, 0, 0, 0, 5, 32, 0,
			0, 0, 33, 2, 0, 0
		};
		if (!NativeMethods.LookupAccountSid(null, sid, stringBuilder, ref cchName, stringBuilder2, ref cchReferencedDomainName, out var _))
		{
			return "BUILTIN\\Users";
		}
		return $"{stringBuilder2}\\{stringBuilder}";
	}

	private static string RunSchTasks(string args)
	{
		Process process = Process.Start(new ProcessStartInfo
		{
			FileName = "schtasks.exe",
			Arguments = args,
			UseShellExecute = false,
			RedirectStandardOutput = true,
			WindowStyle = ProcessWindowStyle.Hidden
		});
		process.WaitForExit();
		int exitCode = process.ExitCode;
		string result = process.StandardOutput.ReadToEnd();
		if (exitCode != 0)
		{
			return null;
		}
		return result;
	}
}
