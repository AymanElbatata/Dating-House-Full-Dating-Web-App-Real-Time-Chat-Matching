using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.BLL.Repositories
{
    public class EducationRepository : GenericRepository<EducationTBL> , IEducationRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public EducationRepository(AymanDatingCoreDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
