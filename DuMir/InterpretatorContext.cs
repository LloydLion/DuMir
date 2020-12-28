using DuMir.Models.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	class InterpretatorContext
	{
		public IList<ProgramVariable> Variables { get; } = new List<ProgramVariable>();

		public DuAnalyzedProject Project { get; set; }

		public IList<int> ExecutablesIterators { get; } = new List<int>();

		public bool IsExecutablesIteratorsChanged { get; set; }

		public IList<ProgramBookmark> Bookmarks { get; } = new List<ProgramBookmark>();

		public IDictionary<PragmaKey, bool> Pragmas { get; } = new Dictionary<PragmaKey, bool>();


		public ProgramVariable GetVariable(string name) => Variables.Single(s => s.Name == name);

		public ProgramBookmark GetBookmark(string name) => Bookmarks.Single(s => s.Name == name);

		public void ChangeIterators(IList<int> iterators)
		{
			ExecutablesIterators.Clear();
			ExecutablesIterators.AddRange(iterators);

			IsExecutablesIteratorsChanged = true;
		}
	}
}
