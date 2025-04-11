using DirectoryOfIndividuals.Api.Components;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class ConnectedIndDto
    {
        public int ConnectionId { get; set; }
        public string ConnectionKind { get; set; } = string.Empty;
        public int ConnectedToIndId { get; set; }
    }
}
