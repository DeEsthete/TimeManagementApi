using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Schedule : BaseEntity
    {
        public DateTime ScheduleDate { get; set; }
        public IEnumerable<SchedulePeriod> SchedulePeriods { get; set; }
    }
}
