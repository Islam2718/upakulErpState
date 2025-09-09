using MessageBroker.Services.Persistence;
using Utility.Domain.DBDomain;

namespace MessageBroker.Services.Contacts.Service.DBService
{
    public class HolidayService
    {
        private string _connectionString { get; set; }
        public HolidayService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<string> Add(CommonHoliday model)
        {
            try
            {
                using (var db = new AppDbContext(_connectionString))
                {
                    var obj = await db.holidays.FindAsync(model.HolidayId);
                    /*Update*/
                    if (obj != null)
                    {
                        obj.HolidayName = model.HolidayName;
                        obj.HolidayType = model.HolidayType;
                        obj.DateNumber = model.DateNumber;
                        obj.MonthNumber = model.MonthNumber;
                        obj.EndDate = model.EndDate;
                        obj.StartDate = model.StartDate;
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
