using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using VetServer.Data;
using VetServer.Models.Interfaces;

namespace VetServer.Models.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly ApplicationDbContext appDbContext;

        public PetRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Pet> GetPet(int petId)
        {
            return await appDbContext.Pet.FirstOrDefaultAsync(p => p.Id == petId);
        }

        public async Task<IEnumerable<Pet>> GetPets()
        {
            return await appDbContext.Pet.ToListAsync();
        }

        public async Task<Pet> CreatePet(Pet pet)
        {
            var result = await appDbContext.Pet.AddAsync(pet);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Pet> UpdatePet(Pet pet)
        {
            var result = await appDbContext.Pet.FirstOrDefaultAsync(p => p.Id == pet.Id);

            if (result != null)
            {
                appDbContext.Entry(pet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
