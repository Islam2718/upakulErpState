using Global.Domain.Models;
using Global.Domain.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace Global.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetBankType), new[] { typeof(string) })!)
                .HasName("ufn_BankType");
            modelBuilder.Entity<VWGeoLocation>().HasNoKey().ToView(null);
            modelBuilder.Entity<VWOffice>().HasNoKey().ToView(null);

            base.OnModelCreating(modelBuilder);
        }

        #region DB Set
        public DbSet<GeoLocation> geoLocations { get; set; }
        public DbSet<Office> offices { get; set; }
        public DbSet<Bank> banks { get; set; }
        public DbSet<Country> countries { get; set; }
        #endregion DB Set

        #region View
        public DbSet<VWGeoLocation> vwGeoLocations { get; set; }
        public DbSet<VWOffice> vw_Office { get; set; }
        #endregion view
        
        #region    Function
        public string GetBankType(string val)
        => throw new InvalidOperationException();
        #endregion Function
    }
}
