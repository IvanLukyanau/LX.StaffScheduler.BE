using LX.StaffScheduler.BLL.Services.Common;
using LX.StaffScheduler.BLL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LX.StaffScheduler.DAL.DependencyInjection;

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
