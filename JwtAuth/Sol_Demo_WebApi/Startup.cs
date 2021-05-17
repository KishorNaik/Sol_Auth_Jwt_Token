using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthJwt.Services;
using JwtAuth.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sol_Demo_WebApi.Models;
using Sol_Demo_WebApi.Policies;
using Sol_Demo_WebApi.Repository;

namespace Sol_Demo_WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions((leSetUp) =>
                {
                    // Pascal Casing
                    leSetUp.JsonSerializerOptions.PropertyNamingPolicy = null;

                    // Ignore Json Property Null Value from Response
                    leSetUp.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.Configure<AppSettingsModel>(Configuration.GetSection("Jwt"));
            var getSecretKey = Configuration.GetSection("Jwt").Get<AppSettingsModel>();

            services.AddJwtToken(getSecretKey.SecretKey,
                (authOption) =>
                {
                    authOption.AddPolicy("AdminOnly", (policy) => policy.RequireRole("Admin"));// Role Base
                    authOption.AddPolicy("UserOnly", (policy) => policy.RequireClaim("UserID")); // Claim base
                    authOption.AddPolicy("Over21Only", (policy) => policy.Requirements.Add(new MinimumAgeRequirement(21))); // Policy Base
                }
                ); // Add Jwt Token Service

            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
            services.AddTransient<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseJwtToken(); // Use Jwt Token Middleware

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}