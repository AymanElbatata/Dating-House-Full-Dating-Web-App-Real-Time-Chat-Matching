using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;

namespace AYMDatingCore.BLL.Repositories
{
    public class UserAddressListTBLRepository : GenericRepository<UserAddressListTBL>, IUserAddressListTBLRepository
    {
        private readonly AymanDatingCoreDbContext _context;

        public UserAddressListTBLRepository(AymanDatingCoreDbContext context) :base(context)
        {
            _context = context;
        }

    }
}
