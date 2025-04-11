using DirectoryOfIndividuals.Api.Components;
using DirectoryOfIndividuals.Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class CreateIndividualDto
    {
        [Required]
        [Length(2, 50)]
        [OnlyGeorgianOrEnglish]
        public required string Name { get; set; }

        [Required]
        [Length(2, 50)]
        [OnlyGeorgianOrEnglish]
        public required string LastName { get; set; }
        public Sex Sex { get; set; }

        [Required]
        [Length(11, 11)]
        public required string PersonalNumber { get; set; }

        [Required]
        [MinAgeValidator(18)]
        public DateOnly BirthDate { get; set; }

        public int? CityId { get; set; } = null;

        public List<CreatePhoneNumberDto>? PhoneNumbers { get; set; } = null!;

        public string? ImageAdress { get; set; } = null;

        public List<CreateConnectedIndividualDto>? ConnectedPersons { get; set; } = null;
    }
}
