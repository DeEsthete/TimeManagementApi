using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Deed : BaseEntity
    {
        public long UserId { get; set; }
        public AppUser User { get; set; }
        public string Name { get; set; }
        public IEnumerable<Period> Periods { get; set; }
        public IEnumerable<Purpose> Purposes { get; set; }
    }
}
