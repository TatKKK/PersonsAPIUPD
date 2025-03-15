using PersonsBLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class PhoneNumberDto
    {
        public int Id { get; set; }

        public PhoneType Type { get; set; }

        public string Number { get; set; }

        // Foreign key
        public int PersonId { get; set; }
    }
}
