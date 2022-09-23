using AutoMapper;
using LoginUser.WebApi.Authorization;
using LoginUser.WebApi.Context;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.Exceptions;
using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginUser.WebApi.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ClientService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public ClientService(ApplicationDbContext dbContext, IMapper mapper, ILogger<ClientService> logger, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public async Task<IEnumerable<ClientDto>> GetAll()
        {
            if (!_dbContext.Clients.Any())
            {
                throw new NotFoundException("Client not found");
            }
            var model = await _dbContext.Clients
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<ClientDto>>(model);

            return result;
        }

        public async Task<ClientDto> GetById(int id)
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client == null)
            {
                throw new NotFoundException("client not found");
            }

            var result = _mapper.Map<ClientDto>(client);
            return result;
        }

        public async Task<Client> Create(ClientDto dto, int userId)
        {
            var result = _mapper.Map<Client>(dto);
            result.CreatedById = userId;

            var client = await _dbContext.Clients.FindAsync(dto.Id);
            if (client != null)
            {
                throw new NotFoundException("Client exist yet!");
            }

            await _dbContext.Clients.AddAsync(result);
            await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task<Client> CreateWithoutAuthorize(ClientDto dto)
        {
            var result = _mapper.Map<Client>(dto);

            var client = await _dbContext.Clients.FindAsync(dto.Id);
            if (client != null)
            {
                throw new NotFoundException("Client exist yet!");
            }

            await _dbContext.Clients.AddAsync(result);
            await _dbContext.SaveChangesAsync();

            return result;
        }

        public async Task Delete(int id, ClaimsPrincipal user)//https://youtu.be/Ei7Uk-UgSAY?t=2874
        {
            var client = await _dbContext.Clients.FindAsync(id);
            if (client == null)
            {
                throw new NotFoundException("Client not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(user, client,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _logger.LogWarning($"Client with email {client.Email} was deleted!");
            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync();
        }
    }
}
