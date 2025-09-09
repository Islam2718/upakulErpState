using MessageBroker.Services.Persistence;
using Utility.Domain.DBDomain;

namespace MessageBroker.Services.Contacts.Service.DBService
{
    public class EmployeeService
    {
        private string _connectionString { get; set; }
        public EmployeeService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async Task<string> Add(CommonEmployee model)
        {
            try
            {
                using (var db = new AppDbContext(_connectionString))
                {
                    var obj = await db.employees.FindAsync(model.EmployeeId);
                    /*Update*/
                    if (obj != null)
                    {
                        obj.OfficeId = model.OfficeId;
                        obj.ProjectId = model.ProjectId;
                        obj.DesignationId = model.DesignationId;
                        obj.EmployeeFullName = model.EmployeeFullName;
                        obj.EmployeeCode = model.EmployeeCode;
                        obj.EmployeeNameBn = model.EmployeeNameBn;
                        obj.DepartmentName = model.DepartmentName;
                        obj.DesignationName = model.DesignationName;
                        obj.Gender = model.Gender;
                        //obj.ParentId = model.ParentId;
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
