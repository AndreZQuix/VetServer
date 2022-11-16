﻿using Microsoft.AspNetCore.Mvc;
using VetServer.Models;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;
using VetServer.Utils;

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
        /// Create client record. FIELD ID IS CREATED AUTOMATICALLY. Do not fill it.
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
                    return StatusCode(StatusCodes.Status303SeeOther, "This username is already in use");

                var createdClient = await clientRepository.CreateClient(client);
                return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, 
                    new { Id = createdClient.Id, 
                    Name = createdClient.Name, 
                    Username = createdClient.Username, 
                    Email = createdClient.Email });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }

        /// <summary>
        /// Update FULL client data (even password).
        /// </summary>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClientFull(int id, Client client)
        {
            try
            {
                if (client == null)
                    return BadRequest();

                var clientToUpdate = await clientRepository.GetClient(id);

                if (clientToUpdate == null)
                    return NotFound($"Client with Id = {id} not found");

                var result = await clientRepository.UpdateClientFull(id, client);
                return Ok(new { Id = result.Id, 
                    Name = result.Name, 
                    Username = result.Username, 
                    Email = result.Email });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }

        /// <summary>
        /// Try to log in by client username.
        /// </summary>
        [HttpGet("login/{username}/{password}")]
        public async Task<IActionResult> ClientLogIn(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(username))
                    return BadRequest();

                var result = await clientRepository.ClientLogIn(username, password);
                if (result != null)
                {
                    return Ok(new
                    {
                        Id = result.Id,
                        Name = result.Name,
                        Username = result.Username,
                        Email = result.Email
                    });
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Wrong client data");
                }

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error sending data to the database");
            }
        }
    }
}
