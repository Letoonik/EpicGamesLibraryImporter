using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EGSIT.Core;

namespace EGSIT.UI
{
	public static class AppStart
	{
		public static void Main(string[] args)
		{
			var testFile = @"D:\Games\_EpicStoreGames\VoidBastards\.egstore\629CB14A4E70F7D2C2C8C8A777AA509D.mancpn";
			//MANCPNEntry.FromFile(testFile);
			var manager = new EntryManager();
			manager.Initialize();
		}
	}
}
