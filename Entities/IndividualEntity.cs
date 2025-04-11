using System.ComponentModel.DataAnnotations;
using DirectoryOfIndividuals.Api.Components;
using DirectoryOfIndividuals.Api.Validators;

namespace DirectoryOfIndividuals.Api.Entities
{
    public record class IndividualEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public Sex Sex { get; set; }
        public required string PersonalNumber { get; set; }
        public DateOnly BirthDate { get; set; }
        public int? CityId { get; set; }
        public List<PhoneNumbersEntity> PhoneNumbers { get; set; } = new List<PhoneNumbersEntity>();
        public string? ImageAdress { get; set; }
        public List<ConnectedIndividualsEntity>? ConnectionPersonsA { get; set; } = new List<ConnectedIndividualsEntity>();
        public List<ConnectedIndividualsEntity>? ConnectionPersonsB { get; set; } = new List<ConnectedIndividualsEntity>();
    }
}
