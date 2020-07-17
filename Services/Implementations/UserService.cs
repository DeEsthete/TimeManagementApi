using Domain.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Dtos;
using Services.Interfaces;
using Shared;
using Shared.Configs;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TokenDto> Authenticate(string username, string password)
        {
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                throw new ArgumentException("Invalid username or password.");
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenDto
            {
                AccessToken = encodedJwt,
                UserName = identity.Name
            };

            return response;
        }

        public async Task Registration(RegistrationDto model)
        {
            var users = _context.Users;
            if (string.IsNullOrWhiteSpace(model.UserName) || model.UserName.Length < 6)
            {
                throw new ArgumentException("Username is incorrect");
            }
            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 6)
            {
                throw new ArgumentException("Password is incorrect");
            }
            if (await users.AnyAsync(u => u.UserName == model.UserName))
            {
                throw new ArgumentException("Username already exist");
            }
            var user = new AppUser
            {
                Nickname = model.Nickname,
                UserName = model.UserName,
                PasswordHash = PasswordHasher.HashPassword(model.Password),
                Role = Roles.USER
            };
            users.Add(user);
            await _context.SaveChangesAsync();
        }

        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user != null)
            {
                if (PasswordHasher.VerifyHashedPassword(user.PasswordHash, password))
                {
                    var claims = new List<Claim> {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
                    };
                    ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }

            return null;
        }
    }
}
