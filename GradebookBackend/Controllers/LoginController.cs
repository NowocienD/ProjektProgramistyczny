﻿using System.Collections.Generic;
using System.Security.Claims;
using GradebookBackend.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GradebookBackend
{
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGeneratorService tokenService;

        private readonly IUserDataService userDataService;

        public LoginController(
            ITokenGeneratorService tokenService,
            IUserDataService userDataService)
        {
            this.tokenService = tokenService;
            this.userDataService = userDataService;
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
        [HttpGet("user/myProfile")]
        public IActionResult Get_MyProfile()
        {
            UserDataDTO dto = new UserDataDTO 
            { 
                Firstname = "Jan",
                Surname = "kowalski",
                Role = "admin",
            };

            return Ok(dto);
        }
    }
}