using Newtonsoft.Json;
using StandardLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DuMir.Models
{
	class DuProjectFileInfo : Model
	{
		[JsonProperty(PropertyName = "type")]
		public Type ContentType { get; private set; }


		[JsonProperty(PropertyName = "path")]
		public string Path { get; private set; }


		public override string ToString()
		{
			return $"Project {ContentType} file on {Path}";
		}


		public enum Type
		{
			Properties,
			Code,
			Asset
		}
	}
}
