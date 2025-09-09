using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using EF.Core.Repository.Repository;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using Global.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Utility.Response;

namespace Global.Infrastructure.Repository
{
    public class OfficeRepository : CommonRepository<Office>, IOfficeRepository
    {
        AppDbContext _context;
        public OfficeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Office GetById(int id)
        {
            var obj = _context.offices.FirstOrDefault(c => c.IsActive && c.OfficeId == id);
            return obj;
        }

        public VWOffice GetByIdFromView(int id)
        {
            var obj = _context.vw_Office.FirstOrDefault(c => c.OfficeId == id);
            return obj;
        }

        public async Task<IEnumerable<Office>> GetOfficeByParentId(int pId)
        {
            var objLst = await _context.offices.Where(c => c.IsActive && (c.ParentId ?? 0) == pId).OrderBy(x => x.OfficeCode).ToListAsync();
            return objLst;
        }

        public IEnumerable<OfficeForDropDownVM> GetOfficeDropdown(int officeId, int officeType)
        {
            string qry = @$"SELECT * FROM dbo.udf_OfficeHierarchical({officeId},{officeType})";
            var lst = _context.Database.SqlQueryRaw<OfficeForDropDownVM>(qry);

            //var objLst =  _context.offices.Where(c => c.IsActive && c.OfficeType==(int)OfficeType.OfficeTypeEnum.Branch).OrderBy(x => x.OfficeCode).ToList(); /* && (c.ParentId ?? 0) == officeid)*/
            return lst;
        }

        public async Task<PaginatedResponse<VWOffice>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {

            search = search ?? "0";
            string src_Qry = string.IsNullOrEmpty(search) ? "@0=@0" : "PrincipalOfficeCode.Contains(@0) OR PrincipalOfficeName.Contains(@0) OR ZonalOfficeCode.Contains(@0) " +
               "OR ZonalOfficeName.Contains(@0) OR RegonalOfficeCode.Contains(@0) OR RegonalOfficeName.Contains(@0) OR AreaOfficeCode.Contains(@0) " +
               "OR AreaOfficeName.Contains(@0) OR BranchOfficeCode.Contains(@0) OR BranchOfficeName.Contains(@0)";
            
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "OfficeId" : sortOrder;
            var query = _context.vw_Office.Where(src_Qry, search).OrderBy(sortOrder).AsQueryable();
            
            // Pagination
            var totalRecords = await query.CountAsync();
            var listData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<VWOffice>(listData, totalRecords);
        }

        public List<Office> GetAll()
        {
            var objlst = _context.offices.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public IEnumerable<Office> GetMany(Expression<Func<Office, bool>> where)
        {
            var entities = _context.offices.Where(where).Where(b => b.IsActive);
            return entities;
        }
    }
}
