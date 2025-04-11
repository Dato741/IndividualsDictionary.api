namespace DirectoryOfIndividuals.Api.Dtos
{
    public class ConnectionReportDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;

        //Connections count by type
        public int? Colleague { get; set; }
        public int? Friend { get; set; }
        public int? Relative { get; set; }
        public int? Other { get; set; }

    }
}
