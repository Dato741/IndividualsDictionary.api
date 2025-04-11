using DirectoryOfIndividuals.Api.Components;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class CreateConnectedIndSeparate
    {
        public int PersonAInd { get; set; }
        public int PersonBInd { get; set; }
        public ConnectionType ConnectionKind { get; set; }
    }
}
