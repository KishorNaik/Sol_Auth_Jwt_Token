using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sol_Demo_WebApi.Models;
using Sol_Demo_WebApi.Repository;

namespace Sol_Demo_WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/demo")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IUserRepository userRepository = null;

        public DemoController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UsersModel usersModel)
        {
            if (usersModel == null) return base.BadRequest();

            var data =
                    await
                        userRepository
                        ?.LoginAsync(usersModel);

            return base.Ok((Object)data);
        }

        //[Authorize(Roles = "Admin")]
        [Authorize(Policy = "AdminOnly")]
        [Authorize(Policy = "Over21Only")]
        [Authorize(Policy = "UserOnly")]
        [HttpPost("dowork")]
        public IActionResult DoWork()
        {
            return base.Ok((Object)"Authorize User");
        }
    }
}