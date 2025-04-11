using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DirectoryOfIndividuals.Api.Validators
{
    public class OnlyGeorgianOrEnglish : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string str && (Regex.IsMatch(str, @"^[\u10A0-\u10FF]+$") || Regex.IsMatch(str, @"^[a-zA-Z]+$")))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Input must contain only English or Georgian characters");
        }
    }
}
