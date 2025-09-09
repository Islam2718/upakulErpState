using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Queries.Occupation;
using MF.Domain.ViewModels;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Queries.EmployeeRegister
{
    public class EmployeeRegisterGridQueryHandler : IRequestHandler<EmployeeRegisterGridQuery, PaginatedResponse<GroupWiseEmployeeAssignVM>>
    {
        private readonly IGroupWiseEmployeeAssignRepository _repository;

        public EmployeeRegisterGridQueryHandler(IGroupWiseEmployeeAssignRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<GroupWiseEmployeeAssignVM>> Handle(EmployeeRegisterGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(request.Page, request.PageSize, request.Search, request.SortOrder, request.logedInOfficeId);
        }
    }

}
