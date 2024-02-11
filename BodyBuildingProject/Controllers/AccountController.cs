using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BodyBuildingProject.Areas.Identity.Data;
using BodyBuildingProject.Models;
using BodyBuildingProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BodyBuildingProject.Controllers
{
    public class AccountController : Controller
    {
        DBBodyBuilding db { get; }
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        public AccountController(DBBodyBuilding _db, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Register()
        {
            //TempData["msg"] = "hiiiiii";
            ViewData["Title"] = "ثبت نام";
            return View();
        }

        public IActionResult Login()
        {
            ViewData["Title"] = "ورود";
            return View();
        }

        public async Task<IActionResult> RegisterConfirm(RegisterViewModel model)
        {
            ApplicationUser user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            var status = await userManager.CreateAsync(user, model.Password);
            if (status.Succeeded)
            {
                try
                {
                    Users u1 = new Users
                    {
                        UserId = user.Id
                    };
                    await db.Tbl_Users.AddAsync(u1);
                    var i = await db.SaveChangesAsync();

                    TempData["msg"] = "ثبت نام با موفقیت انجام شد";
                    return RedirectToAction("Index", "Home");
                }
                catch
                {
                    TempData["msg"] = "خطایی در ثبت نام رخ داده است";
                    return RedirectToAction("Register", "Account");
                }

            }
            else
            {
                TempData["msg"] = "خطایی در ثبت نام رخ داده است";
                return RedirectToAction("Register", "Account");
            }
        }

        public async Task<IActionResult> VerifyUserName(string userName)
        {
            var result = await userManager.FindByNameAsync(userName);
            if (result != null)
                return Json(false);
            return Json(true);
        }
        public async Task<IActionResult> VerifyEmail(string Email)
        {
            var result = await userManager.FindByEmailAsync(Email);
            if (result != null)
                return Json(false);
            return Json(true);
        }

        public async Task<IActionResult> LoginConfirm(LoginViewModel model)
        {
            var userapp = await userManager.FindByNameAsync(model.UserName);
            if (userapp != null)
            {
                var status = await signInManager.PasswordSignInAsync(userapp, model.Password, model.RememberMe, false); //Lockout off for now
                if (status.Succeeded)
                {
                    if (await userManager.IsInRoleAsync(userapp, "Admins"))
                        TempData["msg"] = "مدیر عزیز، خوش آمدید";

                    else if (await userManager.IsInRoleAsync(userapp, "Trainers"))
                        TempData["msg"] = "مربی عزیز، خوش آمدید";
                    else
                        TempData["msg"] = "ورزشکار عزیز، خوش آمدید";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["msg"] = "نام کاربری یا پسورد اشتباه است";
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}