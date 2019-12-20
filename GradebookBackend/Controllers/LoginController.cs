using System.Collections.Generic;
using System.Security.Claims;
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
            if (dto == null | string.IsNullOrWhiteSpace(dto.Login) | string.IsNullOrEmpty(dto.Password))
            {
                return BadRequest();
            }
            string token = tokenService.GenerateToken(8);
            //string token = tokenService.GenerateToken(userDataService.GetId(dto.Login));

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
            UserDataDTO dto = new UserDataDTO
            {
                Imie = "Jan",
                Nazwisko = "kowalski",
                Rola = "admin",
            };

            return Ok(dto);
        }


    }
}