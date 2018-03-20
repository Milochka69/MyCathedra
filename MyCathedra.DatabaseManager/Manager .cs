using System.IO;
using MyCathedra.DatabaseManager.Model;
using SQLite;

namespace MyCathedra.DatabaseManager
{
    public class Manager
    {
        private readonly string _dbPath;

        public Manager() : this("db.sqlite")
        {
        }

        public Manager(string dbPath)
        {
            _dbPath = dbPath;
            Init();
        }


        private void Init()
        {
            if (File.Exists(_dbPath)) return;
           
            using (var db = new SQLiteConnection(_dbPath))
            {
                db.CreateTable<Employee>();
                db.CreateTable<RefDocument>();
                db.CreateTable<RefDocumentCategory>();
            }
        }
    }
}
