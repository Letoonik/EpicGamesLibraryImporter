using EGSIT.Core;
using EGSIT.Core.FileHandling;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EGSIT.UI.Controls;

/// <summary>
/// Interaction logic for EntryList.xaml
/// </summary>
public partial class EntryList : UserControl
{
	public readonly BackgroundWorker ScanBackgroundWorker = new();
	private EntryManager m_entryManager = new();

	public EntryList()
	{
		InitializeComponent();
		ScanBackgroundWorker.DoWork += (sender, e) => m_entryManager.ScanAndAdd(e.Argument?.ToString()!).Wait();
		ScanBackgroundWorker.RunWorkerCompleted += (sender, e) => EntryListView.DataContext = m_entryManager.AllEntries.Select(x => new GameEntryView(x));
	}

	private void InitManager()
	{
		m_entryManager.Initialize();

	}

	private async void UserControl_Loaded(object sender, RoutedEventArgs e)
	{
		await Task.Delay(150);
		InitManager();
		EntryListView.DataContext = m_entryManager.InstalledEntries.Select(x => new GameEntryView(x));
	}

	public async Task ScanAndDisplayGamesAsync(string rootPath)
	{
		await m_entryManager.ScanAndAdd(rootPath);
		EntryListView.DataContext = m_entryManager.AllEntries.Select(x => new GameEntryView(x));
	} 

	public void ScanAndDisplayGames(string rootPath)
	{
		ScanBackgroundWorker.RunWorkerAsync(rootPath);
	}

	public void SaveToFile( string fullPath = @"C:\ProgramData\Epic\UnrealEngineLauncher\ExportTest.dat")
	{
		var selectedEntries = m_entryManager.AllEntries;
		m_entryManager.SaveToFile(selectedEntries, fullPath);
	}
}

