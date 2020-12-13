using StandardLibrary.Functions;
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


		public virtual IList<CodeExecutable> GetExecutablesForBlockExecute(InterpretatorContext ctx) => Executables;

		public override void Execute(InterpretatorContext ctx)
		{
			GetExecutablesForBlockExecute(ctx).InvokeForAll(s => s.Execute(ctx));
		}

		public override void OnStart(InterpretatorContext ctx)
		{
			ctx.ExecutablesIterators.Add(-1);

			for(int i = 0; i < Executables.Count; i++)
			{
				var exec = Executables[i];
				ctx.ExecutablesIterators[^1] = i;

				exec.OnStart(ctx);
			}

			ctx.ExecutablesIterators.RemoveAt(ctx.ExecutablesIterators.Count - 1);
		}
	}
}
