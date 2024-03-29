﻿using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IScheduleService
    {
        Task<long> AddSchedule(string userName, ScheduleDto scheduleDto);
        Task<long> AddSchedulePeriod(string userName, SchedulePeriodDto schedulePeriodDto);
        Task<ScheduleDto> GetScheduleByDate(string userName, DateTime scheduleDate);
        Task<List<ScheduleDto>> GetScheduleByDate(string userName, DateTime from, DateTime to);
        Task<IEnumerable<SchedulePeriodDto>> GetSchedulePeriods(string userName, long scheduleId);
    }
}
