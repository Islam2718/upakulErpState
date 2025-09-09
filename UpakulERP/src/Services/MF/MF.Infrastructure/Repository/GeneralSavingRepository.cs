using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using EF.Core.Repository.Repository;
using MF.Application.Contacts.Persistence;
using MF.Domain.Models.Loan;
using MF.Domain.Models.Saving;
using MF.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MF.Infrastructure.Repository
{
   
    public class GeneralSavingRepository :  IGeneralSavingRepository
    {
        private readonly AppDbContext _context;

        public GeneralSavingRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task NewSavings(GeneralSavingSummary obj)
        {
            _context.generalSavingSummary.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task Collection(long summaryId, int amount, DateTime transactionDt)
        {
            var obj = _context.generalSavingSummary.FirstOrDefault(x => x.GeneralSummaryId == summaryId);
            if (obj != null)
            {
                GeneralSavingSummaryDetails details = new GeneralSavingSummaryDetails
                {
                    GeneralSummaryId = summaryId,
                    Payment = amount,
                    TransactionDate = transactionDt,
                    TransactionType = "Deposit",
                    Receipt = amount
                };
                obj.PrincipleAmount += amount;
                await _context.SaveChangesAsync();
            }
        }
    }

}
