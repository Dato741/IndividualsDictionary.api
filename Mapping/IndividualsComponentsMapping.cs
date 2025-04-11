using DirectoryOfIndividuals.Api.Dtos;
using DirectoryOfIndividuals.Api.Entities;

namespace DirectoryOfIndividuals.Api.Mapping
{
    public static class IndividualsComponentsMapping
    {
        public static PhoneNumbersEntity ToPhoneNumberComponent(this CreatePhoneNumberDto phoneNumberDto)
        {
            return new PhoneNumbersEntity
            {
                Number = phoneNumberDto.Number,
                NumberType = phoneNumberDto.NumberType,
            };
        }

        public static PhoneNumbersDto ToPhoneNumbersDto(this PhoneNumbersEntity phoneNumber)
        {
            return new PhoneNumbersDto
            {
                PhoneNumberId = phoneNumber.PhoneNumberId,
                NumberType = Enum.GetName(phoneNumber.NumberType)!,
                Number = phoneNumber.Number,
            };
        }

        public static ConnectedIndividualsEntity ToConnectionEntity(this CreateConnectedIndividualDto connectedIndDto)
        {
            return new ConnectedIndividualsEntity
            {
                ConnectionKind = connectedIndDto.ConnectionKind,
                PersonBInd = connectedIndDto.ConnectedToId
            };
        }

        public static ConnectedIndividualsEntity ToConnectionEntity(this CreateConnectedIndSeparate connectionDto)
        {
            return new ConnectedIndividualsEntity
            {
                ConnectionKind = connectionDto.ConnectionKind,
                PersonAInd = connectionDto.PersonAInd,
                PersonBInd = connectionDto.PersonBInd
            };
        }

        public static ConnectedIndDto ToConnectionDto(this ConnectedIndividualsEntity connectedInd, int id)
        {
            return new ConnectedIndDto
            {
                ConnectionId = connectedInd.ConnectionId,
                ConnectionKind = Enum.GetName(connectedInd.ConnectionKind)!,
                ConnectedToIndId = connectedInd.PersonAInd != id ? connectedInd.PersonAInd : connectedInd.PersonBInd
            };
        }

        public static ConnectedIndDto ToConnectedIndDto(this CreateConnectedIndividualDto connectedInd)
        {
            return new ConnectedIndDto
            {
                ConnectedToIndId = connectedInd.ConnectedToId,
                ConnectionKind = Enum.GetName(connectedInd.ConnectionKind)!
            };
        }
    }
}
