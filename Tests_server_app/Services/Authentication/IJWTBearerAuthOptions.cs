using Microsoft.IdentityModel.Tokens;

namespace Tests_server_app.Services.Authentication
{
    public interface IJWTBearerAuthOptions
    {
        string AuthType { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        string Key { get; set; }
        int Lifetime { get; set; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}
