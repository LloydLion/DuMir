using StandardLibrary.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code
{
	abstract class CodeBlock : CodeExecutable
	{
		public List<CodeExecutable> Executables { get; } = new List<CodeExecutable>();


		public CodeBlock(IEnumerable<CodeExecutable> executables) : base()
		{
			Executables.AddRange(executables);
		}


		public override void Execute()
		{
			Executables.InvokeForAll(s => s.Execute());
		}
	}
}
