using PersonsBLL.Models;
using PersonsDAL.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class AddPersonDto
    {
        [Required, NameValidation]
        [MinLength(2), MaxLength(50)]
        public string Name { get; set; }

        [Required, NameValidation]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        //[GenderValidation]
        public int Gender { get; set; }

        [Required]
        [MinLength(11), MaxLength(11)]
        public string IdCard { get; set; }

        [Required]
        [AgeValidation(18)]
        public DateTime BirthDate { get; set; }
        public int CityId { get; set; }

        [MaxLength(255)]
        public string ImagePath { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
        public List<PersonRelationshipDto> RelatedPersons { get; set; }
    }
}
