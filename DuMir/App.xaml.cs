using StandardLibrary.Console;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DuMir
{
	partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			Static.LaunchArguments = new ConsoleArgumentEngine
			(
				new ConsoleArgumentEngine.FlagParameter() { Key = "auto-run", Name = "IsAutoRun" },
				new ConsoleArgumentEngine.FlagParameter() { Key = "auto-closing", Name = "IsAutoClosing" },
				new ConsoleArgumentEngine.FlagParameter() { Key = "debug", Name = "EnableDebug" }

			).Calculate(e.Args);
		}
	}
}
