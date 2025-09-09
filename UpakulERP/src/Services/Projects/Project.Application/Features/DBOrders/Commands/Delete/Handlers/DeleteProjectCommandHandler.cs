using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Project.Application.Contacts.Persistence;
using Project.Application.Features.DBOrders.Commands.Delete.Commands;
using Project.Domain.Models;
using Utility.Constants;
using Utility.Response;

namespace Project.Application.Features.DBOrders.Commands.Delete.Handlers
{
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, CommadResponse>
    {
        IMapper _mapper;
        IProjectRepository _repository;
        public DeleteProjectCommandHandler(IMapper mapper, IProjectRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<CommadResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var _obj = _repository.GetById(request.ProjectId);
            if (_obj != null)
            {
                var obj = _mapper.Map<DeleteProjectCommand, Projects>(request, _obj);
                bool isSuccess = await _repository.UpdateAsync(obj);
                return (isSuccess ? new CommadResponse(MessageTexts.delete_success, HttpStatusCode.Accepted) : new CommadResponse(MessageTexts.delete_failed, HttpStatusCode.BadRequest));
            }
            else
                return new CommadResponse(MessageTexts.data_not_found, HttpStatusCode.NotFound);
        }
    }
}
