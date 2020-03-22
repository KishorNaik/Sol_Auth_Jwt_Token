using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthJwt.Generates
{
    public interface IGenerateJwtToken
    {
        Task<string> CreateJwtTokenAsync(String secretKey, Claim[] claims, DateTime? expires = null);
    }
}