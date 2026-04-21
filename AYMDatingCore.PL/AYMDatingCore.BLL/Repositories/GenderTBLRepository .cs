using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.BLL.Repositories
{
    public class GenderTBLRepository : GenericRepository<GenderTBL>, IGenderTBLRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public GenderTBLRepository(AymanDatingCoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
