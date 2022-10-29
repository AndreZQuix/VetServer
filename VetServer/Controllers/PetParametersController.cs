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
        public IActionResult GetPetParamsPerHour(int petId)
        {
            return Ok(petParamsRepository.GetPetParamsPerHour(petId));
        }

        [HttpGet("{petId}/lastday")]
        public IActionResult GetPetParamsPerDay(int petId)
        {
            return Ok(petParamsRepository.GetPetParamsPerDay(petId));
        }

        [HttpGet("{petId}/lastweek")]
        public IActionResult GetPetParamsPerWeek(int petId)
        {
            return Ok(petParamsRepository.GetPetParamsPerWeek(petId));
        }

        [HttpGet("{petId}/lastmonth")]
        public IActionResult GetPetParamsPerMonth(int petId)
        {
            return Ok(petParamsRepository.GetPetParamsPerMonth(petId));
        }

        [HttpGet("{petId}/{startDate}/{endDate}")]
        public IActionResult GetPetParamsPerTimePeriod(int petId, DateTime startDate, DateTime endDate)
        {
            return Ok(petParamsRepository.GetPetParamsPerTimePeriod(petId, startDate, endDate));
        }

        [HttpPost]
        public IActionResult CreatePetParams(PetParameters petParams)
        {
            if (petParams == null)
                return BadRequest();

            var createdPetParams = petParamsRepository.CreatePetParameters(petParams);
            return CreatedAtAction(nameof(CreatePetParams), new { id = createdPetParams.Id }, createdPetParams);
        }
    }
}
