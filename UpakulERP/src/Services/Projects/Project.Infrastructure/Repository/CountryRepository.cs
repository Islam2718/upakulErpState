using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF.Core.Repository.Repository;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contacts.Persistence;
using Project.Infrastructure.Persistence;
using Utility.Domain.DBDomain;

namespace Project.Infrastructure.Repository
{
    class CountryRepository : CommonRepository<CommonCountry>, ICountryRepository
    {
        AppDbContext _context;
        public CountryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<CommonCountry> GetAll()
        {
            var objlst = _context.countries.Where(c => c.IsActive).ToList();
            return objlst;
        }

    }


}
