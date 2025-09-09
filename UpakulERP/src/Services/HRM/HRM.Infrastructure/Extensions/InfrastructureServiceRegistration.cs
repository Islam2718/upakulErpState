using CommonServices.Repository.Abastract;
using CommonServices.Repository.Implementation;
using HRM.Application.Contacts.Persistence;
using HRM.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UpakulHRM.Infrastructure.Persistence;

namespace HRM.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeTypeRepository, EmployeeTypeRepository>();
            services.AddScoped<IEmployeeStatusRepository, EmployeeStatusRepository>();
            services.AddScoped<IBoardUniversityRepository, BoardUniversityRepository>();
            services.AddScoped<IDesignationRepository, DesignationRepository>();
            services.AddScoped<IEducationRepository, EducationRepository>();
            services.AddScoped<IFileUploadTestRepository, FileUploadTestRepository>();
            services.AddScoped<IHoliDayRepository, HoliDayRepository>();
            services.AddScoped<ITrainingRepository, TrainingRepository>();
            services.AddScoped<ILeaveSetupRepository, LeaveSetupRepository>();
            services.AddScoped<IOfficeTypeXConfigMasterRepository, OfficeTypeXConfigMasterRepository>();
            services.AddScoped<IOfficeTypeXConfigureDetailsRepository, OfficeTypeXConfigureDetailsRepository>();


            // Common  service
            services.AddScoped<IFileService, FileService>();
            return services;
        }
    }
}
