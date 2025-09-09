using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Utility.Response;

namespace MF.Infrastructure.Repository
{
    public class ComponentRepository : CommonRepository<Component>, IComponentRepository
    {
        AppDbContext _context;
        public ComponentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Component GetById(int id)
        {
            var obj = _context.components.FirstOrDefault(c => c.IsActive && c.Id == id);
            return obj;
        }

        public List<Component> GetOfficeXComponent(int officeId, string componentType, string? loanType)
        {
            if (componentType == MF.Application.Contacts.Enums.Component.Loan && loanType == null)
                return (_context.components.Where(x => x.IsActive && x.ComponentType == componentType && x.LoanType == "G")// General Loan
                 .Union(
                    _context.components.Where(x => x.IsActive && x.ComponentType == componentType && x.LoanType == "P" && // Project Loan
                    _context.officeComponentMappingList.Any(o => o.IsActive && o.OfficeId == officeId && o.ComponentId == x.Id))

                    )).ToList();
            else if (componentType == MF.Application.Contacts.Enums.Component.Loan && loanType != null) // General OR Project
                return _context.components.Where(x => x.IsActive && x.ComponentType == componentType && x.LoanType == loanType).ToList();
            else if (componentType == "S") // Saving
                return _context.components.Where(x => x.IsActive &&
                (x.ComponentType == MF.Application.Contacts.Enums.Component.Security_Saving || x.ComponentType == MF.Application.Contacts.Enums.Component.Volenter_Saving)).ToList();
            else  // DPS or FDR
                return _context.components.Where(x => x.IsActive &&
                x.ComponentType == componentType).ToList();
        }

        public IEnumerable<Component> GetMany(Expression<Func<Component, bool>> where)
        {
            var entities = _context.components.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<ComponentVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "ComponentName.Contains(@0) OR ComponentCode.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "Id" : sortOrder;
            var query = _context.components.Where(b => b.IsActive)
                 .Select(x => new ComponentVM
                 {
                     Id = x.Id,
                     ComponentName = x.ComponentName,
                     ComponentCode = x.ComponentCode,
                     ComponentType = x.ComponentType,
                     CalculationMethod = x.CalculationMethod,
                     InterestRate = x.InterestRate,
                     NoOfInstalment = x.NoOfInstalment,
                 }).Where(src_qry, search).OrderBy(sortOrder).AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var listData = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<ComponentVM>(listData, totalRecords);
        }

        //public async Task<Component> Add(Component obj)
        //{
        //    // Add the new row to the context
        //    await _context.component.AddAsync(obj);

        //    // Save the changes to the database
        //    await _context.SaveChangesAsync();
        //    return obj;
        //}

    }
}

