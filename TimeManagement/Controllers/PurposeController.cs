using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Dtos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeManagement.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PurposeController : ControllerBase
    {
        private readonly IPurposeService _purposeService;

        public PurposeController(IPurposeService purposeService)
        {
            _purposeService = purposeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPurpose(PurposeDto purposeDto)
        {
            var userName = User.Identity.Name;
            var result = await _purposeService.AddPurpose(userName, purposeDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePurpose(PurposeDto purposeDto)
        {
            var userName = User.Identity.Name;
            await _purposeService.UpdatePurpose(userName, purposeDto);
            return NoContent();
        }

        [HttpGet("{statusCode}")]
        public async Task<IActionResult> GetPurposesByStatus(string statusCode)
        {
            var userName = User.Identity.Name;
            var result = await _purposeService.GetPurposesByStatus(userName, statusCode);
            return Ok(result);
        }
    }
}
