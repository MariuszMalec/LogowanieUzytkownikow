using LoginUser.WebApi.Entities;
using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginUser.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [AllowAnonymous]//bez naglowka autoryzacji
        public async Task<IActionResult> Get()
        {
            //HttpContext.User.IsInRole("Admin"); //mozna tak ale lepiej nadac atrybuty
            var users = await _userService.GetAll();
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [Authorize(Policy = "HasNationality")]
        public async Task<UserDto> Get(int id)
        {
            return await _userService.GetById(id);
        }

        // POST api/<UsersController>
        [HttpPost("{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] UserEditDto user)
        {
            await _userService.Update(id, user);
            return Ok($"User with name {user.LastName} was edited");
        }

        // PUT api/<UsersController>/5
        [HttpPut]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> Put([FromBody] User user)
        {            
            await _userService.Create(user);
            return Ok($"User with email {user.Email} was created");
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _userService.Delete(id);
            return Ok($"User with with id {id} was deleted");
        }
    }
}
