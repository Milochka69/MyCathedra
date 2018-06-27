using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace MyCathedra.DataManager
{
    public class DbManager
    {
        private const string Path = "./MyCathedraDb.sqlite"; //имя базы 

        public DbManager(PasswordService passwordService) // конструктор класса базы данных 
        {
            if (File.Exists(Path)) return;

            using (var db = new SQLiteConnection(Path)) // конект с базой
            {
                db.CreateTable<User>();
                db.CreateTable<Activity>();
                var id = Guid.NewGuid();
                db.Insert(new User {Id = id, Login = "mila", Password = passwordService.MakeHash(id, "mila123")});
                id = Guid.NewGuid();
                db.Insert(new User {Id = id, Login = "korovay", Password = passwordService.MakeHash(id, "korovay123")});
            }
        }

        public IEnumerable<LogInfo> GetLogInfo(int page, int pageSize) // получение лога
        {
            Activity[] activities; // массивчик
            User[] users;
            using (var db = new SQLiteConnection(Path))
            {
                activities = db.Table<Activity>().OrderByDescending(a => a.DataUtc)
                    .Skip(page * pageSize).Take(pageSize)
                    .ToArray(); // сортируем по дате из активностей
                var usersId = activities.Select(a => a.UserId).Distinct().ToArray();
                users = db.Table<User>().Where(u => usersId.Contains(u.Id)).ToArray();
            }


            return activities.Select(a => new LogInfo
            {
                Id = a.Id,
                DataUtc = a.DataUtc,
                File = a.File,
                Type = a.Type,
                UserName = users.Single(u => u.Id == a.UserId).Login
            });
        }

        public Task InsertActivity(Guid userId, string path, ActivityType type, string newName = null)
        {
            var db = new SQLiteAsyncConnection(Path);

            return db.InsertAsync(new Activity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                File = path.Replace('\\', '/'),
                DataUtc = DateTime.UtcNow,
                Type = type,
                NewName = newName?.Replace('\\', '/')
            });
        }

        public async Task<UserActivity[]> GetUserActivity(string[] paths)
        {
            paths = paths.Select(p => p.Replace('\\', '/')).ToArray();

            var db = new SQLiteAsyncConnection(Path);

            var activitys = (await db.Table<Activity>().OrderByDescending(a => a.DataUtc)
                    .Where(a => paths.Contains(a.File)).ToListAsync())
                .GroupBy(a => a.File)
                .Select(a => new UserActivity {UserId = a.First().UserId, Path = a.Key, Data = a.First().DataUtc})
                .ToArray();

            var usersId = activitys.Select(a => a.UserId).Distinct().ToArray();

            var users = await db.Table<User>().Where(u => usersId.Contains(u.Id)).ToListAsync();

            return activitys.Select(a =>
                {
                    a.UserName = users.Single(u => u.Id == a.UserId).Login;
                    return a;
                })
                .ToArray();
        }

        public User GetUserByLogin(string login)
        {
            login = login.ToLower().Trim();
            using (var db = new SQLiteConnection(Path))
            {
                return db.Table<User>().SingleOrDefault(u => u.Login == login);
            }
        }
    }
}