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
        public IActionResult GetPets()
        {
            return Ok(petRepository.GetPets());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPet(int id)
        {
            return Ok(petRepository.GetPet(id));
        }

        [HttpPost]
        public IActionResult CreatePet(Pet pet)
        {
            if (pet == null)
                return BadRequest();

            var createdPet = petRepository.CreatePet(pet);
            return CreatedAtAction(nameof(CreatePet), new { id = createdPet.Id }, createdPet);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdatePet(int petId, Pet pet)
        {
            if (petId != pet.Id)
                return BadRequest("ID mismatch");

            if (pet == null)
                return BadRequest();

            var petToUpdate = petRepository.GetPet(petId);

            if (petToUpdate == null)
                return NotFound("Pet with Id = {petId} not found");

            return Ok(petRepository.UpdatePet(pet));
        }
    }
}
