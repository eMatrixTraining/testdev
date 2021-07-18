using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Serilog;
using TestDev.Data.Entities;
using TestDev.Data.Interfaces;

namespace TestDev.Services.Implementation
{
    public class AppDataSeederService : IAppDataSeederService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        private const string DEFAULT_ADMIN_EMAIL = "root@ematrix.com";
        private const string DEFAULT_PASSWORD = "Letmein!123";

        public AppDataSeederService(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IHostingEnvironment hostingEnvironment        
        )
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<bool> CreateDefaultUsers()
        {
            try
            {
                var identityResult = await CreateIdentityRoles();
                if (identityResult)
                {
                    await AddRootUser("Root", "Admin", DEFAULT_ADMIN_EMAIL, DEFAULT_PASSWORD);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }


        private async Task<bool> CreateIdentityRoles()
        {
            try
            {
                string[] roleNames = { "Root", "Admin", "Member" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await _roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Log.Logger.Debug("Create Identityroles: Exception: " + ex.InnerException);
            }

            return false;
        }

        private async Task AddRootUser(string firstName, string lastName, string userName, string password)
        {
            var newUser = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
                FirstName = firstName,
                LastName = lastName,
                EmailConfirmed = true,
                IsAdmin = false,
                IsRoot = true,
                IsDemoLogin = false,
            };

            var existingUser = await _userManager.FindByEmailAsync(password);
            if (existingUser == null)
            {
                var userResult = await _userManager.CreateAsync(newUser, password);
                if (userResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "Root");
                }
            }
        }

        
    }
}