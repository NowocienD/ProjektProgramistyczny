using GradebookBackend.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using GradebookBackend.ServicesCore;

namespace GradebookBackend.Controllers
{
    [Route("api/user")]
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
            this.userProviderService = userProviderService;
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
        [HttpGet("myId")]
        public IActionResult GetIdFromToken()
        {
            string userId = userProviderService.GetUserId();
            if (userId.Equals(string.Empty))
            {
                return BadRequest("Niepoprawny token");
            }
            return Ok(userId);
        }

        [Authorize]
        [HttpGet("myProfile")]
        public IActionResult GetUserData()
        {
            int userId = Int32.Parse(userProviderService.GetUserId());
            UserDataDTO userDataDTO = userService.GetUserDataByUserId(userId);
            return Ok(userDataDTO);
        }

        [Authorize]
        [HttpPost("admin/adduser")]
        public IActionResult AddUser([FromBody] UserDTO newUserDTO)
        {
            if (userService.IsAdmin(Int32.Parse(userProviderService.GetUserId())))
            {
                try
                {
                    userService.AddUser(newUserDTO);
                    return Ok("Uzytkownik zostal pomyslnie dodany");
                }
                catch (GradebookServerException exception)
                {
                    return BadRequest(exception.Message);
                }
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [HttpDelete("admin/deactivateuser/{userId}")]
        public IActionResult DeactivateUser(int userId)
        {
            if (userService.IsAdmin(Int32.Parse(userProviderService.GetUserId())))
            {
                try
                {
                    userService.DeactivateUser(userId);
                    return Ok("Uzytkownik zostal pomyslnie dezaktywowany");
                }
                catch(GradebookServerException exception)
                {
                    return BadRequest(exception.Message);
                }
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpPatch("admin/updateUser/{userId}")]
        public IActionResult UpdatedUser([FromBody] UserDTO newUserDTO, int userId)
        {
            if (userService.IsAdmin(Int32.Parse(userProviderService.GetUserId())))
            {
                try
                {
                    userService.UpdateUser(newUserDTO, userId);
                    return Ok("Uzytkownik zostal pomyslnie zaktualizowany");
                }
                catch (GradebookServerException exception)
                {
                    return BadRequest(exception.Message);
                }
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpPost("updateMyPassword")]
        public IActionResult UpdatedUserPassword([FromBody] UserPasswordChangeDTO userPasswordChangeDTO)
        {
            try
            {
                int userId = Int32.Parse(userProviderService.GetUserId());
                userService.UpdateUserPassword(userPasswordChangeDTO, userId);
                return Ok("Haslo uzytkownika zostalo pomyslnie zaktualizowane");
            }
            catch (GradebookServerException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("admin/allusers")]
        public IActionResult GetAllUsers()
        {
            int userId = Int32.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(userId))
            {
                UserListDTO userDataListDTO = userService.GetAllUsers();
                return Ok(userDataListDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }

        [Authorize]
        [HttpGet("admin/user/{userId}")]
        public IActionResult GetUser(int userId)
        {
            int loggedUserId = int.Parse(userProviderService.GetUserId());
            if (userService.IsAdmin(loggedUserId))
            {
                UserDTO userDTO = userService.GetUserByUserId(userId);
                return Ok(userDTO);
            }
            else
            {
                return BadRequest("Brak uprawnien administratora");
            }
        }
    }
}
