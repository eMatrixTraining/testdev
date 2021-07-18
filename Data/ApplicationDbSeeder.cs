using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TestDev.Data.Entities;
using TestDev.Services;

namespace TestDev
{
    public class ApplicationDbSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAppDataSeederService _appDataSeeder;
        
        private bool _seeded;
        private const string DEFAULT_ADMIN_EMAIL = "root@ematrix.com";

        public ApplicationDbSeeder(
            UserManager<ApplicationUser> userManager,
            IAppDataSeederService appDataSeeder
          )
        {
            // We take a dependency on the manager as we want to create a valid user
            _userManager = userManager;
            _appDataSeeder = appDataSeeder;

        }

        /// <summary>
        /// Performs the data store seeding of the demo user if it does not exist yet.
        /// </summary>
        /// <returns>A <c>bool</c> indicating whether the seeding has occurred.</returns>
        public async Task EnsureSeed()
        {
            if (!_seeded)
            {
                try
                {
                    // First we check if an existing user can be found for the configured demo credentials
                    var existingUser = await _userManager.FindByEmailAsync(DEFAULT_ADMIN_EMAIL);

                    if (existingUser != null)
                    {
                        Console.WriteLine("Database already seeded!");

                        _seeded = true;
                        return;
                    }


                    var seedResult = await _appDataSeeder.CreateDefaultUsers();

                    Console.WriteLine(seedResult ? "Database successfully seeded!" : "Database already seeded!");

                    _seeded = true;
                    return;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Error trying to seed the database");
                    Console.Error.WriteLine(ex);
                    return;
                }
            }

            // Notify the developer
            Console.WriteLine("Database already seeded!");
        }

        
    }
}