using LoginUser.WebApi.InterFaces;
using LoginUser.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LoginUser.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //TODO to musi byc aby zadzialaly automatyczne validatory!!
    public class AccountController : ControllerBase
    {
         private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userDto)
        {
            await _accountService.RegisterUser(userDto);
            return Ok($"User with email {userDto.Email} was register");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            string token = await _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View("Error!");
        // }
    }
}