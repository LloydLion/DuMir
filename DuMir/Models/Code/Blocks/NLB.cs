using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Blocks
{
	class NLB : CodeBlock
	{
		public const string CODEDEFINE = "nlb";


		public NLB(IEnumerable<CodeExecutable> executables) : base(executables)
		{

		}
	}
}
