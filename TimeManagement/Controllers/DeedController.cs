﻿using Microsoft.AspNetCore.Authorization;
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
    public class DeedController : ControllerBase
    {
        private readonly IDeedService _deedService;

        public DeedController(IDeedService deedService)
        {
            _deedService = deedService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeed(DeedDto deedDto)
        {
            var userName = User.Identity.Name;
            var result = await _deedService.CreateDeed(userName, deedDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeed(DeedDto deedDto)
        {
            var userName = User.Identity.Name;
            await _deedService.UpdateDeed(userName, deedDto);
            return NoContent();
        }

        [HttpGet("{isArchiveInclusive}")]
        public async Task<IActionResult> GetUserDeeds(bool isArchiveInclusive)
        {
            var userName = User.Identity.Name;
            var result = await _deedService.GetUserDeeds(userName, isArchiveInclusive);
            return Ok(result);
        }

        [HttpGet("periods-count/{from}/{to}")]
        public async Task<IActionResult> GetDeedsPeriodsCount(DateTime from, DateTime to)
        {
            var userName = User.Identity.Name;
            var result = await _deedService.GetDeedsPeriodsCount(userName, from, to);
            return Ok(result);
        }

        [HttpPost("archive/{id}")]
        public async Task<IActionResult> ArchiveDeed(long id)
        {
            var userName = User.Identity.Name;
            await _deedService.ArchiveDeed(userName, id);
            return NoContent();
        }

        [HttpPost("unarchive/{id}")]
        public async Task<IActionResult> UnarchiveDeed(long id)
        {
            var userName = User.Identity.Name;
            await _deedService.UnarhiveDeed(userName, id);
            return NoContent();
        }
    }
}
