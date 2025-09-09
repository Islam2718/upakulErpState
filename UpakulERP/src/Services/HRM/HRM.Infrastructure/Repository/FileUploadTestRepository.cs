using EF.Core.Repository.Repository;
using HRM.Application.Contacts.Persistence;
using HRM.Domain.Models.Test;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpakulHRM.Infrastructure.Persistence;

namespace HRM.Infrastructure.Repository
{
    public class FileUploadTestRepository : CommonRepository<FileUploadTest>, IFileUploadTestRepository
    {
        AppDbContext _context;
        public FileUploadTestRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<FileUploadTest> GetById(int id)
        {
            var lst = await _context.fileUploadTests.FindAsync(id);
            return lst;
        }


    }
}
