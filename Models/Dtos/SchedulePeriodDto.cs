using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class SchedulePeriodDto
    {
        public long? Id { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public long ScheduleId { get; set; }
        public long DeedId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }

        public SchedulePeriodDto()
        {

        }

        public SchedulePeriodDto(SchedulePeriod schedulePeriod)
        {
            Id = schedulePeriod.Id;
            DateCreate = schedulePeriod.DateCreate;
            DateUpdate = schedulePeriod.DateUpdate;
            ScheduleId = schedulePeriod.ScheduleId;
            DeedId = schedulePeriod.DeedId;
            StartDate = schedulePeriod.StartDate;
            EndDate = schedulePeriod.EndDate;
            Description = schedulePeriod.Description;
        }

        public SchedulePeriod ToSchedulePeriod()
        {
            return new SchedulePeriod
            {
                Id = Id.GetValueOrDefault(),
                DateCreate = DateCreate.GetValueOrDefault(),
                DateUpdate = DateUpdate.GetValueOrDefault(),
                ScheduleId = ScheduleId,
                DeedId = DeedId,
                StartDate = StartDate,
                EndDate = EndDate,
                Description = Description
            };
        }
    }
}
