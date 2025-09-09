using System.Net;
using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Domain.Models;
using MediatR;
using Utility.Constants;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Commands.Delete.Handlers
{

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, CommadResponse>
    {
        IMapper _mapper;
        IEmployeeRepository _repository;
        public DeleteEmployeeCommandHandler(IMapper mapper, IEmployeeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var _obj = await _repository.GetById(request.Employeeid);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteEmployeeCommand, Employee>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }

}
