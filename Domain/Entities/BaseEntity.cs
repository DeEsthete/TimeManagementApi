using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
    }
}
