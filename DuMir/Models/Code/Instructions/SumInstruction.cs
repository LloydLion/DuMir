using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions
{
	class SumInstruction : CodeInstruction
	{
		public const string CODEDEFINE = "$# = $# + $#";


		public override void OnStart(InterpretatorContext ctx)
		{
			if(ctx.Variables.SingleOrDefault(s => s.Name == InnerCodeAttributes[0]) == null)
				ctx.Variables.Add(new ProgramVariable(InnerCodeAttributes[0], DefineBlock));
		}

		public override void Execute(InterpretatorContext ctx)
		{
			var a = ((IConvertible)ctx.Variables.Single(s => InnerCodeAttributes[1] == s.Name).CurrentValue).ToDecimal(new NumberFormatInfo());
			var b = ((IConvertible)ctx.Variables.Single(s => InnerCodeAttributes[2] == s.Name).CurrentValue).ToDecimal(new NumberFormatInfo());
			var target = ctx.Variables.SingleOrDefault(s => s.Name == InnerCodeAttributes[0]);

			target.SetValue(a + b);
		}
	}
}
