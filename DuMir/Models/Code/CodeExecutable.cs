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
		public string[] InnerCodeAttributes { get; set; }


		public CodeExecutable() { }


		public override string ToString()
		{
			return $"EXECUTABLE: " + GetType().ToString();
		}

		public abstract void Execute(InterpretatorContext ctx);

		public virtual void OnStart(InterpretatorContext ctx) { }
	}
}
