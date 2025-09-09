using EF.Core.Repository.Interface.Repository;
using HRM.Domain.Models.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Application.Contacts.Persistence
{
    public interface IFileUploadTestRepository : ICommonRepository<FileUploadTest>
    {
        Task<FileUploadTest> GetById(int id);
    }
}
