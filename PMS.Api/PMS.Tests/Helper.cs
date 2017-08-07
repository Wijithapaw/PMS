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
        public static DbContextOptions<DataContext> GetContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                   .AddEntityFrameworkInMemoryDatabase()
                   .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase()
            .UseInternalServiceProvider(serviceProvider)
            .Options;

            return options;
        }

    }
}
