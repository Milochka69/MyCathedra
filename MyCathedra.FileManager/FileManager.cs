using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MyCathedra.DataManager;

namespace MyCathedra.FileManager
{
    public class FileManager
    {
        private const string BaseFolder = "./Документы";

        public FileManager()
        {
            var directories = new[]
            {
                $"{BaseFolder}/Общие сведения о кафедре/История кафедры",
                $"{BaseFolder}/Общие сведения о кафедре/Перечень дисциплин",
                $"{BaseFolder}/Общие сведения о кафедре/Состав кафедры",
                $"{BaseFolder}/Документация кафедры/Выписки/2017",
                $"{BaseFolder}/Документация кафедры/Выписки/2018",
                $"{BaseFolder}/Документация кафедры/Протоколы/2017",
                $"{BaseFolder}/Документация кафедры/Протоколы/2018",
                $"{BaseFolder}/Документация кафедры/Графики/2017/Самостоятельные работы",
                $"{BaseFolder}/Документация кафедры/Графики/2017/Контрольные работы",
                $"{BaseFolder}/Документация кафедры/Графики/2017/Практические работы",
                $"{BaseFolder}/Документация кафедры/Графики/2017/Дипломные работы",
                $"{BaseFolder}/Документация кафедры/Графики/2018/Самостоятельные работы",
                $"{BaseFolder}/Документация кафедры/Графики/2018/Контрольные работы",
                $"{BaseFolder}/Документация кафедры/Графики/2018/Практические работы",
                $"{BaseFolder}/Документация кафедры/Графики/2018/Дипломные работы",
                $"{BaseFolder}/Документация кафедры/Нагрузка/2017",
                $"{BaseFolder}/Документация кафедры/Нагрузка/2018",
                $"{BaseFolder}/Документация кафедры/Планы/НИР/2017",
                $"{BaseFolder}/Документация кафедры/Планы/НИР/2018",
                $"{BaseFolder}/Документация кафедры/Планы/НИРС/2017",
                $"{BaseFolder}/Документация кафедры/Планы/НИРС/2018",
                $"{BaseFolder}/Документация кафедры/Планы/Кафедра/2017",
                $"{BaseFolder}/Документация кафедры/Планы/Кафедра/2018",
                $"{BaseFolder}/Документация кафедры/Отчеты/НИР/2017",
                $"{BaseFolder}/Документация кафедры/Отчеты/НИР/2018",
                $"{BaseFolder}/Документация кафедры/Отчеты/НИРС/2017",
                $"{BaseFolder}/Документация кафедры/Отчеты/НИРС/2018",
                $"{BaseFolder}/Документация кафедры/Отчеты/Кафедра/2017",
                $"{BaseFolder}/Документация кафедры/Отчеты/Кафедра/2018",
                $"{BaseFolder}/Документация кафедры/Нормативные документы/",
                $"{BaseFolder}/Сотрудники/Преподаватели/Ф.И.О./Данные о преподавателе",
                $"{BaseFolder}/Сотрудники/Преподаватели/Ф.И.О./Индивидуальный план",
                $"{BaseFolder}/Сотрудники/Преподаватели/Ф.И.О./Кураторство",
                $"{BaseFolder}/Сотрудники/Преподаватели/Ф.И.О./Научные труды",
                $"{BaseFolder}/Сотрудники/Преподаватели/Ф.И.О./Руководство дипломными работами",
                $"{BaseFolder}/Сотрудники/Преподаватели/Ф.И.О./Нагрузка/2017",
                $"{BaseFolder}/Сотрудники/Преподаватели/Ф.И.О./Нагрузка/2018",
                $"{BaseFolder}/Сотрудники/Учебно-вспомагательный персонал/Ф.И.О./Данные о сотруднике",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/ООП",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Стандарты",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Рабочая программа",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/ФОСЫ",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Практики/Учебная практика",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Практики/НИР",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Практики/Преподавательская",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Расписание",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Индивидуальный план",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/ИГА/Приказы",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/ИГА/Программы",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/ИГА/Экзаменационный матриал",
                $"{BaseFolder}/Учебный процесс/Магистры/Специальность/2017/Аналитическая справка",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/ООП",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/Рабочая программа",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/ФОСЫ",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/Практики/Учебная практика",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/Практики/НИР",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/Практики/Преподавательская",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/Расписание",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/ИГА",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/Димломные",
                $"{BaseFolder}/Учебный процесс/Бакалавры/Специальность/2017/Курс распределения",
                $"{BaseFolder}/УМКД/Специальность/2017/Дисциплина/Аннотация",
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
            return Directory.GetDirectories(BaseFolder).Select(ParsePath).OrderBy(f => f);
        }

        public string GetParentPath(string path)
        {
            var i = path.LastIndexOf('\\');
            return i < 0 ? null : path.Remove(i);
        }

        public bool OpenFile(FileInfo file)
        {
            try
            {
                if (!file.IsFle) return false;
                var fileName = GetPath(file.Path); // востанавливает путь.
                Process.Start(fileName); // запускает файл
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Duplicate(FileInfo file, string newName, DbManager dbManager, Guid userId)
        {
            var sourcePath = GetPath(file.Path);

            if (file.IsFle)
            {
                var fileName = Path.GetFileName(sourcePath);
                if (string.IsNullOrWhiteSpace(fileName)) return;
                var destFileName = sourcePath.Replace(fileName, newName);
                File.Copy(sourcePath, destFileName);
                dbManager.InsertActivity(userId, NormalPath(destFileName), ActivityType.Create);
            }
            else
            {
                var directoryName = sourcePath.Remove(0, sourcePath.LastIndexOf('\\') + 1);
                if (string.IsNullOrWhiteSpace(directoryName)) return;
                foreach (var dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                {
                    var regex = new Regex(Regex.Escape(directoryName));
                    var replace = regex.Replace(dirPath, newName, 1);
                    Directory.CreateDirectory(replace);
                }

                foreach (var filePath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                {
                    var regex = new Regex(Regex.Escape(directoryName));
                    var destFileName = regex.Replace(filePath, newName, 1);
                    File.Copy(filePath, destFileName, true);
                    dbManager.InsertActivity(userId, NormalPath(destFileName), ActivityType.Create);
                }
            }
        }

        public string Move(FileInfo file, string newName) // переименовывает папку или файл справа окна
        {
            var path = GetPath(file.Path);
            var newPath = GetParentPath(path) + "/" + newName;

            if (file.IsFle) File.Move(path, newPath);
            else Directory.Move(path, newPath);

            return $"{GetParentPath(file.Path)}/{newName}";
        }

        public string MoveFolder(string path, string newName) //переименовывает папку в навигационной панели
        {
            var fullPath = GetPath(path);
            var i = fullPath.LastIndexOf('/');
            var newPath = fullPath.Remove(i) + "/" + newName;
            Directory.Move(fullPath, newPath);

            return $"{GetParentPath(path)}/{newName}".Remove(0, 1);
        }

        public string AddFile(string sourceFileName, string destFileName)
        {
            var fileName = ParsePath(sourceFileName);
            destFileName += $"/{fileName}"; //добавляет впереди /
            File.Copy(sourceFileName, GetPath(destFileName));
            return destFileName;
        }

        public IEnumerable<FileInfo> GetChildren(string path)
        {
            path = GetPath(path);
            return FileInfos(path);
        }

        public void CreateFolder(string path, string folderName)
        {
            Directory.CreateDirectory(GetPath($"{path}/{folderName}"));
        }

        public void Delete(FileInfo fileInfo, DbManager dbManager, Guid userId)
        {
            var path = GetPath(fileInfo.Path);
            if (!fileInfo.IsFle)
            {
                foreach (var filePath in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    dbManager.InsertActivity(userId, NormalPath(filePath), ActivityType.Delete);
                }

                Directory.Delete(path, true);
            }
            else
            {
                File.Delete(path);
            }

            dbManager.InsertActivity(userId, fileInfo.Path, ActivityType.Delete);
        }

        public void DeleteFolder(string path)
        {
            Directory.Delete(GetPath(path), true);
        }


        public IEnumerable<FileInfo> Search(string path, string text)
        {
            var searchRec = SearchRec(GetPath(path));
            var regex = new Regex(BaseFolder + @"/.+\\.+");
            searchRec = searchRec.Where(s => regex.Match(s).Success).ToArray(); // отсеивает новигационное меню

            var split = text.Split(' ').Select(t => t.ToLower());
            var list = searchRec
                    .Select(p =>
                        new {p, k = split.Aggregate(true, (current, s1) => current && p.ToLower().Contains(s1))})
                    .Where(t => t.k)
                    .Select(t => new FileInfo
                    {
                        Name = ParsePath(t.p),
                        Path = NormalPath(t.p),
                        UpdateUtc = Directory.GetLastWriteTimeUtc(t.p),
                        IsFle = File.Exists(t.p)
                    })
                    .OrderByDescending(p => p.Name)
                ;

            return list;
        }

        private IEnumerable<string> SearchRec(string path)
        {
            var list = new List<string>(); //захожу в папочку
            var paths = Directory.GetDirectories(path);
            foreach (var p in paths)
            {
                list.AddRange(SearchRec(p));
            }

            list.Add(path);
            list.AddRange(Directory.GetFiles(path));

            return list;
        }

        private IEnumerable<FileInfo> FileInfos(string path)
        {
            return Directory.GetDirectories(path)
                .Select(p => new FileInfo
                {
                    Name = ParsePath(p),
                    Path = NormalPath(p),
                    UpdateUtc = Directory.GetLastWriteTimeUtc(p),
                    IsFle = false
                })
                .Union(Directory.GetFiles(path)
                    .Select(p => new FileInfo
                    {
                        Name = ParsePath(p),
                        Path = NormalPath(p),
                        UpdateUtc = Directory.GetLastWriteTimeUtc(p),
                        IsFle = true
                    }))
                .OrderBy(f => f.IsFle)
                .ThenBy(f => f.Name);
        }

        private string GetPath(string folder)
        {
            return $"{BaseFolder}/{folder}";
        }

        private string NormalPath(string path)
        {
            return path.Replace(BaseFolder, string.Empty).Remove(0, 1); // удаляет базовую директорию 
        }

        private string ParsePath(string path)
        {
            var indexOf = path.LastIndexOf('\\');
            return path.Substring(indexOf + 1);
        }
    }
}