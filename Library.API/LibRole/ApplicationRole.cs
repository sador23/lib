﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.LibRole
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base()
        { }
        public ApplicationRole(string roleName) : base(roleName)
        {
        }
    }
}
