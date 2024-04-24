﻿// <auto-generated />
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Modulith.Modules.Users.Infrastructure.Data;

#pragma warning disable 219, 612, 618
#nullable disable

namespace Modulith.Modules.Users.Infrastructures.Data.CompiledModels
{
    [DbContext(typeof(UserDbContext))]
    public partial class UserDbContextModel : RuntimeModel
    {
        private static readonly bool _useOldBehavior31751 =
            System.AppContext.TryGetSwitch("Microsoft.EntityFrameworkCore.Issue31751", out var enabled31751) && enabled31751;

        static UserDbContextModel()
        {
            var model = new UserDbContextModel();

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

        private static UserDbContextModel _instance;
        public static IModel Instance => _instance;

        partial void Initialize();

        partial void Customize();
    }
}
