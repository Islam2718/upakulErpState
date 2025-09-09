using Auth.API.Models.View;
using Utility.Domain;

namespace Auth.API.Repositories.Interfaces
{
    public interface IEmployeeStrategy
    {
        Task<VWEmployee> FindByEmpId(int id);
        //List<CustomSelectListItem> GetAllEmployeeforDropdown(int? empId);
        List<CustomSelectListItem> GetEmployeeDropdown(int officeId, int? empId);
    }
}
