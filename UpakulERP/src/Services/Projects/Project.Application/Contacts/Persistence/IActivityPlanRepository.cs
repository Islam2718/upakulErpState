using EF.Core.Repository.Interface.Repository;
using Project.Domain.Models;
using Project.Domain.ViewModels;
using System.Linq.Expressions;


namespace Project.Application.Contacts.Persistence
{

    public interface IActivityPlanRepository : ICommonRepository<ActivityPlan>
    {
        ActivityPlan GetById(int id);
        List<ActivityPlan> GetProjectXActivity(int projectId);
        IEnumerable<ActivityPlan> GetMany(Expression<Func<ActivityPlan, bool>> where);

        // insert method
        bool ChangeTable(List<RequestActivityPlan> lst, int projectId, int loggedinEmpId);
    }

}
