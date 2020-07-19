using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Purpose : BaseEntity
    {
        public string Name { get; set; }
        public long PurposeStatusId { get; set; }
        public PurposeStatus PurposeStatus { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public long ActionId { get; set; }
        public Deed Action { get; set; }
        public int RequiredHours { get; set; }
    }
}
