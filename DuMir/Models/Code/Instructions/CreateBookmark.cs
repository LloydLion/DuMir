using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions
{
	class CreateBookmark : CodeInstruction
	{
		public const string CODEDEFINE = "bookmark #";


		public override void Execute(InterpretatorContext ctx) { }

		public override void OnStart(InterpretatorContext ctx)
		{
			ctx.Bookmarks.Add(new ProgramBookmark(InnerCodeAttributes[0], ctx.ExecutablesIterators.ToArray(), DefineBlock));
		}
	}
}
