using DirectoryOfIndividuals.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DirectoryOfIndividuals.Api.Data
{
    public class IndividualsDirectoryDbContext : DbContext
    {
        public IndividualsDirectoryDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<IndividualEntity> Individuals { get; set; }
        public DbSet<CitiesEntity> Cities { get; set; }
        public DbSet<PhoneNumbersEntity> PhoneNumbers { get; set; }
        public DbSet<ConnectedIndividualsEntity> ConnectedIndividuals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ConnectedIndividualsEntity>()
                .HasOne(c => c.PersonA)
                .WithMany(i => i.ConnectionPersonsA)
                .HasForeignKey(c => c.PersonAInd)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ConnectedIndividualsEntity>()
                .HasOne(c => c.PersonB)
                .WithMany(i => i.ConnectionPersonsB)
                .HasForeignKey(c => c.PersonBInd)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
