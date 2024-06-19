using LX.StaffScheduler.BLL.DependencyInjection;

namespace LX.StaffScheduler.Api.DependencyInjection
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureApp(this IServiceCollection services, string connectionString)
        {
            services.ConfigureServices(connectionString);
        }
    }
}
