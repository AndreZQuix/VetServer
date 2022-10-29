namespace VetServer.Models.Interfaces
{
    public interface IPetRepository
    {
        public Pet GetPet(int petId);
        public IEnumerable<Pet> GetPets();
        public Pet CreatePet(Pet pet);

        public Pet UpdatePet(Pet pet);
    }
}
