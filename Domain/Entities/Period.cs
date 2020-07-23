using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Period : BaseEntity
    {
        public long DeedId { get; set; }
        public Deed Deed { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
