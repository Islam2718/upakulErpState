using System.Linq.Expressions;
using EF.Core.Repository.Interface.Repository;
using Global.Domain.Models;
using Global.Domain.ViewModels;
using Utility.Response;

namespace Global.Application.Contacts.Persistence
{
    public interface ICountryRepository : ICommonRepository<Country>
    {

        Country GetById(int id);
        List<Country> GetAll();
        Task<PaginatedResponse<CountryVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Country> GetMany(Expression<Func<Country, bool>> where);
        // insert method
        Task<Country> Add(Country obj);

    }
}
