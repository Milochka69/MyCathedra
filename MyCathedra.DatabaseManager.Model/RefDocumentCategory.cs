using SQLite;

namespace MyCathedra.DatabaseManager.Model
{
    public class RefDocumentCategory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}