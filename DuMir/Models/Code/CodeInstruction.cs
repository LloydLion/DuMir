﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Code
{
	abstract class CodeInstruction : CodeExecutable
	{
		public CodeBlock DefineBlock { get; set; }


		protected CodeInstruction() : base()
		{

		}
	}
}