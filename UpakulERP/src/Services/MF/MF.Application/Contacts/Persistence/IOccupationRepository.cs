using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
    public interface IOccupationRepository : ICommonRepository<Occupation>
    {
        Occupation GetById(int id);
        List<Occupation> GetAll();
        IEnumerable<Occupation> GetMany(Expression<Func<Occupation, bool>> where);
        Task<PaginatedResponse<OccupationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        //Task<PaginatedOccupationResponse> GetListAsync(int page, int pageSize, string search, string sortColumn, string sortDirection);
    }
}
