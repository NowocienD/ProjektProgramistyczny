using GradebookBackend.DTO;
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
        public UserController(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
        }
        //[Authorize]
        [HttpGet("users/{Id}")]
        public IActionResult GetUserDataFromId(int Id)
        {
            UserDataDTO userDataDTO = userDataService.GetUserData(Id);

            return Ok(userDataDTO);
        }
    }
}
