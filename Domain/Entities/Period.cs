using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Period : BaseEntity
    {
        public long ActionId { get; set; }
        public Deed Action { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
