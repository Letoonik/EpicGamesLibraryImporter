
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EGSIT.Core;

/// <summary>
/// Instances of this class represent game entries from the LauncherInstalled.dat file, a JSON file that contains a record of locally installed games.
/// <para>Some of its fields (including essential ones) can be matched from .mancpn files, some others not, but it seems that the EGL can regenerate some of missing values by itself.</para>
/// </summary>
public class GameEntry 
{
	public const string InstallLocationKey = "InstallLocation";
	public const string NamespaceIDKey = "NamespaceId";
	public const string ItemIdKey = "ItemId";
	public const string ArtifactIdKey = "ArtifactId";
	public const string AppVersionKey = "AppVersion";
	public const string AppNameKey = "AppName";

	private readonly Dictionary<string, string> m_data = new();
	public IReadOnlyDictionary<string, string> Data => m_data;

	public bool IsFromLoadedLibrary { get; init; }
	public string? InstallLocation => m_data.TryGetValue(InstallLocationKey, out var str) ? str : null;

	public string? ItemId => m_data.TryGetValue(ItemIdKey, out var str) ? str : null;

	private ManifestItemEntry? m_manifestItem;
	public ManifestItemEntry ManifestItem => m_manifestItem;

	private GameEntry() { }

	/// <summary>
	/// Create object by matching data recovered from .mancpn file entry.
	/// </summary>
	/// <param name="scannedEntry"></param>
	public GameEntry(MANCPNEntry scannedEntry)
	{
		m_data[InstallLocationKey] = scannedEntry.GamePath;
		m_data[ItemIdKey] = scannedEntry.CatalogItemId;
		m_data[NamespaceIDKey] = scannedEntry.CatalogNamespace;
		m_data[AppNameKey] = scannedEntry.AppName;
		m_data[AppVersionKey] = "1.0.0";
		m_data[ArtifactIdKey] = "";
		m_manifestItem = new(scannedEntry);
	}

	public GameEntry(JsonElement element, bool isFromLibrary)
	{
		m_data = element.EnumerateObject().ToDictionary(x => x.Name, x => x.Value.ToString());
		IsFromLoadedLibrary = isFromLibrary;
	}

	public GameEntry(JToken element, ManifestItemEntry manifestItem, bool isFromLibrary)
	{
		m_data = element.OfType<JProperty>().ToDictionary(x => x.Name, x => x.Value.ToString());
		IsFromLoadedLibrary = isFromLibrary;
		m_manifestItem = manifestItem;
	}

	public static GameEntry CreateDummy()
	{
		var entry = new GameEntry();
		entry.m_data[InstallLocationKey] = @"C:\temp\EGSFakeGame";
		entry.m_data[ItemIdKey] = "0";
		entry.m_data[NamespaceIDKey] = "0";
		entry.m_data[AppNameKey] = "EGS Fake Game";
		return entry;
	}

	public override bool Equals(object? obj)
	{
		return obj is GameEntry entry &&
			   (InstallLocation == entry.InstallLocation || entry.ItemId == ItemId);
	}

	public override int GetHashCode()
	{
		return HashCode.Combine(ItemId);
	}
}
