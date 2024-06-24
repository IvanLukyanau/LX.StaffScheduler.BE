using LX.StaffScheduler.BLL.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace LX.StaffScheduler.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var LocalAllowSpecificOrigins = "_localAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: LocalAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200", "https://blue-plant-0d3c2f103-32.westeurope.5.azurestaticapps.net").AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.ConfigureServices(connectionString);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(LocalAllowSpecificOrigins);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        
    }
}
