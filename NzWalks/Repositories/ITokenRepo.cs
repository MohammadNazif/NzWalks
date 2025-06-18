using Microsoft.AspNetCore.Identity;

namespace NzWalks.Repositories
{
    public interface ITokenRepo
    {

        string CreateToken(IdentityUser user, List<string> roles);
    }
}
