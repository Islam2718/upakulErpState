using MF.Domain.Models;
using MF.Domain.Models.Functions;
using MF.Domain.Models.Loan;
using MF.Domain.Models.Saving;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;

namespace MF.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Function
            modelBuilder.Entity<OfficeCommonVM>().HasNoKey();
            modelBuilder.Entity<LoanProposal_NextApprovalStatus>().HasNoKey();
            modelBuilder.Entity<RepaymentSchedule>().HasNoKey();
            //modelBuilder.HasDbFunction(
            //    typeof(AppDbContext).GetMethod(nameof(GetOfficeHierarchi), new[] { typeof(int), typeof(int) })!
            //)
            //.HasName("udf_OfficeHierarchical");


            #endregion Function

            modelBuilder.Entity<VwPurpose>().HasNoKey().ToView(null);
            modelBuilder.Entity<VwGroup>().HasNoKey().ToView(null);
            modelBuilder.Entity<VWMember>().HasNoKey().ToView(null);
            modelBuilder.Entity<VWmemberCommonData>().HasNoKey().ToView(null);
            modelBuilder.Entity<VWGraceSchedule>().HasNoKey().ToView(null);
            modelBuilder.Entity<VWLoanForm>().HasNoKey().ToView(null);
        }

        #region DB Set
        public DbSet<MF.Domain.Models.Group> groups { get; set; }
        public DbSet<MRAPurpose> mRAPurposes { get; set; }
        public DbSet<Purpose> mainPurposes { get; set; }

        public DbSet<DailyProcess> dailyProcess { get; set; }
        public DbSet<HolyDay> holidays { get; set; }
        public DbSet<Occupation> occupations { get; set; }
        public DbSet<Member> members { get; set; }

        public DbSet<LoanApproval> loanApprovals { get; set; }
        public DbSet<LoanApplication> loanProposals { get; set; }
        //public DbSet<LoanProposal> loanProposals { get; set; }

        public DbSet<MasterComponent> mastercomponents { get; set; }
        public DbSet<Component> components { get; set; }
        public DbSet<IdGenerate> codeGenerator { get; set; }
        public DbSet<BankAccountMapping> bankAccountMappings { get; set; }
        public DbSet<BankAccountCheque> bankAccountCheques { get; set; }
        public DbSet<BankAccountChequeDetails> bankAccountChequeDetails { get; set; }
        public DbSet<GraceSchedule> graceschedules { get; set; }
        public DbSet<OfficeComponentMapping> officeComponentMappingList { get; set; }
        public DbSet<GroupWiseEmployeeAssign> groupWiseEmployeeAssigns { get; set; }
        public DbSet<GeneralSavingSummary> generalSavingSummary { get; set; }
        public DbSet<GeneralSavingSummaryDetails> generalSavingSummaryDetails { get; set; }
        public DbSet<GroupCommittee> groupCommittees { get; set; }

        #region Saving
        public DbSet<GeneralSavingSummary> savingSummaries { get; set; }
        public DbSet<GeneralSavingSummaryDetails> savingsummaryDetails { get; set; }
        #endregion

        #region
        public DbSet<LoanSummary> loanSummaries { get; set; }
        public DbSet<LoanSummaryDetail> loanSummaryDetails { get; set; }
        #endregion

        #region View
        public DbSet<VwPurpose> vwPurposes { get; set; }
        public DbSet<VwGroup> vwGroups { get; set; }
        public DbSet<VWMember> vw_members_details { get; set; }
        public DbSet<VWmemberCommonData> vw_members { get; set; }
        public DbSet<VWGraceSchedule> vwGraceSchedules { get; set; }
        public DbSet<VWLoanForm> vwLoanForms { get; set; }
        #endregion view

        #region Function
        [DbFunction("udf_OfficeHierarchical", "dbo")]
        public IQueryable<OfficeCommonVM> GetOfficeHierarchi(int officeid, int officeTypeid)
        => FromExpression(() => GetOfficeHierarchi(officeid, officeTypeid));
        
        [DbFunction("udf_NextApprovalStatus", "loan")]
        public IQueryable<LoanProposal_NextApprovalStatus> NextApprovalStatusChecking(long loanApplicationId, int nextLevel,int proposedAmount)
       => FromExpression(() => NextApprovalStatusChecking(loanApplicationId, nextLevel, proposedAmount));
        
        [DbFunction("udf_RepaymentSchedule", "loan")]
        public IQueryable<RepaymentSchedule> RepaymentSchedule(int officeid, int groupId,long loanApplicationId,decimal principal,decimal interestRate,int loanPeriodMonth,int noOfSchedule,DateTime startDate,string scheduleType)
       => FromExpression(() => RepaymentSchedule(officeid, groupId, loanApplicationId, principal, interestRate, loanPeriodMonth, noOfSchedule, startDate, scheduleType));
        #endregion Function

        #endregion DB Set
        #region Consumer DB Set
        public DbSet<CommonOffice> offices { get; set; }
        public DbSet<CommonGeoLocation> geoLocations { get; set; }
        public DbSet<CommonBank> banks { get; set; }
        public DbSet<CommonEmployee> employees { get; set; }
        public DbSet<CommonDesignation> designations { get; set; }
        #endregion Consumer DB Set
    }
}
