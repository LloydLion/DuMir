using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions
{
	class LogicOperators : CodeInstruction
	{
		public const string CODEDEFINE =	"$# = $# > $#\uF13C" +
											"$# = $# >= $#\uF13C" +
											"$# = $# < $#\uF13C" +
											"$# = $# <= $#\uF13C" +
											"$# = $# == $#\uF13C" +
											"$# = $# != $#";


		public override void Execute(InterpretatorContext ctx)
		{
			var a = ((IConvertible)ctx.Variables.Single(s => InnerCodeAttributes[1] == s.Name).CurrentValue).ToDecimal(new NumberFormatInfo());
			var b = ((IConvertible)ctx.Variables.Single(s => InnerCodeAttributes[2] == s.Name).CurrentValue).ToDecimal(new NumberFormatInfo());
			var target = ctx.Variables.SingleOrDefault(s => s.Name == InnerCodeAttributes[0]);


			bool settable = false;

			switch (SelectedVariant)
			{
				case 0: settable = a > b; break;
				case 1: settable = a >= b; break;
				case 2: settable = a < b; break;
				case 3: settable = a <= b; break;
				case 4: settable = a == b; break;
				case 5: settable = a != b; break;
			}

			target.SetValue(settable);
		}
	}
}
