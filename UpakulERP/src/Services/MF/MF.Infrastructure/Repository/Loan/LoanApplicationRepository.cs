using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence.Loan;
using MF.Domain.Models.Functions;
using MF.Domain.Models.Loan;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Utility.Response;

namespace MF.Infrastructure.Repository.Loan
{
    public class LoanApplicationRepository : CommonRepository<LoanApplication>, ILoanApplicationRepository
    {
        AppDbContext _context;

        public LoanApplicationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public LoanApplication GetById(long id)
        {
            var obj = _context.loanProposals.FirstOrDefault(c => c.IsActive && c.LoanApplicationId == id);
            return obj;
        }

        public LoanFormVM GetLoanForm(long loanId,long loanSummaryId)
        {
            var lst = (from lf in _context.vwLoanForms
                      join mem in _context.vw_members_details on lf.MemberCode equals mem.MemberCode
                      where lf.LoanApplicationId==loanId && (lf.SummaryId??0)==loanSummaryId
                      select new LoanFormVM
                      {
                          AdmissionDate=mem.AdmissionDate,
                          ApplicationDate=lf.ApplicationDate,
                          ApplicationNo=lf.ApplicationNo,
                          ApplicationStatus=lf.ApplicationStatus,
                          Component=lf.Component,
                          ContactNoOwn=lf.ContactNoOwn,
                          DisburseDate=lf.DisburseDate,
                          Emp_SelfFullTimeFemale=lf.Emp_SelfFullTimeFemale,
                          Emp_SelfFullTimeMale=lf.Emp_SelfFullTimeMale,
                          Emp_SelfPartTimeFemale=lf.Emp_SelfPartTimeFemale,
                          Emp_SelfPartTimeMale= lf.Emp_SelfPartTimeMale,
                          Emp_WageFullTimeFemale=lf.Emp_WageFullTimeFemale,
                          Emp_WageFullTimeMale=lf.Emp_WageFullTimeMale,
                          Emp_WagePartTimeFemale=lf.Emp_WagePartTimeFemale,
                          Emp_WagePartTimeMale=lf.Emp_WagePartTimeMale,
                          GroupCode=lf.GroupCode,
                          GroupName=lf.GroupName,
                          MemberCode=lf.MemberCode,
                          MemberName=lf.MemberName,
                          FatherName=mem.FatherName,
                          MotherName = mem.MotherName,
                          SpouseName = mem.SpouseName,
                          MobileNumber =lf.MobileNumber,
                          NationalId=lf.NationalId,
                          OfficeCode=lf.OfficeCode,
                          OfficeName=lf.OfficeName,
                          TotalIncome=lf.TotalIncome,
                          PermanentAddress=mem.PermanentAddress,
                          PermanentDistrict=mem.PermanentDistrict,
                          PermanentDivision=mem.PermanentDivision,
                          PermanentUnion=mem.PermanentUnion,
                          PermanentUpazila=mem.PermanentUpazila,
                          PermanentVillage=mem.PermanentVillage,
                          PhaseNumber=lf.PhaseNumber,
                          PresentAddress=mem.PresentAddress,
                          PresentDistrict=mem.PresentDistrict,
                          PresentDivision=mem.PresentDivision,
                          PresentUnion=mem.PresentUnion,
                          PresentUpazila=mem.PresentUpazila,
                          PresentVillage=mem.PresentVillage,
                          PrincipleAmount=lf.PrincipleAmount,
                          ProposedAmount=lf.ProposedAmount,
                          PurposeName=lf.PurposeName,
                      });
            if (lst.Any())
                return lst.First();
            else return new LoanFormVM();
        }

        public IEnumerable<LoanApplication> GetMany(Expression<Func<LoanApplication, bool>> where)
        {
            var entities = _context.loanProposals.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<LoanApplicationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder, int logedInofficeId)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "ApplicationNo.Contains(@0) OR MemberName.Contains(@0)";  // OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "LoanApplicationId" : sortOrder;
            var query = (from lp in _context.loanProposals
                        join mem in _context.members on lp.MemberId equals mem.MemberId
                        join g in _context.groups on lp.GroupId equals g.GroupId
                        join c in _context.components on lp.ComponentId equals c.Id
                        join p in _context.mainPurposes on lp.PurposeId equals p.Id
                        where lp.IsActive && lp.OfficeId == logedInofficeId
                        select new LoanApplicationVM
                        {
                            LoanApplicationId = lp.LoanApplicationId,
                            ApplicationNo = lp.ApplicationNo,
                            MemberName = mem.MemberCode+" - "+mem.MemberName,
                            GroupName = g.GroupCode+" - "+g.GroupName,
                            PurposeName=p.Code + " - " + p.Name,
                            PhaseNumber = lp.PhaseNumber,
                            ApplicationDate = lp.ApplicationDate,
                            ProposedBy = lp.ProposedBy,
                            ProposedAmount = lp.ProposedAmount,
                            ApplicationStatus = lp.ApplicationStatus,
                            ApprovedLevel = lp.ApprovedLevel,
                        }).Where(src_qry, search).OrderBy(sortOrder).AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var obj = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<LoanApplicationVM>(obj, totalRecords);
        }

        public LoanProposal_NextApprovalStatus GetNextApprovalStatus(long appId,int level,int proposedAmount)
        {
            var lst=_context.NextApprovalStatusChecking(appId, level, proposedAmount).ToList();
            if (lst.Any())
                return lst.First();
            else return new LoanProposal_NextApprovalStatus();
        }
    }
}