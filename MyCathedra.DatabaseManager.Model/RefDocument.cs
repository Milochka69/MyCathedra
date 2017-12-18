using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCathedra.DatabaseManager.Model
{
    [Table("REF_DOCUMENT")]
    class RefDocument
    {
        public RefDocument() { }

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
        public RefDocumentCategory Category { get; set; }

        [Column("AUTOR_ID")]
        public Employee Author { get; set; }

        [Column("CREATE_DATE")]
        public DateTime? CreateDate { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }
    }
}
