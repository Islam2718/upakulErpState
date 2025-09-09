using MessageBroker.Services.Persistence;
using Utility.Domain.DBDomain;

namespace MessageBroker.Services.Contacts.Service.DBService
{
    public class OfficeService
    {
        private string _connectionString { get; set; }
        public OfficeService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<string> Add(CommonOffice model)
        {
            try
            {
                using (var db = new AppDbContext(_connectionString))
                {
                    var obj = await db.offices.FindAsync(model.OfficeId);
                    /*Update*/
                    if (obj != null)
                    {
                        obj.OfficeCode = model.OfficeCode;
                        obj.OfficeName = model.OfficeName;
                        obj.OfficeEmail = model.OfficeEmail;
                        obj.OfficePhoneNo = model.OfficePhoneNo;
                        obj.OfficeType = model.OfficeType;
                        obj.ParentId = model.ParentId;
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
