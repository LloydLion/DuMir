using DuMir.Models;
using DuMir.Models.Code;
using DuMir.Models.Code.Blocks;
using DuMir.Models.Code.Instructions;
using DuMir.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DuMir
{
	class CodeAnalyser
	{
		public const string StartBlockString = "{";
		public const string EndBlockString = "}";

		public readonly static Type[] definedBlocks = new Type[]
		{
			typeof(IfBlock)
		};

		public readonly static Type[] definedInstructions = new Type[]
		{
			typeof(SumInstruction)
		};

		
		public DuAnalyzedProject Run(DuProject project)
		{
			var analyzedCode = new List<DuAnalysedCode>();

			foreach (var item in project.CodeFiles)
			{
				NonLecsicAnalys(item);
				var analyzed = BlockSplitting(item);
				analyzed = TypiseBlocks(analyzed);
				analyzed = AliasReplace(analyzed);
				analyzedCode.Add(analyzed);
			}

			var analyzedProject = new DuAnalyzedProject(project);
			analyzedProject.AnalysedCode.AddRange(analyzedCode);

			analyzedProject = FinalizeAnalyse(analyzedProject);

			return analyzedProject;
		}

		private static DuAnalyzedProject FinalizeAnalyse(DuAnalyzedProject analyzedProject)
		{
			return analyzedProject;
		}

		private static DuAnalysedCode TypiseBlocks(DuAnalysedCode analyzed)
		{
			var constructors = definedBlocks.Select(s => s.GetConstructor(new Type[] { typeof(IEnumerable<CodeExecutable>) })).ToList();
			var names = definedBlocks.Select(s => s.GetField("CODEDEFINE", BindingFlags.Static | BindingFlags.Public).GetValue(null) as string).ToList();
			var blocks = analyzed.Executables.Select((s, i) => (s as NullBlock, i)).Where(s => s.Item1 != null).ToList();

			for(int i = 0; i < blocks.Count; i++)
			{
				var block = blocks[i].Item1;

				var index = names.FindIndex(s => MatchCodeObjectNameWithPattern(s, block.StringForm));
				analyzed.Executables[blocks[i].i] = constructors[index].Invoke(new object[] { block.Executables as IEnumerable<CodeExecutable> } ) as CodeBlock;
			}

			return analyzed;
		}

		private static DuAnalysedCode TypiseInstructions(DuAnalysedCode analyzed)
		{
			var constructors = definedInstructions.Select(s => s.GetConstructor(Array.Empty<Type>())).ToList();
			var names = definedInstructions.Select(s => s.GetField("CODEDEFINE", BindingFlags.Static | BindingFlags.Public).GetValue(null) as string).ToList();
			var instructions = analyzed.Executables.Select((s, i) => (s as NullInstruction, i)).Where(s => s.Item1 != null).ToList();

			for (int i = 0; i < instructions.Count; i++)
			{
				var instruction = instructions[i].Item1;

				var index = names.FindIndex(s => MatchCodeObjectNameWithPattern(s, instruction.StringForm));
				analyzed.Executables[instructions[i].i] = constructors[index].Invoke(Array.Empty<object>()) as CodeBlock;
			}

			return analyzed;
		}

		private static DuAnalysedCode AliasReplace(DuAnalysedCode analyzed)
		{
			return analyzed;
		}

		private static DuAnalysedCode BlockSplitting(DuCode code)
		{
			var result = new NullBlock(Array.Empty<CodeExecutable>(), null);

			var blockStack = new Stack<CodeBlock>(4);
			blockStack.Push(result);

			var lines = code.Text.Split("\r\n");

			for (int i = 0; i < lines.Length; i++)
			{
				var line = lines[i];

				if(i != lines.Length - 1 && lines[i + 1] == StartBlockString) continue;

				if(line == StartBlockString)
				{
					blockStack.Push(new NullBlock(Array.Empty<CodeExecutable>(), lines[i - 1]));
				}
				else if(line == EndBlockString)
				{
					blockStack.Pop();
				}
				else
				{
					blockStack.Peek().Executables.Add(new NullInstruction(line));
				}
			}

			return new DuAnalysedCode(code) { Executables = result.Executables };
		}

		private static void NonLecsicAnalys(DuCode code)
		{
			//Blocks work
			code.Text = code.Text.Replace(StartBlockString, "\r\n" + StartBlockString + "\r\n");
			code.Text = code.Text.Replace(EndBlockString, "\r\n" + EndBlockString + "\r\n");

			while(code.Text.Contains("\r\n\r\n") || code.Text.Contains("\t") || code.Text.Contains("  "))
			{
				//White spaces
				code.Text = string.Join("\n", code.Text.Split('\n').Select(s => s.Trim()));

				//Lines shortify
				code.Text = code.Text.Replace("\r\n\r\n", "\r\n");

				//Tabulation remove
				code.Text = code.Text.Replace("\t", "");

				//Spaces shortify
				code.Text = code.Text.Replace("  ", " ");
			}
		}

		private static bool MatchCodeObjectNameWithPattern(string name, string pattern)
		{
			var parts = pattern.Split("#");
			StringBuilder builder = new StringBuilder(name.Length);

			string currentPart = parts[0];
			parts = parts[1..];

			while(true)
			{
				if(name.Length == 0) return false;

				builder.Append(name[0]);
				name = name[1..];

				if(builder.ToString().Contains(currentPart))
				{
					currentPart = parts[0];
					parts = parts[1..];
					builder.Clear();

					if(parts.Length == 0) return true;
				}
			}
		}
	}
}
