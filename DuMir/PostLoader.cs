using DuMir.Models;
using DuMir.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuMir
{
	class PostLoader
	{
		public DuProject Run(DuProject preLoaderResult)
		{
			Logger.LogMessage("POSTLOADING STADE START", Logger.LogLevel.Warning);
			var files = preLoaderResult.LinkedFiles;

			Logger.LogMessage("Start files sorting", Logger.LogLevel.Info);
			Logger.LogMessage("Code files....", Logger.LogLevel.Info);
			preLoaderResult.CodeFiles.AddRange(files.Where(s => s.ContentType == DuProjectFileInfo.Type.Code).Select(s => new DuCode(s)));
			Logger.LogMessage("Property file....", Logger.LogLevel.Info);
			preLoaderResult.PropertyFile = new DuProjectProperies(files.Single(s => s.ContentType == DuProjectFileInfo.Type.Properties));
			Logger.LogMessage("Assets files....", Logger.LogLevel.Info);
			preLoaderResult.AssestFiles.AddRange(files.Where(s => s.ContentType == DuProjectFileInfo.Type.Asset).Select(s => new DuProjectAsset(s)));
			Logger.LogMessage("End files sorting", Logger.LogLevel.Info);

			Logger.LogMessage("Finalizing project object", Logger.LogLevel.Info);
			preLoaderResult.FinalizeObject();

			Logger.LogMessage("POSTLOADING STADE END", Logger.LogLevel.Warning);
			return preLoaderResult;
		}
	}
}
