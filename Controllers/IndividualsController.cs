using DirectoryOfIndividuals.Api.Components;
using DirectoryOfIndividuals.Api.Dtos;
using DirectoryOfIndividuals.Api.Entities;
using DirectoryOfIndividuals.Api.Mapping;
using DirectoryOfIndividuals.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace DirectoryOfIndividuals.Api.Controllers
{
    [ApiController]
    [Route("api/individuals")]
    public class IndividualsController : ControllerBase
    {
        private readonly IIndividualsService _indService;

        public IndividualsController(IIndividualsService service)
        {
            _indService = service;
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetIndsFast([FromQuery] FastSearchParameters searchParams, int pageNumber = 1, int pageSize = 10)
        {
            List<IndividualEntity> indEntities = await _indService.GetAllFast(searchParams, pageNumber, pageSize);

            List<IndividualDto> indDtos = indEntities.Select(ind => ind.ToIndDto()).ToList();

            return Ok(indDtos);
        }

        [HttpGet("search/detailed")]
        public async Task<IActionResult> GetIndsDetailed([FromQuery] DetailedSearchParameters detailedSearchParams, int pageNumber = 1, int pageSize = 10)
        {
            List<IndividualEntity> indEntities = await _indService.GetAllDetailed(detailedSearchParams, pageNumber, pageSize);

            List<IndividualDto> indDtos = indEntities.Select(ind => ind.ToIndDto()).ToList();

            return Ok(indDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleInd([FromRoute] int id)
        {
            IndividualEntity? ind = await _indService.GetByIdAsync(id);

            if (ind is null) return NotFound();

            return Ok(ind.ToIndDto());
        }

        [HttpPost]
        public async Task<IActionResult> CreateInd([FromBody] CreateIndividualDto createdIndDto)
        {
            IndividualEntity indEntity = createdIndDto.ToIndEntity();

            if (createdIndDto.ConnectedPersons is not null)
            {
                foreach (var connection in createdIndDto.ConnectedPersons)
                {
                    var connectedInd = _indService.GetByIdAsync(connection.ConnectedToId);

                    if (connectedInd is null)
                        return NotFound($"Individual not found with id {connection.ConnectedToId} listed in connectedPersons >> connectedToInd");
                }
            }

            await _indService.CreateIndividualAsync(indEntity);

            return Ok(indEntity.ToIndDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInd([FromRoute]int id, [FromBody] UpdateIndInfoDto updatedInd)
        {
            IndividualEntity? existingInd = await _indService.GetByIdAsync(id);

            if (existingInd is null) return NotFound();

            if (!string.IsNullOrWhiteSpace(updatedInd.Name)) 
                existingInd.Name = updatedInd.Name!;

            if (!string.IsNullOrWhiteSpace(updatedInd.LastName)) 
                existingInd.LastName = updatedInd.LastName!;

            if (!string.IsNullOrWhiteSpace(updatedInd.Sex)) 
                existingInd.Sex = Enum.Parse<Sex>(updatedInd.Sex!);

            if (!string.IsNullOrWhiteSpace(updatedInd.PersonalNumber))
                existingInd.PersonalNumber = updatedInd.PersonalNumber!;

            if (updatedInd.BirthDate is not null)
                existingInd.BirthDate = DateOnly.Parse(updatedInd.BirthDate.ToString()!);

            if (updatedInd.CityId is not null)
                existingInd.CityId = updatedInd.CityId!;

            if (updatedInd.PhoneNumbers != null && existingInd.PhoneNumbers != null)
            {
                var updatedDict = updatedInd.PhoneNumbers.ToDictionary(n => n.PhoneNumberId);

                foreach (var existingNum in existingInd.PhoneNumbers)
                {
                    if (updatedDict.TryGetValue(existingNum.PhoneNumberId, out var updatedNum))
                    {
                        existingNum.Number = updatedNum.Number;
                        existingNum.NumberType = updatedNum.NumberType;
                    }
                }
            }

            await _indService.UpdateIndividualAsync(id, existingInd);

            return NoContent();
        }

        [HttpPost("connection")]
        public async Task<IActionResult> AddConnectedInd([FromBody] CreateConnectedIndSeparate newConnection)
        {
            ConnectedIndividualsEntity? connection = await _indService.AddConnectionAsync(newConnection.ToConnectionEntity());

            if (connection is null) return Conflict("Connection already exists");

            return Created();
        }

        [HttpDelete("connection/{id}")]
        public async Task<IActionResult> RemoveConnection([FromRoute] int id)
        {
            await _indService.RemoveConnectedIndividualAsync(id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveIndividual([FromRoute] int id)
        {
            await _indService.DeleteIndividualAsync(id);

            return NoContent();
        }

        [HttpGet("report/connections")]
        public async Task<IActionResult> GetIndConnectionReport()
        {
            List<IndividualEntity> individuals = await _indService.GetConnectionsReport();
            
            List<ConnectionReportDto> connectionsReport = individuals.Select(ind => ind.ToConnectionReportDto()).ToList();

            return Ok(connectionsReport);
        }
    }
}
