﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Dtos
{
    public class AutheticateDto
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
