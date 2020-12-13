using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Blocks
{
	class IfBlock : CodeBlock
	{
		public const string CODEDEFINE = "if(#)";


		public IfBlock(IEnumerable<CodeExecutable> executables) : base(executables)
		{
			
		}


		public override void Execute(InterpretatorContext ctx)
		{
			if((bool)ctx.Variables.Single(s => s.Name == InnerCodeAttributes[0]).CurrentValue == true)
			{
				base.Execute(ctx);
			}
		}
	}
}
