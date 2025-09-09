using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Domain.Models.Voucher;
using Accounts.Domain.ViewModel;
using Accounts.Infrastructure.Persistence;
using AutoMapper;
using EF.Core.Repository.Repository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Utility.Enums;

namespace Accounts.Infrastructure.Repository.Voucher
{
    public class AccountHeadRepository : CommonRepository<AccountHead>, IAccountHeadRepository
    {
        AppDbContext _context;
        IMapper _mapper;
        public AccountHeadRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }
        public AccountHead GetById(int id)
        {
            var obj = _context.accountheads.FirstOrDefault(c => c.IsActive && c.AccountId == id);
            return obj;
        }
        public List<AccountHeadXChildVM> GetAccountHeadDetails(int? pid, string requestType)
        {
            var lst = new List<AccountHeadXChildVM>();
            if (requestType == "L" && (pid ?? 0) == 0) // Page load time 4 head send
            {
                var _lst = _context.accountheads.Where(x => x.IsActive && x.ParentId == null).ToList();
                foreach (var l in _lst)
                {
                    lst.Add(
                        new AccountHeadXChildVM
                        {
                            HeadCode = l.HeadCode,
                            AccountId = l.AccountId,
                            HeadName = l.HeadName,
                            IsTransactable = l.IsTransactable,
                            ParentId = l.ParentId,
                        });
                }
                //lst = HeadMapping(_lst);
            }
            else if (requestType == "E") // Expand time all data 
            {
                var _lst = _context.accountheads.Where(x => x.IsActive).ToList();
                lst = BuildTree(_lst, 0);
            }
            else if ((pid ?? 0) > 0)
            {
                var _lst = _context.accountheads.Where(x => x.IsActive && x.ParentId == (pid ?? 0)).ToList();
                foreach (var l in _lst)
                {
                    lst.Add(
                        new AccountHeadXChildVM
                        {
                            HeadCode = l.HeadCode,
                            AccountId = l.AccountId,
                            HeadName = l.HeadName,
                            IsTransactable = l.IsTransactable,
                            ParentId = l.ParentId,
                        });
                }
                //lst = HeadMapping(_lst);
            }
            return lst;
        }
        private List<AccountHeadXChildVM> BuildTree(List<AccountHead> heads, int pid)
        {
            var nodes = heads
                .Where(x => (x.ParentId ?? 0) == pid)
                .Select(x => new AccountHeadXChildVM
                {
                    AccountId = x.AccountId,
                    HeadName = x.HeadName,
                    HeadCode = x.HeadCode,
                    ParentId = x.ParentId,
                    IsTransactable = x.IsTransactable,
                    child = BuildTree(heads, x.AccountId) // Recursive call
                })
                .ToList();
            return nodes;
        }

        /*Office Wise Head Assign*/
        #region
        public async Task<bool> OffficeAssignPost(List<HeadXOfficeAssignMapVM> modellst, int loggedinEmpId)
        {
            try
            {
                int accId = modellst.First().AccountId;
                var lst = _context.officeXHeadAssigns.Where(c => c.IsActive && c.AccountId == accId).ToList();
                if (lst.Any())
                    lst.ForEach(c => { c.IsActive = false; c.DeletedOn = DateTime.Now; c.DeletedBy = loggedinEmpId; });
                foreach (var l in modellst)
                {
                    if (lst.Any(x => x.OfficeId == l.OfficeId))
                    {
                        var obj = lst.Where(x => x.OfficeId == l.OfficeId).First();
                        obj.IsActive = true;
                        obj.DeletedBy = null;
                        obj.DeletedOn = null;
                    }
                    else
                    {
                        _context.officeXHeadAssigns.Add(new OfficeXHeadAssign
                        {
                            AccountId = l.AccountId,
                            OfficeId = l.OfficeId,
                            CreatedBy = loggedinEmpId,
                            CreatedOn = DateTime.UtcNow,
                            IsActive=true
                        });
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<HeadXOfficeAssignVM>> GetOfficeAssignDetails(int loggedinOffice, int accountId)
        {
            try
            {
                var prmLst = new List<object>();
                prmLst.Add(new SqlParameter("@officeId", SqlDbType.Int) { Value = loggedinOffice });
                prmLst.Add(new SqlParameter("@branchOfficeType", SqlDbType.Int) { Value = (int)OfficeType.OfficeTypeEnum.Branch });
                prmLst.Add(new SqlParameter("@proncipalOfficeType", SqlDbType.Int) { Value = (int)OfficeType.OfficeTypeEnum.Principal });
                prmLst.Add(new SqlParameter("@accountId", SqlDbType.Int) { Value = accountId });
                string sql = $"EXEC [acc].[udp_OfficeXHeadAssign_Dropdown] @officeId,@branchOfficeType,@proncipalOfficeType,@accountId";
                return await _context.Database.SqlQueryRaw<HeadXOfficeAssignVM>(sql, prmLst.ToArray()).ToListAsync();
            }
            catch
            {
                return new List<HeadXOfficeAssignVM>();
            }

        }
        #endregion
    }
}
