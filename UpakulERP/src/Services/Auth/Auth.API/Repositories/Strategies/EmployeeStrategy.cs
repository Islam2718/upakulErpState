using Auth.API.Context;
using Auth.API.Models.View;
using Auth.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Utility.Domain;
using Utility.Enums.HRM;

namespace Auth.API.Repositories.Strategies
{
    public class EmployeeStrategy(AppDbContext context) : IEmployeeStrategy
    {

        public async Task<VWEmployee> FindByEmpId(int id)
        {
            try
            {
                return await context.vw_employees.FirstAsync(x => x.EmployeeId == id);
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        public List<CustomSelectListItem> GetEmployeeDropdown(int officeId, int? empId)
        {
            var lst = new List<CustomSelectListItem>();
            string sql = "";
            if ((empId??0)>0)
                sql = $"SELECT Value=CAST(EmployeeId AS varchar(10)),Text=EmployeeCode +' - '+ FirstName+' '+ISNULL(LastName,''),Selected=CAST(0 as bit) FROM emp.Employee e INNER JOIN udf_OfficeHierarchical({officeId},0) f ON e.OfficeId=f.OfficeId WHERE e.IsActive=1 AND e.EmployeeStatusId IN('{EmployeeStatus.active}','{EmployeeStatus.salaryHeldup}') AND e.EmployeeId = {empId}";

            //lst.Add(new CustomSelectListItem { Text = MessageTexts.drop_down,Value="",Selected=true });
            else
                sql = $"SELECT Value=CAST(EmployeeId AS varchar(10)),Text=EmployeeCode +' - '+ FirstName+' '+ISNULL(LastName,''),Selected=CAST(0 as bit) FROM emp.Employee e INNER JOIN udf_OfficeHierarchical({officeId},0) f ON e.OfficeId=f.OfficeId WHERE e.IsActive=1 AND e.EmployeeStatusId IN('{EmployeeStatus.active}','{EmployeeStatus.salaryHeldup}') AND NOT EXISTS (SELECT 1 FROM sec.AspNetUsers u WHERE u.IsActive = 1 AND u.EmployeeId = e.EmployeeId)";
            var ddl_lst = context.Database.SqlQueryRaw<CustomSelectListItem>(sql).ToList().OrderBy(x => x.Text).ToList();
            if (ddl_lst.Any())
                lst.AddRange(ddl_lst);
            return lst;
        }
    }
}