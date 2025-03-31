using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        String CreateJWTToken(IdentityUser user,List<String> roles);
    }
}
