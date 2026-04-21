using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.BLL.Repositories
{
    public class AdminPanelTBLRepository : GenericRepository<AdminPanelTBL>, IAdminPanelTBLRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public AdminPanelTBLRepository(AymanDatingCoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
