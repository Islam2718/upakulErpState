using System.Data;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.MainPurpose;
using MF.Domain.Models.Loan;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MF.Infrastructure.Repository
{
    public class PurposeRepository : CommonRepository<Purpose>, IPurposeRepository
    {
        AppDbContext _context;
        public PurposeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<PurposeForGridVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            try
            {
                var prmLst = new List<object>();
                prmLst.Add(new SqlParameter("@pageNumber", SqlDbType.Int) { Value = page });
                prmLst.Add(new SqlParameter("@rowsOfPage", SqlDbType.Int) { Value = pageSize });
                prmLst.Add(new SqlParameter("@searching", SqlDbType.VarChar) { Value = search });
                prmLst.Add(new SqlParameter("@sortOrder", SqlDbType.VarChar) { Value = sortOrder });
                string sql = $"EXEC [loan].[udp_PurposeGrid] @pageNumber,@rowsOfPage,@searching,@sortOrder";
                var lst = await _context.Database.SqlQueryRaw<PurposeForGridVM>(sql, prmLst.ToArray()).ToListAsync();
                return lst;
            }
            catch (Exception ex)
            {
                return new List<PurposeForGridVM>();
            }
        }
        

        public VwPurpose GetByIdXView(int id)
        {
            var result = _context.vwPurposes.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public Purpose GetById(int id)
        {
            var obj = _context.mainPurposes.FirstOrDefault(c => c.IsActive && c.Id == id);
            return obj;
        }
    }
}
