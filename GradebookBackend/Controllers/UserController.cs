using GradebookBackend.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradebookBackend.Controllers
{
    [Route("api")]
    public class UserController : Controller
    {
        private readonly IUserDataService userDataService;
        public UserController(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
        }
        // GET api/values
        [HttpGet("users/{userId}")]
        public IActionResult GetEmpFromId(int id)
        {
            UserDataDTO userDataDTO = userDataService.GetUserData(id);

            return Ok(userDataDTO);
        }
    }
}
