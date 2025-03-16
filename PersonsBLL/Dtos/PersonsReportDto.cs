using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class PersonsReportDto
    {
        public string IdCard { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Type { get; set; }
        public int Count { get; set; }
    }
}
