using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Utility.Domain;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
    public interface IGroupWiseEmployeeAssignRepository : ICommonRepository<GroupWiseEmployeeAssign>
    {
        IEnumerable<CustomSelectListItem> GetGroupByEmployeeId(int? employeeId);
        Task<CommadResponse> CreateOrUpdateAsync(int? AssignEmployeeId, List<int> AssignedGroupListId, int? ReleaseEmployeeId, List<int>? ReleaseGroupListId
            , int? loggedinEmpId, DateTime? releaseDate, string? releaseNote, DateTime? assignDate);
        Task<PaginatedResponse<GroupWiseEmployeeAssignVM>> LoadGrid(int page, int pageSize, string search, string sortOrder, int? logedInOfficeId);
        GroupWiseEmployeeAssign ReleaseById(int id);
        Task<MultipleDropdownForGrpWiseEmployeeVM> AllDropDownForGrpWiseEmployee(int officeId, int officeTypeId);
    }
}
