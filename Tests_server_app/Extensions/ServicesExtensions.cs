using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tests_server_app.Services.Authentication;
using Tests_server_app.Services.DatabaseServ;

namespace Tests_server_app.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddJWTBearerAuthentication(this IServiceCollection services, IJWTBearerAuthOptions authOptions)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false; // :for debug
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = authOptions.Issuer,

                            ValidateAudience = true,
                            ValidAudience = authOptions.Audience,

                            ValidateLifetime = true,

                            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                            ValidateIssuerSigningKey = true,
                        };

                       /* options.Events = new JwtBearerEvents()
                        {
                            OnTokenValidated = context =>
                            {
                                if (context.SecurityToken is JwtSecurityToken accessToken)
                                {
                                    if (context.HttpContext.User.Identity is ClaimsIdentity identity)
                                    {
                                        identity.AddClaim(new Claim("access_token", accessToken.RawData));
                                    }
                                }

                                return Task.CompletedTask;
                            }
                        };*/
                    });

            return services;
        }

        // TODO
        public static IServiceCollection AddRolesAuthorization(this IServiceCollection services)
        {
            services.AddTransient<IAuthorizationHandler, RoleHandler>();

            services.AddAuthorization(o =>
            {
                o.AddPolicy("User", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new RoleRequirement("User"));
                });
                o.AddPolicy("Admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.Requirements.Add(new RoleRequirement("Admin"));
                });
            });

            return services;
        }

        public static IServiceCollection AddJWTTokenGenerationService(this IServiceCollection services)
        {
            services.AddScoped<IJWTTokenGenerationService, JWTTokenGenerationService>();
            return services;
        }

        public static IServiceCollection AddDatabaseService(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
            return services;
        }
    }
}
