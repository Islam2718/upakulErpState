using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using System.Linq.Expressions;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
    public interface IDesignationRepository : ICommonRepository<Designation>
    {
        Designation GetById(int id);
        List<Designation> GetAll();
        Task<PaginatedResponse<DesignationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Designation> GetMany(Expression<Func<Designation, bool>> where);
        // list method
        Task<IEnumerable<Designation>> GetDesignation();
    }
}
