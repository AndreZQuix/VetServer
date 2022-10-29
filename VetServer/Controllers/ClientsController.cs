using Microsoft.AspNetCore.Mvc;
using VetServer.Models;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;

namespace VetServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly IClientRepository clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        [HttpGet("{clientId:int}")]
        public IActionResult GetClient(int clientId)
        {
            return Ok(clientRepository.GetClient(clientId));
        }

        [HttpGet("{clientId:int}/{petId:int}")]
        public IActionResult GetClientPet(int clientId, int petId)
        {
            return Ok(clientRepository.GetClientPet(clientId, petId));
        }

        [HttpGet("{clientId:int}/pets")]
        public IActionResult GetClientPets(int clientId)
        {
            return Ok(clientRepository.GetClientPets(clientId));
        }

        [HttpPost]
        public IActionResult CreateClient(Client client)
        {
            if (client == null)
                return BadRequest();

            var cl = clientRepository.GetClientByUsername(client.Username);

            if (cl != null)
            {
                ModelState.AddModelError("Username", "This username is already in use");
                return BadRequest();
            }

            var createdClient = clientRepository.CreateClient(client);
            return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, createdClient);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateClient(int clientId, Client client)
        {
            if (clientId != client.Id)
                return BadRequest("ID mismatch");

            if (client == null)
                return BadRequest();

            var clientToUpdate = clientRepository.GetClient(clientId);

            if (clientToUpdate == null)
                return NotFound("Pet with Id = {petId} not found");

            return Ok(clientRepository.UpdateClient(client));
        }
    }
}
