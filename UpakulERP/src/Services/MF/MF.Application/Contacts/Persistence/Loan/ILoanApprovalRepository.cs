using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models.Loan;
using MF.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using Utility.Response;

namespace MF.Application.Contacts.Persistence.Loan
{
    public interface ILoanApprovalRepository : ICommonRepository<LoanApproval>
    {
        LoanApproval GetById(int level);
        List<LoanApproval> GetAll();
        bool IsValidEmployee(int employeeId, int currentLevel, string approvalType, int poposedAmt);
        IEnumerable<LoanApproval> GetMany(Expression<Func<LoanApproval, bool>> where);
        bool Delete(int level);
    }


}
