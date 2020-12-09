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
		public DuProject Run()
		{
			Logger.LogMessage("PRELOADING STADE START", Logger.LogLevel.Warning);

			var project = JsonConvert.DeserializeObject<DuProject>(File.ReadAllText(".\\project\\executable.duproj"));
			Logger.LogMessage("Project Base generated", Logger.LogLevel.Info);

			LookUpProjectModules(project, ".\\project");
			Logger.LogMessage("Modules attached to project", Logger.LogLevel.Info);

			LookUpAllModulesAndDepartmentsOfProject(project);

			Logger.LogMessage("PRELOADING STADE END", Logger.LogLevel.Warning);
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
					searchPath = basePath + "\\" + path[1..];
				}

				if(seeAllSybFolders == true)
				{
					foreach (var item in Directory.GetDirectories(searchPath))
					{
						project.ModulesObjects.Add(JsonConvert.DeserializeObject<DuModule>(File.ReadAllText(item + "\\module.duproj")));
						project.ModulesFullPaths.Add(item);
					}
				}
				else
				{
					project.ModulesObjects.Add(JsonConvert.DeserializeObject<DuModule>(File.ReadAllText(searchPath + "\\module.duproj")));
					project.ModulesFullPaths.Add(searchPath);
				}
			}
		}

		private static void LookUpAllModulesAndDepartmentsOfProject(DuProject project)
		{
			var log = project is DuModule module1 ? module1.Name + " MODULE" : "ROOT";
			Logger.LogMessage($"Looking Up Departments And Modules for {log}", Logger.LogLevel.Info);

			for (int i = 0; i < project.ModulesObjects.Count; i++)
			{
				var module = project.ModulesObjects[i];
				Logger.LogMessage($"Start working with module #{i + 1} with name \"{module.Name}\"", Logger.LogLevel.Info);

				Logger.LogMessage($"Attaching modules to {module.Name}", Logger.LogLevel.Info);
				LookUpProjectModules(module, project.ModulesFullPaths[i]);

				Logger.LogMessage($"Recursion invoke started", Logger.LogLevel.Info);
				LookUpAllModulesAndDepartmentsOfProject(module);
				Logger.LogMessage($"Recursion invoke finished", Logger.LogLevel.Info);

				Logger.LogMessage($"Attaching departments", Logger.LogLevel.Info);
				foreach (var department in module.Departments)
					module.ModulesObjects.Add(project.ModulesObjects.Single(s => s.Name == department));
			}

			Logger.LogMessage($"End Looking Up for {log}", Logger.LogLevel.Info);
		}
	}
}
