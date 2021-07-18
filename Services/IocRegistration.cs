using Microsoft.Extensions.DependencyInjection;
using TestDev.Services.Implementation;

namespace TestDev.Services
{
    public static class IocRegistration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAppDataSeederService, AppDataSeederService>();
        }
    }
}
