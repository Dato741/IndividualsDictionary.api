using DirectoryOfIndividuals.Api.Components;
using DirectoryOfIndividuals.Api.Data;
using DirectoryOfIndividuals.Api.Dtos;
using DirectoryOfIndividuals.Api.Entities;
using DirectoryOfIndividuals.Api.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.VisualBasic;
using System.Data;
using System.Linq;

namespace DirectoryOfIndividuals.Api.Services
{
    public class IndividualsService : IIndividualsService
    {
        private readonly IndividualsDirectoryDbContext _context;

        public IndividualsService(IndividualsDirectoryDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<IndividualEntity>> GetAllFast(FastSearchParameters searchParams, int pageNumber, int pageSize)
        {
            IQueryable<IndividualEntity>? searchQuery = _context.Individuals.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchParams.Name))
                searchQuery = searchQuery.Where(ind => ind.Name.ToLower().Contains(searchParams.Name.ToLower()));

            if (!string.IsNullOrWhiteSpace(searchParams.LastName))
                searchQuery = searchQuery.Where(ind => ind.LastName.ToLower().Contains(searchParams.LastName.ToLower()));

            if (!string.IsNullOrWhiteSpace(searchParams.PersonalNumber))
                searchQuery = searchQuery.Where(ind => ind.PersonalNumber.Contains(searchParams.PersonalNumber));

            searchQuery = searchQuery.Include(ind => ind.PhoneNumbers)
                                     .Include(ind => ind.ConnectionPersonsA)
                                     .Include(ind => ind.ConnectionPersonsB)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize);

            List<IndividualEntity> individuals = await searchQuery.ToListAsync();

            return individuals;
        }

        public async Task<List<IndividualEntity>> GetAllDetailed(DetailedSearchParameters detailedSearchParams, int pageNumber, int pageSize)
        {
            IQueryable<IndividualEntity>? searchQuery = _context.Individuals.AsQueryable();

            if (!string.IsNullOrWhiteSpace(detailedSearchParams.Name))
                searchQuery = searchQuery.Where(ind => ind.Name.ToLower() == detailedSearchParams.Name.ToLower());

            if (!string.IsNullOrWhiteSpace(detailedSearchParams.LastName))
                searchQuery = searchQuery.Where(ind => ind.LastName.ToLower() == detailedSearchParams.LastName.ToLower());

            if (detailedSearchParams.Sex != null)
                searchQuery = searchQuery.Where(ind => ind.Sex == detailedSearchParams.Sex);

            if (!string.IsNullOrWhiteSpace(detailedSearchParams.PersonalNumber))
                searchQuery = searchQuery.Where(ind => ind.PersonalNumber == detailedSearchParams.PersonalNumber);

            if (detailedSearchParams.BirthDate != null)
                searchQuery = searchQuery.Where(ind => ind.BirthDate == detailedSearchParams.BirthDate);

            if (detailedSearchParams.CityId != null)
                searchQuery = searchQuery.Where(ind => ind.CityId == detailedSearchParams.CityId);

            searchQuery = searchQuery.Include(ind => ind.PhoneNumbers)
                                     .Include(ind => ind.ConnectionPersonsA)
                                     .Include(ind => ind.ConnectionPersonsB)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize);

            List<IndividualEntity> individuals = await searchQuery.ToListAsync();

            return individuals;
        }

        public async Task<IndividualEntity?> GetByIdAsync(int id)
        {
            IndividualEntity? individual = await _context.Individuals
                                                    .Where(ind => ind.Id == id)
                                                    .Include(ind => ind.PhoneNumbers)
                                                    .Include(ind => ind.ConnectionPersonsA)
                                                    .Include(ind => ind.ConnectionPersonsB)
                                                    .FirstOrDefaultAsync();

            return individual;
        }

        public async Task<List<IndividualEntity>> GetConnectionsReport()
        {
            return await _context.Individuals
                                            .Include(ind => ind.ConnectionPersonsA)
                                            .Include(ind => ind.ConnectionPersonsB)
                                            .ToListAsync();
        }

        public async Task CreateIndividualAsync(IndividualEntity indEntity)
        {
            bool exists = await _context.Individuals.AnyAsync(ind => ind.PersonalNumber == indEntity.PersonalNumber);
            if (exists) throw new Exception("Individual already exists");

            await _context.Individuals.AddAsync(indEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateIndividualAsync(int id, IndividualEntity indEntity)
        {
            IndividualEntity existingInd = await _context.Individuals.FindAsync(id)
                ?? throw new Exception($"Individual with ID {id} not found.");

            _context.Individuals.Entry(existingInd).CurrentValues.SetValues(indEntity);
            foreach (var number in existingInd.PhoneNumbers)
            {
                var existingNumber = await _context.PhoneNumbers.FindAsync(number.PhoneNumberId);

                if (existingNumber != null)
                    _context.PhoneNumbers.Entry(existingNumber).CurrentValues.SetValues(number);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveConnectedIndividualAsync(int connectionId)
        {
            await _context.ConnectedIndividuals.Where(ind => ind.ConnectionId == connectionId).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIndividualAsync(int id)
        {
            await _context.ConnectedIndividuals.Where(c => c.PersonAInd == id || c.PersonBInd == id).ExecuteDeleteAsync();

            await _context.PhoneNumbers.Where(phoneNum => phoneNum.NumberOwnerId == id).ExecuteDeleteAsync();

            await _context.Individuals.Where(ind => ind.Id == id).ExecuteDeleteAsync();
        }

        public async Task<ConnectedIndividualsEntity?> AddConnectionAsync(ConnectedIndividualsEntity connection)
        {
            IQueryable<ConnectedIndividualsEntity> existingConnection = _context.ConnectedIndividuals.AsQueryable();

            existingConnection = existingConnection.Where(c => 
            (c.PersonAInd == connection.PersonAInd && c.PersonBInd == connection.PersonBInd) ||
            (c.PersonAInd == connection.PersonBInd && c.PersonBInd == connection.PersonAInd));

            if (await existingConnection.AnyAsync()) return null;

            await _context.ConnectedIndividuals.AddAsync(connection);
            await _context.SaveChangesAsync();

            return connection;
        }
    }
}
