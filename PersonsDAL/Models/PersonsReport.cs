using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Models
{
    public class PersonsReport
    {
        public string IdCard { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }

        public int Type { get; set; }
        public int Count { get; set; }
    }
}
