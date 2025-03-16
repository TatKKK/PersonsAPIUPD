using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Models
{
    public class PersonsReport
    {
        public string IdCard { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Type { get; set; }
        public int Count { get; set; }
    }
}
