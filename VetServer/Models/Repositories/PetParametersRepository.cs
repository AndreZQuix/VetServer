using VetServer.Data;
using VetServer.Models.Interfaces;

namespace VetServer.Models.Repositories
{
    public class PetParametersRepository : IPetParametersRepository
    {
        private readonly ApplicationDbContext appDbContext;

        public PetParametersRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public PetParameters CreatePetParameters(PetParameters petParams)
        {
            var result = appDbContext.PetParameters.Add(petParams);
            appDbContext.SaveChanges();
            return result.Entity;
        }

        public IEnumerable<PetParameters> GetPetParamsPerHour(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime hourEarlier = currentTime.AddHours(-1);
            return GetPetParamsPerTimePeriod(petId, hourEarlier, currentTime);
        }

        public IEnumerable<PetParameters> GetPetParamsPerDay(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime dayEarlier = currentTime.AddHours(-24);
            return GetPetParamsPerTimePeriod(petId, dayEarlier, currentTime);
        }

        public IEnumerable<PetParameters> GetPetParamsPerWeek(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime weekEarlier = currentTime.AddDays(-7);
            return GetPetParamsPerTimePeriod(petId, weekEarlier, currentTime);
        }
        public IEnumerable<PetParameters> GetPetParamsPerMonth(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime monthEarlier = currentTime.AddMonths(-1);
            return GetPetParamsPerTimePeriod(petId, monthEarlier, currentTime);
        }

        public IEnumerable<PetParameters> GetPetParamsPerTimePeriod(int petId, DateTime startDate, DateTime endDate)
        {
            return appDbContext.PetParameters.Where(p => p.PetId == petId
            && p.CreatedDateTime >= startDate && p.CreatedDateTime <= endDate);
        }
    }
}
