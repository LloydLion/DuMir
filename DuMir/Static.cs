using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	static class Static
	{
		public const string BasePath = @".\project\";

		public static Dictionary<string, object> LaunchArguments { get; set; }

		public static Dictionary<string, Func<string, object>> ShortTypeParsers { get; } = new Dictionary<string, Func<string, object>>()
		{
			{ "number", (s) => decimal.Parse(s) },
			{ "boolean", (s) => bool.Parse(s) },
			{ "string", (s) => s },
		};
	}
}
