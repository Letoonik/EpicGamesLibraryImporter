using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGSIT.Core;

public static class Globals
{
#if DEBUG
	public static bool IsDebugMode => true;

#else
		public static bool IsDebugMode => false;
#endif

	/// <summary>
	/// Allows branching specific to WPF/Winforms design mode in library code. This variable must be initialized from the UI app code.
	/// </summary>
	public static bool IsRunningFromUIApp;

	public static bool EnableEGLRunStateCheck = true;

	public const string EglIsRunningMsg = "EpicGamesLauncher is running. It needs to be closed to prevent access conflicts.";
	public const string EglIsRunningKillNowMsg = "EpicGamesLauncher is running. EGSIT has to kill the process before you can retry.\rAccept when you're ready to continue.";


	public static string InstalledJSONPath => Path.Combine(Environment.GetFolderPath(
		Environment.SpecialFolder.CommonApplicationData), @"Epic\UnrealEngineLauncher", "LauncherInstalled.dat");

	public static string ManifestFolderPath => Path.Combine(Environment.GetFolderPath(
		Environment.SpecialFolder.CommonApplicationData), @"Epic\EpicGamesLauncher\Data\Manifests");
}

