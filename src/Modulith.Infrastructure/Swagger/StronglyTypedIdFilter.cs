using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Modulith.Infrastructure.Swagger;

public class StronglyTypedIdFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.Name.EndsWith("Id") ||
            !context.Type.IsValueType ||
            context.Type.GetCustomAttribute(typeof(TypeConverterAttribute)) is not TypeConverterAttribute attr ||
            Type.GetType(attr.ConverterTypeName) is not { } type)
            return;

        if (Activator.CreateInstance(type) is not TypeConverter converter) return;

        if (!converter.CanConvertTo(typeof(Guid)) && !converter.CanConvertTo(typeof(string))) return;

        schema.Type = "string";
        schema.Format = "uuid";
        schema.Example = new OpenApiString(Guid.NewGuid().ToString());
    }
}