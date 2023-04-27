using EGSIT.Core.FileHandling;
using EGSIT.Core;

using System;
using System.Collections.Generic;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Threading;
using System.ComponentModel;
using System.IO;

namespace EGSIT.UI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	private bool m_firstLoadCalled;

	private BackgroundWorker m_bgWorker = new();

	public MainWindow()
	{
		InitializeComponent();
	}


	private async void Window_Loaded(object sender, RoutedEventArgs e)
	{
		if (!m_firstLoadCalled)
		{
			m_firstLoadCalled = true;
			CreateFirstRunBackup();
		}
	}

	private static void CreateFirstRunBackup()
	{
		var backupName = Globals.InstalledJSONPath.Replace(".dat", ".1stTimeBackup.dat");
		if (File.Exists(Globals.InstalledJSONPath) && !File.Exists(backupName))
		{
			File.Copy(Globals.InstalledJSONPath, backupName);
		}
	}

	private async Task ScanAndAdd()
	{
		void _OnCompleted(object? sender, RunWorkerCompletedEventArgs e)
		{
			TopToolBar.IsEnabled = true;
			ScanProgressBarItem.Visibility = Visibility.Collapsed;
			EntryListControl.ScanBackgroundWorker.RunWorkerCompleted -= _OnCompleted;
		}

		var path = BrowsePathTextBlock.Text;
		TopToolBar.IsEnabled = false;
		ScanProgressBarItem.Visibility = Visibility.Visible;
		await Task.Delay(100);
		EntryListControl.ScanBackgroundWorker.RunWorkerCompleted += _OnCompleted;
		EntryListControl.ScanAndDisplayGames(path);
	}

	private void ScanButton_Click(object sender, RoutedEventArgs e)
	{
		ScanAndAdd();
	}

	private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
	{
		var scanFolderDialog = new CommonOpenFileDialog()
		{
			ShowHiddenItems = true,
			AddToMostRecentlyUsedList = true,
			EnsurePathExists = true,
			IsFolderPicker = true,
			Multiselect=false
		};

		if (System.IO.Directory.Exists(BrowsePathTextBlock.Text))	{
			scanFolderDialog.InitialDirectory = BrowsePathTextBlock.Text;
		}

		if (scanFolderDialog.ShowDialog() == CommonFileDialogResult.Ok) {
			BrowsePathTextBlock.Text = scanFolderDialog.FileName;
		}
	}

	private void ImportButton_Click(object sender, RoutedEventArgs e)
	{
		EntryListControl.SaveToFile(Globals.InstalledJSONPath);
	}
}
