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
		public const string CODEDEFINE = "$# = |#|#| #";


		public override void Execute(InterpretatorContext ctx)
		{
			var var = ctx.Variables.Single(s => s.Name == InnerCodeAttributes[0]);

			if (InnerCodeAttributes[1] == "Pure" && InnerCodeAttributes[2] == "Value") var.SetValue(InnerCodeAttributes[3]);
			else var.SetValue(Type.GetType(InnerCodeAttributes[1]).GetMethod(InnerCodeAttributes[2], 0, BindingFlags.Public | BindingFlags.Static, null, default, new Type[] { typeof(string) }, default)
				.Invoke(null, new object[] { InnerCodeAttributes[3] }));
		}
	}
}
