using StandardLibrary.Functions;
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
	}
}
