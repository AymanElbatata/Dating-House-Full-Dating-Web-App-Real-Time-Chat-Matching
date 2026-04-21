using AutoMapper;
using AYMDatingCore.BLL.Interfaces;
using AYMDatingCore.DAL.BaseEntity;
using AYMDatingCore.DAL.Entities;
using AYMDatingCore.PL.DTO;
using AYMDatingCore.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Utilities.Net;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using System.Web;

namespace AYMDatingCore.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper Mapper;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork unitOfWork, IMapper Mapper, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.Mapper = Mapper;
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl, string? Message)
        {
            if (configuration["AymanDatingCore.Pl.AllowedLogin"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowLogin")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl; // pass returnUrl to view

            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to Home page (or Dashboard, etc.)
                return RedirectToAction("Index", "Home");
            }
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = Message;
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl = null)
        {
            if (configuration["AymanDatingCore.Pl.AllowedLogin"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowLogin")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                if (user is null)
                {
                    ModelState.AddModelError("", "Invalid Email");
                    return View(model);
                }
                else if (user.IsBlocked)
                {
                    ModelState.AddModelError("", "Account is Blocked");
                    return View(model);
                }
                else if (user.IsDeleted)
                {
                    ModelState.AddModelError("", "Account is Deleted");
                    return View(model);
                }
                else if (!user.IsActivated)
                {
                    ModelState.AddModelError("", "Account isn't Activated");
                    return View(model);
                }
                else if (!user.EmailConfirmed)
                {
                    //ModelState.AddModelError("", "Email is not Confirmed, Check your email address now!");

                    user.ActivationCode = unitOfWork.MySPECIALGUID.GetUniqueKey(12);
                    var ActivateUserLink = configuration["AymanDatingCore.Pl.Url"] + "Account/Activation?Email=" + user.Email + "&ActivationCode=" + user.ActivationCode;
                    var result = await unitOfWork.UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var Email = new EmailTBL_VM();
                        Email.To = model.Email;
                        Email.Subject = configuration["AymanDatingCore.Pl.Name"] + " - Activate User";
                        Email.Body = await GetActivationTemplateAsync(user.FirstName, Email.Subject, user.ActivationCode, ActivateUserLink);
                        var newEmail = Mapper.Map<EmailTBL>(Email);

                        // Send email
                        await unitOfWork.EmailTBLRepository.SendEmailAsync(newEmail);
                        // Save Email
                        unitOfWork.EmailTBLRepository.Add(newEmail);

                        return RedirectToAction("Activation", "Account", new { Email = model.Email, Message = "Email is not Confirmed, Check your email address now!" });
                    }
                }

                var password = await unitOfWork.UserManager.CheckPasswordAsync(user, model.Password);

                if (password)
                {
                    var result = await unitOfWork.SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // 🧩 2️⃣ Get Browser and Device Info
                        string userBrowser = Request.Headers["User-Agent"].ToString();
                        var (ip, hostName) = await GetClientInfo(HttpContext);
                        unitOfWork.UserAddressListTBLRepository.Add(new UserAddressListTBL() { AppUserId = user.Id, OperationType ="Login", IpAddress = ip, HostName = hostName, Browser = userBrowser });
                        //

                        // User ONline NOW
                        user.IsOnline = true;
                        user.LastSeen = DateTime.Now;
                        await unitOfWork.UserManager.UpdateAsync(user);

                        var isAdmin = await unitOfWork.UserManager.IsInRoleAsync(user, "Admin");
                        if (isAdmin)
                            return RedirectToAction("Index", "Admin");

                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl); // redirect to the original page

                        var isUser = await unitOfWork.UserManager.IsInRoleAsync(user, "User");
                        if (isUser)
                            return RedirectToAction("UserMessages", "User");
                    }
                    return RedirectToAction("Index", "Home"); // default page
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Password");
                }
            }
            return View(model);
        }
        #endregion

        #region Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var user = await unitOfWork.UserManager.GetUserAsync(User);
            if (user != null)
            {
                user.IsOnline = false;
                user.LastSeen = DateTime.Now;
                await unitOfWork.UserManager.UpdateAsync(user);
            }

            await unitOfWork.SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (configuration["AymanDatingCore.Pl.AllowedRegistration"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowRegister")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to Home page (or Dashboard, etc.)
                return RedirectToAction("Index", "Home");
            }
            var model = new RegisterDTO
            {
                CountryOptions = unitOfWork.CountryTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false)
            .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name })
            .ToList(),
                GenderOptions = unitOfWork.GenderTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false)
            .Select(g => new SelectListItem { Value = g.ID.ToString(), Text = g.Name })
            .ToList(),
                ProfessionOptions = unitOfWork.ProfessionRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false)
            .Select(g => new SelectListItem { Value = g.ID.ToString(), Text = g.Name })
            .ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (configuration["AymanDatingCore.Pl.AllowedRegistration"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowRegister")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await unitOfWork.UserManager.FindByEmailAsync(model.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered");
                }
                else if (model.DateOFBirth > DateTime.Now.AddYears(-18))
                {
                    ModelState.AddModelError("Age", "You Must be +18");
                }

            else
            {

                // Create new user
                var user = new AppUser
                {
                    UserName = model.FirstName + "." + model.LastName + "-" + unitOfWork.MySPECIALGUID.GetUniqueKey(6),
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Address = model.Address,
                    Phone = model.Phone,
                    CountryTBLId = model.CountryTBLId,
                    GenderTBLId = model.GenderTBLId,
                    ActivationCode = unitOfWork.MySPECIALGUID.GetUniqueKey(12),
                    DateOfBirth = Convert.ToDateTime(model.DateOFBirth),
                };

                // Create the user
                var result = await unitOfWork.UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // 🧩 2️⃣ Get Browser and Device Info
                    string userBrowser = Request.Headers["User-Agent"].ToString();
                    var (ip, hostName) = await GetClientInfo(HttpContext);
                    unitOfWork.UserAddressListTBLRepository.Add(new UserAddressListTBL() { AppUserId = user.Id, OperationType = "Register", IpAddress = ip, HostName = hostName, Browser = userBrowser });
                    //

                    await unitOfWork.UserManager.AddToRoleAsync(user, "User");

                    var userhistory = new UserHistoryTBL
                    {
                        AppUserId = user.Id,
                        SearchAgeFrom = 18,
                        SearchAgeTo = 99,
                        CountryId = model.CountryTBLId,
                        EducationId = 1,
                        FinancialModeId = 1,
                        GenderId = model.GenderTBLId,
                        ProfessionId = model.ProfessionTBLId,
                        ProfileHeading = "Your Heading.....",
                        AboutPartner = "AboutPartner...",
                        AboutYou = "AboutYou...",
                        City = "City...",
                        IsMain = true,
                        LanguageId = 1,
                        MaritalStatusId = 1,
                        PurposeId = 1,
                        IsSwitchedOff = false,
                        //MainImageUrl = "blankprofile973460.png"
                    };
                    unitOfWork.UserHistoryRepository.Add(userhistory);

                    var ActivateLink = configuration["AymanDatingCore.Pl.Url"] + "Account/Activation?Email=" + user.Email + "&ActivationCode=" + user.ActivationCode;

                    var Email = new EmailTBL_VM();
                    Email.To = model.Email;
                    Email.Subject = configuration["AymanDatingCore.Pl.Name"] + " - Activate User";
                    Email.Body = await GetActivationTemplateAsync(user.FirstName, Email.Subject, user.ActivationCode, ActivateLink);
                    var newEmail = Mapper.Map<EmailTBL>(Email);

                    // Send email
                    await unitOfWork.EmailTBLRepository.SendEmailAsync(newEmail, 2);
                    // Save Email
                    unitOfWork.EmailTBLRepository.Add(newEmail);

                    // Redirect to home page
                    return RedirectToAction("Activation", "Account", new { Email = model.Email });
                }

                // Add errors if registration failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            }

            // If we got this far, something failed, redisplay form
            model.CountryOptions = unitOfWork.CountryTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false)
                .Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name, Selected = (c.ID == model.CountryTBLId) })
                .ToList();
            model.GenderOptions = unitOfWork.GenderTBLRepository.GetAllCustomized(
                        filter: a => a.IsDeleted == false)
                .Select(g => new SelectListItem { Value = g.ID.ToString(), Text = g.Name, Selected = (g.ID == model.GenderTBLId) })
                .ToList();
            model.ProfessionOptions = unitOfWork.ProfessionRepository.GetAllCustomized(
                                filter: a => a.IsDeleted == false)
                        .Select(g => new SelectListItem { Value = g.ID.ToString(), Text = g.Name, Selected = (g.ID == model.ProfessionTBLId) })
                        .ToList();

            if (!ModelState.IsValid)
            {
                model.Errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
            }

            return View(model);
        }
        #endregion

        #region Activation
        [AllowAnonymous]
        public IActionResult Activation(string? Email, string? ActivationCode, string? Message)
        {
            if (configuration["AymanDatingCore.Pl.AllowedActivation"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowActivation")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to Home page (or Dashboard, etc.)
                return RedirectToAction("Index", "Home");
            }
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = Message;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                var model = new ActivationDTO();
                model.Email = Email;
                if (!string.IsNullOrEmpty(ActivationCode))
                {
                    model.ActivationCode = ActivationCode;
                }
                else
                {
                    ViewBag.Message = "Check your Email Address to get your Activation Code!";
                }
                return View(model);
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activation(ActivationDTO model)
        {
            if (configuration["AymanDatingCore.Pl.AllowedActivation"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowActivation")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await unitOfWork.UserManager.FindByEmailAsync(model.Email) == null)
                {
                    ModelState.AddModelError("Email", "Email is not registered");
                    return View(model);
                }

                // Update User Activation
                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                // Check if ActivationCode already exists
                if (user.ActivationCode != model.ActivationCode)
                {
                    ModelState.AddModelError("ActivationCode", "Activation Code is not correct");
                    return View(model);
                }

                // 🧩 2️⃣ Get Browser and Device Info
                string userBrowser = Request.Headers["User-Agent"].ToString();
                var (ip, hostName) = await GetClientInfo(HttpContext);
                unitOfWork.UserAddressListTBLRepository.Add(new UserAddressListTBL() { AppUserId = user.Id, OperationType = "Activation", IpAddress = ip, HostName = hostName, Browser = userBrowser });
                //

                user.EmailConfirmed = true;
                user.IsActivated = true;
                var result = await unitOfWork.UserManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    // Redirect to home page
                    return RedirectToAction("Login", "Account", new { Message = "Activation Successfully, You can login Now!" });
                }

                // Add errors if registration failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }
        #endregion

        #region Forgot Password & reset password
        [AllowAnonymous]
        public IActionResult ForgetPassword()
        {
            if (configuration["AymanDatingCore.Pl.AllowedForgetPassword"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowForgotPassword")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect to Home page (or Dashboard, etc.)
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO model)
        {
            if (configuration["AymanDatingCore.Pl.AllowedForgetPassword"] != "1" || !unitOfWork.AdminPanelTBLRepository.GetAllCustomized(filter: a => (a.IsDeleted == false && a.PanelName == "AllowForgotPassword")).FirstOrDefault().Activation)
            {
                return RedirectToAction("ServiceIsDown", "Home");
            }
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await unitOfWork.UserManager.FindByEmailAsync(model.Email) == null)
                {
                    ModelState.AddModelError("Email", "Email is not registered");
                    return View(model);
                }


                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                // 🧩 2️⃣ Get Browser and Device Info
                string userBrowser = Request.Headers["User-Agent"].ToString();
                var (ip, hostName) = await GetClientInfo(HttpContext);
                unitOfWork.UserAddressListTBLRepository.Add(new UserAddressListTBL() { AppUserId = user.Id, OperationType = "ForgetPassword", IpAddress = ip, HostName = hostName, Browser = userBrowser });
                //


                var ResetCode = await unitOfWork.UserManager.GeneratePasswordResetTokenAsync(user);

                string encodedToken = HttpUtility.UrlEncode(ResetCode);


                var ActivateLink = configuration["AymanDatingCore.Pl.Url"] + "Account/ResetPassword?Email=" + user.Email + "&ResetCode=" + encodedToken;

                var Email = new EmailTBL_VM();
                Email.To = model.Email;
                Email.Subject = configuration["AymanDatingCore.Pl.Name"] + " - Forgot Password";
                Email.Body = await GetActivationTemplateAsync(user.FirstName, Email.Subject, ResetCode, ActivateLink);
                var newEmail = Mapper.Map<EmailTBL>(Email);

                // Send email
                await unitOfWork.EmailTBLRepository.SendEmailAsync(newEmail, 2);
                // Save Email
                unitOfWork.EmailTBLRepository.Add(newEmail);

                return RedirectToAction("ResetPassword", "Account", new { Email = model.Email, Message = "Check your email address now!" });
            }
            ModelState.AddModelError("", "Error");

            return View(model);
        }


        [AllowAnonymous]
        public IActionResult ResetPassword(string? Email, string? ResetCode, string? Message)
        {
            if (!string.IsNullOrEmpty(Message))
            {
                ViewBag.Message = Message;
            }
            if (!string.IsNullOrEmpty(Email))
            {
                var model = new ResetPasswordDTO();
                model.Email = Email;
                if (!string.IsNullOrEmpty(ResetCode))
                {
                    model.ResetCode = ResetCode;
                }
                else
                {
                    ViewBag.Message = "Check your Email Address to get your Reset Code!";
                }
                return View(model);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                if (await unitOfWork.UserManager.FindByEmailAsync(model.Email) == null)
                {
                    ModelState.AddModelError("Email", "Email is not registered");
                    return View(model);
                }

                var user = await unitOfWork.UserManager.FindByEmailAsync(model.Email);

                var result = await unitOfWork.UserManager.ResetPasswordAsync(user, model.ResetCode, model.Password);

                if (result.Succeeded)
                {
                    // 🧩 2️⃣ Get Browser and Device Info
                    string userBrowser = Request.Headers["User-Agent"].ToString();
                    var (ip, hostName) = await GetClientInfo(HttpContext);
                    unitOfWork.UserAddressListTBLRepository.Add(new UserAddressListTBL() { AppUserId = user.Id, OperationType = "ResetPassword", IpAddress = ip, HostName = hostName, Browser = userBrowser });
                    //
                    // Redirect to home page
                    return RedirectToAction("Login", "Account", new { Message = "Your Password has been changed successfully, You can login now!" });
                }
                else
                {
                    ModelState.AddModelError("", "Reset Code is not correct!");
                }

                // Add errors if registration failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }
        #endregion

        #region Helpers
        public async Task<(string Ip, string HostName)> GetClientInfo(HttpContext context)
        {
            string ip = context.Connection.RemoteIpAddress?.ToString();

            // Handle proxy headers
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                var forwardedIp = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(forwardedIp))
                    ip = forwardedIp.Split(',').FirstOrDefault();
            }

            string hostName = "Unknown";

            if (!string.IsNullOrEmpty(ip))
            {
                try
                {
                    // Reverse DNS lookup (may fail)
                    var entry = await Dns.GetHostEntryAsync(ip);
                    hostName = entry.HostName;
                    if (string.IsNullOrEmpty(hostName) || hostName == "Unknown")
                    {
                        hostName = HttpContext.Request.Headers["X-Forwarded-Host"].FirstOrDefault() ?? HttpContext.Request.Host.Host;
                    }
                }
                catch
                {
                    // Fallback: no RDNS, show structured IP info
                    hostName = $"No RDNS for {ip}";
                }
            }

            return (ip ?? "Unknown", hostName);
        }

        private async Task<string> GetActivationTemplateAsync(string FirstName, string Subject, string Code, string activationLink)
        {
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "template1", "Activation-Register-PW.html");
            string html = await System.IO.File.ReadAllTextAsync(templatePath);

            // Replace placeholders
            html = html.Replace("{{FirstName}}", FirstName);
            html = html.Replace("{{Subject}}", Subject);
            html = html.Replace("{{Code}}", Code);
            html = html.Replace("{{ActivationLink}}", activationLink);
            html = html.Replace("{{Year}}", DateTime.Now.Year.ToString());

            return html;
        }

        #endregion


    }
}
