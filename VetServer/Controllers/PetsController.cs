using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using VetServer.Data;
using VetServer.Models;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;

namespace VetServer.Controllers
{
    /// <summary>
    /// Controller for pets data processing.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : Controller
    {
        private readonly IPetRepository petRepository;

        public PetsController(IPetRepository petRepository)
        {
            this.petRepository = petRepository;
        }

        /// <summary>
        /// Get the list of all pets.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetPets()
        {
            try
            {
                return Ok(await petRepository.GetPets());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Get the pet data by it's id.
        /// </summary>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPet(int id)
        {
            try
            {
                return Ok(await petRepository.GetPet(id));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Create pet record.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePet(/*PetPOST*/ Pet pet)
        {
            try
            {
                if (pet == null)
                    return BadRequest();

                var res = await petRepository.CreatePet(pet);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }

        /// <summary>
        /// Update FULL pet data.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePetFull(int id, /*PetPOST*/ Pet pet)
        {
            try
            {
                if (pet == null)
                    return BadRequest();

                return Ok(await petRepository.UpdatePetFull(id, pet));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
