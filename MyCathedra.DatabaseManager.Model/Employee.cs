using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCathedra.DatabaseManager.Model
{
    class Employee
    {
        public int id { get; set; }
        public string fio { get; set; }
        public byte[] bio { get; set; }
        public string bio_type { get; set; }
        public byte[] info { get; set; }
        public string info_type { get; set; }
        public byte[] photo { get; set; }
        public bool is_teatcher { get; set; }
        public bool is_inner { get; set; }
        public bool is_active { get; set; }

        public ICollection<RefDocument> documents { get; set; }
    }
}
