using AutoMapper;
using MF.Application.Features.DBOrders.Commands.Create.Commands;
using MF.Application.Features.DBOrders.Commands.Delete.Command;
using MF.Application.Features.DBOrders.Commands.Update.Command;
using MF.Application.Features.DBOrders.Commands.Update.Handler;
using MF.Application.Features.DBOrders.Queries.MasterComponent;
using MF.Application.Features.DBOrders.Queries.MRAPurpose;
using MF.Domain.Models;
using MF.Domain.Models.Loan;
using MF.Domain.Models.Saving;
using MF.Domain.Models.View;
using MF.Domain.ViewModels;


namespace MF.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group, CreateGroupCommand>().ReverseMap();
            CreateMap<Group, UpdateGroupCommand>().ReverseMap();
            CreateMap<Group, DeleteGroupCommand>().ReverseMap();
            CreateMap<Group, SamityVM>().ReverseMap();   
            
            CreateMap<MRAPurpose, MRAPurposeDTO>().ReverseMap();             
            CreateMap<Purpose, CreatePurposeCommand>().ReverseMap();
            CreateMap<Purpose, UpdatePurposeCommand>().ReverseMap();
            CreateMap<Purpose, DeletePurposeCommand>().ReverseMap();

            CreateMap<DailyProcess, InitialDayProcessCommand>().ReverseMap();
            CreateMap<DailyProcess, DailyProcessVM>().ReverseMap();

            CreateMap<Occupation, CreateOccupationCommand>().ReverseMap();
            CreateMap<Occupation, UpdateOccupationCommand>().ReverseMap();
            CreateMap<Occupation, DeleteOccupationCommand>().ReverseMap();
            CreateMap<Occupation, OccupationVM>().ReverseMap();

            CreateMap<Member, CreateMemberCommand>().ReverseMap();
            CreateMap<Member, UpdateMemberCommand>().ReverseMap();
            CreateMap<Member, UpdateMemberApprovedCommand>().ReverseMap();
            CreateMap<Member, UpdateMemberMobileNoCheckedCommand>().ReverseMap();
            CreateMap<Member, MemberVM>().ReverseMap();

            CreateMap<LoanApproval, CreateLoanApprovalCommand>().ReverseMap();
            CreateMap<LoanApproval, LoanApprovalVM>().ReverseMap();

            CreateMap<MasterComponent, CreateMasterComponentCommand>().ReverseMap();
            CreateMap<MasterComponent, UpdateMasterComponentCommand>().ReverseMap();
            CreateMap<MasterComponent, DeleteMasterComponentCommand>().ReverseMap();
            CreateMap<MasterComponent, MasterComponentVM>().ReverseMap();

            CreateMap<Domain.Models.Component, CreateComponentCommand>().ReverseMap();
            CreateMap<Domain.Models.Component, UpdateComponentCommand>().ReverseMap();
            CreateMap<Domain.Models.Component, DeleteComponentCommand>().ReverseMap();
            CreateMap<Domain.Models.Component, ComponentVM>().ReverseMap();

            CreateMap<Domain.Models.Component, CreateIdGenerateCommand>().ReverseMap();
            CreateMap<Domain.Models.Component, UpdateCodeGeneratorCommand>().ReverseMap();
            CreateMap<Domain.Models.Component, CodeGeneratorVM>().ReverseMap();

            CreateMap<LoanApplication, CreateLoanApplicationCommand>().ReverseMap();
            CreateMap<LoanApplication, UpdateLoanApplicationCommand>().ReverseMap();
            CreateMap<LoanApplication, DeleteLoanProposalCommand>().ReverseMap();
            CreateMap<LoanApplication, LoadGridForLoanApproveVM>().ReverseMap();
            CreateMap<LoanApplication, UpdateLoanApprovalFlowCommand>().ReverseMap();

            CreateMap<BankAccountMapping, CreateBankAccountMappingCommand>().ReverseMap();
            CreateMap<BankAccountMapping, UpdateBankAccountMappingCommand>().ReverseMap();
            CreateMap<BankAccountMapping, DeleteBankAccountMappingCommand>().ReverseMap();
            CreateMap<BankAccountMapping, BankAccountMappingVM>().ReverseMap();

            CreateMap<BankAccountCheque, CreateBankAccountChequeCommand>().ReverseMap();
            CreateMap<BankAccountCheque, BankAccountChequeVM>().ReverseMap();
            CreateMap<GraceSchedule, CreateGraceScheduleCommand>().ReverseMap();
            CreateMap<GraceSchedule, UpdateGraceScheduleCommand>().ReverseMap();
            CreateMap<GraceSchedule, DeleteGraceScheduleCommand>().ReverseMap();
            CreateMap<GraceSchedule, UpdateGraceScheduleApprovedCommand>().ReverseMap();
            CreateMap<GraceSchedule, UpdateGraceScheduleApprovedCommandHandler>().ReverseMap();
            CreateMap<GraceSchedule, VWGraceSchedule>().ReverseMap();
            CreateMap<OfficeComponentMapping, CreateOfficeComponentMappingCommand>().ReverseMap();
            CreateMap<OfficeComponentMapping, OfficeComponentMappingVM>().ReverseMap();

            CreateMap<GroupWiseEmployeeAssign, CreateGroupWiseEmployeeAssignCommand>().ReverseMap();
            CreateMap<GroupWiseEmployeeAssign, UpdateGroupWiseEmployeeAssignCommand>().ReverseMap();
            CreateMap<GroupWiseEmployeeAssign, GroupWiseEmployeeAssignVM>().ReverseMap();
                        
            CreateMap<GroupCommittee, GroupCommitteeVM>().ReverseMap();
            CreateMap<GroupCommittee, CreateGroupCommitteeCommand>().ReverseMap();

        }
    }
}
