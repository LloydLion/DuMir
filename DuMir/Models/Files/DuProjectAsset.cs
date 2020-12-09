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
	class DuProjectAsset : Model
	{
		[JsonPropertyName("path")]
		public string LinkedAssetPath { get; private set; }

		[JsonIgnore]
		public DuProjectFileInfo FileInfo { get; private set; }


		public DuProjectAsset(DuProjectFileInfo info)
		{
			var obj = JsonConvert.DeserializeObject<DuProjectAsset>(File.ReadAllText(info.Path));

			FileInfo = info;
			LinkedAssetPath = obj.LinkedAssetPath;
		}


		public override string ToString() => FileInfo.ToString();
	}
}
