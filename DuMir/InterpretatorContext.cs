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
	}
}
