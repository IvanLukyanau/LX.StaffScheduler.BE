using LX.StaffScheduler.DAL.Interfaces;
using LX.StaffScheduler.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LX.StaffScheduler.DAL.DependencyInjection
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureSources(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICafeRepository, CafeRepository>();
        }
    }
}
