using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using Utility.Domain.DBDomain;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
    public interface IGeoLocationRepository: ICommonRepository<CommonGeoLocation>
    {
        Task<IEnumerable<CommonGeoLocation>> GetGeoLocationByParentId(int pid);

    }
}