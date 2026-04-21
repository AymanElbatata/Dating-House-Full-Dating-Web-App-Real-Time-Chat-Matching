using AutoMapper;
using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.BLL.Repositories;
using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.Helpers;
using AYMDatingCore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Bcpg;
using System.Linq.Expressions;

namespace AYMDatingCore.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController(ILogger<AdminController> logger, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : Controller
    {
        private readonly ILogger<AdminController> _logger = logger;

        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IMapper Mapper = mapper;
        private readonly IConfiguration configuration = configuration;

        public async Task<IActionResult> Index()
        {
            var model = new UserSelectViewModel
            { 
            Users = await unitOfWork.UserManager.Users.Where(a => a.IsActivated == true && a.Email !="ayman.fathy.elbatata@gmail.com")
                .Select(u => new SelectListItem
                {
                    Value = u.Id,
                    Text = u.UserName + "/" + u.Email,
                })
                .ToListAsync()
                };
            return View(model);
        }

        public async Task<IActionResult> UserMessages(string? userId)
        {
            var userMessages = unitOfWork.UserMessageRepository.GetAllCustomized(
                filter: a => a.SenderAppUserId == userId || a.ReceiverAppUserId == userId,
                includes: new Expression<Func<UserMessageTBL, object>>[]
                {
                    p => p.SenderAppUser!,
                    p => p.ReceiverAppUser!
                }).OrderByDescending(a => a.CreationDate);
            var user = await unitOfWork.UserManager.FindByIdAsync(userId);
            ViewBag.SelectedUserId = userId;
            ViewBag.SelectedUserName = user != null ? user.UserName : "Unknown User";
            ViewBag.SelectedUserEmail = user != null ? user.Email : "Unknown Email";
            return View(Mapper.Map<List<UserMessage_VM>>(userMessages));
        }

        public IActionResult UserIpLists(string? userId)
        {
            var userList = unitOfWork.UserAddressListTBLRepository.GetAllCustomized(
                filter: a => a.AppUserId == userId,
                includes: new Expression<Func<UserAddressListTBL, object>>[]
                {
                    p => p.AppUser!,
                }).OrderByDescending(a => a.CreationDate);

            return View(Mapper.Map<List<UserAddressListTBL_VM>>(userList));
        }

        public IActionResult UserEmails()
        {
            var userEmails = unitOfWork.EmailTBLRepository.GetAll().OrderByDescending(a => a.CreationDate);
            return View(Mapper.Map<List<EmailTBL_VM>>(userEmails));
        }

        public IActionResult UserReports()
        {
            var userReports = unitOfWork.UserReportRepository.GetAllCustomized(
                includes: new Expression<Func<UserReportTBL, object>>[]
                {
                    p => p.SenderAppUser!,
                    p => p.ReceiverAppUser!
                }).OrderByDescending(a => a.CreationDate);
            return View(Mapper.Map<List<UserReportTBL_VM>>(userReports));
        }

        public IActionResult UserContactUs()
        {
            var userContactMsgs = unitOfWork.ContactUsRepository.GetAll().OrderByDescending(a => a.CreationDate);
            return View(Mapper.Map<List<ContactUsTBL_VM>>(userContactMsgs));
        }

        #region Allowances / AllowancePanel
        public IActionResult AllowancePanel()
        {
            var AdminAllowedPanel = unitOfWork.AdminPanelTBLRepository.GetAll().OrderByDescending(a => a.CreationDate);
            return View(Mapper.Map<List<AdminPanelTBL_VM>>(AdminAllowedPanel));
        }

        [HttpPost]
        public IActionResult TogglePanelActivation(int id, bool activation)
        {
            var panel = unitOfWork.AdminPanelTBLRepository.GetById(id);
            if (panel == null)
                return NotFound();

            panel.Activation = activation;
            unitOfWork.AdminPanelTBLRepository.Update(panel);

            return Ok(new { success = true });
        }
        #endregion

        #region Manage Users

        public IActionResult UserManagement()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await unitOfWork.UserManager.Users.Where(a => a.Email != "ayman.fathy.elbatata@gmail.com")
                .Select(u => new
                {
                    u.Id,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.UserName,
                    u.IsBlocked,
                    u.IsDeleted,
                    u.IsOnline,
                    u.IsActivated
                })
                .ToListAsync();

            var userViewModels = new List<UserViewModel>();
            foreach (var user in users)
            {
                var usermodel = new UserViewModel();
                usermodel.Email = user.Email;
                usermodel.FirstName = user.FirstName;
                usermodel.LastName = user.LastName;
                usermodel.UserName = user.UserName;
                usermodel.IsBlocked = user.IsBlocked;
                usermodel.IsDeleted = user.IsDeleted;
                usermodel.isOnline = user.IsOnline;
                usermodel.isActivated = user.IsActivated;
                usermodel.Id = user.Id;
                var UserinRules = unitOfWork.UserManager.Users.FirstOrDefault(u => u.Email == user.Email);
                usermodel.Roles = (List<string>)unitOfWork.UserManager.GetRolesAsync(UserinRules).Result ?? new List<string>();
                userViewModels.Add(usermodel);
            }

            return Json(userViewModels);
        }

        // GET: Users/GetRoles
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await unitOfWork.RoleManager.Roles
                .Select(r => new { Id = r.Id, Name = r.Name })
                .ToListAsync();

            var RolesModels = new List<RoleViewModel>();
            foreach (var user in roles)
            {
                var roleModel = new RoleViewModel();
                roleModel.Id = user.Id;
                roleModel.Name = user.Name;
                RolesModels.Add(roleModel);
            }

            return Json(RolesModels);
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserInputModel model)
        {
            if (ModelState.IsValid)
            {
                if (await unitOfWork.UserManager.FindByEmailAsync(model.Email) != null)
                {
                    return Json(new { success = false, error = "Email is already registered" });
                }
                var user = new AppUser
                {
                    UserName = model.FirstName + "." + model.LastName + "-" + unitOfWork.MySPECIALGUID.GetUniqueKey(6),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = "Default Address",
                    Phone = "0123456789",
                    CountryTBLId = 1,
                    GenderTBLId = 1,
                    ActivationCode = unitOfWork.MySPECIALGUID.GetUniqueKey(12),
                    IsActivated = true,
                    DateOfBirth = new DateTime(1980, 1, 1),
                };
                var result = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var userhistory = new UserHistoryTBL
                    {
                        AppUserId = user.Id,
                        SearchAgeFrom = 18,
                        SearchAgeTo = 99,
                        CountryId = 1,
                        EducationId = 1,
                        FinancialModeId = 1,
                        GenderId = 1,
                        ProfessionId = 1,
                        ProfileHeading = "Your Heading.....",
                        AboutPartner = "AboutPartner...",
                        AboutYou = "AboutYou...",
                        City = "City...",
                        IsMain = true,
                        LanguageId = 1,
                        MaritalStatusId = 1,
                        PurposeId = 1,
                        IsSwitchedOff = false,
                    };
                    unitOfWork.UserHistoryRepository.Add(userhistory);

                    if (model.SelectedRoles != null && model.SelectedRoles.Any())
                    {
                        await unitOfWork.UserManager.AddToRolesAsync(user, model.SelectedRoles);
                    }
                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        // GET: Users/GetUser/{id}
        [HttpGet]
        public async Task<IActionResult> GetUser(string id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new UserInputModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                IsBlocked = user.IsBlocked,
                isDeleted = user.IsDeleted,
                isOnline = user.IsOnline,
                isActivated = user.IsActivated,
                IsSwitchedOff = unitOfWork.UserHistoryRepository.GetAllCustomized(
                                filter: a => a.IsMain == true && a.AppUserId == id).OrderBy(a => a.CreationDate).FirstOrDefault().IsSwitchedOff,

                SelectedRoles = (await unitOfWork.UserManager.GetRolesAsync(user)).ToList()
            };

            return Json(model);
        }

        // POST: Users/Edit/{id}
        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromBody] UserInputModelUpdate model)
        {
            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Json(new { success = false, error = "User not found" });
                }

                if (unitOfWork.UserManager.FindByEmailAsync(model.Email)?.Result?.Id != id)
                {
                    return Json(new { success = false, error = "Email is already registered for another user" });
                }
                else if (unitOfWork.UserManager.FindByNameAsync(model.UserName)?.Result?.Id != id)
                {
                    return Json(new { success = false, error = "Username is already registered for another user" });
                }

                user.Email = model.Email;
                user.UserName = model.UserName;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.IsBlocked = model.IsBlocked;
                user.IsActivated = model.isActivated;
                if (!model.isDeleted && unitOfWork.UserHistoryRepository.GetAllCustomized(filter: a => a.IsDeleted == true && a.IsMain == true  && a.AppUserId == user.Id).Any())
                {
                    user.IsDeleted = false;
                    var UserHistory = unitOfWork.UserHistoryRepository.GetAllCustomized(filter: a => a.IsDeleted == true && a.IsMain == true && a.AppUserId == user.Id).LastOrDefault();
                    if (UserHistory != null)
                    {
                        var newHistory = UserHistory;
                        UserHistory.IsMain = false;
                        unitOfWork.UserHistoryRepository.Update(UserHistory);

                        newHistory.IsMain = true;
                        newHistory.IsDeleted = false;
                        newHistory.IsSwitchedOff = false;
                        newHistory.ID = 0;
                        unitOfWork.UserHistoryRepository.Add(newHistory);
                    }
                }
                else if (model.isDeleted)
                {
                    user.IsDeleted = true;
                    var UserHistory = unitOfWork.UserHistoryRepository.GetAllCustomized(filter: a => a.IsDeleted == false && a.IsMain == true && a.AppUserId == user.Id).FirstOrDefault();
                    if (UserHistory != null)
                    {
                        UserHistory.IsDeleted = true;
                        unitOfWork.UserHistoryRepository.Update(UserHistory);
                    }
                }
                    var result = await unitOfWork.UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var currentRoles = await unitOfWork.UserManager.GetRolesAsync(user);
                    await unitOfWork.UserManager.RemoveFromRolesAsync(user, currentRoles);

                    if (model.SelectedRoles != null && model.SelectedRoles.Any())
                    {
                        await unitOfWork.UserManager.AddToRolesAsync(user, model.SelectedRoles);
                    }
                     await unitOfWork.UserManager.UpdateSecurityStampAsync(user);

                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });
        }

        // POST: Users/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await unitOfWork.UserManager.FindByIdAsync(id);
            if (user != null && user.IsDeleted)
            {
                return Json(new { success = false, error = "User was deleted before!" });
            }
            else if (user != null && !user.IsDeleted)
            {
                var UserHistory = unitOfWork.UserHistoryRepository.GetAllCustomized(filter: a => a.IsDeleted == false && a.IsMain == true && a.AppUserId == id).FirstOrDefault();
                if (UserHistory != null)
                {
                    UserHistory.IsDeleted = true;
                    unitOfWork.UserHistoryRepository.Update(UserHistory);
                }
                user.IsDeleted = true;
                var result = await unitOfWork.UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description) });
            }
            return Json(new { success = false, error = "User not found!" });
        }

        #endregion

        #region LogoutAllUsers
        [HttpGet]
        public async Task<IActionResult> LogoutAllUsers()
        {
            var Users = await unitOfWork.UserManager.Users.Where(a => a.IsActivated == true && a.IsDeleted == false && a.Email != "ayman.fathy.elbatata@gmail.com").ToListAsync();
            foreach (var user in Users)
            {
                user.IsOnline = false;
                user.LastSeen = DateTime.Now;
                await unitOfWork.UserManager.UpdateAsync(user);

                await unitOfWork.UserManager.UpdateSecurityStampAsync(user);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}

