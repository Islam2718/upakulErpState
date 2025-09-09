using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using EF.Core.Repository.Repository;
using Global.Application.Contacts.Persistence;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using Global.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Utility.Response;

namespace Global.Infrastructure.Repository
{
    public class GeoLocationRepository:CommonRepository<GeoLocation>, IGeoLocationRepository
    {
        AppDbContext _context;
        public GeoLocationRepository(AppDbContext context):base(context) 
        {
            _context = context;
        }

        public GeoLocation GetById(int id)
        {
            var obj =  _context.geoLocations.FirstOrDefault(c => c.GeoLocationId == id);
            return obj;
        }
        public VWGeoLocation GetByIdFromView(int id)
        {
            var obj = _context.vwGeoLocations.FirstOrDefault(c => c.GeoLocationId == id);
            return obj;
        }
        public async Task<IEnumerable<GeoLocation>> GetGeoLocationByParentId(int pId)
        {
            var objlst = await _context.geoLocations.Where(c => c.IsActive &&  (c.ParentId??0) == pId).ToListAsync();
            return objlst;
        }
        public IEnumerable<GeoLocation> GetMany(Expression<Func<GeoLocation, bool>> where)
        {
            var entities = _context.geoLocations.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public List<GeoLocation> GetAll()
        {
            var objlst = _context.geoLocations.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public async Task<PaginatedResponse<VWGeoLocation>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_Qry = string.IsNullOrEmpty(search) ? "@0=@0" : "GeoLocationDivisionCode.Contains(@0) OR GeoLocationDivisionName.Contains(@0) OR GeoLocationDistrictCode.Contains(@0) " +
                 "OR GeoLocationDistrictName.Contains(@0) OR GeoLocationThanaCode.Contains(@0) OR GeoLocationThanaName.Contains(@0) OR GeoLocationUnionCode.Contains(@0) " +
                 "OR GeoLocationUnionName.Contains(@0) OR GeoLocationVillageCode.Contains(@0) OR GeoLocationVillageName.Contains(@0)";
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "GeoLocationId" : sortOrder;
            var query = _context.vwGeoLocations.Where(src_Qry, search).OrderBy(sortOrder).AsQueryable();
            // Pagination
            var totalRecords = await query.CountAsync();
            var listData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<VWGeoLocation>(listData, totalRecords);
        }

    }
}
