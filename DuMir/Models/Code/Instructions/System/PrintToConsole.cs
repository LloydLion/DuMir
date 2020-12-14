using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions.System
{
	class PrintToConsole : CodeInstruction
	{
		public const string CODEDEFINE = "@SYSTEM@|PRINTTOCONSOLE #";


		public override void Execute(InterpretatorContext ctx)
		{
			var attr = InnerCodeAttributes[0];

			if(attr.StartsWith("$")) attr = ctx.Variables.Single(s => s.Name == attr[1..]).CurrentValue.ToString();

			Logger.LogMessage(attr, Logger.LogLevel.Applitation);
		}
	}
}
