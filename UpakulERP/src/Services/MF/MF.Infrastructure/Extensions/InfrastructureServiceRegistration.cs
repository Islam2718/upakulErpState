using MF.Application.Contacts.Persistence;
using MF.Infrastructure.Persistence;
using MF.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CommonServices.Repository.Abastract;
using CommonServices.Repository.Implementation;
using MF.Infrastructure.Persistence.Repositories;
using MF.Infrastructure.Repository.Loan;
using MF.Application.Contacts.Persistence.Loan;
using MF.Infrastructure.Repository.Collection;

namespace MF.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IBankRepository, BankRepository>();
            services.AddScoped<IMRAPurposeRepository, MRAPurposeRepository>();
            services.AddScoped<IMainPurposeRepository, MainPurposeRepository>();
            services.AddScoped<IPurposeRepository, PurposeRepository>();
            services.AddScoped<IMasterComponentRepository, MasterComponentRepository>();
            services.AddScoped<IComponentRepository, ComponentRepository>();
            services.AddScoped<IIdGeneratorRepository, IdGeneratorRepository>();
            services.AddScoped<IDailyProcessRepository, DailyProcessRepository>();
            services.AddScoped<IOccupationRepository, OccupationRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ILoanApprovalRepository, LoanApprovalRepository>();
            services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<IGeoLocationRepository, GeoLocationRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IBankAccountMappingRepository, BankAccountMappingRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();            
            services.AddScoped<IGraceScheduleRepository, GraceScheduleRepository>();
            services.AddScoped<IOfficeComponentMappingRepository, OfficeComponentMappingRepository>();
            services.AddScoped<IGroupWiseEmployeeAssignRepository, GroupWiseEmployeeAssignRepository>();

            services.AddScoped<ILoanSummaryRepository, LoanSummaryRepository>();
            services.AddScoped<ICollectionRepository, CollectionRepository>();
            services.AddScoped<IGeneralSavingRepository, GeneralSavingRepository>();
            services.AddScoped<IGroupCommitteeRepository, GroupCommitteeRepository>();
            
            // Common  service
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IConverterService, ConverterService>();

            return services;
        }
    }
}
