using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DirectoryOfIndividuals.Api.Components;

namespace DirectoryOfIndividuals.Api.Entities
{
    public record class PhoneNumbersEntity
    {
        [Key]
        public int PhoneNumberId { get; set; }
        public NumberCategory NumberType { get; set; }
        public string Number { get; set; } = string.Empty;
        public int NumberOwnerId { get; set; }

        [JsonIgnore]
        public IndividualEntity? NumberOwner { get; set; }
    }
}
