using DuMir.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	class PreLoader
	{
		public async Task<DuProject> RunAsync()
		{
			var project = JsonConvert.DeserializeObject<DuProject>(await File.ReadAllTextAsync("executable.duproj"));

			LookUpProjectModules(project, ".");



			return project;
		}

		private static void LookUpProjectModules(DuProject project, string basePath)
		{
			foreach (var path in project.ModulesPaths)
			{
				var seeAllSybFolders = false;
				var searchPath = basePath + "\\" + path;

				if(path.StartsWith("!"))
				{
					seeAllSybFolders = true;
					searchPath = path[1..];
				}

				if(seeAllSybFolders == true)
				{
					foreach (var item in Directory.GetDirectories(searchPath))
					{
						project.Modules.Add(JsonConvert.DeserializeObject<DuModule>(File.ReadAllText(item + "\\module.duproj")));
						project.ModulesFullPaths.Add(item);
					}
				}
				else
				{
					project.Modules.Add(JsonConvert.DeserializeObject<DuModule>(File.ReadAllText(searchPath + "\\module.duproj")));
					project.ModulesFullPaths.Add(searchPath);
				}
			}
		}

		private static void LookUpAllModelsAndDepartmentsOfProject(DuProject project)
		{
			for (int i = 0; i < project.Modules.Count; i++)
			{
				var module = project.Modules[i];

				foreach (var department in module.Departments)
					module.Modules.Add(project.Modules.Single(s => s.Name == department));

				LookUpProjectModules(module, project.ModulesFullPaths[i]);


				LookUpAllModelsAndDepartmentsOfProject(module);
			}
		}
	}
}
