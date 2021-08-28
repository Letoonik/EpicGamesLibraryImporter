using EGSIT.Core;
using EGSIT.Core.Exceptions;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;

namespace EGSIT.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	private void Application_Startup(object sender, StartupEventArgs e)
	{
		Globals.IsRunningFromUIApp = true;
		Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
	}

	private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
	{
		if (e.Exception is EpicGamesException egex)
		{
			e.Handled = true;
			HandleUncaughtEpicException(egex);
			return;
		}
		if (Core.Globals.IsDebugMode)
			e.Handled = true;
		else
			ShowUnhandledException(e);
	}

	void ShowUnhandledException(DispatcherUnhandledExceptionEventArgs e)
	{
		e.Handled = true;

		string errorMessage = string.Format("An application error occurred.\nPlease check whether your data is correct and repeat the action. If this error occurs again there seems to be a more serious malfunction in the application, and you better close it.\n\nError: {0}\n\nDo you want to continue?\n(if you click Yes you will continue with your work, if you click No the application will close)",

		e.Exception.Message + (e.Exception.InnerException != null ? "\n" +
		e.Exception.InnerException.Message : null));

		if (MessageBox.Show(errorMessage, "Application Error", MessageBoxButton.YesNoCancel, MessageBoxImage.Error) == MessageBoxResult.No)
		{
			if (MessageBox.Show("WARNING: The application will close. Any changes will not be saved!\nDo you really want to close it?", "Close the application!", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) == MessageBoxResult.Yes)
			{
				Application.Current.Shutdown();
			}
		}
	}

	public static void HandleUncaughtEpicException(EpicGamesException ex)
	{
		if (ex.ErrorCode == EpicGamesErrorCode.LibraryAccessWhileEGLRunning)
		{			
			MessageBox.Show(Current.MainWindow,Globals.EglIsRunningKillNowMsg);
			Utils.KillEGLProcesses();
			Utils.WaitUntilEGLClosedAsync().Wait(1000);
		}
	}
}
