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
        private readonly IOptions<AppSettingsModel> options = null; // Get SecretKey from appsetting.json

        public UserRepository(IGenerateJwtToken generateJwtToken, IOptions<AppSettingsModel> options)
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
            DateTime tempDateOfBirth = new DateTime(1986, 08, 11);
            int? tempId = 1;
            String tempRole = "Admin";

            dynamic data = null;

            // Demo Purpose (Validate Login Credentials from database)
            if (usersModel.UserName == tempUserName && usersModel.Password == tempPassword)
            {
                // Demo Purpose (Id,Role & fullName will get from database)
                usersModel.Id = tempId;
                usersModel.Role = tempRole;
                usersModel.FullName = tempFullName;
                usersModel.DateOfBirth = tempDateOfBirth;

                // Add Claims for Authorization and Authentication with Jwt Token
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim("UserID", Convert.ToString(usersModel.Id))); // Id Base
                claims.Add(new Claim(ClaimTypes.Role, usersModel.Role)); // Role Base
                claims.Add(new Claim(ClaimTypes.DateOfBirth, usersModel.DateOfBirth.ToString())); // Policy Base

                // Generate Token
                usersModel.Token = await generateJwtToken.CreateJwtTokenAsync(options?.Value?.SecretKey, claims.ToArray(), DateTime.Now.AddDays(1));

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