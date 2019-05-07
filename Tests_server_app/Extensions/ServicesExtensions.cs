using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests_server_app.Services.Authentication;
using Tests_server_app.Services.UsersMapping;

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
                    });

            return services;
        }

        public static IServiceCollection AddJWTTokenGenerationService(this IServiceCollection services)
        {
            services.AddScoped<IJWTTokenGenerationService, JWTTokenGenerationService>();
            return services;
        }

        public static IServiceCollection AddUsersMappingService(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseService, DatabaseService>();
            return services;
        }
    }
}
