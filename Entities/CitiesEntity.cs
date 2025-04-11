namespace DirectoryOfIndividuals.Api.Entities
{
    public record class CitiesEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
