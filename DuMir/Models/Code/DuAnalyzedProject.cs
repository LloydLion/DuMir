using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code
{
	class DuAnalyzedProject : Model
	{
		private DuProject baseProject;
		private IList<DuAnalysedCode> analysedCode = new List<DuAnalysedCode>();

		public DuProject BaseProject { get => baseProject; set { OnPropertyChanging(); baseProject = value; OnPropertyChanged(); } }

		public IList<DuAnalysedCode> AnalysedCode { get => analysedCode; set { OnPropertyChanging(); analysedCode = value; OnPropertyChanged(); } }


		public DuAnalyzedProject(DuProject baseProject)
		{
			BaseProject = baseProject;
		}


		public override string ToString()
		{
			return "";
		}
	}
}
