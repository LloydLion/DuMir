using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	static class Logger
	{
		public static void LogMessage(string text, LogLevel level)
		{
			if((bool)Static.LaunchArguments["EnableDebug"] == false)
				if (level != LogLevel.Applitation && level != LogLevel.UserInput && level != LogLevel.Console) return;

			ConsoleHandler.Global.WriteLine($"[{new TimeSpan(DateTime.Now.Ticks):hh\\:mm\\:ss}|{level.ToString().ToUpper()}]: " + text);
		}


		public enum LogLevel
		{
			Info,
			Warning,
			Error,
			UserInput,
			Applitation,
			Console
		}
	}
}
