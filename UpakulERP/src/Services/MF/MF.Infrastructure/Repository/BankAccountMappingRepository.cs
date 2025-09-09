using System.Data;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks.Dataflow;
using Dapper;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Enums;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Utility.Constants;
using Utility.Domain;
using Utility.Response;

namespace MF.Infrastructure.Repository
{
    class BankAccountMappingRepository : CommonRepository<BankAccountMapping>, IBankAccountMappingRepository
    {
        AppDbContext _context;
        private readonly string _connectionString;

        public BankAccountMappingRepository(AppDbContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public BankAccountMapping GetById(int id)
        {
            var obj = _context.bankAccountMappings.FirstOrDefault(c => c.IsActive && c.BankAccountMappingId == id);
            return obj;
        }

        public bool ChequeBook(int id)
        {
            var result = (from map in _context.bankAccountMappings
                          join cheque in _context.bankAccountCheques
                          on map.BankAccountMappingId equals cheque.BankAccountMappingId
                          where map.IsActive && cheque.IsActive && map.BankAccountMappingId == id
                          select new
                          {
                              map.BankAccountMappingId,
                              map.BankAccountNumber
                          }).ToList();
            if (result.Any())
                return true;
            else
                return false;
        }

        public List<BankAccountMapping> GetAll()
        {
            var objlst = _context.bankAccountMappings.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public async Task<PaginatedResponse<BankAccountMappingVM>> LoadGrid(int page, int pageSize, string search, string sortOrder, int? logedInOfficeId)
        {
            var query = from bm in _context.bankAccountMappings
                        join bnk in _context.banks on bm.BankId equals bnk.BankId
                        where bm.IsActive && bm.OfficeId == logedInOfficeId
                        select new BankAccountMappingVM
                        {
                            BankName = bnk.BankName,
                            BranchName = bm.BranchName,
                            BankAccountMappingId = bm.BankAccountMappingId,
                            BankId = bm.BankId,
                            BankAccountName = bm.BankAccountName,
                            BankAccountNumber = bm.BankAccountNumber,
                            RoutingNo = bm.RoutingNo,
                            IsRefData = _context.bankAccountCheques
                                            .Any(chq => chq.BankAccountMappingId == bm.BankAccountMappingId)
                        };

            int totalRecords = await query.CountAsync();

            var pageObj = await query
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResponse<BankAccountMappingVM>(pageObj, totalRecords);
        }

        public IEnumerable<BankAccountMapping> GetMany(Expression<Func<BankAccountMapping, bool>> where)
        {
            var entities = _context.bankAccountMappings.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public List<CustomSelectListItem> GetOfficeXBankDropdown(int officeId)
        {
            var lst = new List<CustomSelectListItem>();
            //lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "" });
            lst.AddRange((from m in _context.bankAccountMappings
                          join b in _context.banks on m.BankId equals b.BankId
                          where m.IsActive && b.IsActive && m.OfficeId == officeId
                          select new CustomSelectListItem
                          {
                              Text = b.BankShortCode + " - " + m.BankAccountName + " - " + m.BankAccountNumber,
                              Value = m.BankAccountMappingId.ToString()
                          }
                    ).OrderBy(x => x.Text));
            return lst;
        }

        public async Task<bool> AddChequeAsync(BankAccountCheque entityObj)
        {

            try
            {
                int numberLength = entityObj.ChequeNumberTo.Length;
                // Generate cheque numbers first
                var chequeNumbersToInsert = new List<string>();
                for (int i = Convert.ToInt32(entityObj.ChequeNumberFrom); i <= Convert.ToInt32(entityObj.ChequeNumberTo); i++)
                {
                    var chequeNumber = $"{entityObj.ChequeNumberPrefix}{i.ToString().PadLeft(numberLength, '0')}"; //{i.ToString().PadLeft(6, '0')}
                    chequeNumbersToInsert.Add(chequeNumber);
                }

                // Check for duplicates
                var existingChequeNumbers = await _context.bankAccountChequeDetails
                    .Where(cd => chequeNumbersToInsert.Contains(cd.ChequeNumber))
                    .Select(cd => cd.ChequeNumber)
                    .ToListAsync(); //cancellationToken

                if (existingChequeNumbers.Any())
                {
                    return false;
                }

                // Save main entity
                var chequeEntity = new BankAccountCheque
                {
                    BankAccountMappingId = entityObj.BankAccountMappingId,
                    ChequeNumberPrefix = entityObj.ChequeNumberPrefix,
                    ChequeNumberFrom = entityObj.ChequeNumberFrom,
                    ChequeNumberTo = entityObj.ChequeNumberTo,
                    CreatedBy = entityObj.CreatedBy,
                    CreatedOn = DateTime.Now
                };

                await _context.bankAccountCheques.AddAsync(chequeEntity);
                await _context.SaveChangesAsync();

                // Insert details
                int insertedChequeId = chequeEntity.BankAccountChequeId;

                foreach (var chequeNumber in chequeNumbersToInsert)
                {
                    var detail = new BankAccountChequeDetails
                    {
                        BankAccountChequeId = insertedChequeId,
                        ChequeNumber = chequeNumber,
                        Status = Utility.Constants.ChequeStatus.UnUsed
                        //CreatedBy = entityObj.CreatedBy,
                        //CreatedOn = DateTime.Now
                    };

                    await _context.bankAccountChequeDetails.AddAsync(detail);
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<CustomSelectListItem>> ChequeDetailsDropdown(int id)
        {
            var query = from bm in _context.bankAccountMappings
                        join chkMaster in _context.bankAccountCheques on bm.BankAccountMappingId equals chkMaster.BankAccountMappingId
                        join chkDetail in _context.bankAccountChequeDetails on chkMaster.BankAccountChequeId equals chkDetail.BankAccountChequeId
                        where bm.IsActive  && chkDetail.Status == Utility.Constants.ChequeStatus.UnUsed
                        select new CustomSelectListItem
                        {
                            Text = chkDetail.ChequeNumber,
                            Value = chkDetail.ChequeNumber
                        };
            return query.ToList();
        }

        public async Task<PaginatedResponse<BankAccountChequeDetailsVM>> LoadChequeDetailsGrid(int page, int pageSize, string search, string sortOrder, int? BankAccountMappingId)
        {
            var query = from bm in _context.bankAccountMappings
                        join bnk in _context.banks on bm.BankId equals bnk.BankId
                        join chkMaster in _context.bankAccountCheques on bm.BankAccountMappingId equals chkMaster.BankAccountMappingId
                        join chkDetail in _context.bankAccountChequeDetails on chkMaster.BankAccountChequeId equals chkDetail.BankAccountChequeId
                        //join brn in _context.bankBranches on bnk.BankId equals brn.BankBranchId
                        where bm.IsActive && chkMaster.BankAccountMappingId == BankAccountMappingId
                        select new BankAccountChequeDetailsVM
                        {
                            BankName = bnk.BankName,
                            //BankBranchName = brn.BranchName,
                            AccountNumber = bm.BankAccountNumber,
                            ChequeNumber = chkDetail.ChequeNumber,
                            Status = chkDetail.Status == Utility.Constants.ChequeStatus.Used ? "Used" :
                                      chkDetail.Status == Utility.Constants.ChequeStatus.UnUsed ? "Unused" :
                                      chkDetail.Status == Utility.Constants.ChequeStatus.Reject ? "Rejected" :
                                      chkDetail.Status == Utility.Constants.ChequeStatus.Delete ? "Deleted" : "Unknown",
                            Remarks = chkDetail.Remarks
                        };

            // Pagination
            var totalRecords = await query.CountAsync();
            var pageObj = await query.Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
            return new PaginatedResponse<BankAccountChequeDetailsVM>(pageObj, totalRecords);
        }

        public async Task<OfficeBankAssignDropdownVM> GetOfficeBankAssignDropdownData(int officeId)
        {
            try
            {
                var obj = new OfficeBankAssignDropdownVM();

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync("udp_OfficeBankAssign_Dropdown", commandType: CommandType.StoredProcedure))
                    {
                        obj.bank = multi.Read<CustomSelectListItem>().ToList();
                        obj.accountHead = multi.Read<CustomSelectListItem>().ToList();

                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                return new OfficeBankAssignDropdownVM();
            }
        }
    }
}