using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.BLL.Repositories;
using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Contexts;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.DAL.IRepositories;
using AYMDatingCore.Helpers;
using AYMDatingCore.PL.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AYMDatingCore.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaximumReceiveMessageSize = 1024 * 1024 * 10; // ✅ 10 MB
            });
            builder.Services.AddControllersWithViews()
                .AddViewOptions(options =>
                {
                    options.HtmlHelperOptions.ClientValidationEnabled = true;
                });

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(1); // Session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddDbContext<AymanDatingCoreDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure()
            ));
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .SetIsOriginAllowed(_ => true));
            });

            //services.AddSingleton 
            builder.Services.AddSingleton<IUserIdProvider, NameUserIdProvider>();
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IMySPECIALGUID, MySPECIALGUID>();
            builder.Services.AddScoped<ICountryTBLRepository, CountryTBLRepository>();
            builder.Services.AddScoped<IGenderTBLRepository, GenderTBLRepository>();
            builder.Services.AddScoped<IMaritalStatusRepository, MaritalStatusRepository>();
            builder.Services.AddScoped<IUserAddressListTBLRepository, UserAddressListTBLRepository>();
            builder.Services.AddScoped<IAppErrorTBLRepository, AppErrorTBLRepository>();
            builder.Services.AddScoped<IContactUsRepository, ContactUsRepository>();
            builder.Services.AddScoped<IEmailTBLRepository, EmailTBLRepository>();
            builder.Services.AddScoped<IAdminPanelTBLRepository, AdminPanelTBLRepository>();

            builder.Services.AddScoped<IEducationRepository, EducationRepository>();
            builder.Services.AddScoped<IFinancialModeRepository, FinancialModeRepository>();
            builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
            builder.Services.AddScoped<IProfessionRepository, ProfessionRepository>();
            builder.Services.AddScoped<IPurposeRepository, PurposeRepository>();
            builder.Services.AddScoped<IUserBlockRepository, UserBlockRepository>();
            builder.Services.AddScoped<IUserFavoriteRepository, UserFavoriteRepository>();
            builder.Services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();
            builder.Services.AddScoped<IUserImageRepository, UserImageRepository>();
            builder.Services.AddScoped<IUserLikeRepository, UserLikeRepository>();
            builder.Services.AddScoped<IUserMessageGroupRepository, UserMessageGroupRepository>();
            builder.Services.AddScoped<IUserMessageRepository, UserMessageRepository>();
            builder.Services.AddScoped<IUserReportRepository, UserReportRepository>();
            builder.Services.AddScoped<IUserViewRepository, UserViewRepository>();

            //services.AddTransient

            builder.Services.AddAutoMapper(m => m.AddProfile(new MappingProfiles()));



            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<AymanDatingCoreDbContext>()
            .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.AccessDeniedPath = "/Home/Error";

                options.ExpireTimeSpan = TimeSpan.FromDays(1); // جلسة 24 ساعة
                options.SlidingExpiration = true;

                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddAntiforgery(options =>
            {
                options.Cookie.Name = ".DatingHouse.AntiForgery";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

            builder.Services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.Zero;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            SeedInitialData.SeedData(app);

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowAll");
            app.UseSession();

            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var exceptionHandlerPathFeature =
                        context.Features.Get<IExceptionHandlerPathFeature>();

                    if (exceptionHandlerPathFeature?.Error != null)
                    {
                        var ex = exceptionHandlerPathFeature.Error;

                        // Log
                        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                        logger.LogError(ex, "Global exception caught at {Path}", exceptionHandlerPathFeature.Path);

                        // Save to DB
                        using var scope = context.RequestServices.CreateScope();
                        var repo = scope.ServiceProvider.GetRequiredService<IAppErrorTBLRepository>();
                        repo.Add(new AppErrorTBL
                        {
                            Message = ex.Message,
                            StackTrace = ex.StackTrace ?? "",
                            Controller = exceptionHandlerPathFeature.Path ?? "",
                            Action = "" // optional
                        });
                    }

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new
                    {
                        error = "An unexpected error occurred. Please try again later."
                    }));
                });
            });
            app.MapHub<ChatHub>("/chatHub");
            app.MapHub<NotificationHub>("/notificationHub");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.Run();
        }
    }
}
