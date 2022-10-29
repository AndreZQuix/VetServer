using Microsoft.AspNetCore.Mvc;
using System.Net;
using VetServer.Data;
using VetServer.Models;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;

namespace VetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : Controller
    {
        private readonly IPetRepository petRepository;

        public PetsController(IPetRepository petRepository)
        {
            this.petRepository = petRepository;
        }

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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePet(int petId, Pet pet)
        {
            try
            {
                if (petId != pet.Id)
                    return BadRequest("ID mismatch");

                if (pet == null)
                    return BadRequest();

                var petToUpdate = await petRepository.GetPet(petId);

                if (petToUpdate == null)
                    return NotFound("Pet with Id = {petId} not found");

                return Ok(await petRepository.UpdatePet(pet));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
