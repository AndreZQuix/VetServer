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

        public Pet GetPet(int petId)
        {
            return appDbContext.Pet.FirstOrDefault(p => p.Id == petId);
        }

        public IEnumerable<Pet> GetPets()
        {
            return appDbContext.Pet.ToList();
        }

        public Pet CreatePet(Pet pet)
        {
            var result = appDbContext.Pet.Add(pet);
            appDbContext.SaveChanges();
            return result.Entity;
        }

        public Pet UpdatePet(Pet pet)
        {
            var result = appDbContext.Pet.FirstOrDefault(p => p.Id == pet.Id);

            if (result != null)
            {
                appDbContext.Entry(pet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appDbContext.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
