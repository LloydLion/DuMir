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
		public const string CODEDEFINE = "@SYSTEM@|READFROMCONSOLE |#|#| $#";


		public override void Execute(InterpretatorContext ctx)
		{
			var var = ctx.Variables.Single(s => s.Name == InnerCodeAttributes[2]);

			if (InnerCodeAttributes[0] == "Pure" && InnerCodeAttributes[1] == "Value") var.SetValue(ConsoleHandler.Global.ReadLine());
			else var.SetValue(Type.GetType(InnerCodeAttributes[0]).GetMethod(InnerCodeAttributes[1], genericParameterCount: 0,
				BindingFlags.Public | BindingFlags.Static, null, default, new Type[] { typeof(string) }, default)
				.Invoke(null, new object[] { ConsoleHandler.Global.ReadLine() }));
		}
	}
}
