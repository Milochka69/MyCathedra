using SQLite;

namespace MyCathedra.DatabaseManager.Model
{
    public class Employee
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Fio { get; set; }

        public byte[] Bio { get; set; }

        public string BioType { get; set; }

        public byte[] Info { get; set; }

        public string InfoType { get; set; }

        public byte[] Photo { get; set; }

        public bool IsTeatcher { get; set; }

        public bool IsInner { get; set; }

        public bool IsActive { get; set; }
    }
}