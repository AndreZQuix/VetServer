using Microsoft.AspNetCore.Mvc;
using VetServer.Models;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;

namespace VetServer.Controllers
{
    /// <summary>
    /// Controller for clients data processing.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : Controller
    {
        private readonly IClientRepository clientRepository;

        public ClientsController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        /// <summary>
        /// Get the client data by it's id.
        /// </summary>
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

        /// <summary>
        /// Get the list of all clients.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                return Ok(await clientRepository.GetClients());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        /// <summary>
        /// Get the client pet data by their id's.
        /// </summary>
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

        /// <summary>
        /// Get the list of all client pets.
        /// </summary>
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

        /// <summary>
        /// Create client record.
        /// </summary>
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

        /// <summary>
        /// Update client data.
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClient(int id, Client client)
        {
            try
            {
                if (client == null)
                    return BadRequest();

                var clientToUpdate = await clientRepository.GetClient(id);

                if (clientToUpdate == null)
                    return NotFound($"Client with Id = {id} not found");

                return Ok(await clientRepository.UpdateClient(id, client));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
