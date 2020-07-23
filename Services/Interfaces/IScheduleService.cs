using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IScheduleService
    {
        Task<long> AddSchedule(string userName, ScheduleDto scheduleDto);
        Task<ScheduleDto> GetScheduleByDate(string userName, DateTime scheduleDate);
        Task<IEnumerable<SchedulePeriodDto>> GetSchedulePeriods(string userName, long scheduleId);
    }
}
