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

        public Task<ClientModelGET> ShowClient(int clientId);
        public Task<IEnumerable<ClientModelGET>> ShowClients();
        public Task<ClientModelGET> ShowClientByUsername(string username);
        public ClientModelGET ConvertToClientDataModelGET(Client client);
    }
}
