using DirectoryOfIndividuals.Api.Validators;
using System.ComponentModel.DataAnnotations;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class UpdateIndInfoDto
    {
        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Sex { get; set; }

        public string? PersonalNumber { get; set; }

        public DateOnly? BirthDate { get; set; }

        public int? CityId { get; set; }

        public List<UpdatePhoneNumbersDto>? PhoneNumbers { get; set; }
    }
}
