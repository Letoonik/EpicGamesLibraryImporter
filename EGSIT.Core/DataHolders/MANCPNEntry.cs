using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace EGSIT.Core;

/// <summary>
/// Every game installed from the Epig Game Store has some metadata stored in a .mancpn file that's found in a hidden folder named .egstore.
/// <para>Said file is in JSON format, and some of the fields can be used to reinsert missing entries in the file that stores the locally installed game list.</para>
/// </summary>
public class MANCPNEntry
{

	/// <summary>
	/// Full path to the .mancpn file that should be found in the <see cref="[GAME_ROOT_FOLDER]/.egstore"/> folder (which has its attributes set to "hidden" by default).
	/// </summary>
	public string FilePath { get; init; }

	/// <summary>
	/// Should point to the game installation folder. Matches <see cref="GameEntry.InstallLocationKey"/>
	/// </summary>
	public string GamePath { get; init; }

	/// <summary>Use is unknown to me, but it's there anyways just in case.</summary>
	public string FormatVersion { get; private set; }

	/// <summary>Matches <see cref="GameEntry.NamespaceIDKey"/></summary>
	public string CatalogNamespace { get; private set; }

	/// <summary>Matches <see cref="GameEntry.ItemIdKey"/></summary>
	public string CatalogItemId { get; private set; }

	/// <summary>Matches <see cref="GameEntry.AppNameKey"/> ("Yay!" for consistent naming) and seemingly <see cref="GameEntry.ArtifactIdKey"/> too.</summary>
	public string AppName { get; private set; }

	public string AppVersion { get; private set; }

	#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private MANCPNEntry(string filePath, string gamePath)
	{
		FilePath = filePath;
		GamePath = gamePath;
	}

	private void LoadContent()
	{
		var jsonDoc = JsonDocument.Parse(File.ReadAllText(FilePath));
		var jsonRoot = jsonDoc.RootElement.EnumerateObject().OfType<JsonProperty>().ToArray();
		var jsonAsDic = jsonRoot.ToDictionary(x => x.Name, x => x.Value.ToString());

		if (jsonAsDic.TryGetValue("FormatVersion", out var fv))
			FormatVersion = fv;

		if (jsonAsDic.TryGetValue("CatalogNamespace", out var ctns))
			CatalogNamespace = ctns;

		if (jsonAsDic.TryGetValue("CatalogItemId", out var cid))
			CatalogItemId = cid;

		if (jsonAsDic.TryGetValue("AppName", out var an))
			AppName = an;


    }

	/// <summary>Create an instance using data from a specific file.</summary>
	/// <param name="filePath">Path of the file to read content from</param>
	public static MANCPNEntry? FromFile(string filePath)
	{
		var gameDirInfo = Directory.GetParent(Directory.GetParent(filePath)?.FullName!);
		if (gameDirInfo is null)
			return null;

		var entry = new MANCPNEntry(filePath, gameDirInfo.FullName);
		entry.LoadContent();

		return entry;
	}
}
