using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    class MemberDetailByIdQueryHandler : IRequestHandler<MemberDetailByIdQuery, VWMember>
    {
        IMemberRepository _repository;
        IMapper _mapper;
        public MemberDetailByIdQueryHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<VWMember> Handle(MemberDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetMemberDetailById(request.id);
            return _mapper.Map<VWMember>(obj);
        }
    }



}
