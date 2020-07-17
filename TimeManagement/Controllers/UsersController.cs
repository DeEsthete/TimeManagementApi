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
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticationDto model)
        {
            var result = await _userService.Authenticate(model.Username, model.Password);
            return Ok(result);
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody]RegistrationDto model)
        {
            await _userService.Registration(model);
            return NoContent();
        }

        [Authorize(Roles = "User")]
        [HttpGet("info")]
        public async Task<IActionResult> Info()
        {
            var user = new { username = User.Identity.Name };
            return Ok(user);
        }
    }
}