using DirectoryOfIndividuals.Api.Components;
using DirectoryOfIndividuals.Api.Dtos;
using DirectoryOfIndividuals.Api.Entities;
using Microsoft.OpenApi.Extensions;

namespace DirectoryOfIndividuals.Api.Mapping
{
    public static class IndividualsMapping
    {
        // from Entity
        public static IndividualDto ToIndDto(this IndividualEntity individual)
        {
            IndividualDto newDto = new IndividualDto
            {
                Id = individual.Id,
                Name = individual.Name,
                LastName = individual.LastName,
                Sex = Enum.GetName(individual.Sex),
                PersonalNumber = individual.PersonalNumber,
                BirthDate = individual.BirthDate,
                CityId = individual.CityId,
                ImageAdress = individual.ImageAdress,

                PhoneNumbers = individual.PhoneNumbers?.Select(phoneNum => 
                    phoneNum.ToPhoneNumbersDto()).ToList()
            };

            if (individual.ConnectionPersonsA != null && individual.ConnectionPersonsB != null)
                newDto.ConnectedPersons = individual.ConnectionPersonsA.Concat(individual.ConnectionPersonsB).Select(connection => 
                    connection.ToConnectionDto(individual.Id)).ToList();
            else if (individual.ConnectionPersonsA is null || individual.ConnectionPersonsB != null)
                newDto.ConnectedPersons = individual.ConnectionPersonsB!.Select(connection =>
                    connection.ToConnectionDto(individual.Id)).ToList();
            else if (individual.ConnectionPersonsB is null)
                newDto.ConnectedPersons = individual.ConnectionPersonsA.Select(connection =>
                    connection.ToConnectionDto(individual.Id)).ToList();

                return newDto;
        }

        // from CreateDTO
        public static IndividualEntity ToIndEntity(this CreateIndividualDto individual)
        {
            return new IndividualEntity
            {
                Name = individual.Name,
                LastName = individual.LastName,
                Sex = individual.Sex,
                PersonalNumber = individual.PersonalNumber,
                BirthDate = individual.BirthDate,
                CityId = individual.CityId,

                PhoneNumbers = individual.PhoneNumbers?.Select(number => number.ToPhoneNumberComponent()).ToList()!,

                ImageAdress = individual.ImageAdress,

                ConnectionPersonsA = individual.ConnectedPersons?.Select(person => person.ToConnectionEntity()).ToList()!
            };
        }

        //from Entity
        public static ConnectionReportDto ToConnectionReportDto(this IndividualEntity individual)
        {
            ConnectionReportDto connReportDto = new ConnectionReportDto
            {
                Id = individual.Id,
                FullName = individual.Name + " " + individual.LastName,
                Colleague = 0,
                Friend = 0,
                Relative = 0,
                Other = 0
            };

            List<ConnectedIndividualsEntity> individualsConnetions = new List<ConnectedIndividualsEntity>();

            if (individual.ConnectionPersonsA is null && individual.ConnectionPersonsB is null)
                return connReportDto;
            else if (individual.ConnectionPersonsA != null && individual.ConnectionPersonsB is null)
                individualsConnetions = individual.ConnectionPersonsA;
            else if (individual.ConnectionPersonsA is null && individual.ConnectionPersonsB != null)
                individualsConnetions = individual.ConnectionPersonsB;
            else
                individualsConnetions = individual.ConnectionPersonsA!.Concat(individual.ConnectionPersonsB!).ToList();

            connReportDto.Colleague = individualsConnetions.Count(connection => connection.ConnectionKind.GetDisplayName() == "Colleague");

            connReportDto.Friend = individualsConnetions.Count(connection => connection.ConnectionKind.GetDisplayName() == "Friend");

            connReportDto.Relative = individualsConnetions.Count(connection => connection.ConnectionKind.GetDisplayName() == "Relative");

            connReportDto.Other = individualsConnetions.Count(connection => connection.ConnectionKind.GetDisplayName() == "Other");

            return connReportDto;
        }

        //for update Validation
        public static UpdateIndividualsInfoDtoValidations ToUpdateValidations(this UpdateIndInfoDto updateIndDto)
        {
            return new UpdateIndividualsInfoDtoValidations
            {
                Name = updateIndDto.Name,
                LastName = updateIndDto.LastName,
                Sex = updateIndDto.Sex,
                PersonalNumber = updateIndDto.PersonalNumber,
                BirthDate = updateIndDto.BirthDate,
                CityId = updateIndDto.CityId,
                PhoneNumbers = updateIndDto.PhoneNumbers
            };
        }
    }
}
