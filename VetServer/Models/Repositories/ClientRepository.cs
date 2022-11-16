using Microsoft.EntityFrameworkCore;
using VetServer.Data;
using VetServer.Models.Interfaces;
using VetServer.Utils;

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

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await appDbContext.Client.ToListAsync();
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
            client.Salt = HashData.GetSalt();
            client.Password = HashData.HashString(client.Password, client.Salt);
            var result = await appDbContext.Client.AddAsync(client);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> UpdateClientFull(int clientId, Client client)
        {
            var result = await appDbContext.Client.FirstOrDefaultAsync(c => c.Id == clientId);

            if (result != null)
            {
                client.Id = clientId;
                client.Password = HashData.HashString(client.Password, client.Salt);
                appDbContext.Entry(result).CurrentValues.SetValues(client);
                await appDbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Client> ClientLogIn(string username, string password)
        {
            var client = await GetClientByUsername(username);
            if (client.Password == HashData.HashString(password, client.Salt))
                return client;
            else
                return null;
        }
    }
}
