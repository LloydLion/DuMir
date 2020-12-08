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
		[JsonPropertyName("modules")]
		public List<DuModule> Modules { get; private set; } = new List<DuModule>();


		public override string ToString()
		{
			return "";
		}
	}
}
