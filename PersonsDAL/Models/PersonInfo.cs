using PersonsDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Models
{
    public class PersonInfo
    {
        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string IdCard { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string CityName { get; set; }

        public string ImagePath { get; set; } = string.Empty;  // File path

        public List<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public List<PersonRelationship> PersonRelationships { get; set; } = new List<PersonRelationship>();
    }
}
