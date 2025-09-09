using MessageBroker.Services.Persistence;
using Utility.Domain.DBDomain;

namespace MessageBroker.Services.Contacts.Service.DBService
{
    public class DesignationService
    {
        private string _connectionString { get; set; }
        public DesignationService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<string> Add(CommonDesignation model)
        {
            try
            {
                using (var db = new AppDbContext(_connectionString))
                {
                    var obj = await db.designations.FindAsync(model.DesignationId);
                    /*Update*/
                    if (obj != null)
                    {
                        obj.DesignationCode = model.DesignationCode;
                        obj.DesignationName = model.DesignationName;
                        obj.OrderNo = model.OrderNo;
                        obj.IsActive = model.IsActive;
                    }
                    /* Add */
                    else
                        await db.AddAsync(model);
                    await db.SaveChangesAsync();
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
