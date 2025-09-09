using Auth.Api.Models;
using Auth.API.Models;
using Auth.API.Models.Functions;
using Auth.API.Models.View;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        #region 
        public DbSet<Office> offices { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<AspNetModules> modules { get; set; }
        public DbSet<RoleXModule> roleXModules { get; set; }
        public DbSet<UserMenu> userMenus { get; set; }
        public DbSet<RoleXMenu> roleXmenus { get; set; }
        public DbSet<MenuHierarchical> MenuHierarchicals { get; set; }
        //public DbSet<Roles> roles { get; set; }
        #endregion
        #region Function
        //public IQueryable<OfficeCommonField> GetOfficeHierarchi(int officeid, int officeTypeid)
        //=> FromExpression(() => GetOfficeHierarchi(officeid, officeTypeid));

        #endregion Function

        #region view
        public DbSet<VWEmployee> vw_employees { get; set; }
        #endregion
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers", "sec");
            modelBuilder.Entity<ApplicationRole>().ToTable("AspNetRoles", "sec");
            modelBuilder.Entity<MenuHierarchical>().HasNoKey().ToView(null);
            //modelBuilder.Entity<OfficeCommonField>().ToFunction("udf_OfficeHierarchical");
            //modelBuilder.Entity<OfficeCommonField>(eb =>
            //{
            //    eb.HasNoKey(); // Mark as keyless
            //    eb.ToFunction("udf_OfficeHierarchical"); // Bind to the function
            //});
           // modelBuilder.HasDbFunction(
           //    typeof(AppDbContext).GetMethod(nameof(GetOfficeHierarchi), new[] { typeof(int), typeof(int) })!
           //)
           //.HasName("udf_OfficeHierarchical");
        }
    }
}
