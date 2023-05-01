using System;
using Microsoft.EntityFrameworkCore;
using YemekTarifleri.Models;
//using Microsoft.Extensions.Logging;


namespace YemekTarifleri.DataBase
{
    public class ApplicationConnectionDb : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Recipe> recipes { get; set; }
        public DbSet<Comment> comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseMySql("server=database-1.cgoekeddamxk.eu-north-1.rds.amazonaws.com;port=3306;user=admin;password=2!5abA%9TqL*R&7;database=YemekTarifi", new MySqlServerVersion(new Version(8, 0, 32)))
                .UseLoggerFactory(LoggerFactory.Create(b => b
                .AddConsole()
                .AddFilter(level => level >= LogLevel.Information)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
