using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;

namespace MF.Application.Contacts.Persistence
{
    public interface IOfficeComponentMappingRepository : ICommonRepository<OfficeComponentMapping>
    {
        OfficeComponentMapping GetById(int id);

        List<OfficeComponentMapping> GetAllByComponentId(int? officeId);

        Task<bool> CreateOrUpdateAsync(int componentId, int? logginEmpid, List<int> selectedBranchIds);

        IEnumerable<OfficeComponentMapping> GetMany(Expression<Func<OfficeComponentMapping, bool>> where);

    }

}
