using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Services.Interfaces;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class ScheduleService : IScheduleService
    {
        private readonly AppDbContext _context;

        public ScheduleService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddSchedule(string userName, ScheduleDto scheduleDto)
        {
            if (scheduleDto.ScheduleDate <= DateTime.UtcNow)
            {
                throw new Exception("Can't select a past date");
            }

            var schedule = scheduleDto.ToSchedule();

            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            schedule.UserId = user.Id;

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule.Id;
        }

        public async Task<long> AddSchedulePeriod(string userName, SchedulePeriodDto schedulePeriodDto)
        {
            var user = _context.Users.FirstAsync(u => u.UserName == userName);
            var schedule = await _context.Schedules.FirstAsync(s => s.Id == schedulePeriodDto.ScheduleId);
            if (schedule.UserId != user.Id)
            {
                throw new Exception("Schedule does not belong to you");
            }

            var isInRange = await _context.SchedulePeriods.AnyAsync(m =>
                DateChecker.DateRangeIsInRange(m.StartDate, m.EndDate, schedulePeriodDto.StartDate, schedulePeriodDto.EndDate)
            );
            if (isInRange)
            {
                throw new Exception("The period should not fall within the time frame of another period");
            }

            var schedulePeriod = schedulePeriodDto.ToSchedulePeriod();
            _context.SchedulePeriods.Add(schedulePeriod);
            await _context.SaveChangesAsync();
            return schedulePeriod.Id;
        }

        public async Task<ScheduleDto> GetScheduleByDate(string userName, DateTime scheduleDate)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.UserId == user.Id &&
                                                                             s.ScheduleDate.Date == scheduleDate.Date);
            return schedule != null ? new ScheduleDto(schedule) : null;
        }

        public async Task<List<ScheduleDto>> GetScheduleByDate(string userName, DateTime from, DateTime to)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var schedules = await _context.Schedules.Where(s => s.UserId == user.Id &&
                                                                s.ScheduleDate.Date > from.Date &&
                                                                s.ScheduleDate.Date < to.Date)
                                                    .ToListAsync();
            var scheduleDtos = schedules.Select(s => new ScheduleDto(s));
            return scheduleDtos.ToList();
        }

        public async Task<IEnumerable<SchedulePeriodDto>> GetSchedulePeriods(string userName, long scheduleId)
        {
            var schedule = await _context.Schedules.Include(s => s.SchedulePeriods)
                                                   .FirstAsync(s => s.User.UserName == userName &&
                                                                    s.Id == scheduleId);
            return schedule.SchedulePeriods.Select(s => new SchedulePeriodDto(s));
        }
    }
}
