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


		public override void Execute()
		{
			if(ctx.GetInnerVariable<bool>(0).GetValue())
			{
				base.Execute();
			}
		}
	}
}
