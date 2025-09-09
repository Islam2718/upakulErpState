using MessageBroker.Services.Persistence;
using Utility.Domain.DBDomain;

namespace MessageBroker.Services.Contacts.Service.DBService
{
    public class BankService
    {
        private string _connectionString { get; set; }
        public BankService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<string> Add(CommonBank model)
        {
            try
            {
                using (var db = new AppDbContext(_connectionString))
                {
                    var obj = await db.banks.FindAsync(model.BankId);
                    /*Update*/
                    if (obj != null)
                    {
                        obj.BankShortCode = model.BankShortCode;
                        obj.BankName = model.BankName;
                        obj.BankType = model.BankType;
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
