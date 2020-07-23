using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class DeedService : IDeedService
    {
        private AppDbContext _context;

        public DeedService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<long> CreateDeed(string userName, DeedDto deedDto)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            deedDto.UserId = user.Id;
            var deed = deedDto.ToDeed();
            _context.Deeds.Add(deed);
            await _context.SaveChangesAsync();
            return deed.Id;
        }

        public async Task UpdateDeed(string userName, DeedDto deedDto)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            deedDto.UserId = user.Id;
            if (!_context.Deeds.Any(d => d.Id == deedDto.Id && d.UserId == deedDto.UserId))
            {
                throw new Exception("Deed with this id not found");
            }

            var deed = deedDto.ToDeed();
            _context.Deeds.Update(deed);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DeedDto>> GetUserDeeds(string userName, bool isArchiveInclusive)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            return _context.Deeds.Where(d => d.UserId == user.Id)
                                 .ToList()
                                 .Select(d => new DeedDto(d));
        }

        public async Task<IEnumerable<DeedPeriodsCountDto>> GetDeedsPeriodsCount(string userName, DateTime from, DateTime to)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var counts = _context.Deeds.Where(d => d.UserId == user.Id)
                                       .Select(d => new DeedPeriodsCountDto
                                       {
                                           DeedId = d.Id,
                                           PeriodsCount = d.Periods.Where(p => p.StartDate > from && p.StartDate < to).Count()
                                       });
            return counts;
        }

        public async Task ArchiveDeed(string userName, long id)
        {
            var deed = await _context.Deeds.Include(d => d.User)
                                           .FirstAsync(d => d.Id == id);

            if (deed.User.UserName != userName)
            {
                throw new UnauthorizedAccessException("Deed does not belong to this user");
            }

            deed.IsArchived = true;
            await _context.SaveChangesAsync();
        }

        public async Task UnarhiveDeed(string userName, long id)
        {
            var deed = await _context.Deeds.Include(d => d.User)
                                           .FirstAsync(d => d.Id == id);

            if (deed.User.UserName != userName)
            {
                throw new UnauthorizedAccessException("Deed does not belong to this user");
            }

            deed.IsArchived = false;
            await _context.SaveChangesAsync();
        }
    }
}
