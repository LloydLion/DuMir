using DuMir.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code
{
	class DuAnalysedCode : DuCode
	{
		public IList<CodeExecutable> Executables { get; set; }


		public DuAnalysedCode(DuCode baseCode) : base(baseCode.FileInfo) { }
	}
}
