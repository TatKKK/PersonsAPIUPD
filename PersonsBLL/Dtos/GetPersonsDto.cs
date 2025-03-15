using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class GetPersonsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int CityId { get; set; }

        public DateTime BirthDate { get; set; }

        public int GenderId { get; set; }

        public string IdCard { get; set; }

        public List<GetPersonsDto> RelatedPersons { get; set; }

        public List<PhoneNumberDto> PhoneNumbers { get; set; }
    }
}
