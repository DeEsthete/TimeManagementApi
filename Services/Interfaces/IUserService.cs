using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Interfaces
{
    public interface IUserService
    {
        AppUser Authenticate(string username, string password);
    }
}
