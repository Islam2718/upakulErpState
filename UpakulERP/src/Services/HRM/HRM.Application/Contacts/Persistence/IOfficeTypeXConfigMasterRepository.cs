using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Contacts.Persistence
{
    public interface IOfficeTypeXConfigMasterRepository : ICommonRepository<OfficeTypeXConfigMaster>
    {
        List<OfficeTypeXConfigMaster> GetAll();
        OfficeTypeXConfigMaster GetById(int id);

        Task<PaginatedResponse<OfficeTypeXConfigMasterVM>> LoadGrid(int page, int pageSize, string search, string sortOrder);
        IEnumerable<OfficeTypeXConfigMaster> GetMany(Expression<Func<OfficeTypeXConfigMaster, bool>> where);
        Task<IEnumerable<OfficeTypeXConfigMaster>> GetOfficeTypeXConfigMaster();

        Task<int> InsertMasterAsync(OfficeTypeXConfigMaster master);
        Task<bool> InsertDetailAsync(OfficeTypeXConfigureDetails detail);

    }
}

     
