using System;
using System.IO;

namespace SGet
{
	static class AppDataPath
	{	
		public static readonly string AppDataDirPath;

		static AppDataPath()
		{
			AppDataDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PathEdit");
			if (!Directory.Exists(AppDataDirPath))
				Directory.CreateDirectory(AppDataDirPath);
		}
	}
}
