using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using MF.Domain.Models.View;
using Utility.Domain;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
    public interface IGroupRepository : ICommonRepository<Group>
    {
        Group GetById(int id);
        List<Group> GetAll(int? officeId);
        List<Group> AllGroupByOfficeId(int officeId);
        IEnumerable<Group> GetMany(Expression<Func<Group, bool>> where);
        Task<PaginatedResponse<VwGroup>> LoadGrid(int page, int pageSize, string search, string sortOrder, int? OfficeId);

        List<CustomSelectListItem> GetGroupByEmployeeIdDropdown(int empId);
    }
}
