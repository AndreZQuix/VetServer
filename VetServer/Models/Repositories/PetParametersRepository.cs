using Microsoft.EntityFrameworkCore;
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

        public async Task<PetParameters> CreatePetParameters(PetParameters petParams)
        {
            var result = await appDbContext.PetParameters.AddAsync(petParams);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<PetParameters>> GetPetParamsPerHour(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime hourEarlier = currentTime.AddHours(-1);
            return await GetPetParamsPerTimePeriod(petId, hourEarlier, currentTime);
        }

        public async Task<IEnumerable<PetParameters>> GetPetParamsPerDay(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime dayEarlier = currentTime.AddHours(-24);
            return await GetPetParamsPerTimePeriod(petId, dayEarlier, currentTime);
        }

        public async Task<IEnumerable<PetParameters>> GetPetParamsPerWeek(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime weekEarlier = currentTime.AddDays(-7);
            return await GetPetParamsPerTimePeriod(petId, weekEarlier, currentTime);
        }
        public async Task<IEnumerable<PetParameters>> GetPetParamsPerMonth(int petId)
        {
            DateTime currentTime = DateTime.Now;
            DateTime monthEarlier = currentTime.AddMonths(-1);
            return await GetPetParamsPerTimePeriod(petId, monthEarlier, currentTime);
        }

        public async Task<IEnumerable<PetParameters>> GetPetParamsPerTimePeriod(int petId, DateTime startDate, DateTime endDate)
        {
            return await appDbContext.PetParameters.Where(p => p.PetId == petId
            && p.CreatedDateTime >= startDate && p.CreatedDateTime <= endDate).ToListAsync();
        }
    }
}
