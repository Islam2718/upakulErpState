using Accounts.Domain.Models;
using Accounts.Domain.Models.Voucher;
using Accounts.Domain.ViewModel;
using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;

namespace Accounts.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OfficeCommonVM>().HasNoKey();
            modelBuilder.Entity<BudgetEntry>()
                    .HasKey(be => new { be.FinancialYear, be.OfficeId, be.ComponentParentId, be.ComponentId });
            modelBuilder.Entity<BudgetComponentData>()
                .HasNoKey()/*
                .ToTable("vw_BudgetComponentData",schema: "budg")*/ ;
            base.OnModelCreating(modelBuilder);
        }
        #region DB Set
        public DbSet<BudgetComponent> component { get; set; }
        public DbSet<BudgetEntry> budgetentry { get; set; }
        public DbSet<BudgetComponentData> v_budgetComponentDatas { get; set; }
        #region Voucher
        public DbSet<AccountHead> accountheads { get; set; }
        public DbSet<OfficeXHeadAssign> officeXHeadAssigns { get; set; }
        public DbSet<VoucherMaster> voucherMasters { get; set; }
        public DbSet<VoucherDetail> voucherDetails { get; set; }
        #endregion
        #region Function
        [DbFunction("udf_OfficeHierarchical", "dbo")]
        public IQueryable<OfficeCommonVM> GetOfficeHierarchi(int officeid, int officeTypeid)
        => FromExpression(() => GetOfficeHierarchi(officeid, officeTypeid));
        #endregion
        #endregion DB Set
        #region Consumer DB Set
        public DbSet<CommonBank> banks { get; set; }
        #endregion Consumer DB Set
    }
}
