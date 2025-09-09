using HRM.Domain.Models;
using HRM.Domain.Models.Test;
using HRM.Domain.Models.Training;
using HRM.Domain.Models.Views;
using Microsoft.EntityFrameworkCore;

namespace UpakulHRM.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // view build
            modelBuilder.Entity<VWEmployee>().HasNoKey().ToView(null);
            modelBuilder.Entity<VWHoliday>().HasNoKey().ToView(null);
        }
        #region DB Set
        public DbSet<Department> departments { get; set; }
        public DbSet<EmployeeStatus> employeeStatus { get; set; }
        public DbSet<EmployeeType> employeeType { get; set; }
        public DbSet<Education> educations { get; set; }
        public DbSet<BoardUniversity> boardUniversitys { get; set; }
        public DbSet<Designation> designations { get; set; }
        public DbSet<HoliDay> holidays { get; set; }
        public DbSet<Training> trainings { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<LeaveSetup> leavesetups { get; set; }
        public DbSet<OfficeTypeXConfigMaster> OfficeTypeXConfigMaster { get; set; }
        public DbSet<OfficeTypeXConfigureDetails> OfficeTypeXConfigureDetails { get; set; }


        #region View
        public DbSet<VWEmployee> vwEmployees { get; set; }
        public DbSet<VWHoliday> vwHolidays { get; set; }
        #endregion view
        #endregion DB Set

        // Test dbset
        public DbSet<FileUploadTest> fileUploadTests { get; set; }
  
    }
}
