using LoginUser.WebApi.Entities;
using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginUser.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        // GET: ClientController
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        [Authorize]
        //[Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Get()
        {
            var model = await _clientService.GetAll();
            return Ok(model);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return await _clientService.GetById(id);
        }


        [HttpPost("CreateWithoutAuthorize")]
        public async Task<IActionResult> Create([FromBody] ClientDto dto)
        {
            await _clientService.CreateWithoutAuthorize(dto);
            return Created($"/api/client/createwithoutauthorize/{dto.Id}", null);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClientDto dto)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return BadRequest("User is not login");
            }

            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _clientService.Create(dto, userId);
            return Ok($"client with email {dto.Email} was created");
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _clientService.Delete(id, User);
            return Ok($"Client with with id {id} was deleted");
        }
    }
}
