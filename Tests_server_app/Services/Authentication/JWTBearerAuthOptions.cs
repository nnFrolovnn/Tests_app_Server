using System.IO;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Tests_server_app.Services.Authentication
{
    public class JWTBearerAuthOptions : IJWTBearerAuthOptions
    {
        public string AuthType { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Lifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public static JWTBearerAuthOptions LoadJsonOptions(string path)
        {
            string jsonOptions = new StreamReader(path).ReadToEnd();
            return JsonConvert.DeserializeObject<JWTBearerAuthOptions>(jsonOptions);
        }
    }
}
