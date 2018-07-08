﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.DAL;
using Library.API.Helper;
using Library.API.LibRole;
using Library.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Library.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LibContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LibraryDb")));

            services.AddIdentity<User, ApplicationRole>().
                AddEntityFrameworkStores<LibContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Add Database Initializer
            services.AddScoped<DbInit>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DbInit dbInit)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseAuthentication();

            //dbInit.init();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}