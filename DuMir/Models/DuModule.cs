using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models
{
	class DuModule : DuProject
	{
		public string Name { get; private set; }

		public string[] Departments { get; private set; }
	}
}
