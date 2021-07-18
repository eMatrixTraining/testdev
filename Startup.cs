using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestDev.Data;
using TestDev.Data.Entities;
using TestDev.Data.Interfaces;
using TestDev.Services;
using UnitOfWork = TestDev.Data.UnitOfWork;


namespace TestDev
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            CurrentEnvironment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment CurrentEnvironment { get; }

        public static int UploadProgress { get; set; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = GetConnectionStringFor(CurrentEnvironment.EnvironmentName);

            services.AddMvc(option => option.EnableEndpointRouting = false);

            services.AddTransient<ApplicationDbSeeder>();

            services.AddEntityFrameworkSqlServer();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 8;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireDigit = true;
                })
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // IocRegistration.ConfigureServices(services, connectionString);
            IocRegistration.ConfigureServices(services);

            services.AddAuthentication();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ApplicationDbSeeder dbSeeder)
        {
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            if (!env.IsProduction())
            {
               dbSeeder.EnsureSeed().GetAwaiter().GetResult();
            }

            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo> { new CultureInfo("en-AU") },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-AU") },
                DefaultRequestCulture = new RequestCulture("en-AU"),
            };
            app.UseRequestLocalization(localizationOptions);

            app.UseAuthentication();

            app.UseStaticFiles();
            
            app.UseMvcWithDefaultRoute();

        }

        private string GetConnectionStringFor(string environmentName)
        {
            var connectionString = "";
            switch (environmentName)
            {
                case "Development":
                    connectionString = Configuration.GetConnectionString("Development");
                    break;

                case "Production":
                    connectionString = Configuration.GetConnectionString("Production");
                    break;

                default:
                    connectionString = Configuration.GetConnectionString("Development");
                    break;
            }

            return connectionString;
        }

    }
}
