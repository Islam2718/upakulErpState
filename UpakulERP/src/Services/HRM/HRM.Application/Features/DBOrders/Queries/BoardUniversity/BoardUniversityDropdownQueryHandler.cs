using HRM.Application.Contacts.Persistence;
using MediatR;
using Utility.Constants;
using Utility.Domain;

namespace HRM.Application.Features.DBOrders.Queries.BoardUniversity
{
    public class BoardUniversityDropdownQueryHandler : IRequestHandler<BoardUniversityDropdownQuery, List<CustomSelectListItem>>
    {
        IBoardUniversityRepository _repository;
        public BoardUniversityDropdownQueryHandler(IBoardUniversityRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CustomSelectListItem>> Handle(BoardUniversityDropdownQuery request, CancellationToken cancellationToken)
        {
            var list = new List<CustomSelectListItem>();
            list.Add(new CustomSelectListItem { Text = MessageTexts.drop_down, Value = "", Selected = true });
            list.AddRange(_repository.GetAll().ToList().Select(s => new CustomSelectListItem
            {
                Text = s.BUName,
                Value = s.BUId.ToString()
            }));
            return list;
        }

    }
}
