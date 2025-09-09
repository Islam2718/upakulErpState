using AutoMapper;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;

namespace HRM.Application.Features.DBOrders.Queries.BoardUniversity
{
    class BoardUniversityByIdHandler : IRequestHandler<BoardUniversityByIdQuery, BoardUniversityVM>
    {
        IBoardUniversityRepository _repository;
        IMapper _mapper;
        public BoardUniversityByIdHandler(IBoardUniversityRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BoardUniversityVM> Handle(BoardUniversityByIdQuery request, CancellationToken cancellationToken)
        {
            var obj = _repository.GetById(request.id);
            return _mapper.Map<BoardUniversityVM>(obj);
        }
    }
}
