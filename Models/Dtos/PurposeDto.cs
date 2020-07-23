using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class PurposeDto
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        public string Name { get; set; }
        public long PurposeStatusId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public long DeedId { get; set; }
        public int RequiredHours { get; set; }
        public int HaveHours { get; set; }

        public PurposeDto()
        {

        }

        public PurposeDto(Purpose purpose)
        {
            Id = purpose.Id;
            DateCreate = purpose.DateCreate;
            DateUpdate = purpose.DateUpdate;
            Name = purpose.Name;
            PurposeStatusId = purpose.PurposeStatusId;
            DateStart = purpose.DateStart;
            DateEnd = purpose.DateEnd;
            DeedId = purpose.DeedId;
            RequiredHours = purpose.RequiredHours;
        }

        public Purpose ToPurpose()
        {
            return new Purpose
            {
                Id = Id,
                DateCreate = DateCreate,
                DateUpdate = DateUpdate,
                Name = Name,
                PurposeStatusId = PurposeStatusId,
                DateStart = DateStart,
                DateEnd = DateEnd,
                DeedId = DeedId,
                RequiredHours = RequiredHours
            };
        }
    }
}
