using Domain.Entities;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IUserService
    {
        Task<TokenDto> Authenticate(string username, string password);
        Task Registration(RegistrationDto model);
    }
}
