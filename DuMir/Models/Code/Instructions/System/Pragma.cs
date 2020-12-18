using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions.System
{
	class Pragma : CodeInstruction
	{
		public const string CODEDEFINE =	"@SYSTEM@|PRAGMA DISABLE #\uF13C" +
											"@SYSTEM@|PRAGMA #";


		public override void Execute(InterpretatorContext ctx)
		{
			var value = SelectedVariant == 1;
			ctx.Pragmas.SetOrAddValue((PragmaKey)Enum.Parse(typeof(PragmaKey), InnerCodeAttributes[0]), value);
		}
	}
}
