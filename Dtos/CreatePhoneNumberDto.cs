using DirectoryOfIndividuals.Api.Components;
using System.ComponentModel.DataAnnotations;

namespace DirectoryOfIndividuals.Api.Dtos
{
    public class CreatePhoneNumberDto
    {
        public NumberCategory NumberType { get; set; }

        [Length(4, 50)]
        public string Number { get; set; } = string.Empty;
    }
}
