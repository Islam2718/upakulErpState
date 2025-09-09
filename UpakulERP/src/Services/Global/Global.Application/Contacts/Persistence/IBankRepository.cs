using EF.Core.Repository.Interface.Repository;
using Global.Domain.Models;
using Global.Domain.ViewModels;
using System.Linq.Expressions;
using Utility.Response;

namespace Global.Application.Contacts.Persistence
{
    public interface IBankRepository : ICommonRepository<Bank>
    {
        Bank GetById(int id);
        //Task<IEnumerable<BankVM>> GetAll();
        //List<Bank> GetAll();
        Task<PaginatedResponse<BankVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Bank> GetMany(Expression<Func<Bank, bool>> where);
    }
}
