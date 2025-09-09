using MediatR;
using MF.Domain.ViewModels.Collection;

namespace MF.Application.Features.DBOrders.Queries.Component
{
   public class EmployeeXGroupCollectionQuery : IRequest<List<EmployeeXGroupCollectionVM>>
    {
        public int officeId { get; set; }
        public int employeeId { get; set; }
        public DateTime transactionDate { get; set; }
        public EmployeeXGroupCollectionQuery(int officeId, int employeeId, DateTime transactionDate)
        {
            this.officeId = officeId;
            this.employeeId = employeeId;
            this.transactionDate = transactionDate;
        }
    }
}
