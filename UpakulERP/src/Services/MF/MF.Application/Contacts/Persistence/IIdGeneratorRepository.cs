using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using System.Linq.Expressions;

namespace MF.Application.Contacts.Persistence
{
  public  interface IIdGeneratorRepository : ICommonRepository<IdGenerate>
    {
        IdGenerate GetById(int id);
        List<IdGenerate> GetAll();
        IEnumerable<IdGenerate> GetMany(Expression<Func<IdGenerate, bool>> where);
        // insert method
        Task<IdGenerate> Add(IdGenerate obj);
    }
}
