using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.Department
{
    public class EmployeeGridQueryHandler : IRequestHandler<EmployeeGridQuery, PaginatedResponse<EmployeeVM>>
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeGridQueryHandler(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<EmployeeVM>> Handle(EmployeeGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder,request.OfficeId);
        }
    }
}
