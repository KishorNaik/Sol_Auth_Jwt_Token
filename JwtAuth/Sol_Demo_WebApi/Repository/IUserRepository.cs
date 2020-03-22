using Sol_Demo_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Demo_WebApi.Repository
{
    public interface IUserRepository
    {
        Task<dynamic> LoginAsync(UsersModel usersModel);
    }
}