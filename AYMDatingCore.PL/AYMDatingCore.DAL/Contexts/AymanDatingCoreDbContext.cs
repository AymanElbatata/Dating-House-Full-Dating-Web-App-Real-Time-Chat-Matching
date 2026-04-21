using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDatingCore.DAL.Contexts
{
    public class AymanDatingCoreDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AymanDatingCoreDbContext(DbContextOptions<AymanDatingCoreDbContext> options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<GenderTBL> GenderTBLs { get; set; }
        public DbSet<CountryTBL> CountryTBLs { get; set; }
        public DbSet<UserAddressListTBL> UserAddressListTBLs { get; set; }
        public DbSet<EducationTBL> EducationTBLs { get; set; }
        public DbSet<FinancialModeTBL> FinancialModeTBLs { get; set; }
        public DbSet<ProfessionTBL> ProfessionTBLs { get; set; }
        public DbSet<LanguageTBL> LanguageTBLs { get; set; }
        public DbSet<MaritalStatusTBL> MaritalStatusTBLs { get; set; }
        public DbSet<PurposeTBL> PurposeTBLs { get; set; }
        public DbSet<UserBlockTBL> UserBlockTBLs { get; set; }
        public DbSet<UserFavoriteTBL> UserFavorites { get; set; }
        public DbSet<UserHistoryTBL> UserHistoryTBLs { get; set; }
        public DbSet<UserImageTBL> UserImageTBLs { get; set; }
        public DbSet<UserLikeTBL> UserLikeTBLs { get; set; }
        public DbSet<UserMessageGroupTBL> UserMessageGroupTBLs { get; set; }
        public DbSet<UserMessageTBL> UserMessageTBLs { get; set; }
        public DbSet<UserReportTBL> UserReportTBLs { get; set; }
        public DbSet<UserViewTBL> UserViewTBLs { get; set; }
        public DbSet<EmailTBL> EmailTBLs { get; set; }
        public DbSet<AppErrorTBL> AppErrorTBLs { get; set; }
        public DbSet<ContactUsTBL> ContactUsTBLs { get; set; }
        public DbSet<AdminPanelTBL> AdminPanelTBLs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BDataSchema");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().ToTable("Users", "ASecurity");
            modelBuilder.Entity<AppRole>().ToTable("Roles", "ASecurity");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "ASecurity");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "ASecurity");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "ASecurity");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "ASecurity");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "ASecurity");
        }
    }
}
