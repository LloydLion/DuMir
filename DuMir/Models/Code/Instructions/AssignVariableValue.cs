using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions
{
	class AssignVariableValue : CodeInstruction
	{
		public const string CODEDEFINE = "$# = #";


		public override void Execute(InterpretatorContext ctx)
		{
			var var = ctx.Variables.Single(s => s.Name == InnerCodeAttributes[0]);
			var.SetValue(new ExpressionHandler(InnerCodeAttributes[1]).Run(ctx));
		}
	}
}
