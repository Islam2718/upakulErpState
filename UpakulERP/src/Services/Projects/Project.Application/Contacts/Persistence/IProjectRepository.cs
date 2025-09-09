using EF.Core.Repository.Interface.Repository;
using Project.Domain.Models;
using Project.Domain.ViewModels;
using System.Linq.Expressions;
using Utility.Response;


namespace Project.Application.Contacts.Persistence
{

    public interface IProjectRepository : ICommonRepository<Projects>
    {
        Projects GetById(int id);
        Task<PaginatedResponse<ProjectVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Projects> GetMany(Expression<Func<Projects, bool>> where);

        // insert method

    }

}
