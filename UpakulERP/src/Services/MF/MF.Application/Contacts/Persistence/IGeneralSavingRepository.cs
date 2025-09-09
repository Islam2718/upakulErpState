using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Interface.Repository;
using MF.Domain.Models.Saving;
using Utility.Domain.DBDomain;

namespace MF.Application.Contacts.Persistence
{
    public interface IGeneralSavingRepository 
    {
        Task NewSavings(GeneralSavingSummary obj);
        Task Collection(long summaryId, int amount, DateTime transactionDt);
    }

}
