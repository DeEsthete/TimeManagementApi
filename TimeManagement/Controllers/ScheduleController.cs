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
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(ScheduleDto scheduleDto)
        {
            var userName = User.Identity.Name;
            var result = await _scheduleService.AddSchedule(userName, scheduleDto);
            return Ok(result);
        }

        [HttpGet("{scheduleDate}")]
        public async Task<IActionResult> GetScheduleByDate(DateTime scheduleDate)
        {
            var userName = User.Identity.Name;
            var result = await _scheduleService.GetScheduleByDate(userName, scheduleDate);
            return Ok(result);
        }

        [HttpGet("periods/{scheduleId}")]
        public async Task<IActionResult> GetSchedulePeriods(long scheduleId)
        {
            var userName = User.Identity.Name;
            var result = await _scheduleService.GetSchedulePeriods(userName, scheduleId);
            return Ok(result);
        }
    }
}
