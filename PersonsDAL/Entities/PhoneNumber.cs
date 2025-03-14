using PersonsDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [MaxLength(20)]
        public string Number { get; set; }

        // Foreign key
        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
