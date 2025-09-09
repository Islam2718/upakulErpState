using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
   public interface IOfficeTypeXConfigureDetailsRepository : ICommonRepository<OfficeTypeXConfigureDetails>
    {
        List<OfficeTypeXConfigureDetails> GetAll();
        OfficeTypeXConfigureDetails GetById(int id);

        Task<PaginatedResponse<OfficeTypeXConfigureDetailsVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<OfficeTypeXConfigureDetails> GetMany(Expression<Func<OfficeTypeXConfigureDetails, bool>> where);
        Task<IEnumerable<OfficeTypeXConfigureDetails>> GetOfficeTypeXConfigureDetails();
        Task<bool> AddAsync(List<OfficeTypeXConfigureDetails> details);


    }
}