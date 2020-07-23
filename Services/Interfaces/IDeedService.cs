using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IDeedService
    {
        Task<long> CreateDeed(string userName, DeedDto deedDto);
        Task UpdateDeed(string userName, DeedDto deedDto);
        Task<IEnumerable<DeedDto>> GetUserDeeds(string userName, bool isArchiveInclusive);
        Task<IEnumerable<DeedPeriodsCountDto>> GetDeedsPeriodsCount(string userName, DateTime from, DateTime to);
        Task ArchiveDeed(string userName, long id);
        Task UnarhiveDeed(string userName, long id);
    }
}
