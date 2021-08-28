using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGSIT.Core;

public static class Utils
{
	private const string EGLProcessName = "EpicGamesLauncher";

	public static bool CheckIfEGLIsRunning()
	{
		var processes = Process.GetProcessesByName(EGLProcessName);
		return processes.Any();
	}

	public static void KillEGLProcesses()
	{
		var processes = Process.GetProcessesByName(EGLProcessName);
		processes.ToList().ForEach(x=> x.Kill(true));
	}

	public static async Task WaitUntilEGLClosedAsync()
	{
		while (CheckIfEGLIsRunning())
			await Task.Delay(250);

		await Task.CompletedTask;
	}

	/// <summary>
	/// Calculate the difference between 2 strings using the Levenshtein distance algorithm.
	/// <para>Credits go to <see href="https://gist.github.com/Davidblkx/e12ab0bb2aff7fd8072632b396538560"/></para>
	/// </summary>
	/// <param name="source1">First string</param>
	/// <param name="source2">Second string</param>
	/// <returns></returns>
	public static int CalculateLevenshteinDistance(this string source1, string source2) //O(n*m)
	{
		var source1Length = source1.Length;
		var source2Length = source2.Length;

		var matrix = new int[source1Length + 1, source2Length + 1];

		// First calculation, if one entry is empty return full length
		if (source1Length == 0)
			return source2Length;

		if (source2Length == 0)
			return source1Length;

		// Initialization of matrix with row size source1Length and columns size source2Length
		for (var i = 0; i <= source1Length; matrix[i, 0] = i++) { }
		for (var j = 0; j <= source2Length; matrix[0, j] = j++) { }

		// Calculate rows and collumns distances
		for (var i = 1; i <= source1Length; i++)
		{
			for (var j = 1; j <= source2Length; j++)
			{
				var cost = (source2[j - 1] == source1[i - 1]) ? 0 : 1;

				matrix[i, j] = Math.Min(
					Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1),
					matrix[i - 1, j - 1] + cost);
			}
		}
		// return result
		return matrix[source1Length, source2Length];
	}

}

