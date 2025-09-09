using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using System.Linq.Expressions;
using Utility.Response;

namespace MF.Application.Contacts.Persistence
{
  public  interface IComponentRepository : ICommonRepository<Component>
    {

        Component GetById(int id);
        List<Component> GetOfficeXComponent(int officeId, string componentType,string? loanType);
        Task<PaginatedResponse<ComponentVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Component> GetMany(Expression<Func<Component, bool>> where);
        // insert method
        //Task<Component> Add(Component obj);
        //Task<bool> AddAsync(Component obj);
    }
}
