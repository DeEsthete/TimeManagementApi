using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Services.Interfaces;
using Shared.Constants;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class PeriodService : IPeriodService
    {
        private readonly AppDbContext _context;

        public PeriodService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddPeriod(string userName, PeriodDto periodDto)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var deed = await _context.Deeds.FirstAsync(d => d.Id == periodDto.DeedId);
            if (deed.UserId != user.Id)
            {
                throw new Exception("Deed does not belong to you");
            }

            var periodsInRange = await _context.Periods.Where(p => p.DeedId == deed.Id &&
                                                                   p.StartDate >= periodDto.StartDate &&
                                                                   p.EndDate.Value <= periodDto.EndDate.Value)
                                                       .ToListAsync();
            var inRange = periodsInRange.Any(m =>
                DateChecker.DateRangeIsInRange(m.StartDate, m.EndDate, periodDto.StartDate, periodDto.EndDate)
            );
            if (inRange)
            {
                throw new Exception("The period should not fall within the time frame of another period");
            }

            var period = periodDto.ToPeriod();
            _context.Periods.Add(period);
            await _context.SaveChangesAsync();
            //await SetPurposesStatus(deed.Id);
            return period.Id;
        }

        public async Task UpdatePeriod(string userName, PeriodDto periodDto)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);

            var period = await _context.Periods.Include(p => p.Deed)
                                         .FirstAsync(p => p.Id == periodDto.Id);

            if (period.Deed.UserId != user.Id)
            {
                throw new Exception("Period does not belong to you");
            }

            var deed = await _context.Deeds.FirstAsync(d => d.Id == periodDto.DeedId);
            if (deed.UserId != user.Id)
            {
                throw new Exception("Deed does not belong to you");
            }

            period = periodDto.ToPeriod();
            _context.Periods.Update(period);
            await _context.SaveChangesAsync();
        }

        public async Task RemovePeriod(string userName, long periodId)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);

            var period = await _context.Periods.Include(p => p.Deed)
                                         .FirstAsync(p => p.Id == periodId);

            if (period.Deed.UserId != user.Id)
            {
                throw new Exception("Period does not belong to you");
            }

            _context.Periods.Remove(period);
            await _context.SaveChangesAsync();
        }

        public async Task<PeriodDto> GetLastPeriod(string userName)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var period = await _context.Periods.OrderBy(p => p.DateCreate)
                                               .LastOrDefaultAsync(p => p.Deed.UserId == user.Id);
            return period != null ? new PeriodDto(period) : null;
        }

        public async Task<IEnumerable<PeriodDto>> GetPeriodsByDate(string userName, DateTime from, DateTime to)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var periods = await _context.Periods.Where(p => p.Deed.UserId == user.Id &&
                                                            p.StartDate.Date > from.Date &&
                                                            p.StartDate.Date < to.Date)
                                                .OrderByDescending(p => p.DateCreate)
                                                .ToListAsync();
            var periodDtos = periods.Select(p => new PeriodDto(p));
            return periodDtos;
        }

        public async Task<int> GetPeriodsCountByDate(string userName, DateTime from, DateTime to)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var periodsCount = await _context.Periods.Where(p => p.Deed.UserId == user.Id &&
                                                            p.StartDate.Date > from.Date &&
                                                            p.StartDate.Date < to.Date)
                                                     .CountAsync();
            return periodsCount;
        }

        // TODO: Поправить отправку в базу
        private async Task SetPurposesStatus(long deedId)
        {
            var failedPurposeStatus = _context.PurposeStatuses.FirstAsync(ps => ps.Code == ReferencesCodes.PURPOSE_FAILED);
            var completePurposeStatus = _context.PurposeStatuses.FirstAsync(ps => ps.Code == ReferencesCodes.PURPOSE_COMPLETE);
            var purposes = await _context.Purposes.Where(p => p.DeedId == deedId &&
                                                        p.PurposeStatus.Code == ReferencesCodes.PURPOSE_NEW &&
                                                        p.DateEnd <= DateTime.UtcNow)
                                                  .ToListAsync();

            //failed
            purposes.Where(p => p.Deed.Periods.Where(pe => pe.EndDate.HasValue)
                                              .Select(pe => pe.EndDate.Value - pe.StartDate)
                                              .Sum(pe => pe.TotalHours) < p.RequiredHours);
            foreach (var p in purposes)
            {
                var isFailed = _context.Deeds.First(d => d.Id == p.DeedId).Periods.Where(pe => pe.EndDate.HasValue)
                                              .Select(pe => pe.EndDate.Value - pe.StartDate)
                                              .Sum(pe => pe.TotalHours) < p.RequiredHours;
                p.PurposeStatusId = failedPurposeStatus.Id;
            }

            //await _context.Purposes.Where(p => p.PurposeStatus.Code == ReferencesCodes.PURPOSE_NEW &&
            //                                   p.DeedId == deedId &&
            //                                   p.DateEnd <= DateTime.UtcNow &&
            //                                   p.Deed.Periods.Where(pe => pe.EndDate.HasValue)
            //                                                 .Select(pe => pe.EndDate.Value - pe.StartDate)
            //                                                 .Sum(pe => pe.TotalHours) < p.RequiredHours)
            //                       .ForEachAsync(p => p.PurposeStatusId = failedPurposeStatus.Id);

            //complete
            await _context.Purposes.Where(p => p.DeedId == deedId &&
                                               p.DateEnd <= DateTime.UtcNow &&
                                               p.Deed.Periods.Where(pe => pe.EndDate.HasValue)
                                                             .Select(pe => pe.EndDate.Value - pe.StartDate)
                                                             .Sum(pe => pe.TotalHours) < p.RequiredHours)
                                   .ForEachAsync(p => p.PurposeStatusId = failedPurposeStatus.Id);

            await _context.SaveChangesAsync();
        }
    }
}
