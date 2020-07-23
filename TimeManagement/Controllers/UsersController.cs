using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Interfaces;
using Shared;

namespace TimeManagement.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationDto model)
        {
            var result = await _userService.Authenticate(model.Username, model.Password);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody]RegistrationDto model)
        {
            await _userService.Registration(model);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserInfoByUserName()
        {
            var userName = User.Identity.Name;
            var user = await _userService.GetUserInfoByUserName(userName);
            return Ok(user);
        }
    }
}