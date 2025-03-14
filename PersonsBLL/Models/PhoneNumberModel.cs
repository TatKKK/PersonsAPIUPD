using PersonsBLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Models
{
    public class PhoneNumberModel : AbstractModel
    {
        public PhoneNumberModel(int id, PhoneType type, string number, int personId) : base(id)
        {
            Type = type;
            Number = number;
            PersonId = personId;
        }

        public PhoneType Type { get; set; }
        public string Number { get; set; }
        public int PersonId { get; set; }
    }
}
