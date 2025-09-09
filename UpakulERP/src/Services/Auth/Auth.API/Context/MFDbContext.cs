using Auth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Context
{
    public class MFDbContext : DbContext
    {
        public MFDbContext(DbContextOptions<MFDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var config = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json")
        //            .Build();
        //        optionsBuilder.UseSqlServer(config.GetConnectionString("MFConnection"));
        //    }

        //}
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DailyProcess> dailyProcesses { get; set; }
    }
}
