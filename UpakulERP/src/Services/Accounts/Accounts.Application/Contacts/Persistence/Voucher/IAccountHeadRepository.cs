using Accounts.Domain.Models.Voucher;
using Accounts.Domain.ViewModel;
using EF.Core.Repository.Interface.Repository;

namespace Accounts.Application.Contacts.Persistence.Voucher
{
    public interface IAccountHeadRepository : ICommonRepository<AccountHead>
    {
        AccountHead GetById(int id);
        List<AccountHeadXChildVM> GetAccountHeadDetails(int? pid, string requestType);
        Task<bool> OffficeAssignPost(List<HeadXOfficeAssignMapVM> modellst, int loggedinEmpId);
        Task<List<HeadXOfficeAssignVM>> GetOfficeAssignDetails(int loggedinOffice, int accountId);
    }
}
