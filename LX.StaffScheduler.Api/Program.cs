using LX.StaffScheduler.BLL.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using LX.StaffScheduler.Api.Filters;
using LX.StaffScheduler.Api.Converters;

namespace LX.StaffScheduler.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var LocalAllowSpecificOrigins = "_localAllowSpecificOrigins";

            var builder = WebApplication.CreateBuilder(args);

         
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });



            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: LocalAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.WithOrigins("http://localhost:4200",
                                          "https://blue-plant-0d3c2f103-32.westeurope.5.azurestaticapps.net",
                                          "https://blue-plant-0d3c2f103.5.azurestaticapps.net")
                                          .AllowAnyHeader().AllowAnyMethod();
                                  });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SchemaFilter<TimeOnlySchemaFilter>();
            });


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
