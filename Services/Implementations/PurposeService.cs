using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Services.Interfaces;
using Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class PurposeService : IPurposeService
    {
        private readonly AppDbContext _context;

        public PurposeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<long> AddPurpose(string userName, PurposeDto purposeDto)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var deed = await _context.Deeds.FirstAsync(d => d.Id == purposeDto.DeedId);
            if (deed.UserId != user.Id)
            {
                throw new Exception("Deed does not belong to you");
            }
            var purpose = purposeDto.ToPurpose();
            _context.Purposes.Add(purpose);
            await _context.SaveChangesAsync();
            return purpose.Id;
        }

        public async Task UpdatePurpose(string userName, PurposeDto purposeDto)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var purpose = await _context.Purposes.Include(p => p.Deed)
                                           .FirstAsync(p => p.Id == purposeDto.Id);

            if (purpose.Deed.UserId != user.Id)
            {
                throw new Exception("Period does not belong to you");
            }

            var deed = await _context.Deeds.FirstAsync(d => d.Id == purposeDto.DeedId);
            if (deed.UserId != user.Id)
            {
                throw new Exception("Deed does not belong to you");
            }

            purpose = purposeDto.ToPurpose();
            _context.Purposes.Update(purpose);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PurposeDto>> GetPurposesByStatus(string userName, string purposeStatusCode)
        {
            var user = await _context.Users.FirstAsync(u => u.UserName == userName);
            var purposes = await _context.Purposes.Where(p => p.Deed.UserId == user.Id &&
                                                              p.PurposeStatus.Code == purposeStatusCode)
                                                  .ToListAsync();
            var purposeDtos = purposes.Select(p => new PurposeDto(p));
            return purposeDtos;
        }
    }
}
