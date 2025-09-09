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

    public class DeleteDepartmentCommandHandler : IRequestHandler<DeleteDepartmentCommand, CommadResponse>
    {
        IMapper _mapper;
        IDepartmentRepository _repository;
        public DeleteDepartmentCommandHandler(IMapper mapper, IDepartmentRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.DepartmentId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteDepartmentCommand, Department>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
