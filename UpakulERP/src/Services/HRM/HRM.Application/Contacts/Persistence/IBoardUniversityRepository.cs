using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using System.Linq.Expressions;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
    public interface IBoardUniversityRepository : ICommonRepository<BoardUniversity>
    {

        BoardUniversity GetById(int id);
        List<BoardUniversity> GetAll();
        Task<PaginatedResponse<BoardUniversityVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<BoardUniversity> GetMany(Expression<Func<BoardUniversity, bool>> where);

    }
}
