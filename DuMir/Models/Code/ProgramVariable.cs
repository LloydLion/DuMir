using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code
{
	class ProgramVariable : Model
	{
		private object currentValue;


		public string Name { get; }

		public object CurrentValue { get => currentValue; private set { OnPropertyChanging(); currentValue = value; OnPropertyChanged(); } }

		public CodeBlock Context { get; }


		public ProgramVariable(string name, CodeBlock context)
		{
			Name = name;
			Context = context;
		}


		public override string ToString()
		{
			return $"Variable: {Name} [{CurrentValue}]";
		}

		public void SetValue(object value)
		{
			CurrentValue = value;
		}
	}
}
