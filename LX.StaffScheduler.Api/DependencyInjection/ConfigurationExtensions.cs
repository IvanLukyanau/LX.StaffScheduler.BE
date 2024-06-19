using LX.StaffScheduler.BLL.DependencyInjection;
using LX.StaffScheduler.BLL.Services.Common;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.Interfaces;
using LX.StaffScheduler.DAL.Repositories;

namespace LX.StaffScheduler.Api.DependencyInjection
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureApp(this IServiceCollection services, string connectionString)
        {
            services.ConfigureServices(connectionString);
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICityService, CityService>();
        }
    }
}
