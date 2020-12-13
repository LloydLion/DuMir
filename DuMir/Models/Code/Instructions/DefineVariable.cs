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
		public const string CODEDEFINE = "define $# = |#|#| #";


		public override void OnStart(InterpretatorContext ctx)
		{
			var var = new ProgramVariable(InnerCodeAttributes[0], DefineBlock);
			ctx.Variables.Add(var);

			if(InnerCodeAttributes[1] == "Pure" && InnerCodeAttributes[2] == "Value") var.SetValue(InnerCodeAttributes[3]);
			else var.SetValue(Type.GetType(InnerCodeAttributes[1]).GetMethod(InnerCodeAttributes[2], 0, BindingFlags.Public | BindingFlags.Static, null, default, new Type[] { typeof(string) }, default)
				.Invoke(null, new object[] { InnerCodeAttributes[3] }));
		}

		public override void Execute(InterpretatorContext ctx) { }
	}
}
