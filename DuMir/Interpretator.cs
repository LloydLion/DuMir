using DuMir.Models.Code;
using StandardLibrary.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	class Interpretator
	{
		public void Run(DuAnalyzedProject project)
		{
			InterpretatorContext ctx = new InterpretatorContext();

			project.AnalysedCode.InvokeForAll(s => s.Executables.InvokeForAll(s => s.OnStart(ctx)));


			var main = project.AnalysedCode[0];


			for(int i = 0; i < main.Executables.Count; i++)
			{
				var exec = main.Executables[i];

				exec.Execute(ctx);
			}
		}
	}
}
