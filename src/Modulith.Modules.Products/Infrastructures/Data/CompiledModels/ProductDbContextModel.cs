﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Modulith.Modules.Products.Infrastructures.Data.CompiledModels
{
    [DbContext(typeof(ProductDbContext))]
    public partial class ProductDbContextModel : RuntimeModel
    {
        private static readonly bool _useOldBehavior31751 =
            System.AppContext.TryGetSwitch("Microsoft.EntityFrameworkCore.Issue31751", out var enabled31751) && enabled31751;

        static ProductDbContextModel()
        {
            var model = new ProductDbContextModel();

            if (_useOldBehavior31751)
            {
                model.Initialize();
            }
            else
            {
                var thread = new System.Threading.Thread(RunInitialization, 10 * 1024 * 1024);
                thread.Start();
                thread.Join();

                void RunInitialization()
                {
                    model.Initialize();
                }
            }

            model.Customize();
            _instance = model;
        }

        private static ProductDbContextModel _instance;
        public static IModel Instance => _instance;

        partial void Initialize();

        partial void Customize();
    }
}
