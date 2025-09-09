using AutoMapper;
using Global.Application.Features.DBOrders.Commands.Create.Command;
using Global.Application.Features.DBOrders.Commands.Create.Commands;
using Global.Application.Features.DBOrders.Commands.Delete.Command;
using Global.Application.Features.DBOrders.Commands.Update.Command;
using Global.Domain.Models;
using Global.Domain.Models.Views;
using Global.Domain.ViewModels;
using Utility.Domain.DBDomain;

namespace Global.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Office, CreateOfficeCommand>().ReverseMap();
            CreateMap<Office, UpdateOfficeCommand>().ReverseMap();
            CreateMap<Office, DeleteOfficeCommand>().ReverseMap();
            CreateMap<Office, OfficeVM>().ReverseMap();
            CreateMap<CreateOfficeCommand, CommonOffice>().ReverseMap();
            CreateMap<UpdateOfficeCommand, CommonOffice>().ReverseMap();
            CreateMap<Office, CommonOffice>().ReverseMap();
            CreateMap<CommonOffice, VWOffice>().ReverseMap();
            CreateMap<OfficeVM, CommonOffice>().ReverseMap();
            



            CreateMap<Bank, CreateBankCommand>().ReverseMap();
            CreateMap<Bank, UpdateBankCommand>().ReverseMap();
            CreateMap<Bank, DeleteBankCommand>().ReverseMap();
            CreateMap<Bank, BankVM>().ReverseMap();
            CreateMap<CreateBankCommand, CommonBank>().ReverseMap();
            CreateMap<UpdateBankCommand, CommonBank>().ReverseMap();
            CreateMap<BankVM, CommonBank>().ReverseMap();

            //J#
            CreateMap<GeoLocation, CreateGeoLoactionCommand>().ReverseMap();
            CreateMap<GeoLocation, UpdateGeoLocationCommand>().ReverseMap();
            CreateMap<GeoLocation, DeleteGeoLocationCommand>().ReverseMap();
            CreateMap<GeoLocation, GeoLocationVM>().ReverseMap();
            CreateMap<CreateGeoLoactionCommand, CommonGeoLocation>().ReverseMap();
            CreateMap<UpdateGeoLocationCommand, CommonGeoLocation>().ReverseMap();
            CreateMap<GeoLocationVM, CommonGeoLocation>().ReverseMap();
            CreateMap<VWGeoLocation, CommonGeoLocation>().ReverseMap();

            CreateMap<Country, CreateCountryCommand>().ReverseMap();
            CreateMap<Country, UpdateCountryCommand>().ReverseMap();
            CreateMap<Country, DeleteCountryCommand>().ReverseMap();
            CreateMap<Country, CountryVM>().ReverseMap();

        }
    }
}
