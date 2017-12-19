using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCathedra.DatabaseManager.Model
{
    [Table("REF_DOCUMENT")]
    public class RefDocument
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("TYPE")]
        public string Type { get; set; }

        [Column("YEAR")]
        public int Year { get; set; }

        [Column("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [Column("AUTOR_ID")]
        public int AuthorId { get; set; }

        [Column("CREATE_DATE")]
        public DateTime? CreateDate { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        public virtual Employee Author { get; set; }

        public virtual RefDocumentCategory Category { get; set; }
    }
}
