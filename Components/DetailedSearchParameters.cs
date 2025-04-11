using DirectoryOfIndividuals.Api.Entities;

namespace DirectoryOfIndividuals.Api.Components
{
    public class DetailedSearchParameters
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public Sex? Sex { get; set; }
        public string? PersonalNumber { get; set; }
        public DateOnly? BirthDate { get; set; }
        public int? CityId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageAdress { get; set; }
    }
}
