using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminPanel.Models;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly IAdminUserService _adminUserService;
        #endregion

        #region Const
        public AccountController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }
        #endregion

        #region Actions
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            var adminUser = _adminUserService.Valid(model.UserName, model.Password);
            if (adminUser == null)
                return Json(new { res = false, message = "نام کاربری و یا رمز عبور اشتباه میباشد." });
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
                new Claim(ClaimTypes.Name, adminUser.FullName),
            };

            var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");
            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
            HttpContext.SignInAsync(userPrincipal);
            return Json(new { res = true, message = "لطفا منتظر بمانید." });
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
