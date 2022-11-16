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
        /// Create pet record. FIELD ID IS CREATED AUTOMATICALLY. Do not fill it.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreatePet(Pet pet)
        {
            try
            {
                if (pet == null)
                    return BadRequest();

                var createdPet = await petRepository.CreatePet(pet);
                return CreatedAtAction(nameof(CreatePet), new { id = createdPet.Id }, createdPet);
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
        public async Task<IActionResult> UpdatePetFull(int id, Pet pet)
        {
            try
            {
                if (pet == null)
                    return BadRequest();

                var petToUpdate = await petRepository.GetPet(id);

                if (petToUpdate == null)
                    return NotFound($"Pet with Id = {id} not found");

                return Ok(await petRepository.UpdatePetFull(petToUpdate.Id, pet));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
