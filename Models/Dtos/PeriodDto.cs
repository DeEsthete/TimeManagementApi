using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class PeriodDto
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public long DeedId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Description { get; set; }

        public PeriodDto()
        {

        }

        public PeriodDto(Period period)
        {
            Id = period.Id;
            DateCreate = period.DateCreate;
            DateUpdate = period.DateUpdate;
            DeedId = period.DeedId;
            StartDate = period.StartDate;
            EndDate = period.EndDate;
            Description = period.Description;
        }

        public Period ToPeriod()
        {
            return new Period
            {
                Id = Id,
                DateCreate = DateCreate,
                DateUpdate = DateUpdate,
                DeedId = DeedId,
                StartDate = StartDate,
                EndDate = EndDate,
                Description = Description
            };
        }
    }
}
