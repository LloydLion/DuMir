using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code
{
	class ProgramBookmark
	{
		public int[] InterpretatorPointer { get; }

		public string Name { get; }


		public ProgramBookmark(string name, int[] pointer)
		{
			InterpretatorPointer = pointer;
			Name = name;
		}
	}
}
