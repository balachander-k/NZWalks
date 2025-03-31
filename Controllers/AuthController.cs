using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }
        //Post :api/auth/register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };

            var IdentityResult = await _userManager.CreateAsync(identityUser, registerRequestDTO.Password);

            if (IdentityResult.Succeeded)
            {
                //Add roles to the user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    IdentityResult = await _userManager.AddToRolesAsync(identityUser, registerRequestDTO.Roles);

                    if (IdentityResult.Succeeded)
                    {
                        return Ok("User was Registered! Please Login");
                    }

                }
            }
            return BadRequest("Something went wrong");

        }

        //Post: api/auth/loign 
        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var UserExists = await _userManager.FindByEmailAsync(loginRequestDTO.Username);

            if (UserExists != null)
            {
                var CheckPassword = await _userManager.CheckPasswordAsync(UserExists, loginRequestDTO.Password);
                if (CheckPassword)
                {
                    return Ok("User was able to login");
                }
            }
            return BadRequest("Username or Password is incorrect");


        }
    }
}
