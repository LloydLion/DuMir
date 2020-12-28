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
		public const string CODEDEFINE = "if #";


		public IfBlock(IEnumerable<CodeExecutable> executables) : base(executables)
		{
			
		}


		public override IList<CodeExecutable> GetExecutablesForBlockExecute(InterpretatorContext ctx)
		{
			var value = (bool)new ExpressionHandler(InnerCodeAttributes[0]).Run(ctx);

			if (value == true) return base.GetExecutablesForBlockExecute(ctx);
			else return new List<CodeExecutable>();
		}
	}
}
