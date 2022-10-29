
namespace VetServer.Models.Interfaces
{
    public interface IPetParametersRepository
    {
        IEnumerable<PetParameters> GetPetParamsPerHour(int petId);
        IEnumerable<PetParameters> GetPetParamsPerDay(int petId);
        IEnumerable<PetParameters> GetPetParamsPerWeek(int petId);
        IEnumerable<PetParameters> GetPetParamsPerMonth(int petId);
        IEnumerable<PetParameters> GetPetParamsPerTimePeriod(int petId, DateTime startDate, DateTime endDate);
        PetParameters CreatePetParameters(PetParameters petParams);
    }
}
