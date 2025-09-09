using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.Models.Views;
using System.Linq.Expressions;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
    public interface IHoliDayRepository : ICommonRepository<HoliDay>
    {
        HoliDay GetById(int id);
        List<HoliDay> GetAll();
        Task<PaginatedResponse<VWHoliday>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<HoliDay> GetMany(Expression<Func<HoliDay, bool>> where);
        // list method
        Task<IEnumerable<HoliDay>> GetHoliDay();
        // insert method
        // update method
    }
}






































































































































































