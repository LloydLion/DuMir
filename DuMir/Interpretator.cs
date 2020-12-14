using DuMir.Models.Code;
using DuMir.Models.Code.Blocks;
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
		int rootDefinitionIndex = -1;


		public void Run(DuAnalyzedProject project)
		{
			Logger.LogMessage("INTERPRETATING...", Logger.LogLevel.Warning);
			ConsoleHandler.Global.WriteLine(new string('-', 30));

			InterpretatorContext ctx = new InterpretatorContext()
			{
				Project = project
			};

			project.AnalysedCode.InvokeForAll(s =>
			{
				ctx.ExecutablesIterators.Add(-1);

				for (int i = 0; i < s.Executables.Count; i++)
				{
					ctx.ExecutablesIterators[0] = i;
					s.Executables[i].OnStart(ctx);
				}

				ctx.ExecutablesIterators.RemoveAt(0);
			});

			var main = project.AnalysedCode[0];

			ctx.ExecutablesIterators.Clear();

			Execute(new NullBlock(main.Executables, null), ctx);

			Logger.LogMessage("PROGRAM END", Logger.LogLevel.Warning);
		}

		public void Execute(CodeExecutable executable, InterpretatorContext ctx, int recursionDepth = 0, bool isTransitUppingStade = false)
		{
			if(executable is CodeBlock block)
			{
				if(isTransitUppingStade == false) ctx.ExecutablesIterators.Add(-1);
				IList<CodeExecutable> execs = null;
				if(isTransitUppingStade == false) execs = block.GetExecutablesForBlockExecute(ctx);
				else execs = block.Executables;

				int i = 0;
				if (isTransitUppingStade == true)
					if (ctx.ExecutablesIterators.Count - 2 >= recursionDepth)
						i = ctx.ExecutablesIterators[recursionDepth];
					else
					{
						i = ctx.ExecutablesIterators[recursionDepth];
						isTransitUppingStade = false;
						ctx.IsExecutablesIteratorsChanged = false;
						rootDefinitionIndex = -1;
					}


				for(; i < execs.Count; i++)
				{
					var exec = execs[i];
					if(isTransitUppingStade == false) ctx.ExecutablesIterators[^1] = i;

					var executablesIteratorsCopy = new List<int>(ctx.ExecutablesIterators);
					Execute(exec, ctx, recursionDepth + 1, isTransitUppingStade);
					isTransitUppingStade = false;

					if(ctx.IsExecutablesIteratorsChanged == true)
					{
						if(rootDefinitionIndex == -1)
						for(int j = 0; j < executablesIteratorsCopy.Count && j < ctx.ExecutablesIterators.Count; j++)
						{
							if(executablesIteratorsCopy[j] == ctx.ExecutablesIterators[j]) continue;
							else
							{
								rootDefinitionIndex = j;
								break;
							}
						}

						if(rootDefinitionIndex == recursionDepth)
						{
							i = ctx.ExecutablesIterators[rootDefinitionIndex] - 1; //i++ | look upper into cycle definition
							isTransitUppingStade = true;

							if(ctx.ExecutablesIterators.Count - 2 < recursionDepth)
							{
								isTransitUppingStade = false;
								ctx.IsExecutablesIteratorsChanged = false;
								rootDefinitionIndex = -1;
							}
						}
						else return;
					}
				}

				ctx.ExecutablesIterators.RemoveAt(ctx.ExecutablesIterators.Count - 1);
			}
			else executable.Execute(ctx);
		}
	}
}
