using HRM.Application.Contacts.Persistence;
using HRM.Domain.ViewModels;
using MediatR;
using Utility.Response;

namespace HRM.Application.Features.DBOrders.Queries.BoardUniversity
{
    public class BoardUniversityGirdQueryHandler : IRequestHandler<BoardUniversityGirdQuery, PaginatedResponse<BoardUniversityVM>>
    {
        private readonly IBoardUniversityRepository _repository;

        public BoardUniversityGirdQueryHandler(IBoardUniversityRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResponse<BoardUniversityVM>> Handle(BoardUniversityGirdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.LoadGrid(
                request.Page, request.PageSize, request.Search, request.SortOrder);
        }
    }




}
