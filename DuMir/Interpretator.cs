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
		private int rootDefinitionIndex = -1;
		private bool isTransitUppingStade = false;
		private int executeRecursionDepth = 0;
		private InterpretatorContext ctx;


		public void Run(DuAnalyzedProject project)
		{
			Logger.LogMessage("INTERPRETATING...", Logger.LogLevel.Warning);
			ConsoleHandler.Global.WriteLine(new string('-', 30));

			ctx = new InterpretatorContext()
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

			Execute(new NullBlock(main.Executables, null));

			Logger.LogMessage("Program has finished", Logger.LogLevel.Console);
		}

		public void Execute(CodeExecutable executable)
		{
			ctx.UpperBlock = executable.DefineBlock;

			if(executable is CodeBlock block)
			{
				if(isTransitUppingStade == false) ctx.ExecutablesIterators.Add(-1);
				IList<CodeExecutable> execs = null;
				if(isTransitUppingStade == false) execs = block.GetExecutablesForBlockExecute(ctx);
				else execs = block.Executables;

				int i = 0;
				if (isTransitUppingStade == true)
					if (ctx.ExecutablesIterators.Count - 2 >= executeRecursionDepth)
						i = ctx.ExecutablesIterators[executeRecursionDepth];
					else
					{
						i = ctx.ExecutablesIterators[executeRecursionDepth];
						isTransitUppingStade = false;
						ctx.IsExecutablesIteratorsChanged = false;
						rootDefinitionIndex = -1;
					}


				for(; i < execs.Count; i++)
				{
					var exec = execs[i];
					if(isTransitUppingStade == false) ctx.ExecutablesIterators[^1] = i;

					var executablesIteratorsCopy = new List<int>(ctx.ExecutablesIterators);

					executeRecursionDepth++;
					Execute(exec);
					ApplyPragmas();
					executeRecursionDepth--;

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

						if(rootDefinitionIndex == executeRecursionDepth)
						{
							i = ctx.ExecutablesIterators[rootDefinitionIndex] - 1; //i++ | look upper into cycle definition
							isTransitUppingStade = true;

							if(ctx.ExecutablesIterators.Count - 2 < executeRecursionDepth)
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

		private void ApplyPragmas()
		{
			if(ctx.Pragmas.TryGetValue(PragmaKey.DisableConsoleInputAlert, out bool value))
				ConsoleHandler.Global.Flags.SetOrAddValue(ConsoleFlag.DisableConsoleInputAlert, value);
		}
	}
}
