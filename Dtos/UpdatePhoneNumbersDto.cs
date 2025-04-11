using System.Text.Json.Serialization;
using DirectoryOfIndividuals.Api.Components;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class UpdatePhoneNumbersDto
    {
        public int PhoneNumberId { get; set; }
        public NumberCategory NumberType { get; set; }
        public string Number { get; set; } = string.Empty;
    }
}
