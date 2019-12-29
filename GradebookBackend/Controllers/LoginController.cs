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
    public class LoginController : ControllerBase
    {
        private readonly ITokenGeneratorService tokenService;

        private readonly IUserDataService userDataService;

        private readonly IUserProviderService userProvider;

        public LoginController(
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
            catch(GradebookException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpGet("user/id")]
        public IActionResult ReturnIdFromToken()
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
        public IActionResult Get_MyProfile()
        {
            UserDataDTO userDataDTO = userDataService.GetUserData(Int32.Parse(userProvider.GetUserId()));
            return Ok(userDataDTO);
        }
    }
}