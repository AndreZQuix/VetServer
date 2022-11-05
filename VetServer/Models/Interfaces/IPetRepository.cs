namespace VetServer.Models.Interfaces
{
    public interface IPetRepository
    {
        public Task<Pet> GetPet(int petId);
        public Task<IEnumerable<Pet>> GetPets();
        public Task<Pet> CreatePet(Pet pet);

        public Task<Pet> UpdatePet(int petId, Pet pet);
    }
}
