using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Blocks
{
	class NullBlock : CodeBlock
	{
		public string StringForm { get; private set; }


		public NullBlock(IEnumerable<CodeExecutable> executables, string blockName) : base(executables) { StringForm = blockName; }


		public override void Execute()
		{
			base.Execute();
		}
	}
}
