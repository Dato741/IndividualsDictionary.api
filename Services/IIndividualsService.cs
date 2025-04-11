using DirectoryOfIndividuals.Api.Components;
using DirectoryOfIndividuals.Api.Dtos;
using DirectoryOfIndividuals.Api.Entities;

namespace DirectoryOfIndividuals.Api.Services
{
    public interface IIndividualsService
    {
        public Task<List<IndividualEntity>> GetAllFast(FastSearchParameters searchParams, int pageNumber, int pageSize);
        public Task<List<IndividualEntity>> GetAllDetailed(DetailedSearchParameters detailedSearchParams, int pageNumber, int pageSize);
        public Task<IndividualEntity?> GetByIdAsync(int id);
        public Task<List<IndividualEntity>> GetConnectionsReport();
        public Task CreateIndividualAsync(IndividualEntity indEntity);
        public Task UpdateIndividualAsync(int id, IndividualEntity indEntity);
        public Task<ConnectedIndividualsEntity?> AddConnectionAsync(ConnectedIndividualsEntity connectedInd);
        public Task RemoveConnectedIndividualAsync(int connectionId);
        public Task DeleteIndividualAsync(int id);
    }
}
