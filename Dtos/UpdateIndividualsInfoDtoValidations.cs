using System.ComponentModel.DataAnnotations;
using DirectoryOfIndividuals.Api.Entities;
using DirectoryOfIndividuals.Api.Validators;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class UpdateIndividualsInfoDtoValidations
    {
        [Required]
        [Length(2, 50)]
        [OnlyGeorgianOrEnglish]
        public string? Name { get; set; }

        [Required]
        [Length(2, 50)]
        [OnlyGeorgianOrEnglish]
        public string? LastName { get; set; }

        public string? Sex { get; set; }

        [Required]
        [Length(11, 11)]
        public string? PersonalNumber { get; set; }

        [Required]
        [MinAgeValidator(18)]
        public DateOnly? BirthDate { get; set; }

        public int? CityId { get; set; }

        public List<UpdatePhoneNumbersDto>? PhoneNumbers { get; set; }
    }
}
