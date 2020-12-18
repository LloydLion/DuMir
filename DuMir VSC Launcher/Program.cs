using StandardLibrary.Other;
using System;
using System.Diagnostics;

namespace DuMir_VSC_Debuger
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args[0] == "--termenate")
			{
				Process.GetProcessesByName("DuMir").InvokeForAll(s => s.Kill());
			}
			else
			{

				var duMir = Process.Start(new ProcessStartInfo()
				{
					FileName = args[0],
					WorkingDirectory = args[1],
					Arguments = string.Join(" ", args[2..])
				});

				Console.WriteLine("DuMir Started");

				duMir.WaitForExit();

				Console.WriteLine("DuMir Termenated");
				Console.WriteLine("Launcher Termenated");
			}
		}
	}
}
