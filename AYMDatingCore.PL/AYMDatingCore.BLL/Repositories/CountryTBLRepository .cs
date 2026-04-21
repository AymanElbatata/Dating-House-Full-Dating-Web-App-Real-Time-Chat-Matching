using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.BLL.Repositories
{
    public class CountryTBLRepository : GenericRepository<CountryTBL>, ICountryTBLRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public CountryTBLRepository(AymanDatingCoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
