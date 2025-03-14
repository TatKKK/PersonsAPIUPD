using PersonsDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Validators
{
    public class GenderValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Gender gender)
            {
                if (gender == Gender.Male || gender == Gender.Female)
                {
                    return ValidationResult.Success!;
                }
            }

            return new ValidationResult("Invalid gender value. Accepted values: Male, Female.");
        }
    }
}
