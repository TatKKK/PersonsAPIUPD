using PersonsBLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Models
{
    public class PersonModel : AbstractModel
    {
        public PersonModel(int id, string name, string lastName, Gender gender, string idCard, DateTime birthDate, int cityId, List<PhoneNumberModel> phoneNumbers,
            List<PersonRelationshipModel> relatedPersons)
           : base(id)
        {
            Name = name;
            LastName = lastName;
            Gender = gender;
            IdCard = idCard;
            BirthDate = birthDate;
            CityId = cityId;
            PhoneNumbers = phoneNumbers ?? new List<PhoneNumberModel>();
            RelatedPersons = relatedPersons ?? new List<PersonRelationshipModel>();
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string IdCard { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string ImagePath { get; set; }
        public List<PhoneNumberModel> PhoneNumbers { get; set; }
        public List<PersonRelationshipModel> RelatedPersons { get; set; }
    }
}
