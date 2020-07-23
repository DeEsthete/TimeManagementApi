using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class UserDto
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Nickname { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }

        public UserDto()
        {
        }

        public UserDto(AppUser appUser)
        {
            Id = appUser.Id;
            DateCreate = appUser.DateCreate;
            DateUpdate = appUser.DateUpdate;
            Nickname = appUser.Nickname;
            UserName = appUser.UserName;
            Role = appUser.Role;
        }

        public AppUser ToAppUser()
        {
            return new AppUser
            {
                Id = Id,
                DateCreate = DateCreate,
                DateUpdate = DateUpdate,
                Nickname = Nickname,
                UserName = UserName,
                Role = Role
            };
        }
    }
}
