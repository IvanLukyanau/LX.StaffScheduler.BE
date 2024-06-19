using LX.StaffScheduler.BLL.Services.Common;
using LX.StaffScheduler.BLL.Services.Interfaces;
using LX.StaffScheduler.DAL.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace LX.StaffScheduler.BLL.DependencyInjection
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureServices(this IServiceCollection services, string connectionString)
        {
            services.ConfigureSources(connectionString);
            services.AddTransient<ICityService, CityService>();
        }
    }
}
