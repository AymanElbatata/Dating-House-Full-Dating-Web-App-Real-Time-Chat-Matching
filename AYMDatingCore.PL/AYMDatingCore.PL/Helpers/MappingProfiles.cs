using AutoMapper;
using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.PL.Models;
using Microsoft.AspNetCore.Identity;

namespace AYMDatingCore.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ProfessionTBL, ProfessionTBL_VM>().ReverseMap();
            CreateMap<ContactUsTBL, ContactUsTBL_VM>().ReverseMap();
            CreateMap<CountryTBL, CountryTBL_VM>().ReverseMap();
            CreateMap<GenderTBL, GenderTBL_VM>().ReverseMap();
            CreateMap<EmailTBL, EmailTBL_VM>().ReverseMap();
            CreateMap<AppUser, UserTBL_VM>().ReverseMap();
            CreateMap<UserHistoryTBL, UserHistoryTBL_VM>().ReverseMap();
            CreateMap<UserImageTBL, UserImageTBL_VM>().ReverseMap();
            CreateMap<UserMessageTBL, UserMessage_VM>().ReverseMap();
            CreateMap<UserReportTBL, UserReportTBL_VM>().ReverseMap();
            CreateMap<AdminPanelTBL, AdminPanelTBL_VM>().ReverseMap();
            CreateMap<UserAddressListTBL, UserAddressListTBL_VM>().ReverseMap();
        }
    }
}
