using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using Utility.Response;

namespace Global.Application.Contacts.Persistence
{
    public interface IGeoLocationRepository: ICommonRepository<GeoLocation>
    {

        GeoLocation GetById(int id);
        VWGeoLocation GetByIdFromView(int id);
        List<GeoLocation> GetAll();
        IEnumerable<GeoLocation> GetMany(Expression<Func<GeoLocation, bool>> where);
        Task<PaginatedResponse<VWGeoLocation>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        Task<IEnumerable<GeoLocation>> GetGeoLocationByParentId(int pid);

    }
}