using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code.Instructions
{
	class NullInstruction : CodeInstruction
	{
		public string StringForm { get; private set; }


		public NullInstruction(string instruction) : base() { StringForm = instruction; }


		public override void Execute() { }
	}
}
