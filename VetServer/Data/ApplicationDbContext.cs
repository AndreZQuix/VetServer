using Microsoft.EntityFrameworkCore;
using System;
using VetServer.Models;

namespace VetServer.Data
{
    public class ApplicationDbContext : DbContext                                          
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var petParams = modelBuilder.Entity<PetParameters>()
                .OwnsOne(x => x.Pressure);

            petParams.Property(x => x.LowPressure)
                .HasColumnName(nameof(BloodPressure.LowPressure));

            petParams.Property(x => x.TopPressure)
                .HasColumnName(nameof(BloodPressure.TopPressure));
        }

        public DbSet<Pet> Pet { get; set; }
        public DbSet<PetParameters> PetParameters { get; set; }
        public DbSet<Client> Client { get; set; }
    }
}
