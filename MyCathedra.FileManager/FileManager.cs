using System;
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
                $"{BaseFolder}/Документация/Выписки",
                $"{BaseFolder}/Документация/Графики",
                $"{BaseFolder}/Документация/Нагрузка",
                $"{BaseFolder}/Документация/Отчёты",
                $"{BaseFolder}/Документация/Планы/План 1/2017",
                $"{BaseFolder}/Документация/Планы/План 1/2018",
                $"{BaseFolder}/Документация/Планы/План 2/2017",
                $"{BaseFolder}/Документация/Планы/План 2/2018",
                $"{BaseFolder}/Документация/Планы/План 3/2017",
                $"{BaseFolder}/Документация/Планы/План 3/2018",
                $"{BaseFolder}/Общие сведенья о кафедре/Состав кафедры",
                $"{BaseFolder}/Общие сведенья о кафедре/Данные о кафедре",
                $"{BaseFolder}/Общие сведенья о кафедре/Дисцеплины",
                $"{BaseFolder}/Общие сведенья о кафедре/Нормативные документы",
                $"{BaseFolder}/Сотрудники/Леонова Наталья Григорьевна",
                $"{BaseFolder}/Сотрудники/Спиридонова Галина Васильевна",
                $"{BaseFolder}/Сотрудники/Старчук Татьяна Ивановна",
                $"{BaseFolder}/УМКД/Магистры",
                $"{BaseFolder}/УМКД/Бакалавры",
                $"{BaseFolder}/Учебный процесс/",
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

        public IEnumerable<FileInfo> GetChildrenDirectories(string basePath)
        {
            var path = GetPath(basePath);
            return Directory.GetDirectories(path)
                .Select(p => new FileInfo
                {
                    Name = ParsePath(p),
                    Path = p,
                    UpdateUtc = Directory.GetLastWriteTimeUtc(p),
                    IsFle = false
                })
                .Union(Directory.GetFiles(path)
                    .Select(p => new FileInfo
                    {
                        Name = ParsePath(p),
                        Path = p,
                        UpdateUtc = Directory.GetLastWriteTimeUtc(p),
                        IsFle = true
                    }));
        }

        public bool OpenFile(FileInfo file)
        {
            if (!file.IsFle) return false;
            System.Diagnostics.Process.Start(file.Path);
            return true;
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

    public class FileInfo
    {
        public string Name { get; set; }
        public DateTime UpdateUtc { get; set; }
        public string Path { get; set; }
        public bool IsFle { get; set; }
    }
}