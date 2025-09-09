using System.Net;
using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using Utility.Response;

namespace MF.Application.Features.DBOrders.Commands.Create.Handlers
{
    public class CreateGroupWiseEmployeeAssignCommandHandler : IRequestHandler<CreateGroupWiseEmployeeAssignCommand, CommadResponse>
    {
        //#
        IMapper _mapper;
        IGroupWiseEmployeeAssignRepository _repository;
        public CreateGroupWiseEmployeeAssignCommandHandler(IMapper mapper, IGroupWiseEmployeeAssignRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<CommadResponse> Handle(CreateGroupWiseEmployeeAssignCommand request, CancellationToken cancellationToken)
        {
            if ((request.ReleaseEmployeeId ?? 0) > 0 && string.IsNullOrEmpty(request.ReleaseNote))
                return new CommadResponse("Release Note is required", HttpStatusCode.BadRequest);
            //else if ((request.ReleaseEmployeeId ?? 0) > 0 && !(request.ReleaseGroupListId??new List<int>()).Any())
            //    return new CommadResponse("Release Group are required", HttpStatusCode.BadRequest);
            else if ((request.ReleaseEmployeeId ?? 0) > 0 && string.IsNullOrEmpty(request.ReleaseDate))
                return new CommadResponse("Release date is required", HttpStatusCode.BadRequest);
            else if ((request.ReleaseEmployeeId ?? 0) > 0 && (request.AssignEmployeeId ?? 0) == 0)
                return new CommadResponse("Assign Employee is required", HttpStatusCode.BadRequest);

            else if ((request.AssignEmployeeId ?? 0) > 0 && string.IsNullOrEmpty(request.JoiningDate))
                return new CommadResponse("Joining Date is required", HttpStatusCode.BadRequest);
            else if ((request.AssignEmployeeId ?? 0) > 0 && !(request.AssignedGroupListId ?? new List<int>()).Any())
                return new CommadResponse("Assign Group are required", HttpStatusCode.BadRequest);
           
            DateTime? releaseDate = DateTime.TryParse((request.ReleaseDate ?? ""), out var tempRelease) ? tempRelease : null;
            DateTime? assignDate = DateTime.TryParse((request.JoiningDate??""), out var tempAssign) ? tempAssign : null;

            if((DateTime.MinValue.Equals((releaseDate ?? DateTime.Now))))
                return new CommadResponse("Release date is required", HttpStatusCode.BadRequest);
            else if ((DateTime.MinValue.Equals((assignDate ?? DateTime.Now))))
                return new CommadResponse("Joining Date is required", HttpStatusCode.BadRequest);
            return await _repository.CreateOrUpdateAsync(request.AssignEmployeeId, request.AssignedGroupListId, request.ReleaseEmployeeId, request.ReleaseGroupListId
                , request.loginEmployeeId,releaseDate,request.ReleaseNote, assignDate);
        }
    }


}
