using DuMir.Models.Files;
using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DuMir.Models
{
	class DuProject : Model
	{
		[JsonIgnore]
		public List<DuModule> Modules { get; private set; } = new List<DuModule>();

		[JsonIgnore]
		public List<string> ModulesFullPaths { get; private set; } = new List<string>();

		[JsonIgnore]
		public List<DuCode> CodeFiles { get; private set; } = new List<DuCode>();

		[JsonIgnore]
		public List<DuProjectAsset> AssestFiles { get; private set; } = new List<DuProjectAsset>();

		[JsonIgnore]
		public DuProjectProperies PropertyFile { get; set; }

		[JsonPropertyName("files")]
		public List<DuProjectFileInfo> LinkedFiles { get; private set; } = new List<DuProjectFileInfo>();

		[JsonPropertyName("modules")]
		public string[] ModulesPaths { get; private set; }


		private DuProject() { }


		public override string ToString()
		{
			return "DuMir Project object";
		}
	}
}
