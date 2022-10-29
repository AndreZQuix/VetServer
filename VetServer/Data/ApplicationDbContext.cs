using Microsoft.EntityFrameworkCore;
using VetServer.Models;

namespace VetServer.Data
{
    public class ApplicationDbContext : DbContext                                          
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Pet> Pet { get; set; }
        public DbSet<PetParameters> PetParameters { get; set; }
        public DbSet<Client> Client { get; set; }
    }
}
