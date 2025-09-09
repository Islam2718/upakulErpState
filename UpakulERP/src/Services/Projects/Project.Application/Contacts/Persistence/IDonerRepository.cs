using EF.Core.Repository.Interface.Repository;
using Project.Domain.Models;
using roject.Domain.ViewModels;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace Project.Application.Contacts.Persistence
{
    public interface IDonerRepository : ICommonRepository<Doner>
    {
        Doner GetById(int id);
        Task<PaginatedResponse<DonerVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Doner> GetMany(Expression<Func<Doner, bool>> where);
    }
}
