using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class SchedulePeriod : BaseEntity
    {
        public long ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public long DeedId { get; set; }
        public Deed Deed { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }
    }
}
