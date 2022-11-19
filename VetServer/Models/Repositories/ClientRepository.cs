using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VetServer.Data;
using VetServer.Models.Interfaces;
using VetServer.Utils;

namespace VetServer.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext appDbContext;
        private readonly IMapper mapper;
        public ClientRepository(ApplicationDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Client, ClientModelGET>(); }); // cast client to ClientDataModelGET
            mapper = config.CreateMapper();
        }

        public async Task<Client> GetClient(int clientId)
        {
            return await appDbContext.Client.FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<IEnumerable<Client>> GetClients()
        {
            return await appDbContext.Client.ToListAsync();
        }

        public async Task<Client> GetClientByUsername(string username)
        {
            return await appDbContext.Client.FirstOrDefaultAsync(c => c.Username == username);
        }

        public async Task<ClientModelGET> ShowClient(int clientId)
        {
            ClientModelGET res = mapper.Map<Client, ClientModelGET>(await GetClient(clientId));
            return res;
        }

        /// <summary>
        /// Show the list of all clients. This is a very slow method, so use it only if needed.
        /// </summary>
        public async Task<IEnumerable<ClientModelGET>> ShowClients()
        {
            var list = await GetClients();

            List<ClientModelGET> res = new List<ClientModelGET>();
            foreach (var client in list)
                res.Add(mapper.Map<Client, ClientModelGET>(client)); // convert every object of list

            return res;
        }

        public async Task<ClientModelGET> ShowClientByUsername(string username)
        {
            ClientModelGET res = mapper.Map<Client, ClientModelGET>(await GetClientByUsername(username));
            return res;
        }

        public async Task<Pet> GetClientPet(int clientId, int petId)
        {
            return await appDbContext.Pet.FirstOrDefaultAsync(p => p.Id == petId && p.ClientId == clientId);
        }

        public async Task<IEnumerable<Pet>> GetClientPets(int clientId)
        {
            return await appDbContext.Pet.Where(p => p.ClientId == clientId).ToListAsync();
        }

        public async Task<Client> CreateClient(Client client)
        {
            client.Salt = HashData.GetSalt();
            client.Password = HashData.HashString(client.Password, client.Salt);
            var result = await appDbContext.Client.AddAsync(client);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Client> UpdateClientFull(int clientId, Client client)
        {
            Client result = await GetClientByUsername(client.Username);
            if (result != null)
            {
                if (result.Id != clientId)
                    return null;
                else
                    result = await UpdateClientData(clientId, client, result);
            }
            else
            {
                result = await GetClient(clientId);
                if(result != null)
                    result = await UpdateClientData(clientId, client, result);
            }
            return result;
        }

        private async Task<Client> UpdateClientData(int clientId, Client clientPost, Client clientDb)
        {
            clientPost.Id = clientId;
            clientPost.Password = HashData.HashString(clientPost.Password, clientDb.Salt);
            clientPost.Salt = clientDb.Salt;
            appDbContext.Entry(clientDb).CurrentValues.SetValues(clientPost);
            await appDbContext.SaveChangesAsync();
            return clientDb;
        }

        public async Task<Client> ClientLogIn(string username, string password)
        {
            var client = await GetClientByUsername(username);
            if (client != null && client.Password == HashData.HashString(password, client.Salt))
                return client;
            else
                return null;
        }

        public ClientModelGET ConvertToClientDataModelGET(Client client)
        {
            return mapper.Map<Client, ClientModelGET>(client);
        }
    }
}
