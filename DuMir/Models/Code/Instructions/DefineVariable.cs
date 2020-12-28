using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions
{
	class DefineVariable : CodeInstruction
	{
		public const string CODEDEFINE =	"define $# = #\uF13C" +
											"define $#";


		public override void OnStart(InterpretatorContext ctx)
		{
			var var = new ProgramVariable(InnerCodeAttributes[0], DefineBlock);
			ctx.Variables.Add(var);
		}

		public override void Execute(InterpretatorContext ctx)
		{
			if(SelectedVariant == 0)
			{
				var var = ctx.Variables.Single(s => s.Name == InnerCodeAttributes[0]);
				var.SetValue(new ExpressionHandler(InnerCodeAttributes[1]).Run(ctx));
			}
		}
	}
}
