using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Dtos
{
    public class DeedDto
    {
        public long Id { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdate { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsArchived { get; set; }

        public DeedDto()
        {

        }

        public DeedDto(Deed deed)
        {
            Id = deed.Id;
            DateCreate = deed.DateCreate;
            DateUpdate = deed.DateUpdate;
            UserId = deed.UserId;
            Name = deed.Name;
            IsArchived = deed.IsArchived;
        }

        public Deed ToDeed()
        {
            return new Deed
            {
                Id = Id,
                DateCreate = DateCreate,
                DateUpdate = DateUpdate,
                UserId = UserId,
                Name = Name,
                IsArchived = IsArchived
            };
        }
    }
}
