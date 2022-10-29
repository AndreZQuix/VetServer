using Microsoft.EntityFrameworkCore;
using VetServer.Data;
using VetServer.Models.Interfaces;

namespace VetServer.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext appDbContext;

        public ClientRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Client> GetClient(int clientId)
        {
            return await appDbContext.Client.FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<Client> GetClientByUsername(string username)
        {
            return await appDbContext.Client.FirstOrDefaultAsync(c => c.Username == username);
        }

        public async Task<Pet> GetClientPet(int clientId, int petId)
        {
            return await appDbContext.Pet.FirstOrDefaultAsync(p => p.Id == petId && p.ClientId == clientId);
        }

        public async Task<IEnumerable<Pet>> GetClientPets(int clientId)
        {
            return await appDbContext.Pet.Where(p => p.ClientId == clientId).ToListAsync();
        }

        public async Task<Client> CreateClient(Client client)
        {
            var result = await appDbContext.Client.AddAsync(client);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> UpdateClient(Client client)
        {
            var result = await appDbContext.Client.FirstOrDefaultAsync(c => c.Id == client.Id);

            if (result != null)
            {
                appDbContext.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
