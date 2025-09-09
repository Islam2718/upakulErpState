using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Utility.Response;
using MF.Infrastructure.Persistence;
using Utility.Domain.DBDomain;
using MF.Application.Contacts.Persistence;

namespace MF.Infrastructure.Repository
{
    public class GeoLocationRepository:CommonRepository<CommonGeoLocation>, IGeoLocationRepository
    {
        AppDbContext _context;
        public GeoLocationRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }
        
        public async Task<IEnumerable<CommonGeoLocation>> GetGeoLocationByParentId(int pId)
        {
            var objlst = await _context.geoLocations.Where(c => c.IsActive &&  (c.ParentId??0) == pId).ToListAsync();
            return objlst;
        }              

    }
}
