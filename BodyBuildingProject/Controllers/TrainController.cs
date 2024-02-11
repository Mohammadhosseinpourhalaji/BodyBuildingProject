using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BodyBuildingProject.Areas.Identity.Data;
using BodyBuildingProject.Models;
using BodyBuildingProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BodyBuildingProject.Controllers
{
    [Authorize]
    public class TrainController : Controller
    {
        DBBodyBuilding db { get; }
        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;
        public TrainController(DBBodyBuilding _db, UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager)
        {
            db = _db;
            userManager = _userManager;
            signInManager = _signInManager;
        }
        public IActionResult Index()
        {
            return View(); //null
        }

        public IActionResult RequestSchedule()
        {
            ViewData["Title"] = "درخواست برنامه";

            ViewData["TypesList"] = db.Tbl_TrainType.ToList();

            return View();
        }

        public async Task<IActionResult> RequestScheduleConfirm(RequestScheduleViewModel model)
        {
            var userapp = await userManager.FindByNameAsync(User.Identity.Name);
            if (userapp == null)
                return NotFound();
            var userid = userapp.Id;

            if (model.TrainTypeId == 0)
            {
                //Message: Train type ra entekhab konid
                return NotFound();
            }

            //try
            //{
            TrainSchedule trainSchedule = new TrainSchedule
            {
                UserId = userid,
                DateAdded = DateTime.Now,
                isPaid = false
            };

            await db.AddAsync(trainSchedule);
            await db.SaveChangesAsync();

            var commenttoStr = model.Comment;

            UserTrainInfo userTrainInfo = new UserTrainInfo
            {
                TrainScheduleId = trainSchedule.Id,

                Age = model.Age,
                Height = decimal.Parse(model.Height.Replace("/", ".")),
                Weight = decimal.Parse(model.Weight.Replace("/", ".")),
                Chest = decimal.Parse(model.Chest.Replace("/", ".")),
                Waist = decimal.Parse(model.Waist.Replace("/", ".")),
                Bracer = decimal.Parse(model.Bracer.Replace("/", ".")),
                Arm = decimal.Parse(model.Arm.Replace("/", ".")),
                Thighs = decimal.Parse(model.Thighs.Replace("/", ".")),
                TrainTypeId = model.TrainTypeId
            };

            if (!string.IsNullOrWhiteSpace(model.Comment))
                userTrainInfo.Comment = model.Comment.ToString();

            await db.AddAsync(userTrainInfo);
            await db.SaveChangesAsync();

            TempData["msg"] = "برنامه جدید با موفقیت ثبت شد";
            return RedirectToAction("Index", "Home");
            //}
            //catch
            //{
            //    // Make sure the ScheduleId base will be removed after errors
            //    var trainScheduleAfter = db.Tbl_TrainSchedule.Where(x => x.UserId == userid).LastOrDefault();
            //    if (trainScheduleAfter != null)
            //    {
            //        bool checkUserTInfo = db.Tbl_UserTrainInfo.Any(x => x.TrainScheduleId == trainScheduleAfter.Id);
            //        if (!checkUserTInfo)
            //        {
            //            db.Remove(trainScheduleAfter);
            //            db.SaveChanges();
            //        }
            //    }
            //    return NotFound();
            //}


        }

        public async Task<IActionResult> CartList()
        {
            var userapp = await userManager.FindByNameAsync(User.Identity.Name);
            if (userapp == null)
                return NotFound();

            var unpaidUserTrains = db.Tbl_TrainSchedule.Where(x => x.UserId == userapp.Id && x.isPaid == false).Include(x => x.UserTrainInfo).ThenInclude(x => x.TrainType).ToList();

            return View(unpaidUserTrains);
        }

        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var userapp = await userManager.FindByNameAsync(User.Identity.Name);
            if (userapp == null)
                return NotFound();

            var trainSchedule = db.Tbl_TrainSchedule.Where(x => x.Id == id && x.UserId == userapp.Id).FirstOrDefault();
            if (trainSchedule == null)
                return NotFound();

            db.Tbl_TrainSchedule.Remove(trainSchedule);
            db.SaveChanges();

            return RedirectToAction("CartList", "Train");
        }

        public async Task<IActionResult> TrainScheduleList()
        {
            var userapp = await userManager.FindByNameAsync(User.Identity.Name);
            if (userapp == null)
                return NotFound();


            // Need to add where isPaid == true
            var TrainScheduleList = db.Tbl_TrainSchedule.Where(x => x.UserId == userapp.Id).Include(x => x.UserTrainInfo).ThenInclude(x=>x.TrainType).ToList();
            return View(TrainScheduleList);
        }

        [Route("{controller}/{action}/{ScheduleId}")]
        public async Task<IActionResult> UserTrainSchedule(int ScheduleId)
        {
            var userapp = await userManager.FindByNameAsync(User.Identity.Name);
            if (userapp == null)
                return NotFound();

            var schedule = db.Tbl_TrainSchedule.Where(x => x.Id == ScheduleId).FirstOrDefault();
            if (schedule.UserId != userapp.Id)
                return NotFound();

            //if (schedule.Status == false)
            //{
            //    // Error: Barnamei sabt nashode ast
            //}

            ViewData["UserTrainScheduleItems"] = db.Tbl_UserTrainSchedule.Where(x => x.TrainScheduleId == ScheduleId).Include(x => x.DaysOfWeek).OrderBy(x => x.DayId).ToList();
            return View();
        }
    }
}