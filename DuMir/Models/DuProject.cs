using DuMir.Models.Files;
using Newtonsoft.Json;
using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace DuMir.Models
{
	class DuProject : Model
	{
		private DuProjectProperies propertyFile;

		[JsonIgnore]
		public IList<DuModule> ModulesObjects { get; private set; } = new List<DuModule>();

		[JsonIgnore]
		public IList<string> ModulesFullPaths { get; private set; } = new List<string>();

		[JsonIgnore]
		public IList<DuCode> CodeFiles { get; private set; } = new List<DuCode>();

		[JsonIgnore]
		public IList<DuProjectAsset> AssestFiles { get; private set; } = new List<DuProjectAsset>();

		[JsonIgnore]
		public DuProjectProperies PropertyFile { get => propertyFile; set { OnPropertyChanging(); propertyFile = value; OnPropertyChanged(); } }

		[JsonInclude]
		[JsonProperty(PropertyName = "files")]
		public DuProjectFileInfo[] LinkedFiles { get; private set; }

		[JsonInclude]
		[JsonProperty(PropertyName = "modules")]
		public string[] ModulesPaths { get; private set; }


		protected DuProject() { }


		public override string ToString()
		{
			return "DuMir Project object";
		}

		protected override void FinalizeObjectE()
		{
			AssestFiles = (AssestFiles as List<DuProjectAsset>).AsReadOnly();
			ModulesObjects = (ModulesObjects as List<DuModule>).AsReadOnly();
			CodeFiles = (CodeFiles as List<DuCode>).AsReadOnly();
			ModulesFullPaths = (ModulesFullPaths as List<string>).AsReadOnly();

			base.FinalizeObjectE();
		}
	}
}
