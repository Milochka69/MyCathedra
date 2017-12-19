using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCathedra.DatabaseManager.Model
{
    [Table("REF_DOCUMENT_CATEGORY")]
    public class RefDocumentCategory
    {
        public RefDocumentCategory()
        {
            Documents = new ObservableCollection<RefDocument>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }

        public virtual ObservableCollection<RefDocument> Documents { get; private set; }

    }
}
