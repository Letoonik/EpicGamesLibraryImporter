using EGSIT.Core;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using EGSIT.Core.FileHandling;

namespace EGSIT.UI;


public class GameEntryView
{
	private GameEntry m_initialEntry;
	 
	/// <summary>Full installation location path.</summary>
	public string FullPath => m_initialEntry.InstallLocation ?? "[path not found]";

	/// <summary>Game folder name.</summary>
	public string FolderName => Directory.GetParent(m_initialEntry.InstallLocation+"/")?.Name ?? "[path not found]";

	/// <summary>When locked, the entry should be read-only in the UI.</summary>
	public bool IsLocked => m_initialEntry.IsFromLoadedLibrary;

	/// <summary>When locked, the entry should be read-only in the UI.</summary>
	public ImageSource IconSource => m_IconSource ??= AssignExecutableAndIcon().icon;
	private ImageSource? m_IconSource;

	/// <summary>
	/// Filename of the executable that is most likely to be the game or its launch.<br/>
	/// If several executables are found in the game folder, the chosen one will only be an educated guess.
	/// </summary>
	public string ExecutableName => m_ExecutableName ??= AssignExecutableAndIcon().executable;
	private string? m_ExecutableName;

	public string LibraryText => GetLibraryColumnText();

	public GameEntryView() => m_initialEntry = GameEntry.CreateDummy();
	public GameEntryView(GameEntry entry) => m_initialEntry = entry;

	public (ImageSource icon, string executable) AssignExecutableAndIcon()
	{
		var execInfo = FileSearch.FindGameExecutable(FullPath);
		var icon = Icon.ExtractAssociatedIcon(execInfo.FullName)!;

		m_IconSource = Imaging.CreateBitmapSourceFromHIcon(
							icon.Handle, new(0, 0, icon.Width, icon.Height),
							BitmapSizeOptions.FromEmptyOptions());

		return (m_IconSource, execInfo.Name);
	}

	private string GetLibraryColumnText()
	{
		if (m_initialEntry.IsFromLoadedLibrary) return "Already in";
		return "Add to library";
	}
}

