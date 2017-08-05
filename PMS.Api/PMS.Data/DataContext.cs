using Microsoft.EntityFrameworkCore;
using PMS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Data
{
    public class DataContext : DbContext
    {
        public DataContext() {  }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<PlayerTeam> PlayerTeams { get; set; }
    }
}
