using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using Utility.Response;

namespace Global.Application.Contacts.Persistence
{
    public interface IOfficeRepository : ICommonRepository<Office>
    {
        Office GetById(int id);
        VWOffice GetByIdFromView(int id);
        Task<IEnumerable<Office>> GetOfficeByParentId(int pid);
        Task<PaginatedResponse<VWOffice>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<OfficeForDropDownVM> GetOfficeDropdown(int officeId, int officeType);
        List<Office> GetAll();
        IEnumerable<Office> GetMany(Expression<Func<Office, bool>> where);
    }
}
