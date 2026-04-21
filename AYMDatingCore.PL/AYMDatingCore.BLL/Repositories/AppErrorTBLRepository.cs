using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.BLL.Repositories
{
    public class AppErrorTBLRepository : GenericRepository<AppErrorTBL>, IAppErrorTBLRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public AppErrorTBLRepository(AymanDatingCoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
