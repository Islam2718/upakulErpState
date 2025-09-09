using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models;
using HRM.Domain.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.LeaveSetup
{
    public class LeaveSetupGridQueryHandler : IRequestHandler<LeaveSetupGridQuery, PaginatedResponse<LeaveSetupVM>>
    {
        private readonly ILeaveSetupRepository _repository;

        public LeaveSetupGridQueryHandler(ILeaveSetupRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<LeaveSetupVM>> Handle(LeaveSetupGridQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(
                request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }
}

