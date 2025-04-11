using DirectoryOfIndividuals.Api.Components;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class CreateConnectedIndividualDto
    {
        public int ConnectedToId { get; set; }
        public ConnectionType ConnectionKind { get; set; }
    }
}
