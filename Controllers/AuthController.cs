using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;

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
                    //Get roles for this user 
                    var roles =await _userManager.GetRolesAsync(UserExists);
                    if(roles !=null)
                    {
                        var jwtToken=_tokenRepository.CreateJWTToken(UserExists, roles.ToList());

                        var response = new LoginResponseDTO
                        {
                            JWTToken = jwtToken
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("Username or Password is incorrect");


        }
    }
}
