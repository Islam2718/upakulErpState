using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using HRM.Domain.ViewModels;
using HRM.Domain.Models;
using HRM.Domain.Models.Views;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
    public interface IEmployeeRepository : ICommonRepository<Employee>
    {
        Task<Employee> GetById(int id);
        Task<VWEmployee> GetById_View(int id);
        IEnumerable<Employee> GetMany(Expression<Func<Employee, bool>> where);
        Task<MultipleDropdownForEmployeeProfileVM> AllEmployeeProfilesDropDown(int officeId);
        Task<PaginatedResponse<EmployeeVM>> LoadGrid(int page, int pageSize, string search, string sortOrder, int officeId);
    }
}
