using Auth.API.Context;
using Auth.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auth.API.Repositories.Strategies
{
    internal class MFTransactionDateStrategy(MFDbContext context) : IMFTransactionDateStrategy
    {
        public async Task<string?> GetTransactionDate(int officeId)
        {
            var obj = await context.dailyProcesses.FirstOrDefaultAsync(x => x.IsActive && !x.IsDayClose && !x.EndDate.HasValue && x.OfficeId == officeId);
            if (obj != null)
                return obj.TransactionDate.ToString("dd-MMM-yyyy");
            else return "";
        }
    }
}
