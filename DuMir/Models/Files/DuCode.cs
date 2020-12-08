using Newtonsoft.Json;
using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir.Models.Files
{
	class DuCode : Model
	{
		public string Text { get; private set; }

		public DuProjectFileInfo FileInfo { get; private set; }


		public DuCode(DuProjectFileInfo file) 
		{
			Text = File.ReadAllText(file.Path);
			FileInfo = file;
		}


		public override string ToString() => FileInfo.ToString();
	}
}
