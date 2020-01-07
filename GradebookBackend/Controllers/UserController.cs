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
        private readonly IUserService userService;
        private readonly IUserProviderService userProviderService;

        public UserController(
            ITokenGeneratorService tokenService,
            IUserService userService,
            IUserProviderService userProviderService)
        {
            this.tokenService = tokenService;
            this.userService = userService;
            this.userProviderService = userProviderService;// ?? throw new ArgumentNullException(nameof(userProvider));
        }

        [HttpPost("login")]
        public IActionResult GetToken([FromBody] UserLoginCommandDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Login) || string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest("Invalid dto");
            }
            try
            {
                int userId = userService.GetUserIdByLoginAndPassword(dto.Login, dto.Password);
                string token = tokenService.GenerateToken(userId);
                return Ok(token);
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("user/myId")]
        public IActionResult GetIdFromToken()
        {
            string userId = userProviderService.GetUserId();
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
            UserDataDTO userDataDTO = userService.GetUserDataByUserId(Int32.Parse(userProviderService.GetUserId()));
            return Ok(userDataDTO);
        }

        [Authorize]
        [HttpPost("admin/addUser")]
        public IActionResult AddUser([FromBody] NewUserDTO newUserDTO)
        {
            if (userService.IsAdmin(Int32.Parse(userProviderService.GetUserId())))
            {
                try
                {
                    userService.AddUser(newUserDTO);
                    return Ok("User has been added");
                }
                catch (GradebookServerException exception)
                {
                    return BadRequest(exception.Message);
                }
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
            if (userService.IsAdmin(Int32.Parse(userProviderService.GetUserId())))
            {
                try
                {
                    userService.UpdateUser(newUserDTO, userId);
                    return Ok("User has been updated");

                }
                catch (GradebookServerException exception)
                {
                    return BadRequest(exception.Message);
                }
            }
            else
            {
                return BadRequest("Logged user is not a admin");
            }
        }

        [Authorize]
        [HttpPost("user/updateMyPassword")]
        public IActionResult UpdatedUserPassword([FromBody] UserPasswordChangeDTO userPasswordChangeDTO)
        {
            try
            {
                int userId = Int32.Parse(userProviderService.GetUserId());
                userService.UpdateUserPassword(userPasswordChangeDTO, userId);
                return Ok("User password has been updated");
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }

        }
    }
}