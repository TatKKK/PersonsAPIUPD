using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonsDAL.Validators
{
    public class AgeValidationAttribute : ValidationAttribute
    {
        private readonly int minimumAge;

        public AgeValidationAttribute(int minimumAge)
        {
            this.minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                var today = DateTime.UtcNow.Date;
                birthDate = birthDate.Date;
                var age = today.Year - birthDate.Year;

                // Adjust for leap years and cases where birthday hasn't occurred yet
                if (birthDate > today.AddYears(-age))
                {
                    age--;
                }

                if (age < minimumAge)
                {
                    return new ValidationResult($"Person must be at least {minimumAge} years old.");
                }
            }
            else
            {
                return new ValidationResult("Invalid date format.");
            }

            return ValidationResult.Success!;
        }
    }
}
