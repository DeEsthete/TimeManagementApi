using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class AppUser : BaseEntity
    {
        public string Nickname { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public IEnumerable<Deed> Actions { get; set; }
    }
}
