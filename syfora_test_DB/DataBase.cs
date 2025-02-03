using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using syfora_test_DB.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace syfora_test_DB
{
    internal class DataBase : DbContext
    {
        private readonly IConfiguration Config;
        public DataBase(IConfiguration config)
        {
            Config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Config.GetConnectionString("syfora_connection"));
        }
        public DbSet<User> Users { get; set; }
    }
}
