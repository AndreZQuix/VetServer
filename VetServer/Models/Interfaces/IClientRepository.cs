namespace VetServer.Models.Interfaces
{
    public interface IClientRepository
    {
        public Task<Client> GetClient(int clientId);
        public Task<IEnumerable<Client>> GetClients();
        public Task<Client> GetClientByUsername(string username);
        public Task<Pet> GetClientPet(int clientId, int petId);
        public Task<IEnumerable<Pet>> GetClientPets(int clientId);
        public Task<Client> CreateClient(Client client);
        public Task<Client> UpdateClientFull(int clientId, Client client);
        public Task<Client> ClientLogIn(string username, string password);
    }
}
