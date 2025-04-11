using System.ComponentModel.DataAnnotations;

namespace DirectoryOfIndividuals.Api.Validators
{
    public class MinAgeValidator : ValidationAttribute
    {
        private readonly int _minAge;
        public MinAgeValidator(int minAge)
        {
            _minAge = minAge;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateOnly birthDate)
            {
                DateTime birthTime = birthDate.ToDateTime(new TimeOnly(00, 00, 00));
                DateTime today = DateTime.Now.Date;

                if ((today - birthTime).Days / 365 >= _minAge)
                    return ValidationResult.Success;
            }

            return new ValidationResult("Age must be at least 18");
        }
    }
}
