using AutoMapper;
using MediatR;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels;

namespace MF.Application.Features.DBOrders.Queries.Member
{
    class MemberByIdQueryHandler : IRequestHandler<MemberByIdQuery, MemberVM>
    {
        IMemberRepository _repository;
        IMapper _mapper;
        public MemberByIdQueryHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<MemberVM> Handle(MemberByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<MemberVM>(obj);
        }
    }



}
