using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contacts.Persistence;
using Project.Domain.Models;
using Project.Domain.ViewModels;
using Project.Infrastructure.Persistence;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Utility.Response;

namespace Project.Infrastructure.Repository
{

    public class ActivityPlanRepository : CommonRepository<ActivityPlan>, IActivityPlanRepository
    {
        AppDbContext _context;
        public ActivityPlanRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public ActivityPlan GetById(int id)
        {
           return _context.activityPlans.Find(id);
        }

        public IEnumerable<ActivityPlan> GetMany(Expression<Func<ActivityPlan, bool>> where)
        {
            var entities = _context.activityPlans.Where(where).Where(b => b.IsActive);
            return entities;
        }

        public List<ActivityPlan> GetProjectXActivity(int projectId)
        {
            return _context.activityPlans.Where(b => b.IsActive && b.ProjectId == projectId).ToList();
        }

        public bool ChangeTable(List<RequestActivityPlan> lst,int projectId, int loggedinEmpId)
        {
            try
            {
                foreach (var item in lst)
                {
                    if ((item.Id ?? 0) > 0)
                    {
                        if (_context.activityPlans.Any(x => x.IsActive && !(x.IsApproved ?? false) && x.Id == item.Id))
                        {
                            var obj = _context.activityPlans.Find(item.Id.Value);
                            obj.ActivityName = item.ActivityName;
                            obj.ActivityFrom = item.ActivityFrom;
                            obj.ActivityTo = item.ActivityTo;
                            obj.ReportingDate = item.ReportingDate;
                            obj.TargetType = item.TargetType;
                            obj.MonthlyTarget = item.MonthlyTarget;
                            obj.TotalTarget = item.TotalTarget;
                            obj.ProgramParticipantsTarget = item.ProgramParticipantsTarget;
                            obj.BeneficiaryType = item.BeneficiaryType;
                            obj.ProgramParticipants_U_18_Boys = item.ProgramParticipants_U_18_Boys;
                            obj.ProgramParticipants_U_18_Girls = item.ProgramParticipants_U_18_Girls;
                            obj.ProgramParticipants_18_59_Male = item.ProgramParticipants_18_59_Male;
                            obj.ProgramParticipants_18_59_Female = item.ProgramParticipants_18_59_Female;
                            obj.ProgramParticipants_Up_59_Male = item.ProgramParticipants_Up_59_Male;
                            obj.ProgramParticipants_Up_59_Female = item.ProgramParticipants_Up_59_Female;

                            obj.ProgramParticipants_Disable_Male = item.ProgramParticipants_Disable_Male;
                            obj.ProgramParticipants_Disable_Female = item.ProgramParticipants_Disable_Female;

                            obj.ProgramParticipantsEthnicityandMarginalized = item.ProgramParticipantsEthnicityandMarginalized;
                            obj.WomenHeadedProgramParticipants = item.WomenHeadedProgramParticipants;
                            obj.TransgenderProgramParticipants = item.TransgenderProgramParticipants;
                            obj.UpdatedBy = loggedinEmpId;
                            obj.UpdatedOn = DateTime.Now;
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                        var obj = new ActivityPlan
                        {
                            ActivityFrom = item.ActivityFrom,
                            ActivityTo = item.ActivityTo,
                            ActivityName = item.ActivityName,
                            BeneficiaryType = item.BeneficiaryType,
                            CreatedBy = loggedinEmpId,
                            CreatedOn = DateTime.Now,
                            IsActive = true,
                            IsApproved = false,
                            MonthlyTarget = item.MonthlyTarget,
                            ProgramParticipantsEthnicityandMarginalized = item.ProgramParticipantsEthnicityandMarginalized,
                            ProgramParticipantsTarget = item.ProgramParticipantsTarget,
                            ProgramParticipants_18_59_Female = item.ProgramParticipants_18_59_Female,
                            ProgramParticipants_18_59_Male = item.ProgramParticipants_18_59_Male,
                            ProgramParticipants_Disable_Female = item.ProgramParticipants_Disable_Female,
                            ProgramParticipants_Disable_Male = item.ProgramParticipants_Disable_Male,
                            ProgramParticipants_Up_59_Female = item.ProgramParticipants_Up_59_Female,
                            ProgramParticipants_Up_59_Male = item.ProgramParticipants_Up_59_Male,
                            ProgramParticipants_U_18_Boys = item.ProgramParticipants_U_18_Boys,
                            ProgramParticipants_U_18_Girls = item.ProgramParticipants_U_18_Girls,
                            ProjectId = projectId,
                            ReportingDate = item.ReportingDate,
                            TargetType = item.TargetType,
                            TotalTarget = item.TotalTarget,
                            TransgenderProgramParticipants = item.TransgenderProgramParticipants,
                            WomenHeadedProgramParticipants = item.WomenHeadedProgramParticipants,
                        };
                        _context.activityPlans.Add(obj);
                        _context.SaveChanges();
                    }
                }
                return true;
            }
            catch 
            {
                return false;
            }
            
        }

    }
}
