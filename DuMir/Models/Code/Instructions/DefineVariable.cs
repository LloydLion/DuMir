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
		public const string CODEDEFINE =	"define $# = |#|#| #\uF13C" +
											"define $#";


		public override void OnStart(InterpretatorContext ctx)
		{
			var var = new ProgramVariable(InnerCodeAttributes[0], DefineBlock);
			ctx.Variables.Add(var);
		}

		public override void Execute(InterpretatorContext ctx)
		{
			if (SelectedVariant == 0)
			{

				var var = ctx.Variables.Single(s => s.Name == InnerCodeAttributes[0]);

				if (InnerCodeAttributes[1] == "Pure" && InnerCodeAttributes[2] == "Value") var.SetValue(InnerCodeAttributes[3]);
				else var.SetValue(Type.GetType(InnerCodeAttributes[1]).GetMethod(InnerCodeAttributes[2], genericParameterCount: 0,
					BindingFlags.Public | BindingFlags.Static, null, default, new Type[] { typeof(string) }, default)
					.Invoke(null, new object[] { InnerCodeAttributes[3] }));
			}
		}
	}
}
