using AuthJwt.Generates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AuthJwt.Services
{
    public static class JwtTokenServiceExtension
    {
        public static void AddJwtToken(this IServiceCollection services, string secretKey, Action<AuthorizationOptions> authOption = null)

        {
            services.AddTransient<IGenerateJwtToken, GenerateJwtToken>();

            var key = Encoding.ASCII.GetBytes(secretKey);
            services
            .AddAuthorization(x => authOption?.Invoke(x))
            .AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
            .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });
        }
    }
}