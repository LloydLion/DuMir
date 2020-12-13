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
			var bookmark = ctx.Bookmarks.Single(s => s.Name == InnerCodeAttributes[0]);

			ctx.IsExecutablesIteratorsChanged = true;
			ctx.ExecutablesIterators.Clear();
			ctx.ExecutablesIterators.AddRange(bookmark.InterpretatorPointer);
		}
	}
}
