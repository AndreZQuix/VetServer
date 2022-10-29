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

        public Client GetClient(int clientId)
        {
            return appDbContext.Client.FirstOrDefault(c => c.Id == clientId);
        }

        public Client GetClientByUsername(string username)
        {
            return appDbContext.Client.FirstOrDefault(c => c.Username == username);
        }

        public Pet GetClientPet(int clientId, int petId)
        {
            return appDbContext.Pet.FirstOrDefault(p => p.Id == petId && p.ClientId == clientId);
        }

        public IEnumerable<Pet> GetClientPets(int clientId)
        {
            return appDbContext.Pet.Where(p => p.ClientId == clientId);
        }

        public Client CreateClient(Client client)
        {
            var result = appDbContext.Client.Add(client);
            appDbContext.SaveChanges();
            return result.Entity;
        }

        public Client UpdateClient(Client client)
        {
            var result = appDbContext.Client.FirstOrDefault(c => c.Id == client.Id);

            if (result != null)
            {
                appDbContext.Entry(client).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                appDbContext.SaveChanges();
                return result;
            }
            return null;
        }
    }
}
