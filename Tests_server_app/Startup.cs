using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Tests_server_app.Models;
using Microsoft.AspNetCore.Authentication;
using Tests_server_app.Extensions;
using Tests_server_app.Services.Authentication;

namespace Tests_server_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // authentication
            var authOptions = JWTBearerAuthOptions.LoadJsonOptions(@"\Services\Authentication\authsettings.json");
            services.AddJWTBearerAuthentication(authOptions);

            // auth options
            services.AddSingleton(typeof(IJWTBearerAuthOptions), authOptions);
            
            // configuration
            Configuration = configuration;

            // configure db
            var connectionString = Configuration.GetConnectionString("CW_Task_Core");
            services.AddDbContext<TestsDbContext>(
                options => options.UseSqlServer(connectionString),
                ServiceLifetime.Singleton);

            // configure mvc
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
