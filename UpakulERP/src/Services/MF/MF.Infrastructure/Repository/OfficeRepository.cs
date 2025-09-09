using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;
using MF.Infrastructure.Persistence;
using MF.Domain.ViewModels;
using MF.Application.Contacts.Persistence;

namespace MF.Infrastructure.Repository
{
    public class OfficeRepository : CommonRepository<CommonOffice>, IOfficeRepository
    {
        AppDbContext _context;
        public OfficeRepository(AppDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<IEnumerable<CommonOffice>> GetOfficeByParentId(int pId)
        {
            var objLst = await _context.offices.Where(c => c.IsActive && (c.ParentId ?? 0) == pId).OrderBy(x => x.OfficeCode).ToListAsync();
            return objLst;
        }

        public IEnumerable<OfficeForDropDownVM> GetOfficeDropdown(int officeId, int officeType)
        {
            string qry = @$"SELECT * FROM dbo.udf_OfficeHierarchical({officeId},{officeType})";
            var lst = _context.Database.SqlQueryRaw<OfficeForDropDownVM>(qry);

            return lst;
        }
    }
}
