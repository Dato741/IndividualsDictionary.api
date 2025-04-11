using DirectoryOfIndividuals.Api.Components;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class PhoneNumbersDto
    {
        public int PhoneNumberId { get; set; }
        public string NumberType { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
    }
}
