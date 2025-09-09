using Auth.API.Context;
using Auth.API.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Utility.Domain.DBDomain;
using Dapper;

namespace Auth.API.Repositories.Strategies
{
    public class NotificationStrategy(AppDbContext context, IConfiguration configuration) : INotificationStrategy
    {
        public async Task<Notification> GetNotification(int officeId,int officeTypeId,int loggedinEmpId)
        {
            Notification notification=new Notification();
            string _connectionString = configuration.GetConnectionString("AuthConnection");
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var param = new { officeId = officeId, officeTypeId = officeTypeId, loggedinEmpId= loggedinEmpId };
                var lst = await connection.QueryAsync<NotificationModel>("[dbo].[udp_Notification]", param, commandType: CommandType.StoredProcedure);
                connection.Close();
                if(lst.Any())
                {
                    notification.count = lst.Count();
                    notification.summary = lst.ToList();
                }
            }
            return notification;

        }
    }
}
