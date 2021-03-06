﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Tests_server_app.Models;
using Tests_server_app.Models.DBModels;

namespace Tests_server_app.Services.Authentication
{
    public class JWTTokenGenerationService: IJWTTokenGenerationService
    {
        private readonly IJWTBearerAuthOptions authOptions;

        public JWTTokenGenerationService(IJWTBearerAuthOptions authOptions)
        {
            this.authOptions = authOptions;
        }

        public JwtSecurityToken GetToken(User user)
        {
            var identity = GetIdentity(user);

            if (identity != null)
            {
                var now = DateTime.UtcNow;

                var jwt = new JwtSecurityToken(
                        issuer: authOptions.Issuer,
                        audience: authOptions.Audience,
                        notBefore: now,
                        claims: identity.Claims,
                        expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                        signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), 
                                                                   SecurityAlgorithms.HmacSha256));

                return jwt;
            }

            return null;
        }

        private ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name),
                };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                                   ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        public IEnumerable<Claim> GetClaimsFromToken(string token)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = authOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = authOptions.Audience,

                ValidateLifetime = true,

                IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                ValidateIssuerSigningKey = true,
            };

            ClaimsPrincipal principal = new JwtSecurityTokenHandler()
                .ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            return principal.Claims;
        }
    }
}
