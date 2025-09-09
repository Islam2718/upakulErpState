using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models;
using MF.Domain.Models.View;
using MF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Utility.Constants;
using Utility.Domain;
using Utility.Response;

namespace MF.Infrastructure.Repository
{
    public class GroupRepository : CommonRepository<MF.Domain.Models.Group>, IGroupRepository
    {
        AppDbContext _context;

        public GroupRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public MF.Domain.Models.Group GetById(int id)
        {
            var obj = _context.groups.FirstOrDefault(c => c.IsActive && c.GroupId == id);
            return obj;
        }

        public List<MF.Domain.Models.Group> GetAll(int? officeId)
        {
            var objlst = _context.groups.Where(c => c.IsActive).ToList();
            return objlst;
        }

        public List<MF.Domain.Models.Group> AllGroupByOfficeId(int officeId)
        {
            var objlst = _context.groups.Where(c => c.IsActive && c.OfficeId == officeId).ToList();
            return objlst;
        }

        public IEnumerable<MF.Domain.Models.Group> GetMany(Expression<Func<MF.Domain.Models.Group, bool>> where)
        {
            var entities = _context.groups.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public async Task<PaginatedResponse<VwGroup>> LoadGrid(int page, int pageSize, string search, string sortOrder, int? officeId)
        {
            search = search ?? "0";
            string src_qry = string.IsNullOrEmpty(search) ? "@0=@0" : "GroupName.Contains(@0) OR GroupCode.Contains(@0) OR MeetingDayName.Contains(@0) OR MeetingDayName.Contains(@0)";// OR (int)OrderNo.ToString().Contains(@0)
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "GroupId" : sortOrder;

            var query = _context.vwGroups
                 .Select(x => new VwGroup
                 {
                     GroupName = x.GroupName,
                     GroupCode = x.GroupCode,
                     MeetingDayName = x.MeetingDayName,
                     Office = x.Office,
                     //  SamityLeader = x.SamityLeader,
                     Union = x.Union,
                     Village = x.Village,
                     District = x.District,
                     Upazila = x.Upazila,
                     SamityTypeName = x.SamityTypeName,
                     StartDate = x.StartDate,
                     GroupId = x.GroupId
                 }).Where(src_qry, search).OrderBy(sortOrder).AsQueryable();

            // Pagination
            var totalRecords = await query.CountAsync();
            var obj = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResponse<VwGroup>(obj, totalRecords);
        }

        public List<CustomSelectListItem> GetGroupByEmployeeIdDropdown(int empId)
        {
            var lst = new List<CustomSelectListItem>();
            lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Selected = true });
            lst.AddRange((from g in _context.groups
                          join gwa in _context.groupWiseEmployeeAssigns
                              on g.GroupId equals gwa.GroupId
                          where g.IsActive && gwa.IsActive && gwa.ReleaseDate == null && gwa.EmployeeId == empId
                          select new CustomSelectListItem
                          {
                              Value = g.GroupId.ToString(),
                              Text = $"{g.GroupCode} - {g.GroupName}",
                          })
                          .Distinct().ToList());
            return lst;
        }
    }
}

