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
 	}
}
