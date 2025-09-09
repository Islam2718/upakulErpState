using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using UpakulHRM.Infrastructure.Persistence;
using Utility.Response;

namespace HRM.Infrastructure.Repository
{
        public class OfficeTypeXConfigMasterRepository : CommonRepository<OfficeTypeXConfigMaster>, IOfficeTypeXConfigMasterRepository
        {
             AppDbContext _context;

            public OfficeTypeXConfigMasterRepository(AppDbContext context) : base(context)
            {
                _context = context;
            }
        //public async Task<bool> AddAsync(OfficeTypeXConfigMaster entity)
        //{
        //    await _context.OfficeTypeXConfigMaster.AddAsync(entity);
        //    var result = await _context.SaveChangesAsync();

        //   // Console.WriteLine("Save result: " + result); // ✅ Add this log
        //    //Console.WriteLine("Master ID: " + entity.ConfigureMasterId); // ✅ Check if ID is set

        //    return result > 0;
        //}


        public async Task<int> InsertMasterAsync(OfficeTypeXConfigMaster master)
        {
            _context.OfficeTypeXConfigMaster.Add(master);
            await _context.SaveChangesAsync();
            return master.ConfigureMasterId; // Assuming Id is auto-generated
        }


        public async Task<bool> InsertDetailAsync(OfficeTypeXConfigureDetails detail)
        {
            _context.OfficeTypeXConfigureDetails.Add(detail);
            await _context.SaveChangesAsync();
            return true;
        }
        public OfficeTypeXConfigMaster GetById(int id)
            {
                return _context.OfficeTypeXConfigMaster.FirstOrDefault(x => x.ConfigureMasterId == id);
            }

            public List<OfficeTypeXConfigMaster> GetAll()
            {
                return _context.OfficeTypeXConfigMaster.ToList();
            }

            public IEnumerable<OfficeTypeXConfigMaster> GetMany(Expression<Func<OfficeTypeXConfigMaster, bool>> where)
            {
                return _context.OfficeTypeXConfigMaster.Where(where);
            }

            public async Task<PaginatedResponse<OfficeTypeXConfigMasterVM>> LoadGrid(int page, int pageSize, string search, string sortOrder)
            {
                search = search ?? "0";
                string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "LeaveCategoryId.Contains(@0)";
                sortOrder = string.IsNullOrEmpty(sortOrder) ? "ConfigureMasterId" : sortOrder;

                var query = _context.OfficeTypeXConfigMaster
                    .Select(x => new OfficeTypeXConfigMasterVM
                    {
                       // ConfigureMasterId = x.ConfigureMasterId,
                        OfficeTypeId = x.OfficeTypeId,
                        ApplicantDesignationId = x.ApplicantDesignationId,
                        LeaveCategoryId = x.LeaveCategoryId
                    })
                    .AsQueryable()
                    .Where(src_qry, search)
                    .OrderBy(sortOrder);

                var totalRecords = await query.CountAsync();
                var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                return new PaginatedResponse<OfficeTypeXConfigMasterVM>(items, totalRecords);
            }

            public async Task<IEnumerable<OfficeTypeXConfigMaster>> GetOfficeTypeXConfigMaster()
            {
                return await _context.OfficeTypeXConfigMaster.ToListAsync();
            }

       
    }
    }
