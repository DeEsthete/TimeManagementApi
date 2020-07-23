using Domain.Entities;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IPeriodService
    {
        Task<long> AddPeriod(string userName, PeriodDto periodDto);
        Task UpdatePeriod(string userName, PeriodDto periodDto);
        Task RemovePeriod(string userName, long periodId);
        Task<IEnumerable<PeriodDto>> GetPeriodsByDate(string userName, DateTime from, DateTime to);
        Task<PeriodDto> GetLastPeriod(string userName);
        Task<int> GetPeriodsCountByDate(string userName, DateTime from, DateTime to);
    }
}
