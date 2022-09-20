using LoginUser.WebApi.Entities;
using LoginUser.WebApi.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginUser.WebApi.InterFaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAll();
        Task<Client> Create(ClientDto client, int userId);
        Task Delete(int id, ClaimsPrincipal user);
    }
}
