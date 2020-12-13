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
			typeof(SumInstruction),
			typeof(DefineVariable)
		};

		
		public DuAnalyzedProject Run(DuProject project)
		{
			var analyzedProject = new DuAnalyzedProject(project);

			foreach (var item in project.CodeFiles)
			{
				NonLecsicAnalys(item);
				var analyzed = BlockSplitingAndTypping(item);
				analyzed = AliasReplace(analyzed);
				analyzedProject.AnalysedCode.Add(analyzed); 
			}

			analyzedProject = FinalizeAnalyse(analyzedProject);

			return analyzedProject;
		}

		private static DuAnalyzedProject FinalizeAnalyse(DuAnalyzedProject analyzedProject)
		{
			return analyzedProject;
		}

		private static DuAnalysedCode AliasReplace(DuAnalysedCode analyzed)
		{
			return analyzed;
		}

		private static DuAnalysedCode BlockSplitingAndTypping(DuCode code)
		{
			var instConstructors = definedInstructions.Select(s => s.GetConstructor(Array.Empty<Type>())).ToList();
			var instNames = definedInstructions.Select(s => s.GetField("CODEDEFINE", BindingFlags.Static | BindingFlags.Public).GetValue(null) as string).ToList();

			var blocksConstructors = definedBlocks.Select(s => s.GetConstructor(new Type[] { typeof(IEnumerable<CodeExecutable>) })).ToList();
			var blocksNames = definedBlocks.Select(s => s.GetField("CODEDEFINE", BindingFlags.Static | BindingFlags.Public).GetValue(null) as string).ToList();

			var result = new NullBlock(Array.Empty<CodeExecutable>(), null);

			var blockStack = new Stack<CodeBlock>(4);
			blockStack.Push(result);

			var lines = code.Text.Split("\r\n");

			for(int i = 0; i < lines.Length; i++)
			{
				var line = lines[i];

				if(i != lines.Length - 1 && lines[i + 1] == StartBlockString) continue;

				if(line == StartBlockString)
				{
					string[] innerCodeAttrs = null;
					var block = blocksConstructors[blocksNames.FindIndex(s => MatchCodeObjectNameWithPattern(lines[i - 1], s, out innerCodeAttrs))].Invoke(new object[] { Array.Empty<CodeExecutable>() }) as CodeBlock;
					block.InnerCodeAttributes = innerCodeAttrs;
					blockStack.Push(block);
				}
				else if(line == EndBlockString)
				{
					blockStack.Pop();
				}
				else
				{
					string[] innerCodeAttrs = null;
					var inst = instConstructors[instNames.FindIndex(s => MatchCodeObjectNameWithPattern(line, s, out innerCodeAttrs))].Invoke(Array.Empty<object>()) as CodeInstruction;
					inst.DefineBlock = blockStack.Peek();
					inst.InnerCodeAttributes = innerCodeAttrs;
					blockStack.Peek().Executables.Add(inst);
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
				//code.Text = string.Join("\n", code.Text.Split('\n').Select(s => s.Trim()));

				//Lines shortify
				code.Text = code.Text.Replace("\r\n\r\n", "\r\n");

				//Tabulation remove
				code.Text = code.Text.Replace("\t", "");

				//Spaces shortify
				code.Text = code.Text.Replace("  ", " ");
			}
		}

		private static bool MatchCodeObjectNameWithPattern(string name, string pattern, out string[] attrs)
		{
			var parts = pattern.Split("#");
			StringBuilder builder = new StringBuilder(name.Length);
			attrs = new string[pattern.Count(s => s == '#')];

			string currentPart = parts[0];
			parts = parts[1..];
			int i = 1;
				
			while(true)
			{
				if(name.Length == 0) return false;

				builder.Append(name[0]);
				name = name[1..];

				if(builder.ToString().Contains(currentPart))
				{
					if(i >= 2) attrs[i - 2] = builder.ToString().Substring(0, builder.ToString().Length - currentPart.Length);
					currentPart = parts[i - 1];
					builder.Clear();

					i++;

					if(parts.Length == i - 1)
					{
						attrs[^1] = name;
						return true;
					}
				}

			}
		}
	}
}
