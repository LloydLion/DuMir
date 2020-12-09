using Newtonsoft.Json;
using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonIgnoreAttribute = Newtonsoft.Json.JsonIgnoreAttribute;

namespace DuMir.Models.Files
{
	class DuProjectProperies : Model
	{
		[JsonPropertyName("version")]
		public string Version { get; private set; }

		[JsonPropertyName("authors")]
		public string[] Authors { get; private set; }

		[JsonIgnore]
		public DuProjectFileInfo FileInfo { get; private set; }


		public DuProjectProperies(DuProjectFileInfo info)
		{
			var obj = JsonConvert.DeserializeObject<DuProjectProperies>(File.ReadAllText(info.Path));

			FileInfo = info;
			Authors = obj.Authors;
			Version = obj.Version;
		}


		public override string ToString() => $"Project Properies [Version:{Version}] described on file at {FileInfo.Path} by {string.Join(", ", Authors)}";
	}
}
