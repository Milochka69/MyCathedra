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
        private const string Path = "./MyCathedraDb.sqlite";

        public DbManager(PasswordService passwordService)
        {
            if (File.Exists(Path)) return;

            using (var db = new SQLiteConnection(Path))
            {
                db.CreateTable<User>();
                db.CreateTable<Activity>();
                var id = Guid.NewGuid();
                db.Insert(new User {Id = id, Login = "mila", Password = passwordService.MakeHash(id, "mila123")});
                id = Guid.NewGuid();
                db.Insert(new User {Id = id, Login = "korovay", Password = passwordService.MakeHash(id, "korovay123")});
            }
        }


        public Task InsertActivity(Guid userId, string path)
        {
            var db = new SQLiteAsyncConnection(Path);

            return db.InsertAsync(new Activity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                File = path.Replace('\\', '/'),
                DataUtc = DateTime.UtcNow
            });
        }

        public async Task<UserActivity[]> GetUserActivity(string[] paths)
        {
            UserActivity[] activitys;
            List<User> users;
            paths = paths.Select(p => p.Replace('\\', '/')).ToArray();

            var db = new SQLiteAsyncConnection(Path);

            activitys = (await db.Table<Activity>().OrderByDescending(a => a.DataUtc)
                    .Where(a => paths.Contains(a.File)).ToListAsync())
                .GroupBy(a => a.File)
                .Select(a => new UserActivity {UserId = a.First().UserId, Path = a.Key, Data = a.First().DataUtc})
                .ToArray();

            var usersId = activitys.Select(a => a.UserId).Distinct().ToArray();

            users = await db.Table<User>().Where(u => usersId.Contains(u.Id)).ToListAsync();
            
            return activitys.Select(a =>
                {
                    a.UserName = users.Single(u => u.Id == a.UserId).Login;
                    return a;
                })
                .ToArray();
        }

        public User GetUserByLogin(string login)
        {
            login = login.ToLower();
            using (var db = new SQLiteConnection(Path))
            {
                return db.Table<User>().SingleOrDefault(u => u.Login == login);
            }
        }
    }
}