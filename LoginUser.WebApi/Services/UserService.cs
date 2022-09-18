using AutoMapper;
using LoginUser.WebApi.Context;
using LoginUser.WebApi.Entities;
using LoginUser.WebApi.Exceptions;
using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginUser.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(ApplicationDbContext dbContext, IMapper mapper, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            if (!_dbContext.Users.Any())
            {
                throw new NotFoundException("Users not found");
            }
            var users = await _dbContext.Users.ToListAsync();

            var result = _mapper.Map<IEnumerable<UserDto>>(users);

            return result;
        }

        public async Task Update(int id, User user)
        {
            var updateUser = await _dbContext.Users.FindAsync(id);
            if (updateUser == null)
            {
                throw new NotFoundException("User not found");
            }
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.DataOfBirth = user.DataOfBirth;
            updateUser.Nationality = user.Nationality;

            _logger.LogInformation($"User with email {user.Email} was edited");
            _dbContext.Users.Update(updateUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public async Task Delete(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            _logger.LogWarning($"User with email {user.Email} was deleted!");
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
