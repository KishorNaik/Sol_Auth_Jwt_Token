using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtAuth.Middlewares
{
    public static class JwtTokenMiddlewareExtension
    {
        public static void UseJwtToken(this IApplicationBuilder builder)
        {
            builder.UseAuthentication();
            builder.UseAuthorization();
        }
    }
}