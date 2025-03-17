using PersonsBLL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsBLL.Dtos
{
    public class PhoneNumberDto
    {
        public int Id { get; set; }

        [Required]
        public PhoneType Type { get; set; }

        [Required]
        [MinLength(4), MaxLength(20)]
        [RegularExpression(@"^[0-9]{11}$", ErrorMessage = "Phone number must contain only digits.")]
        public string? Number { get; set; }

        [Required]
        // Foreign key
        public int PersonId { get; set; }
    }
}
