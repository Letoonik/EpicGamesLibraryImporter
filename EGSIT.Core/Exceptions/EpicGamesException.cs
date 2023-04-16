using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EGSIT.Core.Exceptions
{
	public enum EpicGamesErrorCode
	{
		Undefined,
		LibraryFileNotFound,
		LibraryAccessWhileEGLRunning
	}

	public class EpicGamesException : Exception
	{
		public EpicGamesException() : base()
		{
		}

		public EpicGamesException(string? message) : base(message)
		{
		}

		public EpicGamesException(string? message, Exception? innerException) : base(message, innerException)
		{
		}

		public EpicGamesErrorCode ErrorCode {  get; init;}
	}
}
