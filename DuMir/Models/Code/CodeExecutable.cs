using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code
{
	abstract class CodeExecutable : Model
	{
		public CodeExecutable() { }


		public override string ToString()
		{
			return $"EXECUTABLE: " + (this as object).ToString();
		}

		public abstract void Execute();

		public virtual void OnStart() { }
	}
}
