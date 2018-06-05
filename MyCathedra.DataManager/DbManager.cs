using System;
using System.IO;
using System.Linq;
using SQLite;

namespace MyCathedra.DataManager
{
    public class DbManager
    { 
        private const  string Path = "./MyCathedraDb.sqlite";

        public DbManager()
        {
            
            if (!File.Exists(Path)) return;

            using (var db = new SQLiteConnection(Path))
            {
                db.CreateTable<User>();
                db.CreateTable<Activity>();
            }
        }

        public User GetUserByLogin(string login )
        {
            login = login.ToLower();
            using (var db = new SQLiteConnection(Path))
            {
                return db.Table<User>().Single(u => u.Login == login);

            }
        }

    }
}