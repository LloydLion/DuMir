using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	class ConsoleHandler
	{
		public static ConsoleHandler Global { get; private set; }

		public IDictionary<ConsoleFlag, bool> Flags { get; } = new Dictionary<ConsoleFlag, bool>();


		private readonly Func<int, string> readMethod;
		private readonly Action<string> writeMethod;


		public ConsoleHandler(Func<int, string> readMethod, Action<string> writeMethod)
		{
			this.readMethod = readMethod;
			this.writeMethod = writeMethod;
		}


		public static void InitGlobal(ConsoleHandler console)
		{
			if(Global != null) throw new MemberAccessException("Global property was inited. You can't overwrite value");
			else Global = console;
		}

		public void WriteLine(string msg) => writeMethod(msg + "\n");

		public string ReadLine()
		{
			if(!(Flags.TryGetValue(ConsoleFlag.DisableConsoleInputAlert, out bool val) && val))
				Logger.LogMessage("Getting user input", Logger.LogLevel.Console);

			StringBuilder result = new StringBuilder();

			while(true)
			{
				var symbol = readMethod(1)[0];
				if(symbol == '\n')
					break;
				else
					result.Append(symbol);
			}

			return result.ToString();
		}
	}
}
