using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class ScheduleDto
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public DateTime ScheduleDate { get; set; }

        public ScheduleDto()
        {

        }

        public ScheduleDto(Schedule schedule)
        {
            Id = schedule.Id;
            DateCreate = schedule.DateCreate;
            DateUpdate = schedule.DateUpdate;
            ScheduleDate = schedule.ScheduleDate;
        }

        public Schedule ToSchedule()
        {
            return new Schedule
            {
                Id = Id,
                DateCreate = DateCreate,
                DateUpdate = DateUpdate,
                ScheduleDate = ScheduleDate
            };
        }
    }
}
