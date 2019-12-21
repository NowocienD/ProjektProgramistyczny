using GradebookBackend.DTO;
using GradebookBackend.ServicesCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : Controller
    {
        private readonly IUserDataService userDataService;
        private readonly IUserProvider userProvider;
        public UserController(IUserDataService userDataService, IUserProvider userProvider)
        {
            this.userDataService = userDataService;
            this.userProvider = userProvider;
        }
        [HttpGet("users/{Id}")]
        public IActionResult GetUserDataFromId(int Id)
        {
            UserDataDTO userDataDTO = userDataService.GetUserData(Id);

            return Ok(userDataDTO);
        }

        [Authorize]
        [HttpGet("users")]
        public IActionResult GetUserDataFromToken()
        {
            int Id = Int32.Parse(userProvider.GetUserId());
            UserDataDTO userDataDTO = userDataService.GetUserData(Id);

            return Ok(userDataDTO);
        }
    }
}
