using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Constants;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Domain.Models;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;
using MF.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Utility.Constants;
using Utility.Domain;
using Utility.Response;

namespace MF.Infrastructure.Repository
{
    public class GroupCommitteeRepository : CommonRepository<GroupCommittee>, IGroupCommitteeRepository
    {
        AppDbContext _context;

        public GroupCommitteeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<GroupCommitteeVM> GetActiveCommitteeMember(int groupId)
        {
            return _context.groupCommittees.Where(x => x.IsActive && x.GroupId == groupId && x.EndDate == null)
                .Select(s => new GroupCommitteeVM
                {
                    Id = s.Id,
                    CommitteePositionId = s.CommitteePositionId,
                    MemberId = s.MemberId,
                    GroupId = s.GroupId,
                    StartDate = s.StartDate,
                }).ToList();
        }

        //public IEnumerable<GroupCommittee> GetMany(Expression<Func<GroupCommittee, bool>> where)
        //{
        //    var entities = _context.groupCommittees.Where(where).Where(b => b.IsActive);
        //    return entities;
        //}

        public async Task<bool> AddRangeAsync(CreateGroupCommitteeCommand models)
        {
            try
            {
                var gid = models.groupCommitteeRequests.First().GroupId;
                var empId = models.loggedInEmpId;
                var lst = _context.groupCommittees.Where(x=>x.IsActive && x.GroupId == gid && x.EndDate==null ).ToList();
                lst.ForEach(c => { c.IsActive = false; c.DeletedOn = DateTime.Now; c.DeletedBy = empId; });
                foreach (var bId in models.groupCommitteeRequests.Where(x=>x.MemberId.HasValue && x.MemberId > 0))
                {
                    #region committee logic
                    // true Logic
                    if (lst.Any(x => x.CommitteePositionId == bId.CommitteePositionId))
                    {
                        #region Member 
                        // False Logic
                        if (!lst.Any(x => x.CommitteePositionId == bId.CommitteePositionId 
                        && x.MemberId==bId.MemberId))
                        {
                            _context.groupCommittees.Add(new GroupCommittee
                            {
                                CommitteePositionId = bId.CommitteePositionId,
                                GroupId = bId.GroupId,
                                MemberId = bId.MemberId??0,
                                StartDate = Convert.ToDateTime(bId.StartDate),
                                CreatedBy = empId,
                                CreatedOn = DateTime.UtcNow
                            });
                        }
                        else // true Logic
                        {
                            var obj = lst.Where(x => x.CommitteePositionId == bId.CommitteePositionId
                            && x.MemberId == bId.MemberId).First();
                            obj.IsActive = true;
                            obj.DeletedBy = null;
                            obj.DeletedOn = null;
                        }
                        #endregion Member  logic
                    }
                    // false logic
                    else
                    {
                        _context.groupCommittees.Add(new GroupCommittee
                        {
                            CommitteePositionId = bId.CommitteePositionId,
                            GroupId = bId.GroupId,
                            MemberId = bId.MemberId??0,
                            StartDate = Convert.ToDateTime(bId.StartDate),
                            CreatedBy = empId,
                            CreatedOn = DateTime.UtcNow
                        });
                    }
                    #endregion committee logic
                }
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
