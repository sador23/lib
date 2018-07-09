using Library.API.DAL;
using Library.API.LibRole;
using Library.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Helper
{
    public class DbInit
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public DbInit(IServiceProvider serviceProvider, IConfiguration configuration, ILogger<DbInit> logger)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
            _logger = logger;
        }

        public async void CreateUser()
        {
            _logger.LogWarning("Enter CreateUser");
            using (var service = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                //var context = service.ServiceProvider.GetService<LibContext>();
                //var roleManager = service.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                var userManager = service.ServiceProvider.GetService<UserManager<User>>();

                string user = "rand@123.com";
                string pswd = "Kh%6kio";

                var success = await userManager.CreateAsync(new User()
                {
                    EmailConfirmed = true,
                    Email = user,
                    Firstname = "user",
                    Lastname = "user",
                    Status = "Active",
                    UserName = user
                }, pswd);
                if (success.Succeeded)
                {
                    _logger.LogWarning("Create user was ok");
                    await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user), "User");
                }
                _logger.LogWarning("End");
            }
        }

        public async void init()
        {
            using (var service = _serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = service.ServiceProvider.GetService<LibContext>();
                context.Database.EnsureCreated();
                

                var roleManager = service.ServiceProvider.GetService<RoleManager<ApplicationRole>>();
                if (! await (roleManager.RoleExistsAsync("Admin")))
                {
                    await roleManager.CreateAsync(new ApplicationRole("Administrator"));
                    await roleManager.CreateAsync(new ApplicationRole("User"));
                }

                var userManager = service.ServiceProvider.GetService<UserManager<User>>();

                string user = _configuration["UserSettings:UserEmail"];
                string pswd = _configuration["UserSettings:UserPassword"];

                var success = await userManager.CreateAsync(new User() {
                                                                        EmailConfirmed = true,
                                                                        Email = user,
                                                                        Firstname = "admin",
                                                                        Lastname = "admin",
                                                                        Status = "Active",
                                                                        UserName = user
                                                                        }, pswd);
                if (success.Succeeded)
                {
                    await userManager.AddToRoleAsync(await userManager.FindByNameAsync(user), "Administrator");
                }
            }

        }

    }
}
