using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCathedra.DatabaseManager.Model
{
    class RefDocumentCategory
    {
        public RefDocumentCategory() { }

        public int id { get; set; } 
        public string name { get; set; }
        public bool is_active { get; set; }

        public ICollection<RefDocument> documents { get;; set; }

    }
}
