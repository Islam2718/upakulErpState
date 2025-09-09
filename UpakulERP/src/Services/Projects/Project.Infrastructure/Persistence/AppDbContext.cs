using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;
using Project.Domain.Models;

namespace Project.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #region DB Set
        public DbSet<Doner> doners { get; set; }
        public DbSet<Projects> projects { get; set; }
        public DbSet<CommonEmployee> employees { get; set; }
        public DbSet<ActivityPlan> activityPlans { get; set; }
        #endregion DB Set
        #region Consumer DB Set
        public DbSet<CommonCountry> countries { get; set; }
        public DbSet<CommonBank> banks { get; set; }
        #endregion Consumer DB Set
    }
}
