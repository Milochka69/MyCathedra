using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCathedra.DatabaseManager.Model
{
    class RefDocument
    {
        public RefDocument() { }

        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int year { get; set; }
        public RefDocumentCategory category { get; set; }
        public Employee author { get; set; }
        public DateTime createDate { get; set; }
        public bool is_active { get; set; }
    }
}
