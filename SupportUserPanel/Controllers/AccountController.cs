using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SupportUserPanel.Functionality;
using SupportUserPanel.Models;

namespace SupportUserPanel.Controllers
{
    public class AccountController : Controller
    {
        #region Fields 
        private readonly ISupportUserService _supportUserService;
        #endregion

        #region Const
        public AccountController(ISupportUserService supportUserService)
        {
            _supportUserService = supportUserService;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult CheckMobile(LoginViewModel model)
        {
            var supportUser = _supportUserService.GetByMobile(model.Mobile);
            if (supportUser == null)
                return Json(new { res = false, message = "شماره موبایل وارد شده اشتباه میباشد" });
            var ranCode = "1234"; //HelperMethod.GenerateRandomCode();
            supportUser.ActiveCode = ranCode;
            _supportUserService.Update(supportUser);
            return Json(new { res = true, message = "کد اعتبار سنجی ارسال شد /n شما در حال انتقال به صفحه ی ورود کد میباشید /n لطفا منتظر بمانید." });
        }
        public IActionResult Verification(string mobile)
        {
            ViewBag.Mobile = mobile;
            return View();
        }
        public IActionResult CheckLoginCode(LoginViewModel model)
        {
            var supportUser = _supportUserService.CheckActiveCode(model.Mobile, model.ActiveCode);
            if (supportUser == null)
                return Json(new { res = false, message = "کد وارد شده اشتباه میباشد" });
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, supportUser.Id.ToString()),
                new Claim(ClaimTypes.Name, supportUser.FirstName +" "+supportUser.LastName),
            };

            var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
            HttpContext.SignInAsync(userPrincipal);
           // HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

            return RedirectToAction("Index", "Home");


        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
