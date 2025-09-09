using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Utility.Domain.DBDomain;

namespace MF.Infrastructure.Repository
{
    public class OfficeComponentMappingRepository : CommonRepository<OfficeComponentMapping>, IOfficeComponentMappingRepository
    {
        AppDbContext _context;
        public OfficeComponentMappingRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public OfficeComponentMapping GetById(int id)
        {
            var obj = _context.officeComponentMappingList.FirstOrDefault(c => c.IsActive && c.OfficeComponentMappingId == id);
            return obj;
        }

        public List<OfficeComponentMapping> GetAllByComponentId(int? componentId)
        {
            var objlst = _context.officeComponentMappingList.Where(c => c.IsActive && c.ComponentId == componentId).ToList();
            return objlst;
        }
        public IEnumerable<OfficeComponentMapping> GetMany(Expression<Func<OfficeComponentMapping, bool>> where)
        {
            var entities = _context.officeComponentMappingList.Where(where).Where(b => b.IsActive);
            return entities;
        }
        public async Task<bool> CreateOrUpdateAsync(int componentId, int? logginEmpid, List<int> selectedBranchIds)
        {
            if (selectedBranchIds != null)
            {
                var lst = GetMany(c => c.ComponentId == componentId).ToList();
                if (lst.Any())
                    lst.ForEach(c => { c.IsActive = false; c.DeletedOn = DateTime.Now; c.DeletedBy = logginEmpid; });
                foreach (var bId in selectedBranchIds)
                {
                    if (lst.Any(x => x.OfficeId == bId))
                    {
                        var obj = lst.Where(x => x.OfficeId == bId).First();
                        obj.IsActive = true;
                        obj.UpdatedBy = logginEmpid;
                        obj.UpdatedOn = DateTime.UtcNow;
                        obj.DeletedBy = null;
                        obj.DeletedOn = null;
                    }
                    else
                    {
                        _context.officeComponentMappingList.Add(new OfficeComponentMapping
                        {
                            ComponentId = componentId,
                            OfficeId = bId,
                            CreatedBy = logginEmpid,
                            CreatedOn = DateTime.UtcNow
                        });
                    }
                }
                await _context.SaveChangesAsync();
                return true;
            }
            else return false;
        }
    }



    //public List<MultipleDropdownForOfficeComponentMappingVM> GetBranchByComponentId(int? componentId)
    //{
    //    var objlst = _context.officeComponentMappingList.Where(c => c.IsActive && c.ComponentId == componentId).ToList();
    //    return objlst;
    //}






}
