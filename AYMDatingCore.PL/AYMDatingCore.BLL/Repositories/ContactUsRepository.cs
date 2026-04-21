using AYMDatingCore.BLL.Repositories;
using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.BLL.Repositories
{
    public class ContactUsRepository : GenericRepository<ContactUsTBL> , IContactUsRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public ContactUsRepository(AymanDatingCoreDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
