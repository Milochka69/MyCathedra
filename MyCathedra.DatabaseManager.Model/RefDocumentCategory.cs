using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCathedra.DatabaseManager.Model
{
    [Table("REF_DOCUMENT_CATEGORY")]
    class RefDocumentCategory
    {
        public RefDocumentCategory() { }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        public ICollection<RefDocument> documents { get;; set; }

    }
}
