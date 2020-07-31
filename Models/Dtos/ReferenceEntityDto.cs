using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dtos
{
    public class ReferenceEntityDto
    {
        public long Id { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ReferenceEntityDto()
        {

        }

        public ReferenceEntityDto(ReferenceEntity referenceEntity)
        {
            Id = referenceEntity.Id;
            DateCreate = referenceEntity.DateCreate;
            DateUpdate = referenceEntity.DateUpdate;
            Name = referenceEntity.Name;
            Code = referenceEntity.Code;
        }

        public ReferenceEntity ToReferenceEntity()
        {
            return new ReferenceEntity
            {
                Id = Id,
                DateCreate = DateCreate.GetValueOrDefault(),
                DateUpdate = DateUpdate.GetValueOrDefault(),
                Name = Name,
                Code = Code
            };
        }
    }
}
