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
        public async Task<IActionResult> GetClient(int clientId)
        {
            try
            {
                return Ok(await clientRepository.GetClient(clientId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{clientId:int}/{petId:int}")]
        public async Task<IActionResult> GetClientPet(int clientId, int petId)
        {
            try
            {
                return Ok(await clientRepository.GetClientPet(clientId, petId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{clientId:int}/pets")]
        public async Task<IActionResult> GetClientPets(int clientId)
        {
            try
            {
                return Ok(await clientRepository.GetClientPets(clientId));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient(Client client)
        {
            try
            {
                if (client == null)
                    return BadRequest();

                var cl = await clientRepository.GetClientByUsername(client.Username);

                if (cl != null)
                {
                    ModelState.AddModelError("Username", "This username is already in use");
                    return BadRequest();
                }

                var createdClient = await clientRepository.CreateClient(client);
                return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, createdClient);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClient(int clientId, Client client)
        {
            try
            {
                if (clientId != client.Id)
                    return BadRequest("ID mismatch");

                if (client == null)
                    return BadRequest();

                var clientToUpdate = await clientRepository.GetClient(clientId);

                if (clientToUpdate == null)
                    return NotFound("Pet with Id = {petId} not found");

                return Ok(await clientRepository.UpdateClient(client));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
