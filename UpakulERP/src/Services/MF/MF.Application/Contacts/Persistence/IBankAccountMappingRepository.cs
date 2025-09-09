using EF.Core.Repository.Interface.Repository;
using MediatR;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Domain;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
    public interface IBankAccountMappingRepository : ICommonRepository<BankAccountMapping>
    {
        #region Mapping
        BankAccountMapping GetById(int id);
        bool ChequeBook(int id);
        List<BankAccountMapping> GetAll();
        List<CustomSelectListItem> GetOfficeXBankDropdown(int officeId);
        Task<PaginatedResponse<BankAccountMappingVM>> LoadGrid(int page, int pageSize, string search, string sortOrder,int? logedInOfficeId);
        IEnumerable<BankAccountMapping> GetMany(Expression<Func<BankAccountMapping, bool>> where);
        Task<OfficeBankAssignDropdownVM> GetOfficeBankAssignDropdownData(int officeId);
        #endregion
        #region Cheque Information
        //Check and CheckDetails
        Task<bool> AddChequeAsync(BankAccountCheque chequeObj);
        Task<List<CustomSelectListItem>> ChequeDetailsDropdown(int Id);
        Task<PaginatedResponse<BankAccountChequeDetailsVM>> LoadChequeDetailsGrid(int page, int pageSize, string search, string sortOrder, int? BankAccountMappingId);
        #endregion
    }
}
