using Accounts.Application.Features.DBOrders.Commands.Create.Commands;
using Accounts.Application.Features.DBOrders.Commands.Delete.Command;
using Accounts.Application.Features.DBOrders.Commands.Update.Command;
using Accounts.Application.Features.DBOrders.Commands.Update.Commands;
using Accounts.Application.Features.DBOrders.Queries.BudgetComponent;
using Accounts.Domain.Models;
using Accounts.Domain.Models.Voucher;
using AutoMapper;

namespace Accounts.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BudgetComponent, CreateBudgetComponentCommand>().ReverseMap();
            CreateMap<BudgetComponent, UpdateBudgetComponentCommand>().ReverseMap();
            CreateMap<BudgetComponent, DeleteBudgetComponentCommand>().ReverseMap();
            CreateMap<BudgetComponent, BudgetComponentVM>().ReverseMap();


            CreateMap<BudgetEntry, CreateBudgetEntryCommand>().ReverseMap();
            CreateMap<BudgetEntry, UpdateBudgetEntryCommand>().ReverseMap();
            //CreateMap<BudgetEntry, BudgetEntryVM>().ReverseMap();


            CreateMap<AccountHead, CreateAccountHeadCommand>().ReverseMap();
            CreateMap<AccountHead, UpdateAccountHeadCommand>().ReverseMap();
            CreateMap<AccountHead, DeleteAccountHeadCommand>().ReverseMap();
        }
    }
}
