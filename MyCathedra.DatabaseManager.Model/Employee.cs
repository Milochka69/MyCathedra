using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCathedra.DatabaseManager.Model
{
    [Table("EMPLOYEE")]
    class Employee
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("FIO")]
        public string Fio { get; set; }

        [Column("BIO")]
        public byte[] Bio { get; set; }

        [Column("BIO_TYPE")]
        public string BioType { get; set; }

        [Column("INFO")]
        public byte[] Info { get; set; }

        [Column("INFO_TYPE")]
        public string InfoType { get; set; }

        [Column("PHOTO")]
        public byte[] Photo { get; set; }

        [Column("IS_TEACHER")]
        public bool IsTeatcher { get; set; }

        [Column("IS_INNER")]
        public bool IsInner { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        public ICollection<RefDocument> Documents { get; set; }
    }
}
