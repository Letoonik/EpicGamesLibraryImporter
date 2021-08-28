using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using System.Diagnostics;

namespace EGSIT.Core;

public class ManifestItemEntry
{
	private JObject m_content;

	public string ItemFileName { get; private init; }
	public bool IsFromMANCPN { get; private init; }

	public string Guid { get; private init; }

	public string? InstallLocation => m_content.Property("InstallLocation")?.Value.ToString();

	public ManifestItemEntry() => m_content = new();

	public ManifestItemEntry(string itemPath)
	{
		ItemFileName = Path.GetFileName(itemPath);
		m_content = JObject.Parse(File.ReadAllText(itemPath));
		Guid = m_content.Property("InstallationGuid")?.Value.ToString();
	}

	public ManifestItemEntry(MANCPNEntry mancpn)
	{
		IsFromMANCPN = true;
		Guid = Path.GetFileNameWithoutExtension(mancpn.FilePath);
		ItemFileName = Path.GetFileName(mancpn.FilePath).Replace("mancpn", "item", StringComparison.InvariantCultureIgnoreCase);
		m_content = JObject.Parse(File.ReadAllText(mancpn.FilePath));
		m_content.Add(new JProperty("InstallationGuid", Guid));
		m_content.Add(new JProperty("InstallLocation", mancpn.GamePath ));
		m_content.Add(new JProperty("ManifestLocation", mancpn.GamePath+"/.egstore"));
		m_content.Add(new JProperty("StagingLocation", mancpn.GamePath + "/.egstore/bps"));
		m_content.Add(new JProperty("bIsIncompleteInstall", false));
		//m_content.Add(new JProperty("bNeedsValidation", true));

	}

	public void SaveToFile()
	{
		var filePath = Path.Combine(Globals.ManifestFolderPath, ItemFileName);
		try
		{
			File.WriteAllText(filePath, m_content.ToString());
		}
		catch (Exception)
		{
			Debug.WriteLine($"Couldn't write {filePath}");
		}
	}
}

