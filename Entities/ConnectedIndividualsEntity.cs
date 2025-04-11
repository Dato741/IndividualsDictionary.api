using DirectoryOfIndividuals.Api.Components;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DirectoryOfIndividuals.Api.Entities
{
    public record class ConnectedIndividualsEntity
    {
        [Key]
        public int ConnectionId { get; set; }
        public ConnectionType ConnectionKind { get; set; }
        public int PersonAInd { get; set; }

        [JsonIgnore]
        public IndividualEntity? PersonA { get; set; }

        public int PersonBInd { get; set; }
        [JsonIgnore]
        public IndividualEntity? PersonB { get; set; }
    }
}
