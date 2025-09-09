using Dapper;
using MF.Application.Contacts.Persistence;
using MF.Domain.ViewModels.Collection;
using MF.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace MF.Infrastructure.Repository.Collection
{
    public class CollectionRepository: ICollectionRepository
    {
        AppDbContext _context;
        private readonly string _connectionString;
        public CollectionRepository(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<List<EmployeeXGroupCollectionVM>> EmployeeXGroupCollectionInfo(int officeId, int employeeId, DateTime transactionDate)
        {
            List<EmployeeXGroupCollectionVM> lst = new List<EmployeeXGroupCollectionVM>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var param = new { officeId = officeId, employeeId = employeeId, transactionDate = transactionDate };
                var data = await connection.QueryAsync<EmployeeXGroupCollectionVM>("udp_EmployeeXGroupCollection", param, commandType: CommandType.StoredProcedure);
                    lst = data.ToList();
            }
            return lst;
        }

        public async Task<List<GroupXMemberCollectionVM>> GroupXMemberCollectionInfo(int officeId, int employeeId, DateTime transactionDate,int groupId)
        {
            List<GroupXMemberCollectionVM> lst = new List<GroupXMemberCollectionVM>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var param = new { officeId = officeId, employeeId = employeeId, transactionDate = transactionDate, groupId= groupId };
                var data = await connection.QueryAsync<GroupXMemberCollectionVM>("udp_GroupXMemberCollection", param, commandType: CommandType.StoredProcedure);
                    lst = data.ToList();
            }
            return lst;
        }
    }
}
