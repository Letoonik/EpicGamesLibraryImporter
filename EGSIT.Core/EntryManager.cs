using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using EGSIT.Core.Exceptions;
using EGSIT.Core.FileHandling;
using System.IO;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;
using WinCopies.Linq;

namespace EGSIT.Core;

public class EntryManager
{
	public bool IsInit { get; private set;  }

	private JObject m_fullJSONdoc;

	public IReadOnlyDictionary<string,ManifestItemEntry> ExistingManifests;

	public IEnumerable<GameEntry> InstalledEntries => m_installedEntries;
	private HashSet<GameEntry> m_installedEntries = new();

	public IEnumerable<GameEntry> ScannedEntries => m_scannedEntries;
	private HashSet<GameEntry> m_scannedEntries = new();

	public IEnumerable<GameEntry> AllEntries => InstalledEntries.Concat(ScannedEntries);

	public void Initialize()
	{
		if (!File.Exists(Globals.InstalledJSONPath))
		{
			throw new EpicGamesException("The Epic Games library file couldn't be accessed.") { ErrorCode = EpicGamesErrorCode.LibraryFileNotFound };
			// Make sure you ran the EPic Games Launcher at least once and you have sufficent access rights.
		}

		if (Globals.EnableEGLRunStateCheck && Utils.CheckIfEGLIsRunning())
		{
			if (Globals.IsRunningFromUIApp)
				throw new EpicGamesException(Globals.EglIsRunningMsg) { ErrorCode = EpicGamesErrorCode.LibraryAccessWhileEGLRunning };
		}

		LoadCurrentlyInstalled();
		IsInit = true;
	}


	private void LoadCurrentlyInstalled()
	{
		//manifest .item files in %ProgramData%\Epic\EpicGamesLauncher\Data\Manifests
		ExistingManifests = Directory.EnumerateFiles(Globals.ManifestFolderPath, "*.item")
										.ToDictionary(x => Path.GetFileNameWithoutExtension(x), x => new ManifestItemEntry(x));

		//entries in %ProgramData%\Epic\UnrealEngineLauncher\LauncherInstalled.dat
		m_fullJSONdoc = JObject.Parse(File.ReadAllText(Globals.InstalledJSONPath));

		var jsonInstallationList = m_fullJSONdoc.GetValue("InstallationList");
		var jsonInstalled = jsonInstallationList.OfType<JObject>().ToArray();
		
		GameEntry _CreateEntryWithMatchingManifest(JObject x)
		{
			var installLocation = x.Property("InstallLocation").Value.ToString();
			ManifestItemEntry matchingManifItem = ExistingManifests.Values.FirstOrDefaultValue( m => Path.Equals( m.InstallLocation, installLocation));
			return new GameEntry(x, matchingManifItem, true);
		}

		m_installedEntries = jsonInstalled.Select(_CreateEntryWithMatchingManifest).ToHashSet();

	}

	public async Task ScanAndAdd(string rootPath)
	{
		var entries = await FileSearch.FindGames(rootPath);
		var views = entries.Select(x => new GameEntry(x)).Except(m_installedEntries);
		views.All(v => m_scannedEntries.Add(v));
	}

	public void SaveToFile(IEnumerable<GameEntry> selectedEntries, string fullPath = @"C:\ProgramData\Epic\UnrealEngineLauncher\ExportTest.dat")
	{
		if (Globals.EnableEGLRunStateCheck && Utils.CheckIfEGLIsRunning())
		{
			if (Globals.IsRunningFromUIApp)
				throw new EpicGamesException(Globals.EglIsRunningMsg) { ErrorCode = EpicGamesErrorCode.LibraryAccessWhileEGLRunning };
		}

		foreach (var entry in selectedEntries.Where(x => x.ManifestItem?.IsFromMANCPN == true))
		{
			entry.ManifestItem.SaveToFile();
		}

		var jsonClone = m_fullJSONdoc.DeepClone() as JObject;
		var jsonInstallationList = jsonClone!.GetValue("InstallationList");
		var entriesAsjObjects = selectedEntries.Select(x => x.Data.ToJObject()).ToArray();
		jsonInstallationList.Replace( JArray.FromObject(entriesAsjObjects));

		var str = jsonClone.ToString();
		File.WriteAllText(fullPath, str);
	//	jsonInstallationList.Replace( new JProperty("InstallationList", JArray.FromObject( entriesAsjObjects)));
	}
}

