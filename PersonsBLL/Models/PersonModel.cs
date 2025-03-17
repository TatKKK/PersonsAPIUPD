using PersonsBLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "An ID Card number must be exactly 11 digits long and consist solely of numeric characters")]
        public string IdCard { get; set; }
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }
        public string ImagePath { get; set; }
        public List<PhoneNumberModel> PhoneNumbers { get; set; }
        public List<PersonRelationshipModel> RelatedPersons { get; set; }
    }
}
