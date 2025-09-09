using MessageBroker.Services.Persistence;
using Utility.Domain.DBDomain;

namespace MessageBroker.Services.Contacts.Service.DBService
{
    public class GeoLOcationService
    {
        private string _connectionString { get; set; }
        public GeoLOcationService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<string> Add(CommonGeoLocation model)
        {
            try
            {
                using (var db = new AppDbContext(_connectionString))
                {
                    var obj = await db.geoLocations.FindAsync(model.GeoLocationId);
                    /*Update*/
                    if (obj != null)
                    {
                        obj.GeoLocationCode = model.GeoLocationCode;
                        obj.GeoLocationName = model.GeoLocationName;
                        obj.GeoLocationType = model.GeoLocationType;
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
