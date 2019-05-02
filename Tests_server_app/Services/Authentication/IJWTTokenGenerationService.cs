using System.IdentityModel.Tokens.Jwt;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Services.Authentication
{
    public interface IJWTTokenGenerationService
    {
        JwtSecurityToken GetToken(User user);
    }
}
