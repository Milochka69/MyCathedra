using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCathedra.DatabaseManager.Model
{
    [Table("EMPLOYEE")]
    public class Employee
    {
        public Employee() {
            this.Documents = new ObservableCollection<RefDocument>();
        }

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

        public virtual ObservableCollection<RefDocument> Documents { get; private set; }
    }
}
