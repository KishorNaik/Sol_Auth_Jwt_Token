using AuthJwt.Generates;
using Microsoft.Extensions.Options;
using Sol_Demo_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Sol_Demo_WebApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenerateJwtToken generateJwtToken = null;
        private readonly IOptions<JwtModel> options = null; // Get SecretKey from appsetting.json

        public UserRepository(IGenerateJwtToken generateJwtToken, IOptions<JwtModel> options)
        {
            this.generateJwtToken = generateJwtToken;
            this.options = options;
        }

        async Task<dynamic> IUserRepository.LoginAsync(UsersModel usersModel)
        {
            // Demo Purpose
            string tempFullName = "Kishor Naik";
            string tempUserName = "kishor11";
            string tempPassword = "123";
            int? tempId = 1;
            String tempRole = "Admin";

            dynamic data = null;

            // Demo Purpose (Validate Login Credentails from database)
            if (usersModel.UserName == tempUserName && usersModel.Password == tempPassword)
            {
                // Demo Purpose (Id,Role & fullName will get from database)
                usersModel.Id = tempId;
                usersModel.Role = tempRole;
                usersModel.FullName = tempFullName;

                // Add Claims for Authorization and Authentication with Jwt Token
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, Convert.ToString(usersModel.Id))); // Id Base
                claims.Add(new Claim(ClaimTypes.Role, usersModel.Role)); // Role Base

                // Generate Token
                usersModel.JwtToken = await generateJwtToken.CreateJwtTokenAsync(options?.Value?.SecretKey, claims.ToArray(), DateTime.Now.AddDays(1));

                usersModel.Password = null;

                data = usersModel;
            }
            else
            {
                data = new
                {
                    Message = "User Name and Password does not match"
                };
            }

            return await Task.FromResult<dynamic>(data);
        }
    }
}