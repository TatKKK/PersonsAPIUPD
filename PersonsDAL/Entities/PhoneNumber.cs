using PersonsDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Entities
{
    public class PhoneNumber : BaseEntity
    {
        [Required]
        public PhoneType Type { get; set; }  // Mobile, Home, Office

        [Required]
        [MinLength(4), MaxLength(20)]
        [RegularExpression(@"^[0-9]{4,20}$", ErrorMessage = "Phone number must contain only digits (4 to 20 digits).")]
        public string Number { get; set; } = string.Empty;

        // Foreign key
        public int PersonId { get; set; }
        public Person Person { get; set; } = new Person();
    }
}
