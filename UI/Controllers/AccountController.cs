using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bipap.Service.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UI.Functionality;
using UI.Models;

namespace UI.Controllers
{
    public class AccountController : Controller
    {
        #region Fields 
        private readonly IDoctorService _doctorService;
        #endregion

        #region Const
        public AccountController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
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
            var doctor = _doctorService.GetDoctorByMobileAndMedicalSystemCode(model.Mobile, model.MedicalSystemCode);
            if (doctor == null)
                return Json(new { res = false, message = "شماره موبایل وارد شده اشتباه میباشد" });
            var ranCode = "1234";// HelperMethod.GenerateRandomCode();
            doctor.ActiveCode = ranCode;
            _doctorService.Update(doctor);
            return Json(new { res = true, message = "کد اعتبار سنجی ارسال شد /n شما در حال انتقال به صفحه ی ورود کد میباشید /n لطفا منتظر بمانید." });
        }
        public IActionResult Verification(string mobile)
        {
            ViewBag.Mobile = mobile;
            return View();
        }
        public IActionResult CheckLoginCode(LoginViewModel model)
        {
            var doctor = _doctorService.CheckActiveCode(model.Mobile, model.ActiveCode);
            if (doctor == null)
                return Json(new { res = false, message = "کد وارد شده اشتباه میباشد" });
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, doctor.Id.ToString()),
                new Claim(ClaimTypes.Name, doctor.FirstName +" "+doctor.LastName),
            };

            var grandmaIdentity = new ClaimsIdentity(userClaims, "User Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { grandmaIdentity });
            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index", "Home");

        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult Profile()
        {
            var currentUserId = HttpContextMethod.GetCurrentUserId(User);
            var doctor = _doctorService.Get(currentUserId);
            return View(doctor);
        }
        [HttpPost]
        public IActionResult Profile(DoctorViewModel model)
        {
            var doctor = _doctorService.Get(model.Id);
            if (doctor == null)
                return Json(new { res = false,message="خطا سیستمی!" });
            doctor.FirstName = model.FirstName;
            doctor.LastName = model.LastName;
            doctor.MedicalSystemCode = model.MedicalSystemCodek;
            _doctorService.Update(doctor);
            return Json(new { res = true, message = "عملیات با موفقیت انجام شد." });
        }

    }
}
