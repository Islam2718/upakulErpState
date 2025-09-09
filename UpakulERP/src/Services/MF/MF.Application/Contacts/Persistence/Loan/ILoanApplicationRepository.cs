using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models.Functions;
using MF.Domain.Models.Loan;
using MF.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace MF.Application.Contacts.Persistence.Loan
{
    public interface ILoanApplicationRepository : ICommonRepository<LoanApplication>
    {
        LoanApplication GetById(long id);
        LoanFormVM GetLoanForm(long loanId, long loanSummaryId);
        IEnumerable<LoanApplication> GetMany(Expression<Func<LoanApplication, bool>> where);
        Task<PaginatedResponse<LoanApplicationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder, int logedinOfficeId);
        ///Task<PaginatedResponse<LoadGridForLoanApproveVM>> LoadGridForLoanApprove(int page, int pageSize, string search, string sortOrder, int loggedInEmployeeId, int logedinOfficeId, int loggedInOfficeTypeId);
        //Task<bool> UpdateLoanWorkFlow(LoanApplication obj, int loggedInEmployeeId, int logedInOfficeId, int loggedInOfficeTypeId);
        LoanProposal_NextApprovalStatus GetNextApprovalStatus(long appId, int level, int proposedAmount);
    }
}
