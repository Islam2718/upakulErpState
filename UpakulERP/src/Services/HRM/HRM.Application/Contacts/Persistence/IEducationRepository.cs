using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using System.Linq.Expressions;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
    public interface IEducationRepository : ICommonRepository<Education>
    {
       // Task<IEnumerable<Education>> GetEducation();

        Education GetById(int id);
        //Task<IEnumerable<BankVM>> GetAll();
        List<Education> GetAll();
        Task<PaginatedResponse<EducationVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<Education> GetMany(Expression<Func<Education, bool>> where);
    }
}
