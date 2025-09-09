using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using System.Linq.Expressions;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
    public interface IDepartmentRepository : ICommonRepository<Department>
    {
        Department GetById(int id);
        List<Department> GetAll();
        Task<PaginatedResponse<DepartmentVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Department> GetMany(Expression<Func<Department, bool>> where);

        // list method
        Task<IEnumerable<Department>> GetDeartment();

    }
}
