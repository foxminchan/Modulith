using System.Reflection;
using Ardalis.SmartEnum;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Modulith.Infrastructure.Swagger;

public sealed class SmartEnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var type = context.Type;

        if (!IsTypeDerivedFromGenericType(type, typeof(SmartEnum<>)) &&
            !IsTypeDerivedFromGenericType(type, typeof(SmartEnum<,>))) return;

        var enumValues = type.GetFields(
            BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy
        ).Select(d => d.Name);

        var openApiValues = new OpenApiArray();
        openApiValues.AddRange(enumValues.Select(d => new OpenApiString(d)));

        schema.Type = "string";
        schema.Enum = openApiValues;
        schema.Properties = null;
    }

    private static bool IsTypeDerivedFromGenericType(Type? typeToCheck, Type genericType)
    {
        while (true)
        {
            if (typeToCheck == typeof(object)) return false;

            if (typeToCheck is null) return false;

            if (typeToCheck.IsGenericType && typeToCheck.GetGenericTypeDefinition() == genericType) return true;

            typeToCheck = typeToCheck.BaseType;
        }
    }
}