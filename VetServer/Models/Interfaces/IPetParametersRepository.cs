
namespace VetServer.Models.Interfaces
{
    public interface IPetParametersRepository
    {
        Task<IEnumerable<PetParameters>> GetPetParamsPerHour(int petId);
        Task<IEnumerable<PetParameters>> GetPetParamsPerDay(int petId);
        Task<IEnumerable<PetParameters>> GetPetParamsPerWeek(int petId);
        Task<IEnumerable<PetParameters>> GetPetParamsPerMonth(int petId);
        Task<IEnumerable<PetParameters>> GetPetParamsPerTimePeriod(int petId, DateTime startDate, DateTime endDate);
        Task<PetParameters> CreatePetParameters(PetParameters petParams);
    }
}
