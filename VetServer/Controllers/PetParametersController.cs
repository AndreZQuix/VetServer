using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VetServer.Models;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;

namespace VetServer.Controllers
{
    /// <summary>
    /// Controller for pet vitals data processing.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PetParametersController : Controller
    {
        private readonly IPetParametersRepository petParamsRepository;

        public PetParametersController(IPetParametersRepository petParamsRepository)
        {
            this.petParamsRepository = petParamsRepository;
        }

        /// <summary>
        /// Get pet vitals data record per hour.
        /// </summary>
        [HttpGet("{petId}/hour")]
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

        /// <summary>
        /// Get pet vitals data record per day.
        /// </summary>
        /// 
        [HttpGet("{petId}/day")]
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

        /// <summary>
        /// Get pet vitals data record per week.
        /// </summary>
        [HttpGet("{petId}/week")]
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

        /// <summary>
        /// Get pet vitals data record per month.
        /// </summary>
        [HttpGet("{petId}/month")]
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

        /// <summary>
        /// Get pet vitals data record per period of time.
        /// </summary>
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

        /// <summary>
        /// Create pet vitals data record. DO NOT FILL THE TIMESTAMP FIELD IF IT'S NOT NECESSARY.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePetParams(PetParameters petParams)
        {
            try
            {
                if (petParams == null)
                    return BadRequest();

                var createdPetParams = await petParamsRepository.CreatePetParameters(petParams);
                return Ok(createdPetParams);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
