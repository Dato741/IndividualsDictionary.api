using DirectoryOfIndividuals.Api.Entities;
using DirectoryOfIndividuals.Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class IndividualDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public required string LastName { get; set; }
        public string? Sex { get; set; }

        public required string PersonalNumber { get; set; }

        public DateOnly BirthDate { get; set; }

        public int? CityId { get; set; }

        public List<PhoneNumbersDto>? PhoneNumbers { get; set; }

        public string? ImageAdress { get; set; }

        public List<ConnectedIndDto>? ConnectedPersons { get; set; }
    }
}
