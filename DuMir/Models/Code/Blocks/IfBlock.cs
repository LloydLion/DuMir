using DuMir.Models.Code.Instructions;
using StandardLibrary.Functions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Blocks
{
	class IfBlock : CodeBlock
	{
		public const string CODEDEFINE =	"if $#\uF13C" +
											"if | $# # $#";


		public IfBlock(IEnumerable<CodeExecutable> executables) : base(executables)
		{
			
		}


		public override IList<CodeExecutable> GetExecutablesForBlockExecute(InterpretatorContext ctx)
		{
			var value = false;

			if(SelectedVariant == 0)
				value = (bool)ctx.Variables.Single(s => s.Name == InnerCodeAttributes[0]).CurrentValue;
			else
			{
				var a = ((IConvertible)ctx.Variables.Single(s => InnerCodeAttributes[0] == s.Name).CurrentValue).ToDecimal(new NumberFormatInfo());
				var b = ((IConvertible)ctx.Variables.Single(s => InnerCodeAttributes[2] == s.Name).CurrentValue).ToDecimal(new NumberFormatInfo());

				switch (InnerCodeAttributes[1])
				{
					case ">": value = a > b; break;
					case ">=": value = a >= b; break;
					case "<": value = a < b; break;
					case "<=": value = a <= b; break;
					case "==": value = a == b; break;
					case "!=": value = a != b; break;
				}
			}

			if (value == true)
			{
				return base.GetExecutablesForBlockExecute(ctx);
			}
			else return new List<CodeExecutable>();
		}
	}
}
