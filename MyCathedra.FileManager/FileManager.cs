using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyCathedra.FileManager
{
    public class FileManager
    {
        private const string BaseFolder = "./Документы";

        public FileManager()
        {
            var directories = new[]
            {
                $"{BaseFolder}/Документы Кафедры/Выписки",
                $"{BaseFolder}/Документы Кафедры/Графики",
                $"{BaseFolder}/Документы Кафедры/Нагрузка",
                $"{BaseFolder}/Документы Кафедры/Отчёты",
                $"{BaseFolder}/Документы Кафедры/Планы/План 1/2017",
                $"{BaseFolder}/Документы Кафедры/Планы/План 1/2018",
                $"{BaseFolder}/Документы Кафедры/Планы/План 2/2017",
                $"{BaseFolder}/Документы Кафедры/Планы/План 2/2018",
                $"{BaseFolder}/Документы Кафедры/Планы/План 3/2017",
                $"{BaseFolder}/Документы Кафедры/Планы/План 3/2018",
                $"{BaseFolder}/Документы Кафедры/Протоколы"
            };

            foreach (var directory in directories)
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
            }
        }

        public IEnumerable<string> GetBaseDirectories()
        {
            return Directory.GetDirectories(BaseFolder).Select(ParsePath);
        }

        public IEnumerable<string> GetChildrenDirectories(string basePath)
        {
            return Directory.GetDirectories(GetPath(basePath)).Select(ParsePath);
        }

        private string GetPath(string folder)
        {
            return $"{BaseFolder}/{folder}";
        }

        private string ParsePath(string path)
        {
            var indexOf = path.LastIndexOf('\\');
            return path.Substring(indexOf + 1);
        }
    }
}