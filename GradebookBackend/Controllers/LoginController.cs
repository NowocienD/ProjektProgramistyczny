using System.Collections.Generic;
using System.Security.Claims;
using GradebookBackend.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using GradebookBackend.ServicesCore;

namespace GradebookBackend
{
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGeneratorService tokenService;

        private readonly IUserDataService userDataService;

        private readonly IUserProvider userProvider;

        public LoginController(
            ITokenGeneratorService tokenService,
            IUserDataService userDataService,
            IUserProvider userProvider)
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
            int userId = userDataService.GetUserId(dto.Login, dto.Password);
            if(userId == 0) return BadRequest("Login failed. Wrong login or password");
            string token = tokenService.GenerateToken(userId);

            return Ok(token); 
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