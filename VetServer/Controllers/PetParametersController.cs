using Microsoft.AspNetCore.Mvc;
using VetServer.Models;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;

namespace VetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetParametersController : Controller
    {
        private readonly IPetParametersRepository petParamsRepository;

        public PetParametersController(IPetParametersRepository petParamsRepository)
        {
            this.petParamsRepository = petParamsRepository;
        }

        [HttpGet("{petId}/lasthour")]
        public async Task<IActionResult> GetPetParamsPerHour(int petId)
        {
            return Ok(await petParamsRepository.GetPetParamsPerHour(petId));
        }

        [HttpGet("{petId}/lastday")]
        public async Task<IActionResult> GetPetParamsPerDay(int petId)
        {
            return Ok(await petParamsRepository.GetPetParamsPerDay(petId));
        }

        [HttpGet("{petId}/lastweek")]
        public async Task<IActionResult> GetPetParamsPerWeek(int petId)
        {
            return Ok(await petParamsRepository.GetPetParamsPerWeek(petId));
        }

        [HttpGet("{petId}/lastmonth")]
        public async Task<IActionResult> GetPetParamsPerMonth(int petId)
        {
            return Ok(await petParamsRepository.GetPetParamsPerMonth(petId));
        }

        [HttpGet("{petId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetPetParamsPerTimePeriod(int petId, DateTime startDate, DateTime endDate)
        {
            return Ok(await petParamsRepository.GetPetParamsPerTimePeriod(petId, startDate, endDate));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePetParams(PetParameters petParams)
        {
            if (petParams == null)
                return BadRequest();

            var createdPetParams = await petParamsRepository.CreatePetParameters(petParams);
            return CreatedAtAction(nameof(CreatePetParams), new { id = createdPetParams.Id }, createdPetParams);
        }
    }
}
