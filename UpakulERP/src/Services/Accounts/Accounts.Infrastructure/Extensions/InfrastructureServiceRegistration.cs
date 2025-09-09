using Accounts.Application.Contacts.Persistence;
using Accounts.Application.Contacts.Persistence.Voucher;
using Accounts.Infrastructure.Persistence;
using Accounts.Infrastructure.Repository;
using Accounts.Infrastructure.Repository.Voucher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accounts.Infrastructure.Extensions
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IBudgetComponentRepository, BudgetComponentRepository>();
            services.AddScoped<IBudgetEntryRepository, BudgetEntryRepository>();
            services.AddScoped<IAccountHeadRepository, AccountHeadRepository>();
            return services;
        }
    }


}
