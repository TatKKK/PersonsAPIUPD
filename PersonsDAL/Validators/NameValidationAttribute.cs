using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonsDAL.Validators
{
    public class NameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not string name || string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult("Name is required.");
            }

            string georgianPattern = @"^[\u10A0-\u10FF\s]+$";
            string englishPattern = @"^[A-Za-z\s]+$";

            bool isGeorgian = Regex.IsMatch(name, georgianPattern);
            bool isEnglish = Regex.IsMatch(name, englishPattern);

            if (!(isGeorgian ^ isEnglish))
            {
                return new ValidationResult("Name must contain only Georgian or only English letters, not both.");
            }

            return ValidationResult.Success;
        }
    }
}
