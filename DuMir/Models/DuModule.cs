using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DuMir.Models
{
	class DuModule : DuProject
	{
		[JsonInclude]
		[JsonProperty(PropertyName = "name")]
		public string Name { get; private set; }

		[JsonInclude]
		[JsonProperty(PropertyName = "departments")]
		public string[] Departments { get; private set; }


		private DuModule() { }
	}
}
