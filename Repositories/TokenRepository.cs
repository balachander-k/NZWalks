using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace NZWalks.API.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            //Create Claims 
            //A claim is a key-value pair inside a JWT (JSON Web Token) that carries user-related information.
            //Claims tell who the user is and what they are allowed to do.

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach(var role in roles )
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            //Generate Security Key
            //Retrieves the secret key (Jwt:Key) from appsettings.json.
            //Converts the string key into a byte array using Encoding.UTF8.GetBytes().

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            //Create Signing Credentials
            //Uses HMAC SHA-256 encryption to digitally sign the JWT.
            //Prevents unauthorized modifications to the token.

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Generate JWT Token
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
