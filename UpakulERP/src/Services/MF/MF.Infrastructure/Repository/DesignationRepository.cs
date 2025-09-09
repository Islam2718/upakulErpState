using System.Linq.Expressions;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Infrastructure.Persistence;
using Utility.Domain.DBDomain;

namespace MF.Infrastructure.Repository
{
    public class DesignationRepository : CommonRepository<CommonDesignation>, IDesignationRepository
    {
        AppDbContext _context;
        public DesignationRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<CommonDesignation> GetOfficeLevelWiseDesignation(int officeId,int officeTypeid)
        {
            var entities = (from dg in _context.designations
                            join emp in _context.employees on dg.DesignationId equals emp.DesignationId
                            join f in _context.GetOfficeHierarchi(officeid: officeId, officeTypeid: officeTypeid)
                            on emp.OfficeId equals f.OfficeId
                            where dg.IsActive
                            select new CommonDesignation
                            {
                                DesignationCode = dg.DesignationCode,
                                DesignationId = dg.DesignationId,
                                DesignationName = dg.DesignationName,
                            }
                           ).ToList();
            return entities;
        }

    }


}
