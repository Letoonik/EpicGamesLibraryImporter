using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace EGSIT.Core.FileHandling;

public static class FileSearch
{
	public static FileInfo FindGameExecutable(string rootGamePath)
	{
		var dirInfo = new DirectoryInfo(rootGamePath);
		var folderName = Directory.GetParent(rootGamePath + "/")?.Name ?? "";
		var foundExecutables = dirInfo.GetFiles("*.exe", SearchOption.AllDirectories);

		return foundExecutables.MinBy( x=> x.Name.Replace(x.Extension,"").TrimEnd('.').CalculateLevenshteinDistance(folderName));
	}

	public static async Task< ConcurrentQueue<MANCPNEntry>> FindGames(string rootSearchPath, int maxDepth = int.MaxValue)
	{
		ConcurrentQueue<MANCPNEntry> foundEntries = new();
		await FindGames(rootSearchPath, foundEntries, maxDepth);

		return foundEntries;
	}

	private static async Task FindGames(string currentPath, ConcurrentQueue<MANCPNEntry> foundEntries, int callDepth)
	{
		var egStoreFolder = Path.Combine(currentPath, ".egstore");

		if (Directory.Exists(egStoreFolder))
		{
			try
			{
				var found = Directory.EnumerateFiles(egStoreFolder, "*.mancpn", SearchOption.TopDirectoryOnly).FirstOrDefault();
				if (found is null)
					return;

				var entry = MANCPNEntry.FromFile(found);
				if (entry is not null)
					foundEntries.Enqueue(entry);
				await Task.CompletedTask;
				return;
			} catch (Exception e)
			{
				Debug.WriteLine($"Unable to scan directory '{currentPath}'. Skipping! ");
			}
		}

		if (--callDepth >= 0)
		{
			var opt = new EnumerationOptions() { IgnoreInaccessible = true,  };
			foreach (string dir in Directory.EnumerateDirectories(currentPath, "*", opt))
			{
				try
				{
					FindGames(dir, foundEntries, callDepth);
				}
				catch (Exception e)
				{
					Debug.WriteLine($"Unable to scan directory '{dir}'. Skipping! ");
				}

			}
		}
	}

	public  static async Task<ConcurrentQueue<MANCPNEntry>> FindGames_WindowsSearchIndex(string rootSearchPath)
	{
		//using var oleConnec = new OleDbConnection("Provider=Search.CollatorDSO;Extended Properties='Application=Windows';");
		//try
		//{
		//	oleConnec.Open();
		//}
		//catch (Exception)
		//{

		//	throw;
		//}
		//finally
		//{
		//	oleConnec.Close();
		//}
		return null;
	}
}

