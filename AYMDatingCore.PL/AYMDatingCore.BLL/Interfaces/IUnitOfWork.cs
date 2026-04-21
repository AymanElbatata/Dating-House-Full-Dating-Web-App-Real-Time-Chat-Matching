using AYMDatingCore.BLL.Repositories;
using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.IRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        ICountryTBLRepository CountryTBLRepository { get; }
        IGenderTBLRepository GenderTBLRepository { get; }
        IUserAddressListTBLRepository UserAddressListTBLRepository { get; }

        IAppErrorTBLRepository AppErrorTBLRepository { get; }
        IContactUsRepository ContactUsRepository { get; }
        IEmailTBLRepository EmailTBLRepository { get; }
        SignInManager<AppUser> SignInManager { get; }
        RoleManager<AppRole> RoleManager { get; }
        UserManager<AppUser> UserManager { get; }
        IMySPECIALGUID MySPECIALGUID { get; }
        IAdminPanelTBLRepository AdminPanelTBLRepository { get; }

         IEducationRepository EducationRepository { get; }
         IFinancialModeRepository FinancialModeRepository { get; }
         ILanguageRepository LanguageRepository { get; }
         IProfessionRepository ProfessionRepository { get; }
         IPurposeRepository PurposeRepository { get; }
         IMaritalStatusRepository MaritalStatusRepository { get; }
         IUserBlockRepository UserBlockRepository { get; }
         IUserFavoriteRepository UserFavoriteRepository { get; }
         IUserHistoryRepository UserHistoryRepository { get; }
         IUserImageRepository UserImageRepository { get; }
         IUserLikeRepository UserLikeRepository { get; }
         IUserMessageGroupRepository UserMessageGroupRepository { get; }
         IUserMessageRepository UserMessageRepository { get; }
         IUserReportRepository UserReportRepository { get; }
         IUserViewRepository UserViewRepository { get; }
    }
}
