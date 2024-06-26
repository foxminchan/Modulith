﻿// <auto-generated />
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using Modulith.Modules.Orders.Domain;
using Modulith.SharedKernel.Entities;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Npgsql.EntityFrameworkCore.PostgreSQL.Storage.Internal.Mapping;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Modulith.Modules.Orders.Infrastructures.Data.CompiledModels
{
    internal partial class OrderEntityType
    {
        public static RuntimeEntityType Create(RuntimeModel model, RuntimeEntityType baseEntityType = null)
        {
            var runtimeEntityType = model.AddEntityType(
                "Modulith.Modules.Orders.Domain.Order",
                typeof(Order),
                baseEntityType);

            var id = runtimeEntityType.AddProperty(
                "Id",
                typeof(Guid),
                propertyInfo: typeof(Order).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Order).GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                valueGenerated: ValueGenerated.OnAdd,
                afterSaveBehavior: PropertySaveBehavior.Throw,
                sentinel: new Guid("00000000-0000-0000-0000-000000000000"));
            id.TypeMapping = GuidTypeMapping.Default.Clone(
                comparer: new ValueComparer<Guid>(
                    (Guid v1, Guid v2) => v1 == v2,
                    (Guid v) => v.GetHashCode(),
                    (Guid v) => v),
                keyComparer: new ValueComparer<Guid>(
                    (Guid v1, Guid v2) => v1 == v2,
                    (Guid v) => v.GetHashCode(),
                    (Guid v) => v),
                providerValueComparer: new ValueComparer<Guid>(
                    (Guid v1, Guid v2) => v1 == v2,
                    (Guid v) => v.GetHashCode(),
                    (Guid v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "uuid"));
            id.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
            id.AddAnnotation("Relational:ColumnName", "id");
            id.AddAnnotation("Relational:DefaultValueSql", "uuid_generate_v4()");

            var code = runtimeEntityType.AddProperty(
                "Code",
                typeof(string),
                propertyInfo: typeof(Order).GetProperty("Code", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                fieldInfo: typeof(Order).GetField("<Code>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
                nullable: true,
                maxLength: 16);
            code.TypeMapping = NpgsqlStringTypeMapping.Default.Clone(
                comparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                keyComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                providerValueComparer: new ValueComparer<string>(
                    (string v1, string v2) => v1 == v2,
                    (string v) => v.GetHashCode(),
                    (string v) => v),
                mappingInfo: new RelationalTypeMappingInfo(
                    storeTypeName: "character varying(16)",
                    size: 16));
            code.TypeMapping = ((NpgsqlStringTypeMapping)code.TypeMapping).Clone(npgsqlDbType: NpgsqlTypes.NpgsqlDbType.Varchar);
        code.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        code.AddAnnotation("Relational:ColumnName", "code");

        var createdDate = runtimeEntityType.AddProperty(
            "CreatedDate",
            typeof(DateTime),
            propertyInfo: typeof(EntityBase).GetProperty("CreatedDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(EntityBase).GetField("<CreatedDate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            valueGenerated: ValueGenerated.OnAdd,
            sentinel: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        createdDate.TypeMapping = NpgsqlTimestampTzTypeMapping.Default.Clone(
            comparer: new ValueComparer<DateTime>(
                (DateTime v1, DateTime v2) => v1.Equals(v2),
                (DateTime v) => v.GetHashCode(),
                (DateTime v) => v),
            keyComparer: new ValueComparer<DateTime>(
                (DateTime v1, DateTime v2) => v1.Equals(v2),
                (DateTime v) => v.GetHashCode(),
                (DateTime v) => v),
            providerValueComparer: new ValueComparer<DateTime>(
                (DateTime v1, DateTime v2) => v1.Equals(v2),
                (DateTime v) => v.GetHashCode(),
                (DateTime v) => v));
        createdDate.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        createdDate.AddAnnotation("Relational:ColumnName", "created_date");
        createdDate.AddAnnotation("Relational:DefaultValue", new DateTime(2024, 4, 23, 6, 40, 40, 788, DateTimeKind.Utc).AddTicks(6046));

        var customerId = runtimeEntityType.AddProperty(
            "CustomerId",
            typeof(Guid?),
            propertyInfo: typeof(Order).GetProperty("CustomerId", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(Order).GetField("<CustomerId>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true);
        customerId.TypeMapping = GuidTypeMapping.Default.Clone(
            comparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) => v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            keyComparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) => v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            providerValueComparer: new ValueComparer<Guid?>(
                (Nullable<Guid> v1, Nullable<Guid> v2) => v1.HasValue && v2.HasValue && (Guid)v1 == (Guid)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<Guid> v) => v.HasValue ? ((Guid)v).GetHashCode() : 0,
                (Nullable<Guid> v) => v.HasValue ? (Nullable<Guid>)(Guid)v : default(Nullable<Guid>)),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "uuid"));
        customerId.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        customerId.AddAnnotation("Relational:ColumnName", "customer_id");

        var updateDate = runtimeEntityType.AddProperty(
            "UpdateDate",
            typeof(DateTime?),
            propertyInfo: typeof(EntityBase).GetProperty("UpdateDate", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(EntityBase).GetField("<UpdateDate>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            nullable: true,
            valueGenerated: ValueGenerated.OnAdd);
        updateDate.TypeMapping = NpgsqlTimestampTzTypeMapping.Default.Clone(
            comparer: new ValueComparer<DateTime?>(
                (Nullable<DateTime> v1, Nullable<DateTime> v2) => v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
                (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)),
            keyComparer: new ValueComparer<DateTime?>(
                (Nullable<DateTime> v1, Nullable<DateTime> v2) => v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
                (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)),
            providerValueComparer: new ValueComparer<DateTime?>(
                (Nullable<DateTime> v1, Nullable<DateTime> v2) => v1.HasValue && v2.HasValue && (DateTime)v1 == (DateTime)v2 || !v1.HasValue && !v2.HasValue,
                (Nullable<DateTime> v) => v.HasValue ? ((DateTime)v).GetHashCode() : 0,
                (Nullable<DateTime> v) => v.HasValue ? (Nullable<DateTime>)(DateTime)v : default(Nullable<DateTime>)));
        updateDate.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        updateDate.AddAnnotation("Relational:ColumnName", "update_date");
        updateDate.AddAnnotation("Relational:DefaultValue", new DateTime(2024, 4, 23, 6, 40, 40, 788, DateTimeKind.Utc).AddTicks(6268));

        var version = runtimeEntityType.AddProperty(
            "Version",
            typeof(Guid),
            propertyInfo: typeof(EntityBase).GetProperty("Version", BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            fieldInfo: typeof(EntityBase).GetField("<Version>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly),
            concurrencyToken: true,
            valueGenerated: ValueGenerated.OnAdd,
            sentinel: new Guid("00000000-0000-0000-0000-000000000000"));
        version.TypeMapping = GuidTypeMapping.Default.Clone(
            comparer: new ValueComparer<Guid>(
                (Guid v1, Guid v2) => v1 == v2,
                (Guid v) => v.GetHashCode(),
                (Guid v) => v),
            keyComparer: new ValueComparer<Guid>(
                (Guid v1, Guid v2) => v1 == v2,
                (Guid v) => v.GetHashCode(),
                (Guid v) => v),
            providerValueComparer: new ValueComparer<Guid>(
                (Guid v1, Guid v2) => v1 == v2,
                (Guid v) => v.GetHashCode(),
                (Guid v) => v),
            mappingInfo: new RelationalTypeMappingInfo(
                storeTypeName: "uuid"));
        version.AddAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.None);
        version.AddAnnotation("Relational:ColumnName", "version");
        version.AddAnnotation("Relational:DefaultValue", new Guid("9f1b85d1-21b2-424c-b5dd-cf52dc1561fc"));

        var key = runtimeEntityType.AddKey(
            new[] { id });
        runtimeEntityType.SetPrimaryKey(key);
        key.AddAnnotation("Relational:Name", "pk_order");

        return runtimeEntityType;
    }

    public static void CreateAnnotations(RuntimeEntityType runtimeEntityType)
    {
        runtimeEntityType.AddAnnotation("Relational:FunctionName", null);
        runtimeEntityType.AddAnnotation("Relational:Schema", null);
        runtimeEntityType.AddAnnotation("Relational:SqlQuery", null);
        runtimeEntityType.AddAnnotation("Relational:TableName", "order");
        runtimeEntityType.AddAnnotation("Relational:ViewName", null);
        runtimeEntityType.AddAnnotation("Relational:ViewSchema", null);

        Customize(runtimeEntityType);
    }

    static partial void Customize(RuntimeEntityType runtimeEntityType);
}
}
