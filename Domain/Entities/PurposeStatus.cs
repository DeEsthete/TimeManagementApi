﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PurposeStatus : ReferenceEntity
    {
        public IEnumerable<Purpose> Purposes { get; set; }
    }
}
