using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PMS.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Tests
{
    public static class Helper
    {
        //public static DbContextOptions<DataContext> GetContextOptions()
        //{
        //    var serviceProvider = new ServiceCollection()
        //           .AddEntityFrameworkInMemoryDatabase()
        //           .BuildServiceProvider();

        //    var options = new DbContextOptionsBuilder<DataContext>()
        //    .UseInMemoryDatabase()
        //    .UseInternalServiceProvider(serviceProvider)
        //    .Options;

        //    return options;
        //}

        public static SqliteConnection GetSqliteConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            return connection;
        }

        public static DbContextOptions<DataContext> GetSqliteContextOptions(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                   .UseSqlite(connection)
                   .Options;

            using (var context = new DataContext(options))
            {
                context.Database.EnsureCreated();
            }

            return options;
        }

    }
}
