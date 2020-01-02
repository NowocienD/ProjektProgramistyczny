using System.Collections.Generic;
using System.Security.Claims;
using GradebookBackend.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using GradebookBackend.ServicesCore;

namespace GradebookBackend.Controllers
{
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly ITokenGeneratorService tokenService;

        private readonly IUserDataService userDataService;

        private readonly IUserProviderService userProvider;

        public UserController(
            ITokenGeneratorService tokenService,
            IUserDataService userDataService,
            IUserProviderService userProvider)
        {
            this.tokenService = tokenService;
            this.userDataService = userDataService;
            this.userProvider = userProvider;// ?? throw new ArgumentNullException(nameof(userProvider));
        }

        [HttpPost("login")]
        public IActionResult GetToken([FromBody] UserLoginCommandDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Invalid dto");
            }
            try
            {
                int userId = userDataService.GetUserIdByLoginAndPassword(dto.Login, dto.Password);
                string token = tokenService.GenerateToken(userId);
                return Ok(token);
            }
            catch(GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("user/myId")]
        public IActionResult GetIdFromToken()
        {
            string userId = userProvider.GetUserId();
            if (userId.Equals(string.Empty))
            {
                return BadRequest("niepoprawny token, pusty token albo inny chuj strzelił metode wyciagajaca id z tokenu");
            }
            return Ok(userId);
        }

        [Authorize]
        [HttpGet("user/myProfile")]
        public IActionResult GetUserData()
        {
            UserDataDTO userDataDTO = userDataService.GetUserDataByUserId(Int32.Parse(userProvider.GetUserId()));
            return Ok(userDataDTO);
        }

        [Authorize]
        [HttpPost("admin/addUser")]
        public IActionResult AddUser([FromBody] NewUserDTO newUserDTO)
        {
            if (userDataService.IsAdmin(Int32.Parse(userProvider.GetUserId())))
            {
                userDataService.AddUser(newUserDTO);
                return Ok("User has been added");
            }
            else
            {
                return BadRequest("Logged user is not a admin");
            }
        }

        [Authorize]
        [HttpPost("admin/updateUser/{userId}")]
        public IActionResult UpdatedUser([FromBody] NewUserDTO newUserDTO, int userId)
        {
            if (userDataService.IsAdmin(Int32.Parse(userProvider.GetUserId())))
            {
                userDataService.UpdateUser(newUserDTO, userId);
                return Ok("User has been updated");
            }
            else
            {
                return BadRequest("Logged user is not a admin");
            }
        }

        [Authorize]
        [HttpPost("admin/deleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            if (userDataService.IsAdmin(Int32.Parse(userProvider.GetUserId())))
            {
                userDataService.DeleteUser(userId);
                return Ok("User has been deleted");
            }
            else
            {
                return BadRequest("Logged user is not a admin");
            }
        }
    }
}