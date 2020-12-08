using Newtonsoft.Json;
using StandardLibrary.Models;
using System;
using System.Collections.Generic;
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


		private DuProjectAsset() { }


		public static DuProjectAsset CreateInstanceFromJSON(string json, DuProjectFileInfo info)
		{
			var obj = JsonConvert.DeserializeObject<DuProjectAsset>(json);
			obj.FileInfo = info;
			return obj;
		}

		public override string ToString() => FileInfo.ToString();
	}
}
