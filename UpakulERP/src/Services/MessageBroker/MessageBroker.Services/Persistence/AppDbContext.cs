using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;

namespace MessageBroker.Services.Persistence
{
    public class AppDbContext: DbContext
    {
        private readonly string _connectionString;
        public AppDbContext(string connectionString)
        {
        this._connectionString = connectionString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString
                    /*,option=>option.EnableRetryOnFailure()*/);
        }
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #region Db Set
        public virtual DbSet<CommonBank> banks { get; set; }
        public virtual DbSet<CommonOffice> offices { get; set; }
        public virtual DbSet<CommonGeoLocation> geoLocations { get; set; }
        public virtual DbSet<CommonEmployee> employees { get; set; }
        public virtual DbSet<CommonDesignation> designations { get; set; }
        public virtual DbSet<CommonHoliday> holidays { get; set; }
        #endregion
    }
}
