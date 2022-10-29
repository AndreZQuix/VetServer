using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            try
            {
                return Ok(await petParamsRepository.GetPetParamsPerHour(petId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{petId}/lastday")]
        public async Task<IActionResult> GetPetParamsPerDay(int petId)
        {
            try
            {
                return Ok(await petParamsRepository.GetPetParamsPerDay(petId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{petId}/lastweek")]
        public async Task<IActionResult> GetPetParamsPerWeek(int petId)
        {
            try
            {
                return Ok(await petParamsRepository.GetPetParamsPerWeek(petId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{petId}/lastmonth")]
        public async Task<IActionResult> GetPetParamsPerMonth(int petId)
        {
            try
            {
                return Ok(await petParamsRepository.GetPetParamsPerMonth(petId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{petId}/{startDate}/{endDate}")]
        public async Task<IActionResult> GetPetParamsPerTimePeriod(int petId, DateTime startDate, DateTime endDate)
        {
            try
            {
                return Ok(await petParamsRepository.GetPetParamsPerTimePeriod(petId, startDate, endDate));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePetParams(PetParameters petParams)
        {
            try
            {
                if (petParams == null)
                    return BadRequest();

                var createdPetParams = await petParamsRepository.CreatePetParameters(petParams);
                return CreatedAtAction(nameof(CreatePetParams), new { id = createdPetParams.Id }, createdPetParams);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
