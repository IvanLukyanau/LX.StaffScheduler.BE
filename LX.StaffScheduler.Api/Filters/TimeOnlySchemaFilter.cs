using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LX.StaffScheduler.Api.Filters
{
    public class TimeOnlySchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TimeOnly))
            {
                schema.Type = "string";
                schema.Format = "time";
            }
        }
    }
}
