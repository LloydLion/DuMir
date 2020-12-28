using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions.System
{
	class Delay : CodeInstruction
	{
		public const string CODEDEFINE = "@SYSTEM@|DELAY #";


		public override void Execute(InterpretatorContext ctx)
		{
			var attr = 0;

			if(InnerCodeAttributes[0].StartsWith("$")) attr = (int)ctx.GetVariable(InnerCodeAttributes[0][1..]).CurrentValue;
			else attr = int.Parse(InnerCodeAttributes[0]);

			Thread.Sleep(Math.Abs(attr));
		}
	}
}
