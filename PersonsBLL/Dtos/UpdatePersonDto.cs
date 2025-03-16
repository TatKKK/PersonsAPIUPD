using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class UpdatePersonDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Gender { get; set; }

        public string IdCard { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public int CityId { get; set; }

        public string ImagePath { get; set; } = string.Empty;

        public List<PhoneNumberDto> PhoneNumbers { get; set; } = new List<PhoneNumberDto>();
    }
}
