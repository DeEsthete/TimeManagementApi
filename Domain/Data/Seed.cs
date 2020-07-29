using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Shared.Constants;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class Seed
    {
        private AppDbContext _context;

        public Seed(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<AppDbContext>();
        }

        public async Task InitializeDatabase()
        {
            await CreateReferences();
            await CreateDefaultUsers();
            await CreateDefaultDeeds();
        }

        private async Task CreateDefaultUsers()
        {
            var users = _context.Users;
            List<AppUser> defaultUsers = new List<AppUser>
            {
                new AppUser
                {
                    Nickname = "DefaultUser",
                    UserName = "DefaultUser",
                    PasswordHash = PasswordHasher.HashPassword("DefaultUser"),
                    Role = Roles.USER
                }
            };

            users.AddRange(defaultUsers.Where(du => !users.Any(u => u.UserName == du.UserName)).ToList());
            await _context.SaveChangesAsync();
        }

        private async Task CreateReferences()
        {
            var purposeStatuses = _context.PurposeStatuses;
            List<PurposeStatus> defaultPurposeStatuses = new List<PurposeStatus>
            {
                new PurposeStatus
                {
                    Name = "New",
                    Code = ReferencesCodes.PURPOSE_NEW
                },
                new PurposeStatus
                {
                    Name = "Complete",
                    Code = ReferencesCodes.PURPOSE_COMPLETE
                },
                new PurposeStatus
                {
                    Name = "Failed",
                    Code = ReferencesCodes.PURPOSE_FAILED
                },
            };

            purposeStatuses.AddRange(defaultPurposeStatuses.Where(dps => !purposeStatuses.Any(ps => ps.Code == dps.Code)));
            await _context.SaveChangesAsync();
        }

        private async Task CreateDefaultDeeds()
        {
            var userId = _context.Users.First(u => u.UserName == "DefaultUser").Id;
            var deeds = _context.Deeds;
            List<Deed> defaultDeeds = new List<Deed>
            {
                new Deed
                {
                    UserId = userId,
                    Name = "Programming",
                },
                new Deed
                {
                    UserId = userId,
                    Name = "Reading",
                },
                new Deed
                {
                    UserId = userId,
                    Name = "Writing",
                },
                new Deed
                {
                    UserId = userId,
                    Name = "Sport",
                },
            };

            deeds.AddRange(defaultDeeds.Where(dd => !deeds.Any(d => d.Name == dd.Name)).ToList());
            await _context.SaveChangesAsync();
        }
    }
}
