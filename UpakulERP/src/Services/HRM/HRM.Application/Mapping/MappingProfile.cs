using AutoMapper;
using HRM.Application.Features.DBOrders.Commands.Create.Command;
using HRM.Application.Features.DBOrders.Commands.Create.Commands;
using HRM.Application.Features.DBOrders.Commands.Delete.Commands;
using HRM.Application.Features.DBOrders.Commands.Update.Commands;
using HRM.Domain.Models;
using HRM.Domain.Models.Views;
using HRM.Domain.ViewModels;
using Utility.Domain.DBDomain;

namespace HRM.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, CreateDepartmentCommand>().ReverseMap();
            CreateMap<EmployeeStatus, CreateEmployeeStatusCommand>().ReverseMap();
            CreateMap<EmployeeType, CreateEmployeeTypeCommand>().ReverseMap();
            
            CreateMap<Education, CreateEducationCommand>().ReverseMap();
            CreateMap<Education, UpdateEducationCommand>().ReverseMap();
            CreateMap<Education, DeleteEducationCommand>().ReverseMap();
            CreateMap<Education, EducationVM>().ReverseMap();

            CreateMap<BoardUniversity, CreateBoardUniversityCommand>().ReverseMap();
            CreateMap<BoardUniversity, UpdateBoardUniversityCommand>().ReverseMap();
            CreateMap<BoardUniversity, DeleteBoardUniversityCommand>().ReverseMap();
            CreateMap<BoardUniversity, BoardUniversityVM>().ReverseMap();

            CreateMap<Department, CreateDepartmentCommand>().ReverseMap();
            CreateMap<Department, UpdateDepartmentCommand>().ReverseMap();
            CreateMap<Department, DeleteDepartmentCommand>().ReverseMap();
            CreateMap<Department, DepartmentVM>().ReverseMap();

        

            CreateMap<Designation, CreateDesignationCommand>().ReverseMap();
            CreateMap<Designation, UpdateDesignationCommand>().ReverseMap();
            CreateMap<Designation, DeleteDesignationCommand>().ReverseMap();
            CreateMap<Designation, DesignationVM>().ReverseMap();
            CreateMap<DesignationVM, CommonDesignation>();
            CreateMap<DesignationVM, CommonDesignation>().ReverseMap();
            CreateMap<Designation, CommonDesignation>().ReverseMap();
            CreateMap<CreateDesignationCommand, CommonDesignation>().ReverseMap();
            CreateMap<UpdateDesignationCommand, CommonDesignation>().ReverseMap();


            CreateMap<HoliDay, CreateHoliDayCommand>().ReverseMap();
            CreateMap<HoliDay, UpdateHoliDayCommand>().ReverseMap();
            CreateMap<HoliDay, DeleteHoliDayCommand>().ReverseMap();
            CreateMap<HoliDay, HolidayVM>().ReverseMap();
            CreateMap<HolidayVM, CommonHoliday>().ReverseMap();
            CreateMap<CreateHoliDayCommand, CommonHoliday>().ReverseMap();
            CreateMap<UpdateHoliDayCommand, CommonHoliday>().ReverseMap();

            CreateMap<Employee, CreateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeCommand>().ReverseMap();
            CreateMap<Employee, DeleteEmployeeCommand>().ReverseMap();
            CreateMap<VWEmployee, CommonEmployee>().ReverseMap();


            CreateMap<LeaveSetup, CreateLeaveSetupCommand>().ReverseMap();
            CreateMap<LeaveSetup, UpdateLeaveSetupCommand>().ReverseMap();
            CreateMap<LeaveSetup, DeleteLeaveSetupCommand>().ReverseMap();
            CreateMap<LeaveSetup, LeaveSetupVM>().ReverseMap();

            CreateMap<OfficeTypeXConfigMaster, CreateOfficeTypeXConfigMasterCommand>().ReverseMap();
            CreateMap<OfficeTypeXConfigureDetails, CreateOfficeTypeXConfigureDetailsCommand>().ReverseMap();
            CreateMap<OfficeTypeXConfigMaster, OfficeTypeXConfigMasterVM>().ReverseMap();
            CreateMap<OfficeTypeXConfigureDetails, OfficeTypeXConfigureDetailsVM>().ReverseMap();
        }

    }
}
