using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions
{
	class GotoBookmark : CodeInstruction
	{
		public const string CODEDEFINE = "goto #";


		public override void Execute(InterpretatorContext ctx)
		{
			ctx.ChangeIterators(ctx.GetBookmark(InnerCodeAttributes[0]).InterpretatorPointer);
		}
	}
}
