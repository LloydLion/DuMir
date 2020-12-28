using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions.System
{
	class ReadLineFromConsole : CodeInstruction
	{
		public const string CODEDEFINE = "@SYSTEM@|READFROMCONSOLE $#";


		public override void Execute(InterpretatorContext ctx)
		{
			ctx.GetVariable(InnerCodeAttributes[0]).SetValue(ConsoleHandler.Global.ReadLine());
		}
	}
}
