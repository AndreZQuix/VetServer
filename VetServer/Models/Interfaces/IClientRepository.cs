namespace VetServer.Models.Interfaces
{
    public interface IClientRepository
    {
        public Client GetClient(int clientId);
        public Client GetClientByUsername(string username);
        public Pet GetClientPet(int clientId, int petId);
        public IEnumerable<Pet> GetClientPets(int clientId);
        public Client CreateClient(Client client);
        public Client UpdateClient(Client client);
    }
}
