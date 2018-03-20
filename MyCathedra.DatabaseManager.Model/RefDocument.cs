using System;
using SQLite;

namespace MyCathedra.DatabaseManager.Model
{
    public class RefDocument
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int Year { get; set; }

        [Indexed]
        public int CategoryId { get; set; }

        [Indexed]
        public int AuthorId { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool IsActive { get; set; }
    }
}