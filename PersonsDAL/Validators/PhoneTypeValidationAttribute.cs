using PersonsDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Validators
{
    public class PhoneTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is PhoneType type)
            {
                if (type == PhoneType.Home || type == PhoneType.Office || type == PhoneType.Mobile)
                {
                    return ValidationResult.Success!;
                }
            }

            return new ValidationResult("Invalid type value. Accepted values: Home, Office, Mobile.");
        }

    }
}
