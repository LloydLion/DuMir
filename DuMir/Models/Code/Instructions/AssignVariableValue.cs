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
		public const string CODEDEFINE =	"$# = |#|#| #\uF13C" +
											"$# = |#| #";


		public override void OnStart(InterpretatorContext ctx)
		{
			if(SelectedVariant == 1)
			{
				var copy = InnerCodeAttributes;
				InnerCodeAttributes = new string[4];
				InnerCodeAttributes[0] = copy[0];
				InnerCodeAttributes[3] = copy[2];

				var pClass = "";
				var pMethod = "Parse";

				switch(copy[1])
				{
					case "number": pClass = "System.Decimal"; break;
					case "string": pClass = "Pure"; pMethod = "Value"; break;
					case "bool": pClass = "System.Boolean"; break;
				}

				InnerCodeAttributes[1] = pClass;
				InnerCodeAttributes[2] = pMethod;
			}
		}

		public override void Execute(InterpretatorContext ctx)
		{
			var var = ctx.Variables.Single(s => s.Name == InnerCodeAttributes[0]);

			if (InnerCodeAttributes[1] == "Pure" && InnerCodeAttributes[2] == "Value") var.SetValue(InnerCodeAttributes[3]);
			else var.SetValue(Type.GetType(InnerCodeAttributes[1]).GetMethod(InnerCodeAttributes[2], 0, BindingFlags.Public | BindingFlags.Static, null, default, new Type[] { typeof(string) }, default)
				.Invoke(null, new object[] { InnerCodeAttributes[3] }));
		}
	}
}
