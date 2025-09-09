using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels.Collection;

namespace MF.Application.Features.DBOrders.Queries.Component
{
   public class EmployeeXGroupCollectionQueryHandler : IRequestHandler<EmployeeXGroupCollectionQuery, List<EmployeeXGroupCollectionVM>>
    {
        ICollectionRepository _repository;
        public EmployeeXGroupCollectionQueryHandler(ICollectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<EmployeeXGroupCollectionVM>> Handle(EmployeeXGroupCollectionQuery request, CancellationToken cancellationToken)
        {
            var obj = await _repository.EmployeeXGroupCollectionInfo(request.officeId,request.employeeId,request.transactionDate);
            return obj;
        }
    }
}
