using PersonsDAL.Entities.Enums;
using PersonsDAL.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PersonsDAL.Entities
{
    [Table("persons")]
    public class Person : BaseEntity
    {
        // Basic Information
        [Required, NameValidation]
        [MinLength(2), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, NameValidation]
        [MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [GenderValidation]
        public Gender Gender { get; set; }

        // Identification
        [Required]
        [MinLength(11), MaxLength(11)]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "ID Card must contain exactly 11 digits.")]
        public string IdCard { get; set; } = string.Empty;

        [Required]
        [AgeValidation(18)]
        public DateTime BirthDate { get; set; }

        // City Relationship
        [Column("city_id")]
        public int CityId { get; set; }
        public City City { get; set; } // Navigation property

        // Additional Data
        [MaxLength(255)]
        public string ImagePath { get; set; } = string.Empty;  // File path

        // Relationships
        [JsonIgnore]
        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();

        [JsonIgnore]
        public ICollection<PersonRelationship> PersonRelationships { get; set; } = new List<PersonRelationship>();
    }
}
