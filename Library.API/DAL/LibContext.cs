﻿using Library.API.LibRole;
using Library.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.DAL
{
    public class LibContext : IdentityDbContext<User, ApplicationRole,Guid>
    {

        public LibContext(DbContextOptions<LibContext> options)
        : base(options)
        {
        }

        public DbSet<Book> books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Library.API.Models.User> User { get; set; }

    }

    
}
