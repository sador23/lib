﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.DTO
{
    public class UserForLogin
    {
        public string UserName { get; set; }

        public string password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}