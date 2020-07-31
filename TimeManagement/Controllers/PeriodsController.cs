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
    public class PeriodsController : ControllerBase
    {
        private readonly IPeriodService _periodService;

        public PeriodsController(IPeriodService periodService)
        {
            _periodService = periodService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePeriod(PeriodDto periodDto)
        {
            var userName = User.Identity.Name;
            var result = await _periodService.AddPeriod(userName, periodDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePeriod(PeriodDto periodDto)
        {
            var userName = User.Identity.Name;
            var result = await _periodService.AddPeriod(userName, periodDto);
            return Ok(result);
        }

        [HttpGet("{from}/{to}")]
        public async Task<IActionResult> GetPeriodsByDate(DateTime from, DateTime to)
        {
            var userName = User.Identity.Name;
            var result = await _periodService.GetPeriodsByDate(userName, from, to);
            return Ok(result);
        }

        [HttpGet("count/{from}/{to}")]
        public async Task<IActionResult> GetPeriodsCount(DateTime from, DateTime to)
        {
            var userName = User.Identity.Name;
            var result = await _periodService.GetPeriodsCountByDate(userName, from, to);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetLastPeriod()
        {
            var userName = User.Identity.Name;
            var result = await _periodService.GetLastPeriod(userName);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemovePeriod(long id)
        {
            var userName = User.Identity.Name;
            await _periodService.RemovePeriod(userName, id);
            return NoContent();
        }
    }
}
