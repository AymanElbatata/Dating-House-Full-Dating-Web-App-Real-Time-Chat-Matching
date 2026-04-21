using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AYMDatingCore.BLL.Repositories;

namespace AYMDatingCore.BLL.Repositories
{
    public class FinancialModeRepository : GenericRepository<FinancialModeTBL> , IFinancialModeRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public FinancialModeRepository(AymanDatingCoreDbContext context) : base(context)
        {
            _context = context;
        }

    }
}
